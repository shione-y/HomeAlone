using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPhone_eventtrigger_tutorial : MonoBehaviour {
  TutorialManager tutorialManager;
  // Start is called before the first frame update
  void Start() {
    tutorialManager = GameObject.Find("GameManager").GetComponent<TutorialManager>();
    tutorialManager.BlackPhone = true;
  }

  // Update is called once per frame
  void Update() {

  }
}
