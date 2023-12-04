using System.Collections;
using UnityEngine;
//ys TimeLineにて制御したいため、SetActiveでの制御に変更する
public class RestartSpeakCanvas : MonoBehaviour {
  [Header("表示秒数")]
  public float ShowSeconds = 3f;
  [Header("非表示フェードの速度")]
  [Tooltip("アルファ値を減らす量")]
  public float FadeSpeed = 0.02f;

  CanvasGroup canvasGroup;

  bool _active = false;

  void Start() {
    canvasGroup = GetComponentInParent<CanvasGroup>();
    //canvasGroup.alpha = 1f;

    // 指定秒数経過で非表示
    //StartCoroutine(GoToNextScene());
  }

  private void Update() {
    if(!_active && transform.parent.gameObject.activeSelf) {
      _active = true;
      canvasGroup.alpha = 1f;

      // 指定秒数経過で非表示
      StartCoroutine(GoToNextScene());
    }
  }

  IEnumerator GoToNextScene() {
    //　3秒間待つ
    yield return new WaitForSeconds(ShowSeconds);

    while (true) {
      canvasGroup.alpha -= FadeSpeed;

      yield return null;

      // 完全に透明になったら
      if (canvasGroup.alpha == 0) {
        // 親のキャンバス消す
        // 非表示
        //Destroy(transform.parent.gameObject);
        _active = true;
        transform.parent.gameObject.SetActive(false);
      }
    }
  }
}
