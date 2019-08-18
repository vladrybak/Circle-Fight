using System;

namespace Simulation {
    public class CirclesFactory {

        public Settings Settings { get; private set; }

        private Random _rand;


        public CirclesFactory(Settings settings, int randomSeed) {
            _rand = new Random(randomSeed);
            Settings = settings;
        }

        public Circle[] GenerateCircles() {
            var circles = new Circle[Settings.NumUnitsToSpawn];

            for (int i = 0; i < circles.Length; i++) {
                circles[i] = CreateRandomCircle((i % 2 == 0) ? CircleColor.Red : CircleColor.Blue);
            }

            return circles;
        }

        private Circle CreateRandomCircle(CircleColor color) {
            double radius = _rand.NextDouble() * (Settings.MaxUnitRadius - Settings.MinUnitRadius) + Settings.MinUnitRadius;

            float x = _rand.Next(Settings.GameAreaWidth);
            float y = _rand.Next(Settings.GameAreaHeight);

            int angle = _rand.Next(360);

            double speed = _rand.NextDouble() * (Settings.MaxUnitSpeed - Settings.MinUnitSpeed) + Settings.MinUnitSpeed;

            return new Circle(new CircleData() {
                Position = new Vector2(x, y),
                Velocity = new Vector2(speed * Math.Cos(angle), speed * Math.Sin(angle)),
                Radius = radius,
                Color = color
            });
        }

    }
}
