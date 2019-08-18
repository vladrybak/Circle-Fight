namespace Simulation {
    public class RectangleBorderCollisionsResolver {

        private Vector2 _areaSize;


        public RectangleBorderCollisionsResolver(Vector2 areaSize) {
            _areaSize = areaSize;
        }

        public void Resolve(RectangleBorderCollision collision) {
            Displace(collision);
            //Bounce(collision);
            BounceBack(collision);
        }

        private void Displace(RectangleBorderCollision collision) {
            var circle = collision.Circle;
            var radius = circle.Radius;
            if (collision.Border.X > 0)
                circle.Position = new Vector2(_areaSize.X - radius, circle.Position.Y);
            else if (collision.Border.Y > 0)
                circle.Position = new Vector2(circle.Position.X, _areaSize.Y - radius);
            else if (collision.Border.Y < 0)
                circle.Position = new Vector2(circle.Position.X, radius);
            else
                circle.Position = new Vector2(radius, circle.Position.Y);
        }

        private void Bounce(RectangleBorderCollision collision) {
            collision.Circle.Velocity = collision.Circle.Velocity.Reflect(-collision.Border);
        }

        private void BounceBack(RectangleBorderCollision collision) {
            collision.Circle.Velocity = -collision.Circle.Velocity;
        }

    }
}
