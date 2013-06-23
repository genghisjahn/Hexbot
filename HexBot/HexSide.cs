using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class HexSide
    {

        List<HexPoint> hexpoints = new List<HexPoint>();

        public HexSide() { }
        public HexSide(float x1, float y1, float x2, float y2)
        {
            this.StartPoint = new HexPoint(x1, y1);
            this.EndPoint = new HexPoint(x2, y2);
            SetPointList();
        }
        public HexSide(HexPoint hp1, HexPoint hp2)
        {
            this.StartPoint = hp1;
            this.EndPoint = hp2;
            SetPointList();
        }
        private void SetPointList()
        {
            hexpoints.Clear();
            hexpoints.Add(this.StartPoint);
            hexpoints.Add(this.EndPoint);
            this.HexPoints = hexpoints;
        }
        public HexPoint StartPoint { get; private set; }
        public HexPoint EndPoint { get; private set; }
        public List<HexPoint> HexPoints { get; private set; }
        public override string ToString()
        {
            return string.Format("{0},{1} to {2},{3}", this.StartPoint.X.ToString(), this.StartPoint.Y.ToString(), this.EndPoint.X.ToString(), this.EndPoint.Y.ToString());
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            HexSide hs = obj as HexSide;
            if ((System.Object)hs == null)
            {
                return false;
            }
            if (
                (this.StartPoint == hs.StartPoint && this.EndPoint == hs.EndPoint)
                ||
                (this.StartPoint == hs.EndPoint && this.EndPoint == hs.StartPoint)
                )
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int result;
            int xmid = (int)(this.StartPoint.X + this.EndPoint.X) / 2;
            int ymid = (int)(this.StartPoint.Y + this.EndPoint.Y) / 2;
            result = xmid * 1000 + ymid;
            return result;
        }
    }
}
