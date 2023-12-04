using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataManager))]
public class GameOverManager : MonoBehaviour {
  // 参照するセーブデータ
  SaveData data;

  void Start() {
    // セーブデータをDataManagerから参照
    data = GetComponent<DataManager>().data;
  }

  public void ContinueButton() {
    // データ登録
    data.isRestart = true;

    // プレイ画面をロード
    SceneManager.LoadScene((int)SceneNum.GameScene);
  }

  public void TitleButton() {
    // タイトル画面をロード
    SceneManager.LoadScene((int)SceneNum.TitleScene);
  }
}