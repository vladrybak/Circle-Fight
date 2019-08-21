using System;

namespace Simulation {
        public class CollisionsDetector {

        private readonly Vector2 _areaSize;


        public CollisionsDetector(int elementsCount, Vector2 areaSize) {
            _areaSize = areaSize;
        }

        public bool IsColliding(Circle c1, Circle c2) {
            return Vector2.SquaredDistance(c1.Position, c2.Position) <= Math.Pow(c1.Radius + c2.Radius, 2);
        }

        public Vector2 GetCollidingBorder(Circle c) {
            if (c.Position.X + c.Radius > _areaSize.X)
                return Vector2.Right * _areaSize.X;
            else if (c.Position.X - c.Radius < 0)
                return Vector2.Left;
            else if (c.Position.Y + c.Radius > _areaSize.Y)
                return Vector2.Up * _areaSize.Y;
            else if (c.Position.Y - c.Radius < 0)
                return Vector2.Down;
            return Vector2.Zero;
        }

    }
}
