using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;

namespace Simulation {
    public class Simulator {

        private static object _lock = new object();

        public event Action SimulationComplete;

        public int RedCirclesCount { get { return _circleSpawner.RedSpawnedCount - _circleDestroyer.RedDestroyedCount; } }
        public int BlueCirclesCount { get { return _circleSpawner.BlueSpawnedCount - _circleDestroyer.BlueDestroyedCount; } }

        public Settings Settings { get; private set; }
        public float Speed { get; private set; }
        public float SimulationRateInSeconds { get; private set; }
        public int IterationsCount { get; private set; }
        private Timer _timer;

        private List<Circle> _activeCircles;

        private CollisionsDetector _collisionsDetector;

        private CirclesSpawner _circleSpawner;
        private CirclesDestroyer _circleDestroyer;


        public Simulator() {
        }

        public void Restart(Circle[] unspawnedCircles, Circle[] spawnedCircles, Settings settings, int iterations) {
            IterationsCount = iterations;
            if (_timer != null) Clear();
            Init(unspawnedCircles, spawnedCircles, settings);
            Start();
        }

        private void Init(Circle[] unspawnedCircles, Circle[] spawnedCircles, Settings settings) {
            Settings = settings;

            _activeCircles = new List<Circle>(spawnedCircles.Length + unspawnedCircles.Length);
            var size = new Vector2(settings.GameAreaWidth, settings.GameAreaHeight);

            CalculateSimulationRate(settings.MaxUnitSpeed, settings.MinUnitRadius);
            int elapsedTime = IterationsCount * DeltaTime();

            _circleSpawner = new CirclesSpawner(unspawnedCircles, spawnedCircles, settings.UnitSpawnDelay, _activeCircles, elapsedTime);
            _circleDestroyer = new CirclesDestroyer(_activeCircles);

            _collisionsDetector = new CollisionsDetector(settings.NumUnitsToSpawn, size);
        }

        private void CalculateSimulationRate(float maxUnitSpeed, float minUnitRadius) {
            float maxDistancePerIteration = minUnitRadius / 2f;
            SimulationRateInSeconds = maxDistancePerIteration / maxUnitSpeed;
        }

        private void Start(float speed =1f) {
            _timer = new Timer();
            _timer.Elapsed += Update;
            SetSpeed(speed);
            _timer.Start();
        }

        public void SetSpeed(float speed) {
            Speed = speed;
            _timer.Enabled = Speed > 0;
            if (_timer.Enabled)
                _timer.Interval = SimulationRateInSeconds * 1000 * 1 / Speed;
        }

        private void Update(object sender, ElapsedEventArgs e) {
            if (_circleSpawner.HasUnitsToSpawn) {
                _circleSpawner.Spawn(DeltaTime());
            } else {
                ProcessCirclesMovement();
            }
            IterationsCount++;
            TryComplete();
        }

        private int DeltaTime() {
            return (int)(SimulationRateInSeconds * 1000);
        }

        private void ProcessCirclesMovement() {
            for (int i = 0; i < _activeCircles.Count; i++) {
                _activeCircles[i].Position += _activeCircles[i].Velocity * SimulationRateInSeconds;
                for (int j = i + 1; j < _activeCircles.Count; j++) {
                    if (_collisionsDetector.IsColliding(_activeCircles[i], _activeCircles[j])) {
                        lock (_lock) new CirclesCollision(_activeCircles[i], _activeCircles[j], _circleDestroyer).Resolve();
                    }
                }
                var crossedBorder = _collisionsDetector.GetCollidingBorder(_activeCircles[i]);
                if (crossedBorder != Vector2.Zero)
                    new RectangleBorderCollision(_activeCircles[i], crossedBorder).Resolve();
            }
        }

        private void TryComplete() {
            if (_circleSpawner.HasUnitsToSpawn)
                return;

            if ( RedCirclesCount == 0 || BlueCirclesCount == 0) {
                SimulationComplete();
                _timer.Stop();
            }
        }

        public List<CircleData[]> GetCirclesData() {
            List<CircleData[]> data = new List<CircleData[]>();
            var waitingData = _circleSpawner.GetWaitingForSpawnData().ToArray();
            CircleData[] spawned = new CircleData[_activeCircles.Count];
            for (int i = 0; i < _activeCircles.Count; i++) {
                spawned[i] = _activeCircles[i].UpdateAndGetData();
            }
            data.Add(waitingData);
            data.Add(spawned);
            return data;
        }

        public void Clear() {
            _timer.Close();
            _activeCircles.Clear();

            _collisionsDetector = null;

            _circleSpawner = null;
            _circleDestroyer = null;

            GC.Collect();
        }

    }
}
