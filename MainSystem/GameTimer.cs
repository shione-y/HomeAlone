using TMPro;
using UnityEngine;

/// <summary>
/// プレイ開始してから10分間経過でゲームクリア
/// </summary>
public class GameTimer : MonoBehaviour {
  /// <summary>
  /// 制限時間(単位:分)
  /// </summary>
  public float TimeLimit = 10;

  public TextMeshProUGUI TimeText;

  float _cnt = 0f;
  int _second, _preSecond, _lastSeconds = 0;

  GameManager _gameManager;

  void Awake() {
    // 初期化
    _cnt = 0f;
    _second = _preSecond = 0;
    _lastSeconds = (int)(TimeLimit * 60);

    _gameManager = GetComponent<GameManager>();
  }

  void Update() {
    _cnt += Time.deltaTime;
    _second = (int)_cnt;
    if (_preSecond == _second) { return; }  // 以下は1秒ごとに実行

    _lastSeconds = (int)(TimeLimit * 60) - _second;

    //  タイムアップでゲームクリア
    if (_lastSeconds <= 0) {
      _gameManager.GameClear();
      TimeText.text = "00:00";

      // 以下の更新はタイムアップ後行わない
      return; 
    }

    TimeText.text = (_lastSeconds / 60).ToString("00") + ":" + (_lastSeconds % 60).ToString("00");


    // 記録
    _preSecond = _second;
  }
}
