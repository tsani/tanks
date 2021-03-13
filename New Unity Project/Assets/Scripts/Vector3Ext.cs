using System;
using UnityEngine;

public static class Vector3Ext {
  public static Vector3 WhereX(this Vector3 self, Func<float, float> f) {
    self.x = f(self.x);
    return self;
  }

  public static Vector3 WhereY(this Vector3 self, Func<float, float> f) {
    self.y = f(self.y);
    return self;
  }

  public static Vector3 WhereZ(this Vector3 self, Func<float, float> f) {
    self.z = f(self.z);
    return self;
  }
  
  public static Vector3 Where(this Vector3 self, Coordinate c, Func<float, float> f) =>
    c.Eliminate(
      () => self.WhereX(f),
      () => self.WhereY(f),
      () => self.WhereZ(f));
}
