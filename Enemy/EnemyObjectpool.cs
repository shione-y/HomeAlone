using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


//　敵のオブジェクトプール https://qiita.com/NekoCan/items/e3908b8e4e91b95d726a
public class EnemyObjectpool : MonoBehaviour {
  // オブジェクトを保存する空オブジェクトのtransform
  Transform pool;

  // プール内に生成されるオブジェクト
  [SerializeField]
  private GameObject _enemy;

  void Start() {
    pool = new GameObject("enemyPool").transform;
  }

  public void GetObject(Vector3 pos, Quaternion qua) {
    foreach (Transform t in pool) {
      //オブジェが非アクティブなら使い回し
      if (!t.gameObject.activeSelf) {
        //プレイヤーの方向を見る
        t.SetPositionAndRotation(pos, qua);
        //位置と回転を設定後、アクティブにする
        t.gameObject.SetActive(true);
        return;
      }
    }
    //非アクティブなオブジェクトがないなら生成
    //生成と同時にpoolを親に設定　（プレイヤーの方向を見る）
    Instantiate(_enemy, pos, qua, pool);
  }

}
