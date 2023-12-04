using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DataManager))]
/// <summary>
/// ��ʑJ�ڂȂ�
/// ��ɑ���ʂ̊Ǘ�
/// </summary>
public class TitleManager : MonoBehaviour {
  //[Header("�`���[�g���A�����v���C�ς݂�")]
  //[Tooltip("T:�Â������I�ׂ�")]
  //[SerializeField]
  //private bool IsPlayedTutorial = false;

  // �Q�Ƃ���Z�[�u�f�[�^
  SaveData data;

  void Start() {
    // �Z�[�u�f�[�^��DataManager����Q��
    data = GetComponent<DataManager>().data;

    // �ȉ���L��������Ɓu�Â�����v�͂����Ɖ����Ȃ�
    //// �f�[�^�o�^
    //data.isPlayedTutorial = IsPlayedTutorial;

    // ������
    data.isRestart = false;

    // ����v���C�̎�
    if (!data.isPlayedTutorial) {
      // �u�Â�����v�{�^���������Ȃ���Ԃɂ���
      SetContinueButton();
    }
  }

  private void Update() {
    // �{�^���̑I�����O�ꂽ���A�u�͂��߂���v�{�^���������I��
    if (EventSystem.current.currentSelectedGameObject == null) {
      // �͂��߂���{�^���������I�Ɏ擾
      Button startButton = GameObject.Find("Start Button").GetComponent<Button>();
      startButton.Select();
    }
  }

  public void StartButton() {
    if (data.isPlayedTutorial) {
      // �`���[�g���A���v���C�ς�
      // �m�F�|�b�v�A�b�v���C���X�^���X����
      GameObject prefab = (GameObject)Resources.Load("Prefabs/Confirm Canvas");
      Instantiate(prefab, transform.position, Quaternion.identity);

    } else {
      // ����v���C
      // ���̂܂܃`���[�g���A�����[�h
      SceneManager.LoadScene((int)SceneNum.TutorialScene);
    }
  }

  public void ContinueButton() {
    //if (data.isPlayedTutorial) {
    //  // �`���[�g���A���v���C�ς�
    // �Q�[����ʂ����[�h
    SceneManager.LoadScene((int)SceneNum.GameScene);
    //}
  }

  public void ConfigButton() {
    // �ݒ�|�b�v�A�b�v���C���X�^���X����
    GameObject prefab = (GameObject)Resources.Load("Prefabs/Config Canvas");
    Instantiate(prefab, transform.position, Quaternion.identity);
  }

  // �u�Â�����v�{�^���������Ȃ���Ԃɂ���
  void SetContinueButton() {
    if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex((int)SceneNum.TitleScene)) { return; }

    // �Â�����{�^���������I�Ɏ擾
    GameObject continueButton = GameObject.Find("Contine Button");
    Image image = continueButton.GetComponent<Image>();
    Button button = continueButton.GetComponent<Button>();

    // �J���[�R�[�h�����ɁA�摜�̐F��ς���
    Color newColor;
    ColorUtility.TryParseHtmlString("#E6E4E6", out newColor);
    image.color = newColor;

    // �{�^�����I�t
    button.enabled = false;
  }
}
