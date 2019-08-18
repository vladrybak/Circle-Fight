using System.Collections.Generic;
using UnityEngine;

namespace Simulation {
    public class CirclesSpawner {

        private Circle[] _unitsToSpawn;
        private int _spawnedCount = 0;
        public bool HasUnitsToSpawn { get { return _spawnedCount < _unitsToSpawn.Length; } }
        
        //time in milliseconds
        private int _spawnInterval = 0;
        private int _elapsedTime;


        public CirclesSpawner(Circle[] unitsToSpawn, int interval) {
            _unitsToSpawn = unitsToSpawn;
            _spawnInterval = interval;
        }

        public void Spawn(int deltaTime, List<Circle> circles) {
            if (_spawnedCount >= _unitsToSpawn.Length)
                return;

            _elapsedTime += deltaTime;
            for(int i = _spawnedCount; i < _unitsToSpawn.Length; i++) {
                int lastSpawnTime = i * _spawnInterval;
                if (_elapsedTime - lastSpawnTime < _spawnInterval)
                    return;

                _unitsToSpawn[i].OnSpawn();
                circles.Add(_unitsToSpawn[i]);
                _spawnedCount++;
            }
        }

    }
}
