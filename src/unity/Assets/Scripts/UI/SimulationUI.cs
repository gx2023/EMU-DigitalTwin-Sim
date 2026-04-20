
using UnityEngine;
using UnityEngine.UI;
using EMU.DT.Simulation;

namespace EMU.DT.UI
{
    public class SimulationUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button backButton;
        
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartClicked);
            stopButton.onClick.AddListener(OnStopClicked);
            backButton.onClick.AddListener(OnBackClicked);
        }
        
        private void OnStartClicked()
        {
            var emu = DepotController.Instance.GetEMU(0);
            if (emu != null)
            {
                emu.StartMovement();
            }
        }
        
        private void OnStopClicked()
        {
            var emu = DepotController.Instance.GetEMU(0);
            if (emu != null)
            {
                emu.StopMovement();
            }
        }
        
        private void OnBackClicked()
        {
            Debug.Log("Back clicked");
        }
    }
}
