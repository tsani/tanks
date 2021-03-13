using System;
using UnityEngine;

namespace JakeUtils {
  [CreateAssetMenu(menuName = "Jake Utils/RNG")]
  public class RNG : ScriptableObject {
    public System.Random random;
    
    void OnEnable() {
      random = new System.Random();
    }
  }
}
