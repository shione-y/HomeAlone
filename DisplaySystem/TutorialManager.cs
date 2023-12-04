using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataManager))]
public class TutorialManager : MonoBehaviour {
  // �Q�Ƃ���Z�[�u�f�[�^
  SaveData data;
  FirstPersonController _personController;
  CharacterController _characterController;


  //Animation�Ǘ��̃I�u�W�F�N�g
  [SerializeField]
  private PlayableDirector _wakeup;
  [SerializeField]
  private PlayableDirector _ringtone;
  [SerializeField]
  private PlayableDirector _hide;

  //Debug�p�@
  //���d�b�ɑ΂��āu���ׂ�v���s�����Ƃ�true
  public bool BlackPhone = false;
  //�ӂ��܂ɑ΂��āu�B���v���s�����Ƃ�true
  public bool Fusuma = false;

  void Start() {
    // �Z�[�u�f�[�^��DataManager����Q��
    data = GetComponent<DataManager>().data;
  }

  private void FixedUpdate() {
    if (BlackPhone) {
      Event_Ringnote();
    }
    if (_ringtone.state == PlayState.Delayed) {
      //test
      //_personController.enabled = _characterController.enabled = false;
      Debug.Log("Calling event end");
      if (Input.GetKeyDown(KeyCode.B)) {
        Debug.Log("Hide evenet start");
        Event_Hide();
      }
    }

  }

  //���d�b�@���ׂ��Ƃ��Ăяo��
  public void Event_Ringnote() {
    _ringtone.Play();
  }

  //�B���
  public void Event_Hide() {
    _hide.Play();
  }

  public void PlayButton() {
    // �Q�[����ʂ����[�h
    SceneManager.LoadScene((int)SceneNum.GameScene);

    // test ------------------------------------
    // �f�[�^�o�^
    data.isPlayedTutorial = true;
    // -----------------------------------------

  }
}
