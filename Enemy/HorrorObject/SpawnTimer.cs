using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���|�Ώۂɂ���
// �I�u�W�F�N�g�v�[������G���X�|�[��������
public class SpawnTimer : MonoBehaviour {
  // �I�u�W�F�N�g�v�[��
  [SerializeField]
  private EnemyObjectpool pool;

  private CheckWall _checkWall;


  // �w��b��
  [SerializeField]
  private float _spawnTime = 5f;
  // �~�όo�ߕb��
  private float _spawnDelay = 0f;

  

  // Start is called before the first frame update
  void Start() {
    _checkWall = this.GetComponent<CheckWall>();
  }

  void Update() {
  }

  private void OnTriggerStay(Collider other) {
    //�͈͓��ɂ���Player������ꍇ
    if (other.gameObject.CompareTag("Player")) {
      if (!_checkWall.Check()) { return; }
      Timer();
    }
  }

  void Timer() {
    _spawnDelay += Time.deltaTime;
    // �~�όo�ߕb�����w��b�����������Ƃ�
    if (_spawnDelay >= _spawnTime) {
      if (pool != null) {
        // �I�u�W�F�N�g�v�[������G���X�|�[�������A�A
        pool.GetObject(this.transform.position, this.transform.rotation);
      } else { }
      // �~�όo�ߕb�����O�ɂ���
      _spawnDelay = 0f;
    }
    //Debug.Log("�~�όo�ߕb�� : " +  _spawnDelay);
  }
}
