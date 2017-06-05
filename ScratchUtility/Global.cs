using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScratchUtility
{
    public delegate void DoDrawChangedHandler();

    public static class Global
    {
        private static double intersectionTolerance = 0;//0.0000001;
        public static bool DebugMode { get; set; }
        public static bool DesignMode { get; set; }

        static Global()
        {
            DebugMode = false;
            DesignMode = true;
        }

        public static void Print(string s)
        {
            //if(DebugMode)
            //    Console.WriteLine(s);
        }

        /// <summary>Returns true if lines with the provided endpoints intersect (Z values are ignored).</summary>
        public static bool LinesIntersect(Coord startCoord1, Coord endCoord1, Coord startCoord2, Coord endCoord2)
        {
            double a = startCoord1.X;
            double f = startCoord1.Y;
            double b = endCoord1.X;
            double g = endCoord1.Y;

            double c = startCoord2.X;
            double k = startCoord2.Y;
            double d = endCoord2.X;
            double l = endCoord2.Y;

            //Normalization technique will be used to determine if the lines intersect.
            //The following equations will be solved for t and u. If t and u are both between 0 and 1, the lines intersect
            // x1+(x2-x1)t = x3+(x4-x3)u
            // y1+(y2-y1)t = y3+(y4-y3)u
            double den = (a * (k - l) - b * (k - l) - (c - d) * (f - g));
            double t = (a * (k - l) - c * (f - l) + d * (f - k)) / den;
            double u = -(a * (g - k) - b * (f - k) + c * (f - g)) / den;

            return !(IsNotWithinTolerance(t) || IsNotWithinTolerance(u));
        }

        /// <summary>Returns true if the specified double is not between 0 and 1, with a specific tolerance.</summary>
        public static bool IsNotWithinTolerance(double normalizedValue)
        {
            return (normalizedValue <= intersectionTolerance || normalizedValue >= 1 - intersectionTolerance);
        }

        public static System.Drawing.Rectangle GetRectangleWithGivenCorners(PointD p1, PointD p2)
        {
            int left = (int)Math.Min(p1.X, p2.X);
            int right = (int)Math.Max(p1.X, p2.X);
            int top = (int)Math.Min(p1.Y, p2.Y);
            int bottom = (int)Math.Max(p1.Y, p2.Y);
            return System.Drawing.Rectangle.FromLTRB(left, top, right, bottom);
        }
        public static System.Drawing.Rectangle GetRectangleWithGivenCorners(System.Drawing.Point p1, System.Drawing.Point p2)
        {
            int left = Math.Min(p1.X, p2.X);
            int right = Math.Max(p1.X, p2.X);
            int top = Math.Min(p1.Y, p2.Y);
            int bottom = Math.Max(p1.Y, p2.Y);
            return System.Drawing.Rectangle.FromLTRB(left, top, right, bottom);
        }

        public static bool IsBetween(PointD testPoint, PointD startPoint, PointD endPoint)
        {
            return (testPoint.X >= startPoint.X && testPoint.X <= endPoint.X ||
                testPoint.X <= startPoint.X && testPoint.X >= endPoint.X) &&
                (testPoint.Y >= startPoint.Y && testPoint.Y <= endPoint.Y ||
                testPoint.Y <= startPoint.Y && testPoint.Y >= endPoint.Y);
        }

        public static List<double> GetReferenceAngles(double Increment, double ArcSweepAngle)
        {
            List<double> angles = new List<double>();
            double startAngle = -ArcSweepAngle / 2;
            double endAngle = ArcSweepAngle / 2;

            angles.Add(startAngle);

            for (double angle = startAngle + Increment; angle < endAngle; angle += Increment)
            {
                angles.Add(angle);
            }
            angles.Add(endAngle);
            return angles;
        }

    }
}
