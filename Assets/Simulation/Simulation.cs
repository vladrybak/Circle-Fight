using System;
using System.Timers;

namespace Simulation {
    public class Simulation {

        public Circle[] Circles { get; private set; }
        public float SimulationRateInSeconds;

        private Timer _timer;

        private CollisionsDetector _collisionsDetector;
        private CircleCollisionsResolver _circleCollisionsResolver;
        private RectangleBorderCollisionsResolver _borderCollisionsResolver;

        private CirclesDestroyer _circleDestroyer;


        public Simulation(Circle[] circles, Settings simulationSettings) {
            Circles = circles;
            CalculateSimulationRate(simulationSettings.MaxUnitSpeed, simulationSettings.MinUnitRadius);
            var size = new Vector2(simulationSettings.GameAreaWidth, simulationSettings.GameAreaHeight);

            _collisionsDetector = new CollisionsDetector(simulationSettings.NumUnitsToSpawn, size);

            _circleCollisionsResolver = new CircleCollisionsResolver();
            _borderCollisionsResolver = new RectangleBorderCollisionsResolver(size);
            _circleDestroyer = new CirclesDestroyer();
        }

        private void CalculateSimulationRate(float maxUnitSpeed, float minUnitRadius) {
            float maxDistancePerIteration = minUnitRadius / 2f;
            SimulationRateInSeconds = maxDistancePerIteration / maxUnitSpeed;
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
            foreach (var c in Circles)
                MoveCircle(c);

            _collisionsDetector.UpdateCollisions(Circles);

            foreach(var c in _collisionsDetector.BorderCollisions)
                _borderCollisionsResolver.Resolve(c);
            foreach (var c in _collisionsDetector.CircleCollisions)
                _circleCollisionsResolver.Resolve(c);

            _circleDestroyer.Destroy(Circles);
            Circles = _circleDestroyer.GetSurvivors();
        }

        private void MoveCircle(Circle circle) {
            circle.Position += circle.Velocity * SimulationRateInSeconds;
        }

        public void Stop() {
            _timer.Close();
        }

    }
}
