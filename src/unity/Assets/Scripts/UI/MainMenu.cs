
using UnityEngine;
using UnityEngine.UI;

namespace EMU.DT.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button loginButton;
        [SerializeField] private Button exitButton;
        
        private void Awake()
        {
            loginButton.onClick.AddListener(OnLoginClicked);
            exitButton.onClick.AddListener(OnExitClicked);
        }
        
        private void OnLoginClicked()
        {
            Debug.Log("Login clicked");
        }
        
        private void OnExitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
