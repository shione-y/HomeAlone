using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DataManager))]
public class TutorialManager : MonoBehaviour {
  // 参照するセーブデータ
  SaveData data;
  FirstPersonController _personController;
  CharacterController _characterController;


  //Animation管理のオブジェクト
  [SerializeField]
  private PlayableDirector _wakeup;
  [SerializeField]
  private PlayableDirector _ringtone;
  [SerializeField]
  private PlayableDirector _hide;

  //Debug用　
  //黒電話に対して「調べる」を行ったときtrue
  public bool BlackPhone = false;
  //ふすまに対して「隠れる」を行ったときtrue
  public bool Fusuma = false;

  void Start() {
    // セーブデータをDataManagerから参照
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

  //黒電話　調べたとき呼び出す
  public void Event_Ringnote() {
    _ringtone.Play();
  }

  //隠れる
  public void Event_Hide() {
    _hide.Play();
  }

  public void PlayButton() {
    // ゲーム画面をロード
    SceneManager.LoadScene((int)SceneNum.GameScene);

    // test ------------------------------------
    // データ登録
    data.isPlayedTutorial = true;
    // -----------------------------------------

  }
}
