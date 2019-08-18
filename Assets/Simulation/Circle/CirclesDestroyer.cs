using System.Collections.Generic;

namespace Simulation {
    public class CirclesDestroyer {

        List<Circle> _aliveCircles;


        public void Destroy(Circle[] circles) {
            _aliveCircles = new List<Circle>(circles.Length);
            for (int i = 0; i < circles.Length; i++) {
                if (circles[i].Radius < 0.2f) {
                    circles[i].OnDestroy();
                } else {
                    _aliveCircles.Add(circles[i]);
                }
            }
        }

        public Circle[] GetSurvivors() {
            return  _aliveCircles.ToArray();
        }

    }
}
