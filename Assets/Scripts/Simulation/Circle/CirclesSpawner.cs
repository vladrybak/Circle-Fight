using System.Collections.Generic;

namespace Simulation {
    public class CirclesSpawner {

        private static object _locker = new object();

        public int RedSpawnedCount = 0;
        public int BlueSpawnedCount = 0;

        public bool HasUnitsToSpawn { get { return _spawnedCount < _unitsToSpawn.Length; } }

        private List<Circle> _activeCircles;

        private Circle[] _unitsToSpawn;
        private int _spawnedCount = 0;
        
        //time in milliseconds
        private int _spawnInterval = 0;
        private int _elapsedTime;


        public CirclesSpawner(Circle[] unitsToSpawn, Circle[] unitsToSpawnImmediate, int interval, List<Circle> activeCircles, int elapsedTime) {
            _elapsedTime = elapsedTime;
            _activeCircles = activeCircles;
            _unitsToSpawn = unitsToSpawn;
            _spawnInterval = interval;

            SpawnImmediate(unitsToSpawnImmediate);
        }

        public void Spawn(int deltaTime) {
            if (_spawnedCount >= _unitsToSpawn.Length)
                return;

            _elapsedTime += deltaTime;
            for(int i = _spawnedCount; i < _unitsToSpawn.Length; i++) {
                int lastSpawnTime = i * _spawnInterval;
                if (_elapsedTime - lastSpawnTime < _spawnInterval)
                    return;

                SpawnCircle(_unitsToSpawn[i]);
            }
        }

        private void SpawnCircle(Circle c ) {
            lock (_locker) {
                c.OnSpawn();
                _activeCircles.Add(c);
                UpdateCounters(c.Color);
            }
        }

        private void UpdateCounters(CircleColor color) {
            _spawnedCount++;
            if (color == CircleColor.Red)
                RedSpawnedCount++;
            else
                BlueSpawnedCount++;
        }

        public List<CircleData> GetWaitingForSpawnData() {
            int waitingCount = _unitsToSpawn.Length - _spawnedCount;
            List<CircleData> waitingData = new List<CircleData>(waitingCount);
            if (waitingCount > 0)
                for (int i = _spawnedCount; i < _unitsToSpawn.Length; i++)
                    waitingData.Add(_unitsToSpawn[i].GetData());
            return waitingData;
        }

        private void SpawnImmediate(Circle[] circles) {
            foreach (var c in circles)
                SpawnCircle(c);
        }

    }
}
