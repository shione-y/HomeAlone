using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predation : MonoBehaviour {
  //GameManager
  private GameManager _gameManager;

  //親のEnemyスクリプト
  [NonSerialized]
  private Enemy _parentEnemy;


  // Start is called before the first frame update
  void Start() {
    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //親のスクリプトを取得
    _parentEnemy = transform.parent.gameObject.GetComponent<Enemy>();
  }

  // Update is called once per frame
  void Update() {

  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      //Animation再生中にタイマーが進まないようfalseにする
      _parentEnemy.anim.SetTrigger("GameOver");
      //_gameManager.GameOver();

    }
  }
}
