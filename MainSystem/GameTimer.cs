using TMPro;
using UnityEngine;

/// <summary>
/// �v���C�J�n���Ă���10���Ԍo�߂ŃQ�[���N���A
/// </summary>
public class GameTimer : MonoBehaviour {
  /// <summary>
  /// ��������(�P��:��)
  /// </summary>
  public float TimeLimit = 10;

  public TextMeshProUGUI TimeText;

  float _cnt = 0f;
  int _second, _preSecond, _lastSeconds = 0;

  GameManager _gameManager;

  void Awake() {
    // ������
    _cnt = 0f;
    _second = _preSecond = 0;
    _lastSeconds = (int)(TimeLimit * 60);

    _gameManager = GetComponent<GameManager>();
  }

  void Update() {
    _cnt += Time.deltaTime;
    _second = (int)_cnt;
    if (_preSecond == _second) { return; }  // �ȉ���1�b���ƂɎ��s

    _lastSeconds = (int)(TimeLimit * 60) - _second;

    //  �^�C���A�b�v�ŃQ�[���N���A
    if (_lastSeconds <= 0) {
      _gameManager.GameClear();
      TimeText.text = "00:00";

      // �ȉ��̍X�V�̓^�C���A�b�v��s��Ȃ�
      return; 
    }

    TimeText.text = (_lastSeconds / 60).ToString("00") + ":" + (_lastSeconds % 60).ToString("00");


    // �L�^
    _preSecond = _second;
  }
}
