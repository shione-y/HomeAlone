using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 間に壁があるか確認する
/// ステージオブジェクト側に付ける
/// </summary>
public class CheckWall : MonoBehaviour {
  Transform _playerTr;
  Transform _tr;

  Ray ray;
  RaycastHit hit;
  Vector3 direction;   // Rayを飛ばす方向
  float distance = 10;    // Rayを飛ばす距離
  Vector3 _offset = new Vector3(0, 0.2f, 0);  // 調節用

  void Start() {
    // プレイヤーを強制的に取得
    _playerTr = GameObject.FindObjectOfType<PlayerManager>().gameObject.transform;
    _tr = transform;
  }

  /// <summary>
  /// プレイヤーとの間に壁が無いか確認する
  /// </summary>
  /// <returns>treuの時、壁が無い</returns>
  public bool Check() {
    // Rayを飛ばす方向を計算
    Vector3 temp = _playerTr.position + _offset - _tr.position;
    direction = temp.normalized;

    ray = new Ray(_tr.position, direction);  // Rayを飛ばす
    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Rayをシーン上に描画

    // Rayが最初に当たった物体を調べる
    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit)) {
      if (hit.collider.CompareTag("Player")) return true;
    }

    return false;
  }
}