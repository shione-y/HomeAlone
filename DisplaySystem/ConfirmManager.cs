using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmManager : MonoBehaviour {
  public void YesButton() {
    // チュートリアルをロード
    SceneManager.LoadScene((int)SceneNum.TutorialScene);
  }

  public void NoButton() {
    // ポップアップを削除
    Destroy(transform.parent.gameObject);
  }
}
