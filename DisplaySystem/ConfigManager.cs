using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ConfigManager : MonoBehaviour {
  bool _input = false;

  void Update() {
    _input = Gamepad.current != null ? Gamepad.current.buttonSouth.wasPressedThisFrame : false;
    // test
    _input = _input || Input.GetKeyDown(KeyCode.A);


    // A�{�^���Ń|�b�v�A�b�v�����(�폜)
    if (_input) {
      // �e�̃L�����o�X����
      // ��\��
      Destroy(transform.parent.gameObject);
    }
  }
  // �u�^�C�g���ցv�{�^��
  public void TitleButton() {
    SceneManager.LoadScene((int)SceneNum.TitleScene);
  }

  // �u�Q�[���I���v�{�^��
  // �R�s�y���Fhttps://www.popii33.com/how-to-quit-a-game-in-unity/#:~:text=1%20No%20Function%E3%81%AE%E6%89%80%E3%82%92%E6%8A%BC%E4%B8%8B,2%20Test%E3%82%92%E9%81%B8%E6%8A%9E%203%20%E6%9C%80%E5%BE%8C%E3%81%AB%E3%82%B2%E3%83%BC%E3%83%A0%E7%B5%82%E4%BA%86%E9%96%A2%E6%95%B0%E3%81%AEEndGame%E3%82%92%E9%81%B8%E6%8A%9E%E3%81%97%E3%81%A6%E4%B8%8B%E3%81%95%E3%81%84
  public void GameFinishButton() {
    // �A�v���P�[�V�������I������
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
  }
}
