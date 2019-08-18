
using System;

namespace Simulation {
    public class CirclesCollision {

        public Circle C1;
        public Circle C2;

        public double Distance { get; private set; }
        public double Overlap { get; private set; }
        public Vector2 Normal { get; private set; }


        public CirclesCollision(Circle c1, Circle c2) {
            C1 = c1;
            C2 = c2;

            Distance = Math.Sqrt(Vector2.SquaredDistance(C1.Position, C2.Position));
            Normal = (c2.Position - c1.Position) / Distance;
            Overlap = (Distance - C1.Radius - C2.Radius) * 0.5f;
        }

    }
}
