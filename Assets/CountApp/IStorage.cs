#if UNITY_EDITOR
    using UnityEditor;
#endif
    using FrameworkDesign;
    using UnityEngine;

namespace CountApp
{
    public interface IStorage : IUtility
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int defaultValue = 0);
    }

    public class PlayerPrefsStorage : IStorage
    {
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
    }

#if UNITY_EDITOR
    public class EditorPrefsStorage : IStorage
    {
        public void SaveInt(string key, int value)
        {
            EditorPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return EditorPrefs.GetInt(key, defaultValue);
        }
    }
#endif
}