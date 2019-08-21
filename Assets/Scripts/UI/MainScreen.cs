using Simulation;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

    [SerializeField] private Slider _advantageSlider;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _saveGameButton;

    [SerializeField] private Text _redCount;
    [SerializeField] private Text _blueCount;

    private GameSaver _saver;
    private GameLoader _loader;
    private Simulator _simulator;

    public void Setup(GameSaver saver, Simulator simulator, GameLoader loader) {
        _simulator = simulator;
        _saver = saver;
        _loader = loader;

        _loadGameButton.onClick.AddListener(LoadButtonClickHandler);
        _saveGameButton.onClick.AddListener(SaveButtonClickHandler);
        _newGameButton.onClick.AddListener(StartNewClickHandler);

        _speedSlider.maxValue = _simulator.SimulationRateInSeconds * 1000;
        _speedSlider.onValueChanged.AddListener(_simulator.SetSpeed);

        RefreshLoadButton();
    }

    private void StartNewClickHandler() {
        _loader.StartNew();
        _speedSlider.value = _simulator.Speed;
    }

    private void LoadButtonClickHandler() {
        _loader.LoadFromSave();
        _speedSlider.value = _simulator.Speed;
    }

    private void SaveButtonClickHandler() {
        _saver.WriteSave();
        RefreshLoadButton();
    }

    private void RefreshLoadButton() {
        _loadGameButton.enabled = GameSaver.HasSave;
    }

    public float GetSpeedSliderValue() {
        return _speedSlider.value;
    }

    public void SetSpeedSliderValue(float val) {
        _speedSlider.value = val;
    }

    public void SetAdvantageSliderValue(float val) {
        _advantageSlider.value = val;
    }

    private void Update() {
        _blueCount.text = _simulator.BlueCirclesCount.ToString();
        _redCount.text = _simulator.RedCirclesCount.ToString();

    }

}
