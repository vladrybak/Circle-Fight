using System;

namespace Simulation {
    public class Circle {

        public Vector2 Position;
        public Vector2 Velocity;
        public double Radius;
        public CircleColor Color;

        public event Action Destroy;


        public void OnDestroy() {
            if (Destroy != null)
                Destroy();
        }

    }
}
