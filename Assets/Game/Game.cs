using UnityEngine;
using Simulation;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using System;

public class Game : MonoBehaviour {

    [SerializeField] private ViewFactory _viewFactory;
    [SerializeField] private Slider _gameSpeedSlider;

    private CirclesFactory _circlesFactory;
    private Simulation.Simulation _simulation;
    private SimulationView _view;

    private Settings _gameSettings;

    private Vector2 _simulationAreaSize;



    private void Awake() {
        _gameSettings = Configuration.LoadGameConfig();

        _circlesFactory = new CirclesFactory(_gameSettings, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
        var generatedCircles = _circlesFactory.GenerateCircles();
        _simulation = new Simulation.Simulation(generatedCircles, _gameSettings);

        _simulationAreaSize = new Vector2(_gameSettings.GameAreaWidth, _gameSettings.GameAreaHeight);
        SetupCamera();
        SetupUI();

        SetupSimulationView(generatedCircles);

#if UNITY_EDITOR
        new PauseHandlerForEditor(_gameSpeedSlider);
#endif
    }

    private void SetupSimulationView(Circle[] circles) {
        _view = _viewFactory.CreateSimulationView(_simulationAreaSize, circles);
        _view.Setup(_simulationAreaSize);
    }

    private void SetupCamera() {
        if (_simulationAreaSize.x > _simulationAreaSize.y) {
            Camera.main.orthographicSize = _simulationAreaSize.x / (Camera.main.aspect * 2);
        } else {
            Camera.main.orthographicSize = _simulationAreaSize.y / 2f;
        }
    }

    private void SetupUI() {
        _gameSpeedSlider.onValueChanged.AddListener(ChangeSimulationSpeed);
        _gameSpeedSlider.maxValue = _simulation.SimulationRateInSeconds * 1000;
    }

    private void Start() {
        _simulation.Start(1);
    }

    public void ChangeSimulationSpeed(float speed) {
        _simulation.SetSpeed(speed);
    }

    private void Update() {

    }

    private void OnDestroy() {
        _simulation.Stop();
    }

}
