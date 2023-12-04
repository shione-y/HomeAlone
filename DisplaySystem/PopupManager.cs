using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// �|�b�v�A�b�v�S�ʂɊ֘A���鏈��
public class PopupManager : MonoBehaviour {
  [Header("�����I������{�^��")]
  public Button FirstSelectButton;

  void Start() {
    if (FirstSelectButton == null) { return; }
    // �w��̃{�^���������I������

    // ��U�t�H�[�J�X���O��
    // ����UI��I����Ԃ̂܂ܑI������ƌx�����o��̂�h������
    if (!EventSystem.current.alreadySelecting) {
      EventSystem.current.SetSelectedGameObject(null);
    }
    FirstSelectButton.Select();
  }
}
