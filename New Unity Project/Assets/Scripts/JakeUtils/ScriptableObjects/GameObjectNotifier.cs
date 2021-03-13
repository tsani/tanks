using System;
using UnityEngine;

namespace JakeUtils {
  [CreateAssetMenu(menuName = "Jake Utils/Game Object Notifier")]
  public class GameObjectNotifier : ScriptableObject, INotifier<GameObject> {
    public event Action<GameObject> Signalled;
    public void Signal(GameObject obj) => Signalled?.Invoke(obj);
  }
}
