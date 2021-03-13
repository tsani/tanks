using System;
using UnityEngine;

/// <summary>
///   Ensures the gameobject is always moving at the exact same speed.
///   And all collisions are perfectly elastic.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ConstantVelocity : MonoBehaviour {
  [Tooltip("The speed to maintain")]
  public float speed;

  Rigidbody rb;

  Vector3 forward;

  void Awake() {
    rb = GetComponent<Rigidbody>();
    Debug.Assert(
      null != rb,
      "CannonShell requires Rigidbody");
  }

  void Start() {
    rb.velocity = transform.forward * speed;
    forward = transform.forward;
  }

  void OnCollisionEnter(Collision collision) {
    // this implements a perfect reflection of the object through the collision normal
    var contact = collision.GetContact(0);
    forward = forward - 2 * Vector3.Dot(contact.normal, forward) * contact.normal;
    transform.LookAt(transform.position + forward);
    rb.velocity = forward * speed;
  }
}
