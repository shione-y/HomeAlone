using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// �Q�l�Fhttps://gametukurikata.com/program/endroll
public class EndRoll : MonoBehaviour {
  //�@�e�L�X�g�̃X�N���[���X�s�[�h
  [SerializeField]
  private float textScrollSpeed = 30;
  //�@�e�L�X�g�̐����ʒu
  [SerializeField]
  private float limitPosition = 730f;
  //�@�G���h���[�����I���������ǂ���
  private bool isStopEndRoll;
  //�@�V�[���ړ��p�R���[�`��
  private Coroutine endRollCoroutine;

  void Update() {
    //�@�G���h���[�����I��������
    if (isStopEndRoll) {
      endRollCoroutine = StartCoroutine(GoToNextScene());
    } else {
      //�@�G���h���[���p�e�L�X�g�����~�b�g���z����܂œ�����
      if (transform.position.y <= limitPosition) {
        transform.position = new Vector2(transform.position.x, transform.position.y + textScrollSpeed * Time.deltaTime);
      } else {
        isStopEndRoll = true;
      }
    }
  }

  IEnumerator GoToNextScene() {
    //�@3�b�ԑ҂�
    yield return new WaitForSeconds(3f);

    //if (Input.GetKeyDown("space")) {
      StopCoroutine(endRollCoroutine);
      SceneManager.LoadScene((int)SceneNum.TitleScene);
    //}

    yield return null;
  }
}
