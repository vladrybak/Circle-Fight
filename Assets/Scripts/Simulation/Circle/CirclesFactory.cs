using System;

namespace Simulation {
    public class CirclesFactory {

        private static object _locker = new object();

        private Settings _settings;
        private Random _rand;


        public Circle[] GenerateCircles(Settings settings, int randomSeed) {
            _settings = settings;
            _rand = new Random(randomSeed);

            var circles = new Circle[_settings.NumUnitsToSpawn];

            for (int i = 0; i < circles.Length; i++) 
                circles[i] = CreateRandomCircle((i % 2 == 0) ? CircleColor.Red : CircleColor.Blue);

            return circles;
        }

        public Circle[] CreateCirclesFromData(CircleData[] data) {
            Circle[] circles = new Circle[data.Length];
            lock (_locker) {
                for (int i = 0; i < data.Length; i++)
                    circles[i] = CreateCircle(data[i]);
            }
            return circles;
        }

        private Circle CreateRandomCircle(CircleColor color) {
            double radius = RandomRange(_settings.MinUnitRadius, _settings.MaxUnitRadius);
            double x = RandomRange(radius, _settings.GameAreaWidth - radius);
            double y = RandomRange(radius, _settings.GameAreaHeight - radius);
            int angle = _rand.Next(360);
            double speed = RandomRange(_settings.MinUnitSpeed, _settings.MaxUnitSpeed);

            return CreateCircle(new CircleData() {
                Position = new Vector2(x, y),
                Velocity = new Vector2(speed * Math.Cos(angle), speed * Math.Sin(angle)),
                Radius = radius,
                Color = color
            });
        }

        private double RandomRange(double min, double max) {
            return _rand.NextDouble() * (max - min) + min;
        }

        private Circle CreateCircle(CircleData data) {
            return new Circle(data);
        }

    }
}
