using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class Hide_PlayableAsset : PlayableAsset {
  // Factory method that generates a playable based on this asset
  public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
    // �r�w�C�r�A�̂ق�
    Hide_PlayableBehaviour _behaviour = new Hide_PlayableBehaviour();

    //�L���[�u
    _behaviour.cubeObject = go;

    ScriptPlayable<Hide_PlayableBehaviour> playable = ScriptPlayable<Hide_PlayableBehaviour>.Create(graph, _behaviour);

    return playable;

  }
}
