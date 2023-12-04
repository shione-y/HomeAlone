using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckObj : HideObj {
  [Tooltip("調べた後のプレハブ")]
  public GameObject CheckedObj;

  // 壊す
  public override void Hide() {
    // プレイヤーの視界に入っている
    // 手の届く範囲
    // 間に壁が無い時
    if (!_isCheck) { return; }

    _hideUIText.text = "B : Check";

    // Bボタンが押されたら
    if (_input) {
      // オブジェクト変更
      Instantiate(CheckedObj,transform.position,Quaternion.identity, transform.parent);
      Destroy(gameObject);

      // UI非表示
      UiShow();
    }

  }

  public override void Exit() {
    // none
  }
}
