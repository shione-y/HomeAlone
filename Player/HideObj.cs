using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(CheckWall))]
public class HideObj : MonoBehaviour {
  [Tooltip("�v���C���[���B����鋗����")]
  public float Distance = 3.3f;

  GameObject HideUI;

  [Space(20)]
  [Tooltip("���b�J�[�̔�:null����")]
  public List<GameObject> Doors;

  [Space(20)]
  [Tooltip("�O�ɏo��Ƃ��̌Œ�ʒu:�ڈ�̃Q�[���I�u�W�F�N�g���쐬")]
  public Transform OutPos;

  protected Renderer _renderer;
  Transform _playerTr;
  Transform _tr;
  protected CheckWall _checkWall;

  protected bool _isHide = false;

  PlayerManager _playerManager;
  FirstPersonController _personController;
  Transform _playerCameraRootTr;  // �q�v�f�̕��я����ς��ƃG���[�o�邩��
  CharacterController _characterController;

  protected bool _input = false;

  protected TextMeshProUGUI _hideUIText;

  protected bool _isCheck = false;
  bool _preCheck = false;

  private void Awake() {
    // UI�����I�Ɏ擾
    HideUI = GameObject.Find("Hide Canvas");
  }
  void Start() {
    _renderer = GetComponent<Renderer>();
    _checkWall = GetComponent<CheckWall>();
    _tr = transform;

    // �v���C���[�������I�Ɏ擾
    _playerTr = GameObject.FindObjectOfType<PlayerManager>().gameObject.transform;
    _playerManager = _playerTr.GetComponent<PlayerManager>();
    _personController = _playerTr.GetComponent<FirstPersonController>();
    _playerCameraRootTr = _playerTr.GetChild(0);
    _characterController = _playerTr.GetComponent<CharacterController>();

    _hideUIText = HideUI.GetComponentInChildren<TextMeshProUGUI>();

    // ������
    HideUI.SetActive(false);
  }

  void Update() {
    _input = Gamepad.current != null ? Gamepad.current.buttonEast.wasPressedThisFrame : false;
    // test
    _input = _input || Input.GetKeyDown(KeyCode.B);

    _isCheck = _renderer.isVisible && GetDistance() < Distance && _checkWall.Check();

    if (_isCheck != _preCheck) UiShow();

    // �B��Ă���Ԃ�UI�\��
    if (_playerManager.NowState == PlayerState.Hide) {
      if (!HideUI.activeInHierarchy) { HideUI.SetActive(true); }
    }

    if (_isHide) { Exit(); } else { Hide(); }

    // �L�^
    _preCheck = _isCheck;
  }

  // UI�̕\���Ǘ�
  protected void UiShow() { HideUI.SetActive(!HideUI.activeInHierarchy); }

  // �v���C���[�Ƃ̋��������߂�
  protected float GetDistance() {
    Vector3 _aPos = _playerTr.position;
    Vector3 _bPos = _tr.position;
    return Vector3.Distance(_aPos, _bPos);
  }

  // �v���C���[���B���
  virtual public void Hide() {
    // �v���C���[�̎��E�ɓ����Ă���
    // ��̓͂��͈�
    // �Ԃɕǂ�������
    if (!_isCheck) { return; }

    _hideUIText.text = "B : Hide";

    // B�{�^���������ꂽ��
    if (_input) {
      _isHide = true;
      /*
      �E�ʒu�E���_�Œ�
      �E�v���C���[�𒆂Ƀ��[�v
      �E�v���C���[�̐g�̂̌������w��
      �E�v���C���[���̏�ԕύX

      �E�o�邽�߂�UI�\��

      �E�\�����̐؂�ւ�
      */

      _personController.enabled = _characterController.enabled = false;

      _playerTr.position = _tr.position;
      _playerTr.rotation = _tr.localRotation;
      _playerCameraRootTr.rotation = _tr.rotation;

      _playerManager.NowState = PlayerState.Hide;

      _hideUIText.text = "B : Exit";

      LockerDoorChange();
    }
  }

  // �B�ꂽ��Ԃ���O�֏o��
  virtual public void Exit() {
    // B�{�^���������ꂽ��
    if (_input) {
      _isHide = false;
      /*
      �E�v���C���[���O�Ƀ��[�v
      �E�v���C���[�̐g�̂̌������w��
      �E�ʒu�E���_�Œ�������@���@LateUpdate�ɂĎ��s
      �E�v���C���[���̏�ԕύX

      �EUI�\���̐؂�ւ�

      �E�\�����̐؂�ւ�
       */

      _playerTr.position = OutPos.position;
      _playerTr.rotation = _tr.localRotation;

      _personController.enabled = _characterController.enabled = true;

      _playerManager.NowState = PlayerState.Idle;

      _hideUIText.text = "B : Hide";
      HideUI.SetActive(false);

      LockerDoorChange();

    }
  }

  // �\�����̐؂�ւ�
  void LockerDoorChange() {
    if (Doors == null) { return; }

    for (int i = 0; i < Doors.Count; i++) {
      if (Doors[i].activeInHierarchy) { Doors[i].SetActive(false); } else { Doors[i].SetActive(true); }
    }
  }
}
