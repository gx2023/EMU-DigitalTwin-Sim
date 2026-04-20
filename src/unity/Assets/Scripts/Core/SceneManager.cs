using UnityEngine;
using System.Collections.Generic;

namespace EMU.DT.Unity.Core
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField] private List<SceneConfig> scenes;
        private Dictionary<string, SceneConfig> sceneMap;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeScenes();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeScenes()
        {
            sceneMap = new Dictionary<string, SceneConfig>();
            foreach (var scene in scenes)
            {
                sceneMap.Add(scene.sceneName, scene);
            }
        }

        public void LoadScene(string sceneName)
        {
            if (sceneMap.ContainsKey(sceneName))
            {
                var scene = sceneMap[sceneName];
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene.sceneIndex);
                Debug.Log($"Loading scene: {sceneName}");
            }
            else
            {
                Debug.LogError($"Scene {sceneName} not found");
            }
        }

        public void LoadScene(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
            Debug.Log($"Loading scene with index: {sceneIndex}");
        }

        public void ReloadCurrentScene()
        {
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
        }

        [System.Serializable]
        public class SceneConfig
        {
            public string sceneName;
            public int sceneIndex;
            public bool isMainScene;
        }
    }
}