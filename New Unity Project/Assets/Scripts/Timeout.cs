using System;
using System.Collections;
using UnityEngine;

/// <summary>
///   Destroys the object after a given timeout.
/// </summary>
public class Timeout : MonoBehaviour {
  [Tooltip("Seconds after which the object will de-spawn.")]
  public float timeout = 20;

  [Tooltip("Whether the timeout should begin immediately.")]
  public bool autostart = true;

  /// <summary>
  ///   Invoked when the timeout elapses.
  /// </summary>
  public event Action<Timeout> Elapsed;

  Coroutine timeoutCoroutine;

  IEnumerator DespawnTimeout(float seconds) {
    yield return new WaitForSeconds(seconds);
    Elapsed?.Invoke(this);
    GameObject.Destroy(gameObject);
  }

  void Start() {
    if(autostart) Begin();
  }

  public void Begin() {
    if(null == timeoutCoroutine)
      timeoutCoroutine = StartCoroutine(DespawnTimeout(timeout));
    else
      Debug.LogWarning("Called Begin on timeout that was already started");
  }

  public void Cancel() {
    if(null != timeoutCoroutine)
      StopCoroutine(timeoutCoroutine);
    else
      Debug.LogWarning("Tried to cancel a timeout that hadn't started");
  }
}
