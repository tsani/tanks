using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class PlayerControls : MonoBehaviour {
  [Tooltip("The layer that the floor's special collider is on.")]
  public int aimLayer = 8;

  TankController tankController;

  void Awake() {
    tankController = GetComponent<TankController>();
    Debug.Assert(
      null != tankController,
      "PlayerControls requires TankController");
  }

  void Start() {
    tankController.playerVariable.Value = gameObject;
  }

  void Update() {
    var mask = 1 << aimLayer;
    
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
      tankController.AimAt(hit.point);

    if(Input.GetButtonDown("Fire1"))
      tankController.Shoot();
  }

  void FixedUpdate() {
    if(Input.GetKey(KeyCode.A))
      tankController.Left();

    if(Input.GetKey(KeyCode.D))
      tankController.Right();

    if(Input.GetKey(KeyCode.W))
      tankController.Forward();
    else if(Input.GetKey(KeyCode.S))
      tankController.Backward();
  }
}
