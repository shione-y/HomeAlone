using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfigButtonManager : MonoBehaviour {
  [Header("閉じている時に遷移するUI")]
  public Selectable CloseSelect;
  [Header("開いている時に遷移するUI")]
  public Selectable OpenSelect;

  // 本来は、Startで取得したい
  [Header("子要素のUI")]
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
    // 子要素にUIを持っている時のみ、以下を実行
    if (UiObjs == null) { return; }
    // ボタンが開かれたままの時
    if (!_isOpen) { return; }

    // 子要素のUIを確認
    // いずれの子要素も選択していない時
    foreach (GameObject obj in UiObjs) {
      if (EventSystem.current.currentSelectedGameObject == obj) { return; }
    }
    // 他のボタンが選択されている時
    if (EventSystem.current.currentSelectedGameObject != gameObject) {
      // 強制的に閉じる
      OpenAndClose();
    }
  }

  // 開閉する
  public void OpenAndClose() {
    string _tiggerName = _isOpen ? "Close" : "Open";
    _animator.SetTrigger(_tiggerName);

    // フラグ反転
    _isOpen = !_isOpen;

    // 三角の上下反転
    Vector3 _scaleV = _triangleRT.localScale;
    _triangleRT.localScale = new Vector3(_scaleV.x, _scaleV.y * -1, _scaleV.z);
  }

  // 「音量」の開閉によってUIの遷移を変える
  // 参考：Navigation.csのselectOnDownのsummary
  public void SoundButton() {
    if (OpenSelect == null || CloseSelect == null) { return; }

    // 今の設定を取得
    Navigation navigation = thisButton.navigation;
    navigation.mode = Navigation.Mode.Explicit;

    // 対象のUI変更
    navigation.selectOnDown = _isOpen ? OpenSelect : CloseSelect;
    // 再登録
    thisButton.navigation = navigation;

    // 開いた時に強制的に中の要素を選択
    if (_isOpen) { OpenSelect.Select(); }
  }
}
