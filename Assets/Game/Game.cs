using UnityEngine;
using Simulation;
using Vector2 = UnityEngine.Vector2;

public class Game : MonoBehaviour {

    [SerializeField] private ViewFactory _viewFactory;
    [SerializeField] private UIFactory _uiFactory;

    private CirclesFactory _circlesFactory;
    private Simulator _simulator;
    private SimulationView _view;

    private MainScreen _mainScreenUI;

    private GameSave _saver;

    private Settings _gameSettings;

    private Vector2 _simulationAreaSize;


    private void Awake() {
        _gameSettings = Configuration.LoadGameConfig();

        _circlesFactory = new CirclesFactory(_gameSettings, GetRandomSeed());
        var generatedCircles = _circlesFactory.GenerateCircles();
        _simulator = new Simulator(generatedCircles, _gameSettings);

        _saver = new GameSave(_simulator, LoadFromSave);

        _simulationAreaSize = new Vector2(_gameSettings.GameAreaWidth, _gameSettings.GameAreaHeight);
        SetupCamera();
        SetupUI();

        SetupSimulationView(generatedCircles);

#if UNITY_EDITOR
        new PauseHandlerForEditor(_simulator, _mainScreenUI);
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
        _mainScreenUI = _uiFactory.CreatemainScreen();
        _mainScreenUI.Setup(_saver, _simulator, StartNew);
    }

    private void Start() {
        _simulator.Start(1);
    }

    public void StartNew() {
        _gameSettings = Configuration.LoadGameConfig();
        _circlesFactory.Configure(_gameSettings, GetRandomSeed());
        Circle[] circles = _circlesFactory.GenerateCircles();

        Reload(circles, _gameSettings, 1);
    }

    public void LoadFromSave(SaveData data) {
        var circles = _circlesFactory.CreateCirclesFromData(data.Circles);
        Reload(circles, data.Settings, data.Speed);
    }

    private void Reload(Circle[] circles, Settings settings, float speed) {
        Destroy(_view.gameObject);
        _simulator.Stop();

        SetupSimulationView(circles);
        _simulator.Reload(circles, settings);
        _mainScreenUI.SetSpeedSliderValue(speed);
        _simulator.SetSpeed(speed);
    }

    private void OnDestroy() {
        _simulator.Stop();
    }

    private int GetRandomSeed() {
        return Random.Range(int.MinValue, int.MaxValue);
    }

}
