using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 恐怖対象につける
// オブジェクトプールから敵をスポーンさせる
public class SpawnTimer : MonoBehaviour {
  // オブジェクトプール
  [SerializeField]
  private EnemyObjectpool pool;

  private CheckWall _checkWall;


  // 指定秒数
  [SerializeField]
  private float _spawnTime = 5f;
  // 蓄積経過秒数
  private float _spawnDelay = 0f;

  

  // Start is called before the first frame update
  void Start() {
    _checkWall = this.GetComponent<CheckWall>();
  }

  void Update() {
  }

  private void OnTriggerStay(Collider other) {
    //範囲内にいるPlayerがいる場合
    if (other.gameObject.CompareTag("Player")) {
      if (!_checkWall.Check()) { return; }
      Timer();
    }
  }

  void Timer() {
    _spawnDelay += Time.deltaTime;
    // 蓄積経過秒数が指定秒数を上回ったとき
    if (_spawnDelay >= _spawnTime) {
      if (pool != null) {
        // オブジェクトプールから敵をスポーンさせ、、
        pool.GetObject(this.transform.position, this.transform.rotation);
      } else { }
      // 蓄積経過秒数を０にする
      _spawnDelay = 0f;
    }
    //Debug.Log("蓄積経過秒数 : " +  _spawnDelay);
  }
}
