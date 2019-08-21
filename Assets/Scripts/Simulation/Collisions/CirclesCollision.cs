using System;

namespace Simulation {
    public class CirclesCollision : AbstractCollision {

        CirclesDestroyer _destroyer;

        private Circle _c1;
        private Circle _c2;


        public CirclesCollision(Circle c1, Circle c2, CirclesDestroyer destroyer) {
            _destroyer = destroyer;
            _c1 = c1;
            _c2 = c2;
        }

        public override void Resolve() {
            var distance = Math.Sqrt(Vector2.SquaredDistance(_c1.Position, _c2.Position));
            var normal = distance > 0 ? (_c2.Position - _c1.Position) / distance : Vector2.Up;
            var overlap = distance > 0 ? (distance - _c1.Radius - _c2.Radius) * 0.5f : _c1.Radius + _c2.Radius;

            if (_c1.Color == _c2.Color) {
                _c1.Position += normal * overlap;
                _c2.Position -= normal * overlap;
                BounceBack(_c1);
                BounceBack(_c2);
            } else {
                Shrink(overlap);
            }
        }

        private void Shrink(double overlap) {
            double reduceValue = Math.Abs(overlap);
            _c1.Radius -= reduceValue;
            _c2.Radius -= reduceValue;
            if (_c1.Radius < 0.2f || _c2.Radius < 0.2f) {
                _destroyer.Destroy(_c1);
                _destroyer.Destroy(_c2);
            }
        }

    }
}
