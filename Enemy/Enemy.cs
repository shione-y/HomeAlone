using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// 敵の状態管理、出現後の探索、霧散:
// https://goodlucknetlife.com/unity-navmesh-tracking/#toc4
// https://docs.unity3d.com/ja/2019.4/ScriptReference/AI.NavMeshAgent.html

// 1023　隠れる以外の状態は加味してる
public class Enemy : MonoBehaviour {
  public enum EnemyState { wait, move, };

  [NonSerialized]
  public EnemyState state;

  //GameManager
  private GameManager _gameManager;

  // プレイヤー
  //public Transform target;
  private NavMeshAgent _agent;
  private Transform _player;
  private PlayerManager _playerManager;
  private CheckWall _checkWall;

  //GameOver用（捕食）コライダー
  [SerializeField]
  private Collider _gameover;

  //アニメーション
  [NonSerialized]
  public Animator anim;

  // 消滅タイマー
  [NonSerialized]
  public bool timer = true;
  private float _extinctionTimer = 0f;


  void Start() {
    _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    _agent = this.GetComponent<NavMeshAgent>();
    _checkWall = this.GetComponent<CheckWall>();
    anim = this.GetComponent<Animator>();

    _player = GameObject.FindWithTag("Player").transform;
    _playerManager = _player.GetComponent<PlayerManager>();
  }

  void FixedUpdate() {
    if (timer) {
      _extinctionTimer += Time.deltaTime;
      anim.SetFloat("ExtinctionTimer", _extinctionTimer);
    }
    if (state == EnemyState.move && _playerManager.NowState == PlayerState.Hide) {
      if (!timer) {
        timer = true;
        anim.SetBool("Move", false);
      }
    }
  }

  private void OnTriggerEnter(Collider other) {
    //索敵範囲内にプレイヤーが入った場合、プレイヤーの追われている状態を示すBoolをTrueにする
    if (other.gameObject.CompareTag("Player")) {
      anim.SetBool("Move", true);
      state = EnemyState.move;
      //プレイヤーの追われているかどうかを「追われている」に変更する
      _playerManager.IsChasing = true;
      timer = false;
      _extinctionTimer = 0f;
    }
  }

  //索敵範囲内にプレイヤーがいる場合は消滅タイマーのカウントを停止・リセットする
  private void OnTriggerStay(Collider other) {
    Debug.Log("Stay");
    if (other.gameObject.CompareTag("Player")) {
      //プレイヤーが隠れている場合　処理を飛ばす
      //if (_playerManager.NowState == PlayerState.Hide) {
      //  Debug.Log("Player State  Hide");
      //  //_agent.ResetPath();
      //  timer = true;
      //  return;
      //}
      _agent.destination = other.transform.position;
    }
  }

  //索敵範囲内にプレイヤーがいなくなったら消滅のカウントを進める
  private void OnTriggerExit(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      anim.SetBool("Move", false);
      state = EnemyState.wait;
      _agent.ResetPath();
      timer = true;
      other.gameObject.GetComponent<PlayerManager>().IsChasing = false;
    }
  }

  //消滅前の処理　vanishAnimationの開始時に呼び出す
  //捕食コライダーの非アクティブ化とタイマーの停止
  public void BeforeExtinction() {
    //タイマーの加算の停止
    timer = false;
    this.enabled = false;
    _gameover.enabled = false;
  }

  //消滅後の処理　vanishAnimationの終了時に呼び出される
  public void Extinction() {
    //次出てきた時のためにタイマーをリセットする
    _extinctionTimer = 0f;
    this.enabled = true;
    _gameover.enabled = true;
    //オブジェクトプールに返す
    this.gameObject.SetActive(false);
  }

  // ゲームオーバー時の処理　gaburiAnimationの終了付近で呼び出される
  public void GameOver_anim() {
    // GameManagerの処理呼び出し
    _gameManager.GameOver();
  }
}
