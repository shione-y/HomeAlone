using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DataManager))]
public class GameManager : MonoBehaviour {
  [Header("フェードアニメーションのプレハブ")]
  [Tooltip("生成すると自然とフェードアウトして、画面全体が黒になる")]
  [SerializeField]
  private GameObject FadeAnimCanvasPrefab;
  [Header("GameOver時に演出を行う位置")]
  public Transform GameOverPos;

  GameObject _menuUI;
  // 生成後のフェードUIのインスタンス
  GameObject _instantFade;
  Image _image;
  Animator _gameOverAnimator;

  // ゲームオーバーの演出要Animが終了しているか
  bool _isFinishGameOverAnim = false;

  Transform _playerTr;
  FirstPersonController _personController;
  Transform _playerCameraRootTr;  // memo:子要素の並び順が変わるとエラー出るかも

  // 参照するセーブデータ
  SaveData data;


  void Start() {
    _instantFade = null;
    _image = null;

    // プレイヤーを強制的に取得
    _playerTr = GameObject.FindObjectOfType<PlayerManager>().gameObject.transform;
    _personController = _playerTr.GetComponent<FirstPersonController>();
    _playerCameraRootTr = _playerTr.GetChild(0);

    // UIを強制的に取得
    _menuUI = GameObject.Find("Menu Canvas");

    // セーブデータをDataManagerから参照
    data = GetComponent<DataManager>().data;

    // リスタートの時
    if (data.isRestart) {
      // ひとりごとのUIをインスタンス生成
      GameObject prefab = (GameObject)Resources.Load("Prefabs/RestartSpeak Canvas");
      Instantiate(prefab, transform.position, Quaternion.identity);

      // リセット
      data.isRestart = false;
    }
  }

  void Update() {
    if (_gameOverAnimator == null) {
      // 以下は、GameClear()が一度実行された後に実行
      // UI生成済みかつ、画面が真っ暗になったら
      if (_image != null) {
        if (_image.color.a < 1) { return; }
        // エンディング画面をロード
        SceneManager.LoadScene((int)SceneNum.EndingScene);
      }
      return;
    }


    // 以下は、GameOver()が一度実行された後に実行

    // -----------------------------------------------------
    // 本来は、アニメーションイベントで管理しているフラグのみで終了判定を取りたい
    // 仮で直にアニメーションの終了を判定している
    // -----------------------------------------------------

    // Fadeアニメーションが再生されている時のみ
    if (!_gameOverAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade")) return;

    // GameOver演出Anim再生終了で、ゲームオーバー画面へ遷移
    if (_isFinishGameOverAnim && _gameOverAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
      // ゲームオーバー画面をロード
      SceneManager.LoadScene((int)SceneNum.GameOverScene);
    }
  }
  /// <summary>
  /// エンディング画面に遷移する
  /// 遷移する際にフェードする
  /// </summary>
  public void GameClear() {
    if (_image != null) { return; }
    // フェードアウト用のUIをインスタンス生成
    GameObject prefab = (GameObject)Resources.Load("Prefabs/ClearFadeOut Canvas");
    GameObject _i = Instantiate(prefab, transform.position, Quaternion.identity);

    _image = _i.GetComponentInChildren<Image>();
  }

  /// <summary>
  /// ゲームオーバー時の処理
  /// 外部から呼び出しされる
  /// </summary>
  public void GameOver() {
    Debug.Log("GameOver");

    // 移動・視点操作不可
    _personController.IsMove = _personController.IsLook = false;

    // カメラの向き・位置を演出用の固定値に変更
    //_playerTr.position = GameOverPos.position;
    //_playerTr.rotation = GameOverPos.rotation;
    //_playerCameraRootTr.localRotation = Quaternion.Euler(Vector3.zero);

    // Anim
    InstantiateFadeUI();
    _gameOverAnimator.SetBool("IsFade", true);
    _isFinishGameOverAnim = true; // 本来はアニメーションイベントでフラグ管理したい

    // MenuUIを非表示
    _menuUI.SetActive(false);
  }

  /// <summary>
  /// フェード用のUI生成
  /// </summary>
  /// <returns>T:インスタンス生成済み</returns>
  void InstantiateFadeUI() {
    if (_instantFade == null) {
      _instantFade = Instantiate(FadeAnimCanvasPrefab);
      _image = _instantFade.GetComponentInChildren<Image>();
      _gameOverAnimator = _image.GetComponent<Animator>();
    }
  }
}