using System;
using System.Collections;
using UnityEngine;

/// <summary>
///   Fires an event at a fixed interval.
/// </summary>
public class Interval : MonoBehaviour {
  public event Action<Interval> Elapsed;
  public float period = 1f;
  public bool autostart = true;

  Coroutine coro;

  IEnumerator OnElapsed() {
    while(true) {
      yield return new WaitForSeconds(period);
      Elapsed?.Invoke(this);
    }
  }

  void OnEnable() {
    if(coro != null) {
      Debug.LogWarning("Tried to start a coroutine but it was already running.");
      return;
    }
    coro = StartCoroutine(OnElapsed());
  }

  void OnDisable() {
    if(coro == null) {
      Debug.LogWarning("Tried to stop a coroutine that wasn't running.");
      return;
    }
    StopCoroutine(coro);
  }
}
