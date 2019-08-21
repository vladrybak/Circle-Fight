using System;

namespace Simulation {
    public class Circle {

        public event Action Spawn;
        public event Action Destroy;

        public Vector2 Position;
        public Vector2 Velocity;
        public double Radius;
        public CircleColor Color;

        private CircleData _data;


        public Circle(CircleData data) {
            _data = data;
            Color = _data.Color;
        }

        public void OnDestroy() {
            if (Destroy != null)
                Destroy();
        }

        public void OnSpawn() {
            Position = _data.Position;
            Velocity = _data.Velocity;
            Radius = _data.Radius;

            if (Spawn != null)
                Spawn();
        }

        public CircleData UpdateAndGetData() {
            _data.Position = Position;
            _data.Radius = Radius;
            _data.Velocity = Velocity;
            _data.Color = Color;
            return _data;
        }

        public CircleData GetData() {
            return _data;
        }

    }
}
