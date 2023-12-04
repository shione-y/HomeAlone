using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfigButtonManager : MonoBehaviour {
  [Header("���Ă��鎞�ɑJ�ڂ���UI")]
  public Selectable CloseSelect;
  [Header("�J���Ă��鎞�ɑJ�ڂ���UI")]
  public Selectable OpenSelect;

  // �{���́AStart�Ŏ擾������
  [Header("�q�v�f��UI")]
  [SerializeField]
  private GameObject[] UiObjs;

  Animator _animator;
  RectTransform _triangleRT;

  bool _isOpen = false;

  Button thisButton;

  void Start() {
    _animator = GetComponent<Animator>();
    _triangleRT = transform.GetChild(2).GetComponent<RectTransform>();
    thisButton = GetComponent<Button>();
  }

  private void Update() {
    // �q�v�f��UI�������Ă��鎞�̂݁A�ȉ������s
    if (UiObjs == null) { return; }
    // �{�^�����J���ꂽ�܂܂̎�
    if (!_isOpen) { return; }

    // �q�v�f��UI���m�F
    // ������̎q�v�f���I�����Ă��Ȃ���
    foreach (GameObject obj in UiObjs) {
      if (EventSystem.current.currentSelectedGameObject == obj) { return; }
    }
    // ���̃{�^�����I������Ă��鎞
    if (EventSystem.current.currentSelectedGameObject != gameObject) {
      // �����I�ɕ���
      OpenAndClose();
    }
  }

  // �J����
  public void OpenAndClose() {
    string _tiggerName = _isOpen ? "Close" : "Open";
    _animator.SetTrigger(_tiggerName);

    // �t���O���]
    _isOpen = !_isOpen;

    // �O�p�̏㉺���]
    Vector3 _scaleV = _triangleRT.localScale;
    _triangleRT.localScale = new Vector3(_scaleV.x, _scaleV.y * -1, _scaleV.z);
  }

  // �u���ʁv�̊J�ɂ����UI�̑J�ڂ�ς���
  // �Q�l�FNavigation.cs��selectOnDown��summary
  public void SoundButton() {
    if (OpenSelect == null || CloseSelect == null) { return; }

    // ���̐ݒ���擾
    Navigation navigation = thisButton.navigation;
    navigation.mode = Navigation.Mode.Explicit;

    // �Ώۂ�UI�ύX
    navigation.selectOnDown = _isOpen ? OpenSelect : CloseSelect;
    // �ēo�^
    thisButton.navigation = navigation;

    // �J�������ɋ����I�ɒ��̗v�f��I��
    if (_isOpen) { OpenSelect.Select(); }
  }
}
