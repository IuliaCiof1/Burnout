using System.Collections.Generic;
using UnityEngine;

public class GlobalStateManager : MonoBehaviour
{
    public static List<int> CollectedKeys = new List<int>();
    public static Dictionary<string, bool> GlobalBooleans = new Dictionary<string, bool>();

    // Utility Methods (Optional)
    public static void AddKey(int keyId)
    {
        if (!CollectedKeys.Contains(keyId))
        {
            CollectedKeys.Add(keyId);
        }
    }

    public static bool HasKey(int keyId)
    {
        return CollectedKeys.Contains(keyId);
    }

    public static void SetBoolean(string key, bool value)
    {
        GlobalBooleans[key] = value;
    }

    public static bool GetBoolean(string key)
    {
        return GlobalBooleans.TryGetValue(key, out bool value) && value;
    }


}
