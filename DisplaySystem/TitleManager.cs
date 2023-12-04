using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DataManager))]
/// <summary>
/// 画面遷移など
/// 主に他画面の管理
/// </summary>
public class TitleManager : MonoBehaviour {
  //[Header("チュートリアルをプレイ済みか")]
  //[Tooltip("T:つづきからを選べる")]
  //[SerializeField]
  //private bool IsPlayedTutorial = false;

  // 参照するセーブデータ
  SaveData data;

  void Start() {
    // セーブデータをDataManagerから参照
    data = GetComponent<DataManager>().data;

    // 以下を有効化すると「つづきから」はずっと押せない
    //// データ登録
    //data.isPlayedTutorial = IsPlayedTutorial;

    // 初期化
    data.isRestart = false;

    // 初回プレイの時
    if (!data.isPlayedTutorial) {
      // 「つづきから」ボタンを押せない状態にする
      SetContinueButton();
    }
  }

  private void Update() {
    // ボタンの選択が外れた時、「はじめから」ボタンを強制選択
    if (EventSystem.current.currentSelectedGameObject == null) {
      // はじめからボタンを強制的に取得
      Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
      startButton.Select();
    }
  }

  public void StartButton() {
    if (data.isPlayedTutorial) {
      // チュートリアルプレイ済み
      // 確認ポップアップをインスタンス生成
      GameObject prefab = (GameObject)Resources.Load("Prefabs/Confirm Canvas");
      Instantiate(prefab, transform.position, Quaternion.identity);

    } else {
      // 初回プレイ
      // そのままチュートリアルロード
      SceneManager.LoadScene((int)SceneNum.TutorialScene);
    }
  }

  public void ContinueButton() {
    //if (data.isPlayedTutorial) {
    //  // チュートリアルプレイ済み
    // ゲーム画面をロード
    SceneManager.LoadScene((int)SceneNum.GameScene);
    //}
  }

  public void ConfigButton() {
    // 設定ポップアップをインスタンス生成
    GameObject prefab = (GameObject)Resources.Load("Prefabs/Config Canvas");
    Instantiate(prefab, transform.position, Quaternion.identity);
  }

  // 「つづきから」ボタンを押せない状態にする
  void SetContinueButton() {
    if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex((int)SceneNum.TitleScene)) { return; }

    // つづきからボタンを強制的に取得
    GameObject continueButton = GameObject.Find("Contine Button");
    Image image = continueButton.GetComponent<Image>();
    Button button = continueButton.GetComponent<Button>();

    // カラーコードを元に、画像の色を変える
    Color newColor;
    ColorUtility.TryParseHtmlString("#E6E4E6", out newColor);
    image.color = newColor;

    // ボタンをオフ
    button.enabled = false;
  }
}
