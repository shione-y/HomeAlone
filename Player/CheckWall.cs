using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ԃɕǂ����邩�m�F����
/// �X�e�[�W�I�u�W�F�N�g���ɕt����
/// </summary>
public class CheckWall : MonoBehaviour {
  Transform _playerTr;
  Transform _tr;

  Ray ray;
  RaycastHit hit;
  Vector3 direction;   // Ray���΂�����
  float distance = 10;    // Ray���΂�����
  Vector3 _offset = new Vector3(0, 0.2f, 0);  // ���ߗp

  void Start() {
    // �v���C���[�������I�Ɏ擾
    _playerTr = GameObject.FindObjectOfType<PlayerManager>().gameObject.transform;
    _tr = transform;
  }

  /// <summary>
  /// �v���C���[�Ƃ̊Ԃɕǂ��������m�F����
  /// </summary>
  /// <returns>treu�̎��A�ǂ�����</returns>
  public bool Check() {
    // Ray���΂��������v�Z
    Vector3 temp = _playerTr.position + _offset - _tr.position;
    direction = temp.normalized;

    ray = new Ray(_tr.position, direction);  // Ray���΂�
    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Ray���V�[����ɕ`��

    // Ray���ŏ��ɓ����������̂𒲂ׂ�
    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit)) {
      if (hit.collider.CompareTag("Player")) return true;
    }

    return false;
  }
}