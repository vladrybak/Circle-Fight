using System;
using System.Collections.Generic;

namespace Simulation {

    public class CollisionsDetector {

        public List<CirclesCollision> CircleCollisions;
        public List<RectangleBorderCollision> BorderCollisions;

        private readonly Vector2 _areaSize;


        public CollisionsDetector(int elementsCount, Vector2 areaSize) {
            CircleCollisions = new List<CirclesCollision>(elementsCount);
            BorderCollisions = new List<RectangleBorderCollision>(elementsCount);
            _areaSize = areaSize;
        }

        public void UpdateCollisions(List<Circle> circles) {
            CircleCollisions.Clear();
            BorderCollisions.Clear();
            for(int i = 0; i < circles.Count; i++) {
                CheckEdgeCollisions(circles[i]);
                for(int j = i + 1; j < circles.Count; j++) 
                    CheckCircleCollision(circles[i], circles[j]);    
            }
        }

        private void CheckCircleCollision(Circle c1, Circle c2) {
            if (Vector2.SquaredDistance(c1.Position, c2.Position) <= Math.Pow(c1.Radius + c2.Radius, 2))
                CircleCollisions.Add(new CirclesCollision(c1, c2));
        }

        private void CheckEdgeCollisions(Circle c) {
            if (c.Position.X + c.Radius >= _areaSize.X)
                BorderCollisions.Add(new RectangleBorderCollision(c, Vector2.Right));
            else if (c.Position.X - c.Radius <= 0)
                BorderCollisions.Add(new RectangleBorderCollision(c, Vector2.Left));
            else if (c.Position.Y + c.Radius >= _areaSize.Y)
                BorderCollisions.Add(new RectangleBorderCollision(c, Vector2.Up));
            else if (c.Position.Y - c.Radius <= 0)
                BorderCollisions.Add(new RectangleBorderCollision(c, Vector2.Down));
        }

    }
}
