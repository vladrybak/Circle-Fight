using System;

namespace Simulation {
    public class CircleCollisionsResolver {

        public void Resolve(CirclesCollision collision) {
            if (collision.C1.Color == collision.C2.Color)
                //Bounce(collision.C1, collision.C2, collision.Normal, collision.Overlap);
                BounceBack(collision.C1, collision.C2, collision.Normal, collision.Overlap);
            else
                Reduce(collision.C1, collision.C2, collision.Overlap);
        }

        private void Bounce(Circle c1, Circle c2, Vector2 normal, double overlap) {
            c1.Position += normal * overlap;
            c2.Position -= normal * overlap;

            c1.Velocity = c1.Velocity.Reflect(normal);
            c2.Velocity = c2.Velocity.Reflect(normal);
        }

        private void Reduce(Circle c1, Circle c2, double overlap) {
            double reduceValue = Math.Abs(overlap);
            c1.Radius -= reduceValue;
            c2.Radius -= reduceValue;
        }

        private void BounceBack(Circle c1, Circle c2, Vector2 normal, double overlap) {
            c1.Position += normal * overlap;
            c2.Position -= normal * overlap;
            c1.Velocity = -c1.Velocity;
            c2.Velocity = -c2.Velocity;
        }

    }
}
