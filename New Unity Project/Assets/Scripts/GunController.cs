using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
  [Tooltip("Speed of gun rotation in degrees per second")]
  public float rotateSpeed = 15; // degrees per second

  public void AimAt(Vector3 floorPoint) {
    // fix the y coordinate so the target is in the same plane as the
    // gun.
    floorPoint.y = transform.position.y;
    transform.LookAt(floorPoint);
  }
  
  // Start is called before the first frame update
  void Start()
  {
      
  }
  
  // Update is called once per frame
  void Update()
  {
    /*
    if(Input.GetKey(KeyCode.A)) {
      Debug.Log("rotating gun!!");
      var t = gunAxis.transform;
      gun.transform.RotateAround(
        t.position,
        t.up,
        rotateSpeed * Time.deltaTime);
    }
    */
  }
}
