using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour {
  public void SkipButton() {
    // �^�C�g����ʂ����[�h
    SceneManager.LoadScene((int)SceneNum.TitleScene);
  }
}
