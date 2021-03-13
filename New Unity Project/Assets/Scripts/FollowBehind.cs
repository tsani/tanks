using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowBehind : AbstractFollow
{
  public float offsetBack;

  protected override void Follow()
  {
    if(null == target)
      return;
    
    var p = target.position;
    p -= target.forward * offsetBack;

    var q = transform.position;
    q.x = p.x;
    q.z = p.z;

    transform.LookAt(target);
    
    transform.position = q;
  }
}
