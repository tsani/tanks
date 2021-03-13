using System;
using UnityEngine;

namespace JakeUtils {
  public class CollisionProxy : MonoBehaviour {
    public event Action<Collision> CollisionEnter;
    public event Action<Collision> CollisionExit;
    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;

    void OnCollisionEnter(Collision collision) =>
      CollisionEnter?.Invoke(collision);

    void OnCollisionExit(Collision collision) =>
      CollisionExit?.Invoke(collision);

    void OnTriggerEnter(Collider collider) =>
      TriggerEnter?.Invoke(collider);

    void OnTriggerExit(Collider collider) =>
      TriggerExit?.Invoke(collider);
  }
}
