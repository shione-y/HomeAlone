using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmManager : MonoBehaviour {
  public void YesButton() {
    // �`���[�g���A�������[�h
    SceneManager.LoadScene((int)SceneNum.TutorialScene);
  }

  public void NoButton() {
    // �|�b�v�A�b�v���폜
    Destroy(transform.parent.gameObject);
  }
}
