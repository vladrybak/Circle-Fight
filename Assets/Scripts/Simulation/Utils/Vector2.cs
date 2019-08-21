using System;
namespace Simulation {
    [Serializable]
    public struct Vector2 {

        public double X;
        public double Y;

        public static Vector2 Zero = new Vector2(0, 0);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Up = new Vector2(0, 1);
        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 Down = new Vector2(0, -1);


        public Vector2(double x, double y) {
            X = x;
            Y = y;
        }

        public static double Dot(Vector2 v1, Vector2 v2) {
            return (v1.X * v2.X) + (v1.Y * v2.Y);
        }

        public static Vector2 Multiply(Vector2 v1, Vector2 v2) {
            v1.X *= v2.X;
            v1.Y *= v2.Y;
            return v1;
        }

        public static Vector2 Divide(Vector2 v1, Vector2 v2) {
            v1.X /= v2.X;
            v1.Y /= v2.Y;
            return v1;
        }

        public Vector2 Reflect(Vector2 normal) {
            Vector2 result;
            double val = 2.0f * Dot(this, normal);
            result.X = X - (normal.X * val);
            result.Y = Y - (normal.Y * val);
            return result;
        }

        public Vector2 Normalized() {
            double val = 1.0f / Math.Sqrt((X * X) + (Y * Y));
            X *= val;
            Y *= val;
            return this;
        }

        public static Vector2 Tangent(Vector2 normal) {
            return new Vector2(-normal.Y, normal.X);
        }

        public static double SquaredDistance(Vector2 v1, Vector2 v2) {
            double dX = v2.X - v1.X, dY = v2.Y - v1.Y;
            return (dX * dX) + (dY * dY);
        }

        public double Magnitude() {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2) {
            v1.X += v2.X;
            v1.Y += v2.Y;
            return v1;
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2) {
            return -v2 + v1;
        }

        public static Vector2 operator *(Vector2 v, double scaleFactor) {
            v.X *= scaleFactor;
            v.Y *= scaleFactor;
            return v;
        }

        public static Vector2 operator /(Vector2 v1, double divider) {
            double factor = 1 / divider;
            return v1 * factor;
        }

        public static Vector2 operator -(Vector2 v) {
            v.X = -v.X;
            v.Y = -v.Y;
            return v;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2) {
            return v1.X != v2.X || v1.Y != v2.Y;
        }

        public static bool operator ==(Vector2 v1, Vector2 v2) {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public bool Equals(Vector2 other) {
            return (X == other.X) && (Y == other.Y);
        }

        public override bool Equals(object obj) {
            if (obj is Vector2) {
                return Equals((Vector2)this);
            }
            return false;
        }

        public override int GetHashCode() {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public override string ToString() {
            return string.Format("({0}, {1}", X, Y);
        }

    }
}
