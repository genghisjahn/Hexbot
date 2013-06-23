using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class HexPoint
    {
        public HexPoint() { }
        public HexPoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.X.ToString(), this.Y.ToString());
        }
        public static bool operator ==(HexPoint a, HexPoint b)
        {
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
            return Math.Round(a.X, 2) == Math.Round(b.X, 2) && Math.Round(a.Y, 2) == Math.Round(b.Y, 2);
        }
        public static bool operator !=(HexPoint a, HexPoint b)
        {
            return !(a == b);
        }
        public override int GetHashCode()
        {
            int result;

            result = (int)this.X * 1000 + (int)this.Y;
            return result;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            HexPoint hp = obj as HexPoint;
            if ((System.Object)hp == null)
            {
                return false;
            }
            if (this.X == hp.X && this.Y == hp.Y)
            {
                return true;
            }
            return false;
        }
    }

}
