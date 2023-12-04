using System;
using TMPro;
using UnityEngine;

/// <summary>
/// プレイヤーの状態管理
/// </summary>
public enum PlayerState { Idle, Move, Hide, Check } // Move不要かも

/// <summary>
/// プレイヤーの全般管理用
/// 情報の受け渡しは主にこれで行う
/// </summary>
public class PlayerManager : MonoBehaviour {
  // 精神値
  public float SoulValue {
    get {
      return _soul;
    }
    set {
      _soul = value;

      // 値の範囲を制限
      if (_soul > MaxSoulValue) _soul = MaxSoulValue;
      if (_soul < 0) _soul = 0;
    }
  }
  float _soul;

  [Tooltip("精神値が増減するタイミング")]
  public float TermSeconds = 1.5f;
  [Tooltip("精神値の最大値")]
  public float MaxSoulValue = 100f;

  [Space(15)]
  [Header("減少量")]
  [Tooltip("通常時")]
  public float NormalValue = -1;
  [Tooltip("追われている状態")]
  public float ChaseValue = -3;
  [Tooltip("隠れている状態")]
  public float HideValue = -2;

  [Space(15)]
  [Header("上昇量")]
  [Tooltip("明るい部屋")]
  public float LightRoomValue = 6;

  [Space(15)]
  [Header("精神値")]
  public TextMeshProUGUI SoulText;

  // --------------------------------------------------------------

  /// <summary>
  /// 現状の状態
  /// </summary>
  [NonSerialized] public PlayerState NowState = PlayerState.Idle;

  // --------------------------------------------------------------

  /// <summary>
  /// 追われている
  /// 外部から代入予定
  /// </summary>
  [NonSerialized] public bool IsChasing = false;

  /// <summary>
  /// 明るい部屋にいる
  /// </summary>
  bool _isLightRoom = false;

  // --------------------------------------------------------------

  // 明るい部屋を判定するのに使用
  Ray ray;
  RaycastHit hit;
  Vector3 direction;   // Rayを飛ばす方向
  float distance = 2;    // Rayを飛ばす距離

  // --------------------------------------------------------------

  float _cnt;
  bool _gameOver = false;

  // --------------------------------------------------------------

  private void Start() {
    // 初期値登録
    SoulValue = MaxSoulValue;
    SoulText.text = SoulValue.ToString();
  }

  private void Update() {
    // ゲームオーバー後は以下を実行しない
    if (_gameOver) { return; }

    _isLightRoom = GetIsLightRoom();

    // 精神値の更新
    _cnt += Time.deltaTime;
    if (_cnt > TermSeconds) {
      _cnt = 0f;

      SoulValue += GetChangeSoulValue();
      // 表示更新
      SoulText.text = SoulValue.ToString();

      //0になるとゲームオーバー
      if (SoulValue <= 0) {
        _gameOver = true;
        GameObject.FindObjectOfType<GameManager>().GameOver();
      }
    }
  }

  // 下方向へRayを飛ばして、明るい部屋を判定
  bool GetIsLightRoom() {
    // Rayを飛ばす方向を計算
    direction = -transform.up.normalized;

    ray = new Ray(transform.position, direction);  // Rayを飛ばす
    Debug.DrawRay(ray.origin, ray.direction * distance, Color.cyan);  // Rayをシーン上に描画

    // Rayが最初に当たった物体を調べる
    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit)) {
      if (hit.collider.CompareTag("LightRoom")) return true;
    }

    // 明るい部屋に当たらなかった
    return false;
  }

  /// <summary>
  /// 精神値の更新量を求める
  /// </summary>
  /// <returns>足す値</returns>
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