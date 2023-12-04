using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セーブデータ
/// </summary>
[System.Serializable]
public class SaveData {
  //public const int rankCnt = 3;
  //public int[] rank = new int[rankCnt];
  
  //初回プレイかどうか
  public bool isPlayedTutorial = false;

  public bool isRestart = false;

  //設定　音量(スライダーの値)
  public float soundVolume;

  //恐怖対象
  public List<GameObject> horrorObject = new List<GameObject>();
  public List<GameObject> compatibleItems = new List<GameObject>();
  public List<bool> operatingStatus = new List<bool>();

  //所持アイテム
  public List<GameObject> possessionItems = new List<GameObject>();
}

/// <summary>
/// 画面遷移の引数用
/// </summary>
public enum SceneNum : int { TitleScene, GameScene, GameOverScene, EndingScene, TutorialScene }

