using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//�@�G�̃I�u�W�F�N�g�v�[�� https://qiita.com/NekoCan/items/e3908b8e4e91b95d726a
public class EnemyObjectpool : MonoBehaviour {
  // �I�u�W�F�N�g��ۑ������I�u�W�F�N�g��transform
  Transform pool;

  // �v�[�����ɐ��������I�u�W�F�N�g
  [SerializeField]
  private GameObject _enemy;

  void Start() {
    pool = new GameObject("enemyPool").transform;
  }

  public void GetObject(Vector3 pos, Quaternion qua) {
    foreach (Transform t in pool) {
      //�I�u�W�F����A�N�e�B�u�Ȃ�g����
      if (!t.gameObject.activeSelf) {
        //�v���C���[�̕���������
        t.SetPositionAndRotation(pos, qua);
        //�ʒu�Ɖ�]��ݒ��A�A�N�e�B�u�ɂ���
        t.gameObject.SetActive(true);
        return;
      }
    }
    //��A�N�e�B�u�ȃI�u�W�F�N�g���Ȃ��Ȃ琶��
    //�����Ɠ�����pool��e�ɐݒ�@�i�v���C���[�̕���������j
    Instantiate(_enemy, pos, qua, pool);
  }

}
