using UnityEngine;

namespace JakeUtils {
  public static class Vector2Ext {
    /**
     * \brief
     * Upgrades this Vector2 to a Vector3, by considering the Vector2
     * as a point in the xz-plane and injecting a given y coordinate.
     */
    public static Vector3 WithY(this Vector2 self, float y) =>
      new Vector3(self.x, y, self.y);
  }
}
