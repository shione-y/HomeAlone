using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour {
  public void SkipButton() {
    // タイトル画面をロード
    SceneManager.LoadScene((int)SceneNum.TitleScene);
  }
}
