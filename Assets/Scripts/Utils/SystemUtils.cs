using System;
using UnityEngine;

namespace TEDinc.Utils
{
    public static class SystemUtils
    {
        public static Component GetComponentWhichSubclassOrHaveInterface(GameObject gameObject, Type condition)
        {
            if (gameObject != null)
                foreach (Component component in gameObject.GetComponents(typeof(MonoBehaviour)))
                    if (IsSubclassOrHaveInterface(component.GetType(), condition))
                        return component;

            return null;
        }

        public static bool IsSubclassOrHaveInterface(Type testable, Type condition)
        {
            return testable.GetInterface(condition.Name) != null
                    || testable.IsSubclassOf(condition) == true;
        }
    }
}