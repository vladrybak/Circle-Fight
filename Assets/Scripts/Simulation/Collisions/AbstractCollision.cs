namespace Simulation {
    public abstract class AbstractCollision {

        public abstract void Resolve();

        protected void Bounce(Circle c, Vector2 normal) {
            c.Velocity = c.Velocity.Reflect(normal);
        }

        protected void BounceBack(Circle c) {
            c.Velocity = -c.Velocity;
        }
    }
}
