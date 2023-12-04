using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckObj : HideObj {
  [Tooltip("���ׂ���̃v���n�u")]
  public GameObject CheckedObj;

  // ��
  public override void Hide() {
    // �v���C���[�̎��E�ɓ����Ă���
    // ��̓͂��͈�
    // �Ԃɕǂ�������
    if (!_isCheck) { return; }

    _hideUIText.text = "B : Check";

    // B�{�^���������ꂽ��
    if (_input) {
      // �I�u�W�F�N�g�ύX
      Instantiate(CheckedObj,transform.position,Quaternion.identity, transform.parent);
      Destroy(gameObject);

      // UI��\��
      UiShow();
    }

  }

  public override void Exit() {
    // none
  }
}
