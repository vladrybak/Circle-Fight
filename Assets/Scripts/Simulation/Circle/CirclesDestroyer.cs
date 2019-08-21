using System.Collections.Generic;

namespace Simulation {
    public class CirclesDestroyer {

        public int RedDestroyedCount = 0;
        public int BlueDestroyedCount = 0;

        private List<Circle> _activeCircles;

        public CirclesDestroyer(List<Circle> activeCircles) {
            _activeCircles = activeCircles;
        }

        public void Destroy(Circle circle) {
            circle.OnDestroy();
            _activeCircles.Remove(circle);
            UpdateCounters(circle.Color);
        }

        private void UpdateCounters(CircleColor color) {
            if (color == CircleColor.Red)
                RedDestroyedCount++;
            else
                BlueDestroyedCount++;
        }

    }
}
