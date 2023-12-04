using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour {
  Animator _animator;

  bool _input = false;

  void Start() {
    _animator = GetComponent<Animator>();
  }

  void Update() {
    // Yƒ{ƒ^ƒ“
    _input = Gamepad.current != null ? Gamepad.current.buttonNorth.wasPressedThisFrame : false;
    // test
    _input = _input || Input.GetKeyDown(KeyCode.Y);

    if (_input) { _animator.SetBool("IsShow", !_animator.GetBool("IsShow")); }
  }
}
