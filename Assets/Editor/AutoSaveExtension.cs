using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [InitializeOnLoad]
    public static class AutoSaveExtension
    {
        private const float WAIT_TIME_IN_SECONDS = 300;
        private static float timer;

        static AutoSaveExtension()
        {
            EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts;
            EditorApplication.update += AutoSaveEveryXSeconds;
        }

        private static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingEditMode) Save();
        }

        private static void AutoSaveEveryXSeconds()
        {
            if (Time.realtimeSinceStartup - timer > WAIT_TIME_IN_SECONDS && !EditorApplication.isPlaying &&
                SceneManager.GetActiveScene().isDirty)
                Save();
        }

        private static void Save()
        {
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
            timer = Time.realtimeSinceStartup;
            Debug.Log("Auto saved.");
        }
    }
}