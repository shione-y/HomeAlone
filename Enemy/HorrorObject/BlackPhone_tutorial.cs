using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPhone_tutorial : MonoBehaviour
{
  private AudioSource _audio;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player") && !_audio.isPlaying) {
      _audio.Play();
    }
  }
  // Start is called before the first frame update
  void Start()
    {
    _audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
