using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAbove : AbstractFollow
{
  public float offset = 10;
  protected override void Follow() {
    transform.position = target.position;
    transform.position -= transform.forward * offset;
  }
}
