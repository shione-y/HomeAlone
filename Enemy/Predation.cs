using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predation : MonoBehaviour {
  //GameManager
  private GameManager _gameManager;

  //�e��Enemy�X�N���v�g
  [NonSerialized]
  private Enemy _parentEnemy;


  // Start is called before the first frame update
  void Start() {
    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //�e�̃X�N���v�g���擾
    _parentEnemy = transform.parent.gameObject.GetComponent<Enemy>();
  }

  // Update is called once per frame
  void Update() {

  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      //Animation�Đ����Ƀ^�C�}�[���i�܂Ȃ��悤false�ɂ���
      _parentEnemy.anim.SetTrigger("GameOver");
      //_gameManager.GameOver();

    }
  }
}
