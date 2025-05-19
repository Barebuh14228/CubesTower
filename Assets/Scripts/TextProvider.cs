using System;
using System.Collections.Generic;
using UnityEngine;

public static class TextProvider
{
    private static Dictionary<string, string> _texts;

    public static void Init(string textsJson)
    {
        _texts = JsonUtility.FromJson<TextsDictionary>(textsJson).ToDictionary();
    }

    public static string Get(string key, params object[] args)
    {
        return string.Format(_texts[key], args);
    }
}

[Serializable]
public class TextsDictionary
{
    public TextKeyValuePair[] Texts;

    public Dictionary<string, string> ToDictionary()
    {
        var output = new Dictionary<string, string>();

        foreach (var kv in Texts)
        {
            if (output.ContainsKey(kv.Key))
            {
                Debug.LogWarning($"Found duplicate key in texts: {kv.Key}");
                continue;
            }
            
            output.Add(kv.Key, kv.Value);
        }

        return output;
    }
}

[Serializable]
public class TextKeyValuePair
{
    public string Key;
    public string Value;
}