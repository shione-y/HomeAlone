using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Z�[�u�f�[�^
/// </summary>
[System.Serializable]
public class SaveData {
  //public const int rankCnt = 3;
  //public int[] rank = new int[rankCnt];
  
  //����v���C���ǂ���
  public bool isPlayedTutorial = false;

  public bool isRestart = false;

  //�ݒ�@����(�X���C�_�[�̒l)
  public float soundVolume;

  //���|�Ώ�
  public List<GameObject> horrorObject = new List<GameObject>();
  public List<GameObject> compatibleItems = new List<GameObject>();
  public List<bool> operatingStatus = new List<bool>();

  //�����A�C�e��
  public List<GameObject> possessionItems = new List<GameObject>();
}

/// <summary>
/// ��ʑJ�ڂ̈����p
/// </summary>
public enum SceneNum : int { TitleScene, GameScene, GameOverScene, EndingScene, TutorialScene }

