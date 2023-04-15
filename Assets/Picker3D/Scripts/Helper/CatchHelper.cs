using UnityEngine;

namespace Picker3D.Helper
{
    public static class CatchHelper
    {
        public static bool TryGetComponentThisOrChildOrParent<T>(GameObject gameObject, out T component)
        {
            if (gameObject.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }

            if (gameObject.transform.parent != null)
            {
                if (gameObject.transform.parent.TryGetComponent(out T tInParent))
                {
                    component = tInParent;
                    return true;
                }
            }

            component = gameObject.GetComponentInChildren<T>();

            return component != null;
        }
        
        public static bool TryGetComponentThisOrChild<T>(GameObject gameObject, out T component)
        {
            if (gameObject.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }

            component = gameObject.GetComponentInChildren<T>();

            return component != null;
        }
    }
}
