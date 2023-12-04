using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DataManager))]
public class GameManager : MonoBehaviour {
  [Header("�t�F�[�h�A�j���[�V�����̃v���n�u")]
  [Tooltip("��������Ǝ��R�ƃt�F�[�h�A�E�g���āA��ʑS�̂����ɂȂ�")]
  [SerializeField]
  private GameObject FadeAnimCanvasPrefab;
  [Header("GameOver���ɉ��o���s���ʒu")]
  public Transform GameOverPos;

  GameObject _menuUI;
  // ������̃t�F�[�hUI�̃C���X�^���X
  GameObject _instantFade;
  Image _image;
  Animator _gameOverAnimator;

  // �Q�[���I�[�o�[�̉��o�vAnim���I�����Ă��邩
  bool _isFinishGameOverAnim = false;

  Transform _playerTr;
  FirstPersonController _personController;
  Transform _playerCameraRootTr;  // memo:�q�v�f�̕��я����ς��ƃG���[�o�邩��

  // �Q�Ƃ���Z�[�u�f�[�^
  SaveData data;


  void Start() {
    _instantFade = null;
    _image = null;

    // �v���C���[�������I�Ɏ擾
    _playerTr = GameObject.FindObjectOfType<PlayerManager>().gameObject.transform;
    _personController = _playerTr.GetComponent<FirstPersonController>();
    _playerCameraRootTr = _playerTr.GetChild(0);

    // UI�������I�Ɏ擾
    _menuUI = GameObject.Find("Menu Canvas");

    // �Z�[�u�f�[�^��DataManager����Q��
    data = GetComponent<DataManager>().data;

    // ���X�^�[�g�̎�
    if (data.isRestart) {
      // �ЂƂ育�Ƃ�UI���C���X�^���X����
      GameObject prefab = (GameObject)Resources.Load("Prefabs/RestartSpeak Canvas");
      Instantiate(prefab, transform.position, Quaternion.identity);

      // ���Z�b�g
      data.isRestart = false;
    }
  }

  void Update() {
    if (_gameOverAnimator == null) {
      // �ȉ��́AGameClear()����x���s���ꂽ��Ɏ��s
      // UI�����ς݂��A��ʂ��^���ÂɂȂ�����
      if (_image != null) {
        if (_image.color.a < 1) { return; }
        // �G���f�B���O��ʂ����[�h
        SceneManager.LoadScene((int)SceneNum.EndingScene);
      }
      return;
    }


    // �ȉ��́AGameOver()����x���s���ꂽ��Ɏ��s

    // -----------------------------------------------------
    // �{���́A�A�j���[�V�����C�x���g�ŊǗ����Ă���t���O�݂̂ŏI���������肽��
    // ���Œ��ɃA�j���[�V�����̏I���𔻒肵�Ă���
    // -----------------------------------------------------

    // Fade�A�j���[�V�������Đ�����Ă��鎞�̂�
    if (!_gameOverAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade")) return;

    // GameOver���oAnim�Đ��I���ŁA�Q�[���I�[�o�[��ʂ֑J��
    if (_isFinishGameOverAnim && _gameOverAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
      // �Q�[���I�[�o�[��ʂ����[�h
      SceneManager.LoadScene((int)SceneNum.GameOverScene);
    }
  }
  /// <summary>
  /// �G���f�B���O��ʂɑJ�ڂ���
  /// �J�ڂ���ۂɃt�F�[�h����
  /// </summary>
  public void GameClear() {
    if (_image != null) { return; }
    // �t�F�[�h�A�E�g�p��UI���C���X�^���X����
    GameObject prefab = (GameObject)Resources.Load("Prefabs/ClearFadeOut Canvas");
    GameObject _i = Instantiate(prefab, transform.position, Quaternion.identity);

    _image = _i.GetComponentInChildren<Image>();
  }

  /// <summary>
  /// �Q�[���I�[�o�[���̏���
  /// �O������Ăяo�������
  /// </summary>
  public void GameOver() {
    Debug.Log("GameOver");

    // �ړ��E���_����s��
    _personController.IsMove = _personController.IsLook = false;

    // �J�����̌����E�ʒu�����o�p�̌Œ�l�ɕύX
    //_playerTr.position = GameOverPos.position;
    //_playerTr.rotation = GameOverPos.rotation;
    //_playerCameraRootTr.localRotation = Quaternion.Euler(Vector3.zero);

    // Anim
    InstantiateFadeUI();
    _gameOverAnimator.SetBool("IsFade", true);
    _isFinishGameOverAnim = true; // �{���̓A�j���[�V�����C�x���g�Ńt���O�Ǘ�������

    // MenuUI���\��
    _menuUI.SetActive(false);
  }

  /// <summary>
  /// �t�F�[�h�p��UI����
  /// </summary>
  /// <returns>T:�C���X�^���X�����ς�</returns>
  void InstantiateFadeUI() {
    if (_instantFade == null) {
      _instantFade = Instantiate(FadeAnimCanvasPrefab);
      _image = _instantFade.GetComponentInChildren<Image>();
      _gameOverAnimator = _image.GetComponent<Animator>();
    }
  }
}