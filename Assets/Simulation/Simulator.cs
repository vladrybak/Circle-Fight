using System;
using System.Collections.Generic;
using System.Timers;

namespace Simulation {
    public class Simulator {

        public Settings SimulationSettings { get; protected set; }

        private List<Circle> _activeCircles;

        public float SimulationRateInSeconds;
        private Timer _timer;

        private CollisionsDetector _collisionsDetector;
        private CircleCollisionsResolver _circleCollisionsResolver;
        private RectangleBorderCollisionsResolver _borderCollisionsResolver;

        private CirclesSpawner _circleSpawner;
        private CirclesDestroyer _circleDestroyer;


        public Simulator(Circle[] circles, Settings simulationSettings) {
            Init(circles, simulationSettings);
        }

        private void Init(Circle[] circles, Settings simulationSettings) {
            SimulationSettings = simulationSettings;

            _activeCircles = new List<Circle>(circles.Length);
            var size = new Vector2(simulationSettings.GameAreaWidth, simulationSettings.GameAreaHeight);

            CalculateSimulationRate(simulationSettings.MaxUnitSpeed, simulationSettings.MinUnitRadius);

            _collisionsDetector = new CollisionsDetector(simulationSettings.NumUnitsToSpawn, size);

            _circleCollisionsResolver = new CircleCollisionsResolver();
            _borderCollisionsResolver = new RectangleBorderCollisionsResolver(size);

            _circleSpawner = new CirclesSpawner(circles, simulationSettings.UnitSpawnDelay);
            _circleDestroyer = new CirclesDestroyer();
        }

        private void CalculateSimulationRate(float maxUnitSpeed, float minUnitRadius) {
            float maxDistancePerIteration = minUnitRadius / 2f;
            SimulationRateInSeconds = maxDistancePerIteration / maxUnitSpeed;
        }

        public void Load(Circle[] circles, float rate) {
            _activeCircles.AddRange(circles);
            SimulationRateInSeconds = rate;
            Start();
        }

        public void Start(float speed =1f) {
            _timer = new Timer();
            _timer.Elapsed += Update;
            SetSpeed(speed);
            _timer.Start();
        }

        public void SetSpeed(float speed) {
            _timer.Enabled = speed > 0;
            if (_timer.Enabled)
                _timer.Interval = SimulationRateInSeconds * 1000 * 1 / speed;
        }

        private void Update(object sender, ElapsedEventArgs e) {
            if (_circleSpawner.HasUnitsToSpawn) {
                _circleSpawner.Spawn(e.SignalTime.Millisecond, _activeCircles);
            } else {
                MoveCircles();
            }
            ProcessCollisions();
            _circleDestroyer.Destroy(_activeCircles);
        }

        private void MoveCircles() {
            foreach (var c in _activeCircles)
                MoveCircle(c);
        }

        private void ProcessCollisions() {
            _collisionsDetector.UpdateCollisions(_activeCircles);

            foreach (var c in _collisionsDetector.BorderCollisions)
                _borderCollisionsResolver.Resolve(c);
            foreach (var c in _collisionsDetector.CircleCollisions)
                _circleCollisionsResolver.Resolve(c);
        }

        private void MoveCircle(Circle circle) {
            circle.Position += circle.Velocity * SimulationRateInSeconds;
        }

        public CircleData[] GetCirclesData() {
            CircleData[] data = new CircleData[_activeCircles.Count];
            for (int i = 0; i < _activeCircles.Count; i++) {
                data[i] = _activeCircles[i].GetDataForSave();
            }
            return data;
        }

        public void Reload(Circle[] circles, Settings simulationSettings) {
            Stop();
            Init(circles, simulationSettings);
            _circleSpawner.SpawnImmediate();
            Start();
        }

        public void Stop() {
            _timer.Close();
            _activeCircles.Clear();

            _collisionsDetector = null;
            _circleCollisionsResolver = null;
            _borderCollisionsResolver = null;

            _circleSpawner = null;
            _circleDestroyer = null;

            GC.Collect();
        }

    }
}
