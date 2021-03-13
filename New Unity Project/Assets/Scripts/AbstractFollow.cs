using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFollow : MonoBehaviour
{
  public Transform target;

  public enum FollowMode {
    PHYSICS,
    ORDINARY
  };

  public FollowMode mode;

  // Start is called before the first frame update
  void Start()
  {
    if(null == target)
      Debug.LogWarning("FollowBehind has no target.");
  }

  protected abstract void Follow();

  void Update() {
    if (mode == FollowMode.ORDINARY)
      Follow();
  }

  void FixedUpdate() {
    if (mode == FollowMode.PHYSICS)
      Follow();
  }
  
}
