using System;
using UnityEngine;

namespace JakeUtils {
  public class GenericVariable<T> : ScriptableObject, IVariable<T> {
    public T value;
    public T runtimeValue;

    void OnEnable() {
      runtimeValue = value;
    }

    public T InitialValue {
      get {
        return value;
      }
      set {
        this.value = value;
      }
    }

    public T Value {
      get {
        return runtimeValue;
      }
      set {
        runtimeValue = value;
        Changed?.Invoke(value);
      }
    }

    public event Action<T> Changed;
  }
}
