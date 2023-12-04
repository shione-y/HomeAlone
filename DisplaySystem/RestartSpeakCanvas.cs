using System.Collections;
using UnityEngine;
//ys TimeLine�ɂĐ��䂵�������߁ASetActive�ł̐���ɕύX����
public class RestartSpeakCanvas : MonoBehaviour {
  [Header("�\���b��")]
  public float ShowSeconds = 3f;
  [Header("��\���t�F�[�h�̑��x")]
  [Tooltip("�A���t�@�l�����炷��")]
  public float FadeSpeed = 0.02f;

  CanvasGroup canvasGroup;

  bool _active = false;

  void Start() {
    canvasGroup = GetComponentInParent<CanvasGroup>();
    //canvasGroup.alpha = 1f;

    // �w��b���o�߂Ŕ�\��
    //StartCoroutine(GoToNextScene());
  }

  private void Update() {
    if(!_active && transform.parent.gameObject.activeSelf) {
      _active = true;
      canvasGroup.alpha = 1f;

      // �w��b���o�߂Ŕ�\��
      StartCoroutine(GoToNextScene());
    }
  }

  IEnumerator GoToNextScene() {
    //�@3�b�ԑ҂�
    yield return new WaitForSeconds(ShowSeconds);

    while (true) {
      canvasGroup.alpha -= FadeSpeed;

      yield return null;

      // ���S�ɓ����ɂȂ�����
      if (canvasGroup.alpha == 0) {
        // �e�̃L�����o�X����
        // ��\��
        //Destroy(transform.parent.gameObject);
        _active = true;
        transform.parent.gameObject.SetActive(false);
      }
    }
  }
}
