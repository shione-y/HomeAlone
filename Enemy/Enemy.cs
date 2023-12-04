using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// �G�̏�ԊǗ��A�o����̒T���A���U:
// https://goodlucknetlife.com/unity-navmesh-tracking/#toc4
// https://docs.unity3d.com/ja/2019.4/ScriptReference/AI.NavMeshAgent.html

// 1023�@�B���ȊO�̏�Ԃ͉������Ă�
public class Enemy : MonoBehaviour {
  public enum EnemyState { wait, move, };

  [NonSerialized]
  public EnemyState state;

  //GameManager
  private GameManager _gameManager;

  // �v���C���[
  //public Transform target;
  private NavMeshAgent _agent;
  private Transform _player;
  private PlayerManager _playerManager;
  private CheckWall _checkWall;

  //GameOver�p�i�ߐH�j�R���C�_�[
  [SerializeField]
  private Collider _gameover;

  //�A�j���[�V����
  [NonSerialized]
  public Animator anim;

  // ���Ń^�C�}�[
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
    //���G�͈͓��Ƀv���C���[���������ꍇ�A�v���C���[�̒ǂ��Ă����Ԃ�����Bool��True�ɂ���
    if (other.gameObject.CompareTag("Player")) {
      anim.SetBool("Move", true);
      state = EnemyState.move;
      //�v���C���[�̒ǂ��Ă��邩�ǂ������u�ǂ��Ă���v�ɕύX����
      _playerManager.IsChasing = true;
      timer = false;
      _extinctionTimer = 0f;
    }
  }

  //���G�͈͓��Ƀv���C���[������ꍇ�͏��Ń^�C�}�[�̃J�E���g���~�E���Z�b�g����
  private void OnTriggerStay(Collider other) {
    Debug.Log("Stay");
    if (other.gameObject.CompareTag("Player")) {
      //�v���C���[���B��Ă���ꍇ�@�������΂�
      //if (_playerManager.NowState == PlayerState.Hide) {
      //  Debug.Log("Player State  Hide");
      //  //_agent.ResetPath();
      //  timer = true;
      //  return;
      //}
      _agent.destination = other.transform.position;
    }
  }

  //���G�͈͓��Ƀv���C���[�����Ȃ��Ȃ�������ł̃J�E���g��i�߂�
  private void OnTriggerExit(Collider other) {
    if (other.gameObject.CompareTag("Player")) {
      anim.SetBool("Move", false);
      state = EnemyState.wait;
      _agent.ResetPath();
      timer = true;
      other.gameObject.GetComponent<PlayerManager>().IsChasing = false;
    }
  }

  //���őO�̏����@vanishAnimation�̊J�n���ɌĂяo��
  //�ߐH�R���C�_�[�̔�A�N�e�B�u���ƃ^�C�}�[�̒�~
  public void BeforeExtinction() {
    //�^�C�}�[�̉��Z�̒�~
    timer = false;
    this.enabled = false;
    _gameover.enabled = false;
  }

  //���Ō�̏����@vanishAnimation�̏I�����ɌĂяo�����
  public void Extinction() {
    //���o�Ă������̂��߂Ƀ^�C�}�[�����Z�b�g����
    _extinctionTimer = 0f;
    this.enabled = true;
    _gameover.enabled = true;
    //�I�u�W�F�N�g�v�[���ɕԂ�
    this.gameObject.SetActive(false);
  }

  // �Q�[���I�[�o�[���̏����@gaburiAnimation�̏I���t�߂ŌĂяo�����
  public void GameOver_anim() {
    // GameManager�̏����Ăяo��
    _gameManager.GameOver();
  }
}
