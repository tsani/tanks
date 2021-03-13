using System.Collections.Generic;
using UnityEngine;

namespace JakeUtils {
  [CreateAssetMenu(menuName = "Jake Utils/GameObject List Variable")]
  public class GameObjectListVariable : GenericVariable<List<GameObject>> {
    void OnEnable() {
      Value = new List<GameObject>();
    }
  }
}
