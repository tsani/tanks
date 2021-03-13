using System;
using UnityEngine;

namespace JakeUtils {
  /**
   * \brief
   * A lightweight pub-sub system to mediate events.
   */
  [CreateAssetMenu(menuName = "Jake Utils/Basic Notifier")]
  public class Notifier : ScriptableObject, INotifier<object> {
    public event Action<object> Signalled;

    public void Signal(object obj) => Signalled?.Invoke(obj);

    public void Signal() => Signalled?.Invoke(null);
  }
}
