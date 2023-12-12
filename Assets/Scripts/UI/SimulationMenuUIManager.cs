using Simulation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class SimulationMenuUIManager: MonoBehaviour
    {
        private Slider _slider;
        private Button _playButton;
        private AnimationTimeController _animationTimeController;
        
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            Debug.Log("SimulationMenuUIManager enabled");
            
            _playButton = root.Q<Button>("playButton");
            _playButton.clicked += OnPlayButtonClick;
            
            _slider = root.Q<Slider>("slider");
            
            root.Q<Button>("whiteboardButton").clicked += () => SceneManager.LoadScene("UI");
        }
        
        private void Start()
        {
            SetSliderRange(0, PlayersManager.MaxTime);
            _animationTimeController = GameObject.Find("AnimationTimeController").GetComponent<AnimationTimeController>();
        }
        
        private void Update()
        {
            _slider.value = Time.timeSinceLevelLoad;
        }
        
        private void OnPlayButtonClick()
        {
            _animationTimeController.TogglePlay();
            _playButton.text = _animationTimeController.Playing ? "Pause" : "Play";
        }
        
        private void SetSliderRange(float min, float max)
        {
            _slider.lowValue = min;
            _slider.highValue = max;
        }
    }
}