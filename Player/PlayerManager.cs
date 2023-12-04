using System;
using TMPro;
using UnityEngine;

/// <summary>
/// �v���C���[�̏�ԊǗ�
/// </summary>
public enum PlayerState { Idle, Move, Hide, Check } // Move�s�v����

/// <summary>
/// �v���C���[�̑S�ʊǗ��p
/// ���̎󂯓n���͎�ɂ���ōs��
/// </summary>
public class PlayerManager : MonoBehaviour {
  // ���_�l
  public float SoulValue {
    get {
      return _soul;
    }
    set {
      _soul = value;

      // �l�͈̔͂𐧌�
      if (_soul > MaxSoulValue) _soul = MaxSoulValue;
      if (_soul < 0) _soul = 0;
    }
  }
  float _soul;

  [Tooltip("���_�l����������^�C�~���O")]
  public float TermSeconds = 1.5f;
  [Tooltip("���_�l�̍ő�l")]
  public float MaxSoulValue = 100f;

  [Space(15)]
  [Header("������")]
  [Tooltip("�ʏ펞")]
  public float NormalValue = -1;
  [Tooltip("�ǂ��Ă�����")]
  public float ChaseValue = -3;
  [Tooltip("�B��Ă�����")]
  public float HideValue = -2;

  [Space(15)]
  [Header("�㏸��")]
  [Tooltip("���邢����")]
  public float LightRoomValue = 6;

  [Space(15)]
  [Header("���_�l")]
  public TextMeshProUGUI SoulText;

  // --------------------------------------------------------------

  /// <summary>
  /// ����̏��
  /// </summary>
  [NonSerialized] public PlayerState NowState = PlayerState.Idle;

  // --------------------------------------------------------------

  /// <summary>
  /// �ǂ��Ă���
  /// �O���������\��
  /// </summary>
  [NonSerialized] public bool IsChasing = false;

  /// <summary>
  /// ���邢�����ɂ���
  /// </summary>
  bool _isLightRoom = false;

  // --------------------------------------------------------------

  // ���邢�����𔻒肷��̂Ɏg�p
  Ray ray;
  RaycastHit hit;
  Vector3 direction;   // Ray���΂�����
  float distance = 2;    // Ray���΂�����

  // --------------------------------------------------------------

  float _cnt;
  bool _gameOver = false;

  // --------------------------------------------------------------

  private void Start() {
    // �����l�o�^
    SoulValue = MaxSoulValue;
    SoulText.text = SoulValue.ToString();
  }

  private void Update() {
    // �Q�[���I�[�o�[��͈ȉ������s���Ȃ�
    if (_gameOver) { return; }

    _isLightRoom = GetIsLightRoom();

    // ���_�l�̍X�V
    _cnt += Time.deltaTime;
    if (_cnt > TermSeconds) {
      _cnt = 0f;

      SoulValue += GetChangeSoulValue();
      // �\���X�V
      SoulText.text = SoulValue.ToString();

      //0�ɂȂ�ƃQ�[���I�[�o�[
      if (SoulValue <= 0) {
        _gameOver = true;
        GameObject.FindObjectOfType<GameManager>().GameOver();
      }
    }
  }

  // ��������Ray���΂��āA���邢�����𔻒�
  bool GetIsLightRoom() {
    // Ray���΂��������v�Z
    direction = -transform.up.normalized;

    ray = new Ray(transform.position, direction);  // Ray���΂�
    Debug.DrawRay(ray.origin, ray.direction * distance, Color.cyan);  // Ray���V�[����ɕ`��

    // Ray���ŏ��ɓ����������̂𒲂ׂ�
    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit)) {
      if (hit.collider.CompareTag("LightRoom")) return true;
    }

    // ���邢�����ɓ�����Ȃ�����
    return false;
  }

  /// <summary>
  /// ���_�l�̍X�V�ʂ����߂�
  /// </summary>
  /// <returns>�����l</returns>
  float GetChangeSoulValue() {
    float _tmp = 0f;

    switch (NowState) {
      case PlayerState.Idle:
        _tmp += NormalValue;
        break;
      case PlayerState.Hide:
        _tmp += HideValue;
        break;
    }

    if (IsChasing) _tmp += ChaseValue;
    if (_isLightRoom) _tmp += LightRoomValue;

    return _tmp;
  }
}