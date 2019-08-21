using Simulation;
using UnityEngine;

public class GameLoader {
    private GameSaver _saver;
    private CirclesFactory _circlesFactory;
    private Simulator _simulator;
    private SimulationView _view;

    public GameLoader(GameSaver saver, CirclesFactory circlesFactory, Simulator simulator, SimulationView view) {
        _circlesFactory = circlesFactory;
        _simulator = simulator;
        _saver = saver;
        _view = view;
    }

    public void StartNew() {
        var settings  = Configuration.LoadGameConfig();
        Circle[] circles = _circlesFactory.GenerateCircles(settings, GetRandomSeed());
        Reload(new Circle[0], circles, settings, 1, 0);
    }

    public void LoadFromSave() {
        var data = _saver.LoadSave();
        var circles = _circlesFactory.CreateCirclesFromData(data.Spawned);
        var waitingForSpawn = _circlesFactory.CreateCirclesFromData(data.WaitingForSpawn);
        Reload(circles, waitingForSpawn, data.Settings, data.Speed, data.Iterations);
    }

    private void Reload(Circle[] circles, Circle[] waitingForSpawn, Settings settings, float speed, int iterations) {
        _view.Clear();
        _view.UpdateSimulationArea(settings);
        _view.ShowCircles(circles);
        _view.ShowCircles(waitingForSpawn);
        _simulator.Restart(waitingForSpawn, circles, settings, iterations);
        _simulator.SetSpeed(speed);
    }

    private int GetRandomSeed() {
        return Random.Range(int.MinValue, int.MaxValue);
    }

}
