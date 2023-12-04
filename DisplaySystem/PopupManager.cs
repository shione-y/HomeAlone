using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ポップアップ全般に関連する処理
public class PopupManager : MonoBehaviour {
  [Header("初期選択するボタン")]
  public Button FirstSelectButton;

  void Start() {
    if (FirstSelectButton == null) { return; }
    // 指定のボタンを初期選択する

    // 一旦フォーカスを外す
    // 他のUIを選択状態のまま選択すると警告が出るのを防ぐため
    if (!EventSystem.current.alreadySelecting) {
      EventSystem.current.SetSelectedGameObject(null);
    }
    FirstSelectButton.Select();
  }
}
