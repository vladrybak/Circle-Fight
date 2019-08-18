using System.Collections.Generic;

namespace Simulation {
    public class CirclesDestroyer {

        public void Destroy(List<Circle> circles) {
            for (int i = 0; i < circles.Count; i++) {
                if (circles[i].Radius < 0.2f) {
                    circles[i].OnDestroy();
                    circles.Remove(circles[i]);
                    i--;
                } 
            }
        }

    }
}
