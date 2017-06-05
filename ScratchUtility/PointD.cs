using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScratchUtility
{
    public struct PointD
    {
        public double X { get; set; }
        public double Y { get; set; }
        private bool mIsNull;

        public PointD(double x, double y)
            :this()
        {
            X = x;
            Y = y;
            mIsNull = false;
        }
        private PointD(bool isNull)
            : this()
        {
            mIsNull = isNull;
        }
        public static PointD NullPointD
        {
            get { return new PointD(true); }
        }
        public static bool IsNull(PointD point)
        {
            return point.mIsNull;
        }

        public static PointD operator *(PointD c, double rhs)
        {
            return new PointD(c.X * rhs, c.Y * rhs);
        }
        public static PointD operator /(PointD c, double rhs)
        {
            return new PointD(c.X / rhs, c.Y / rhs);
        }
        public static PointD operator -(PointD c, PointD rhs)
        {
            return new PointD(c.X - rhs.X, c.Y - rhs.Y);
        }
        public static PointD operator +(PointD c, PointD rhs)
        {
            return new PointD(c.X + rhs.X, c.Y + rhs.Y);
        }
        public static bool operator ==(PointD a, PointD b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(PointD a, PointD b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(PointD))
            {
                return this == (PointD)obj;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
        public string ToString(int decimalPlaces)
        {
            return "(" + X.ToString("N" + decimalPlaces) + ", " + Y.ToString("N" + decimalPlaces) + ")";
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
        public PointF ToPointF()
        {
            return new PointF((float)X, (float)Y);
        }

        public double Length
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }
        /// <summary>Gets a unit vector in the direction of this PointD.</summary>
        public PointD UnitVector
        {
            get { return this / Length; }
        }
    }
}
