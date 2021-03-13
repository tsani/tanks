using System;

namespace JakeUtils {
  public interface IVariable<T> {
    /**
     * \brief
     * The initial value of the variable. This should be set in the inspector.
     */
    T InitialValue { get; set; }

    /**
     * \brief
     * The runtime value of the variable. This changes at runtime, and
     * changes to this value should invoke the Changed event.
     */
    T Value { get; set; }

    /**
     * \brief
     * Fires every time the variable changes.
     */
    event Action<T> Changed;
  }
}
