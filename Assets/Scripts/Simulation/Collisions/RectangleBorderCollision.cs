namespace Simulation {
    public class RectangleBorderCollision : AbstractCollision {

        public Circle _сircle;
        public Vector2 _border;


        public RectangleBorderCollision(Circle circle, Vector2 border) {
            _сircle = circle;
            _border = border;
        }

        public override void Resolve() {
            if (_border.X > 0)
                _сircle.Position = new Vector2(_border.X - _сircle.Radius, _сircle.Position.Y);
            else if (_border.Y > 0)
                _сircle.Position = new Vector2(_сircle.Position.X, _border.Y - _сircle.Radius);
            else if (_border.Y < 0)
                _сircle.Position = new Vector2(_сircle.Position.X, _сircle.Radius);
            else
                _сircle.Position = new Vector2(_сircle.Radius, _сircle.Position.Y);

            //Bounce(_сircle, -_border.Normalized());
            BounceBack(_сircle);
        }

    }
}
