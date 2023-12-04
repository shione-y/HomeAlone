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
  [Tooltip("プレイヤーが隠れられる距離感")]
  public float Distance = 3.3f;

  GameObject HideUI;

  [Space(20)]
  [Tooltip("ロッカーの扉:nullも可")]
  public List<GameObject> Doors;

  [Space(20)]
  [Tooltip("外に出るときの固定位置:目印のゲームオブジェクトを作成")]
  public Transform OutPos;

  protected Renderer _renderer;
  Transform _playerTr;
  Transform _tr;
  protected CheckWall _checkWall;

  protected bool _isHide = false;

  PlayerManager _playerManager;
  FirstPersonController _personController;
  Transform _playerCameraRootTr;  // 子要素の並び順が変わるとエラー出るかも
  CharacterController _characterController;

  protected bool _input = false;

  protected TextMeshProUGUI _hideUIText;

  protected bool _isCheck = false;
  bool _preCheck = false;

  private void Awake() {
    // UI強制的に取得
    HideUI = GameObject.Find("Hide Canvas");
  }
  void Start() {
    _renderer = GetComponent<Renderer>();
    _checkWall = GetComponent<CheckWall>();
    _tr = transform;

    // プレイヤーを強制的に取得
    _playerTr = GameObject.FindObjectOfType<PlayerManager>().gameObject.transform;
    _playerManager = _playerTr.GetComponent<PlayerManager>();
    _personController = _playerTr.GetComponent<FirstPersonController>();
    _playerCameraRootTr = _playerTr.GetChild(0);
    _characterController = _playerTr.GetComponent<CharacterController>();

    _hideUIText = HideUI.GetComponentInChildren<TextMeshProUGUI>();

    // 初期化
    HideUI.SetActive(false);
  }

  void Update() {
    _input = Gamepad.current != null ? Gamepad.current.buttonEast.wasPressedThisFrame : false;
    // test
    _input = _input || Input.GetKeyDown(KeyCode.B);

    _isCheck = _renderer.isVisible && GetDistance() < Distance && _checkWall.Check();

    if (_isCheck != _preCheck) UiShow();

    // 隠れている間はUI表示
    if (_playerManager.NowState == PlayerState.Hide) {
      if (!HideUI.activeInHierarchy) { HideUI.SetActive(true); }
    }

    if (_isHide) { Exit(); } else { Hide(); }

    // 記録
    _preCheck = _isCheck;
  }

  // UIの表示管理
  protected void UiShow() { HideUI.SetActive(!HideUI.activeInHierarchy); }

  // プレイヤーとの距離を求める
  protected float GetDistance() {
    Vector3 _aPos = _playerTr.position;
    Vector3 _bPos = _tr.position;
    return Vector3.Distance(_aPos, _bPos);
  }

  // プレイヤーが隠れる
  virtual public void Hide() {
    // プレイヤーの視界に入っている
    // 手の届く範囲
    // 間に壁が無い時
    if (!_isCheck) { return; }

    _hideUIText.text = "B : Hide";

    // Bボタンが押されたら
    if (_input) {
      _isHide = true;
      /*
      ・位置・視点固定
      ・プレイヤーを中にワープ
      ・プレイヤーの身体の向きを指定
      ・プレイヤー側の状態変更

      ・出るためのUI表示

      ・表示扉の切り替え
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

  // 隠れた状態から外へ出る
  virtual public void Exit() {
    // Bボタンが押されたら
    if (_input) {
      _isHide = false;
      /*
      ・プレイヤーが外にワープ
      ・プレイヤーの身体の向きを指定
      ・位置・視点固定を解除　→　LateUpdateにて実行
      ・プレイヤー側の状態変更

      ・UI表示の切り替え

      ・表示扉の切り替え
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

  // 表示扉の切り替え
  void LockerDoorChange() {
    if (Doors == null) { return; }

    for (int i = 0; i < Doors.Count; i++) {
      if (Doors[i].activeInHierarchy) { Doors[i].SetActive(false); } else { Doors[i].SetActive(true); }
    }
  }
}
