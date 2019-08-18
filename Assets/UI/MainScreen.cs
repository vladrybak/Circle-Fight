using Simulation;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

    [SerializeField] private Slider _gameSpeedSlider;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _loadGameButton;
    [SerializeField] private Button _saveGameButton;

    private GameSave _saver;
    private Simulator _simulator;

    private Action _startNewHandler;


    public void Setup(GameSave saver, Simulator simulator, UnityAction startNewHandler) {
        _simulator = simulator;
        _saver = saver;

        _loadGameButton.onClick.AddListener(LoadButtonClickHandler);
        _saveGameButton.onClick.AddListener(SaveButtonClickHandler);
        _newGameButton.onClick.AddListener(startNewHandler);

        _gameSpeedSlider.onValueChanged.AddListener(SpeedSliderChangeHandler);
        _gameSpeedSlider.maxValue = _simulator.SimulationRateInSeconds * 1000;

        RefreshLoadButton();
    }

    private void LoadButtonClickHandler() {
        _saver.Load();
    }

    private void SaveButtonClickHandler() {
        _saver.Save(_gameSpeedSlider.value);
    }

    private void RefreshLoadButton() {
        _loadGameButton.enabled = GameSave.HasSave;
    }

    private void SpeedSliderChangeHandler(float speed) {
        _simulator.SetSpeed(speed);
    }

    public float GetSpeedSliderValue() {
        return _gameSpeedSlider.value;
    }

    public void SetSpeedSliderValue(float val) {
        _gameSpeedSlider.value = val;
    }


}
