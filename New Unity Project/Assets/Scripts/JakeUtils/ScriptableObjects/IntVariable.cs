using System;
using UnityEngine;

namespace JakeUtils {
  [CreateAssetMenu(menuName = "Jake Utils/Integer Variable")]
  public class IntVariable : GenericVariable<int> {
    /**
     * \brief
     * Decrements the integer if it is positive and returns true.
     * Otherwise, returns value.
     */
    public bool Decrement() {
      var b = Value > 0;
      if(b) Value--;
      return b;
    }

    public void Increment() {
      Value++;
    }
  }
}
