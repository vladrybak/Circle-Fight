using UnityEngine;
using Simulation;
using System;

public class Game : MonoBehaviour {

    [SerializeField] private ViewFactory _viewFactory;
    [SerializeField] private UIFactory _uiFactory;
    [SerializeField] private Transform _canvas;

    private GameLoader _loader;
    private Simulator _simulator;
    private GameSaver _saver;
    private SimulationView _view;

    private MainScreen _mainUI;

    private bool _isGameComplete = false;


    private void Awake() {
        _simulator = new Simulator();
        _simulator.SimulationComplete += GameCompleteHandler;
        _saver = new GameSaver(_simulator);
        var circlesFactory = new CirclesFactory();

        _view = _viewFactory.CreateSimulationView();
        _view.Setup(_viewFactory);

        _uiFactory.Setup(_canvas);

        _loader = new GameLoader(_saver, circlesFactory, _simulator, _view);
    }

    private void Start() {
        _loader.StartNew();
        _mainUI = _uiFactory.CreatemainScreen();
        _mainUI.Setup(_saver, _simulator, _loader);
        SetupEditor();
    }

    private void SetupEditor() {
#if UNITY_EDITOR
        new PauseHandlerForEditor(_simulator, _mainUI);
#endif
    }

    public void SetDpeed(float speed) {
        _simulator.SetSpeed(speed);
    }

    private void Update() {
        float total = _simulator.RedCirclesCount + _simulator.BlueCirclesCount;
        float val = total > 0 ? _simulator.RedCirclesCount / total : 0.5f;
        _mainUI.SetAdvantageSliderValue(val);
        if(_isGameComplete) {
            _mainUI.gameObject.SetActive(false);
            _view.gameObject.SetActive(false);
            ResultsPanel resultsPanel = _uiFactory.CreateResultsPanel();
            double elapsedTime = _simulator.SimulationRateInSeconds * _simulator.IterationsCount;
            resultsPanel.Setup(TimeSpan.FromSeconds(elapsedTime), _simulator.RedCirclesCount, _simulator.BlueCirclesCount, PlayAgainClickedHandler);
            _isGameComplete = false;
        }
    }

    private void PlayAgainClickedHandler() {
        _mainUI.gameObject.SetActive(true);
        _view.gameObject.SetActive(true);
        _loader.StartNew();
    }

    private void GameCompleteHandler() {
        _isGameComplete = true;
    }

    private void OnDestroy() {
        _simulator.Clear(); 
    }

}
