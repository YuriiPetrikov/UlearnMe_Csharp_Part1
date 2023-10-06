// Вставьте сюда финальное содержимое файла VectorTask.cs
using System;

namespace GeometryTasks
{
    public class Segment
    {
        public Vector Begin;
        public Vector End;
    }

    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Geometry
    {
        public static bool IsVectorInSegment(Vector a, Segment b)
        {
            double length1 = Math.Sqrt((-a.X + b.Begin.X) * (-a.X + b.Begin.X) 
                + (-a.Y + b.Begin.Y) * (-a.Y + b.Begin.Y));
            double length2 = Math.Sqrt((a.X - b.End.X) * (a.X - b.End.X) 
                + (a.Y - b.End.Y) * (a.Y - b.End.Y));
            return GetLength(b) == length1 + length2;
        }
                   
        public static double GetLength(Segment a)
        {
            return Math.Sqrt((a.Begin.X - a.End.X) * (a.Begin.X - a.End.X) 
                + (a.Begin.Y - a.End.Y) * (a.Begin.Y - a.End.Y));
        }
                
        public static double GetLength(Vector a)
        {
            return Math.Abs(Math.Sqrt(a.X * a.X + a.Y * a.Y));
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector() { X = a.X + b.X, Y = a.Y + b.Y };
        }
    }
}
