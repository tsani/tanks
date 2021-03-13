using UnityEngine;

namespace JakeUtils {
  public static class Vector3Ext {
    /**
     * \brief
     * Drop the Y component of this vector to get a Vector2.
     */
    public static Vector2 DropY(this Vector3 self) =>
      new Vector2(self.x, self.z);

    /**
     * \brief
     * Constructs a new vector with an adjusted Y coordinate.
     */
    public static Vector3 WithY(this Vector3 self, float y) =>
      new Vector3(self.x, y, self.z);

    /**
     * \brief
     * Projects this vector onto another.
     * The returned vector is colinear with `direction`, which must be
     * a unit vector.
     * Both vectors must have the same origin for this to make sense.
     */
    public static Vector3
    ProjectOnto(this Vector3 self, Vector3 direction) {
      Debug.Assert(
        Mathf.Approximately(direction.sqrMagnitude, 1),
        "direction is a unit vector");

      var p = Vector3.Dot(self, direction) * direction;

      return p;
    }
  }
}
