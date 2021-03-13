using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConstantVelocity), typeof(Timeout))]
public class CannonShell : MonoBehaviour {
  public int bounces = 1;

  public event Action<CannonShell> Destroyed;

  Timeout timeout;

  void Awake() {
    timeout = GetComponent<Timeout>();
    Debug.Assert(
      null != timeout,
      "CannonShell component requires a Timeout compoennt.");
  }

  void Start() {
    timeout.Elapsed += OnTimeoutElapsed;
  }

  void Die() {
    Destroyed?.Invoke(this);
    GameObject.Destroy(gameObject);
  }

  void OnTimeoutElapsed(Timeout t) {
    // no need to call gameobject.destroy here because Timeout does it
    // for us.
    Destroyed?.Invoke(this);
  }

  void OnCollisionEnter(Collision collision) {
    var obj = collision.gameObject;

    {
      var shell = obj.GetComponent<CannonShell>();
      if(null != shell) {
        Die();
        return;
      }
    }

    {
      var tank = obj.GetComponent<TankController>();
      if(null != tank) {
        Die();
        return;
      }
    }

    // otherwise, we collided with a wall, so we decrement the bounces
    // and possibly die.

    if(bounces --> 0)
      return;

    Die();
  }
}
