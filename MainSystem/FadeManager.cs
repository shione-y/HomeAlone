using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
  public float Speed = 0.02f;        //�t�F�[�h����X�s�[�h
  float red, green, blue, alfa;

  public bool Out = false;
  public bool In = false;

  Image fadeImage;                //�p�l��

  void Start() {
    fadeImage = GetComponent<Image>();
    red = fadeImage.color.r;
    green = fadeImage.color.g;
    blue = fadeImage.color.b;
    alfa = fadeImage.color.a;
  }

  void FixedUpdate() {
    if (In) {
      FadeIn();
    }

    if (Out) {
      FadeOut();
    }
  }

  void FadeIn() {
    alfa -= Speed;
    Alpha();
    if (alfa <= 0) {
      In = false;
      fadeImage.enabled = false;
    }
  }

  void FadeOut() {
    fadeImage.enabled = true;
    alfa += Speed;
    Alpha();
    if (alfa >= 1) {
      Out = false;
    }
  }

  void Alpha() {
    fadeImage.color = new Color(red, green, blue, alfa);
  }
}