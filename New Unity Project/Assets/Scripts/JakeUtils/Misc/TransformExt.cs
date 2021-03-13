using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JakeUtils {
  public static class TransformExt {
    public static IEnumerable<Transform> Children(this Transform self) {
      for(var i = 0; i < self.childCount; i++) {
        yield return self.GetChild(i);
      }
    }
  }
}
