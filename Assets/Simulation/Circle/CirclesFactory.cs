using System;

namespace Simulation {
    public class CirclesFactory {

        public Settings Settings { get; private set; }

        private Random _rand;


        public CirclesFactory(Settings settings, int randomSeed) {
            Configure(settings, randomSeed);
        }

        public void Configure(Settings settings, int randomSeed) {
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

        public Circle[] CreateCirclesFromData(CircleData[] data) {
            Circle[] circles = new Circle[data.Length];
            for (int i = 0; i < data.Length; i++) {
                circles[i] = CreateCircle(data[i]);
            }
            return circles;
        }

        private Circle CreateRandomCircle(CircleColor color) {
            double radius = _rand.NextDouble() * (Settings.MaxUnitRadius - Settings.MinUnitRadius) + Settings.MinUnitRadius;

            float x = _rand.Next(Settings.GameAreaWidth);
            float y = _rand.Next(Settings.GameAreaHeight);

            int angle = _rand.Next(360);

            double speed = _rand.NextDouble() * (Settings.MaxUnitSpeed - Settings.MinUnitSpeed) + Settings.MinUnitSpeed;

            return CreateCircle(new CircleData() {
                Position = new Vector2(x, y),
                Velocity = new Vector2(speed * Math.Cos(angle), speed * Math.Sin(angle)),
                Radius = radius,
                Color = color
            });
        }

        private Circle CreateCircle(CircleData data) {
            return new Circle(data);
        }

    }
}
