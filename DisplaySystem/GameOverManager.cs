using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataManager))]
public class GameOverManager : MonoBehaviour {
  // �Q�Ƃ���Z�[�u�f�[�^
  SaveData data;

  void Start() {
    // �Z�[�u�f�[�^��DataManager����Q��
    data = GetComponent<DataManager>().data;
  }

  public void ContinueButton() {
    // �f�[�^�o�^
    data.isRestart = true;

    // �v���C��ʂ����[�h
    SceneManager.LoadScene((int)SceneNum.GameScene);
  }

  public void TitleButton() {
    // �^�C�g����ʂ����[�h
    SceneManager.LoadScene((int)SceneNum.TitleScene);
  }
}