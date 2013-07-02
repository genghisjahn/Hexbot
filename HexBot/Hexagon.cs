using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace HexBot
{
    public class Hexagon
    {
        private float longside;
        private float shortside;
        private float hexside_F;
        private double hexside_D;
        private HexPoint centerpoint;
        private List<HexSide> hexsides = new List<HexSide>();
        public Hexagon() { }

        public enum eFirstPoint
        {
            SW = 1,
            SE = 2,
            E = 3,
            NE = 4,
            NW = 5,
            W = 6
        }
        private void HexInit(float x, float y, float side, eFirstPoint efirstpoint)
        {
            this.HexSideLength = side;
            hexside_F = this.HexSideLength;
            hexside_D = (double)hexside_F;
            longside = (float)(hexside_D / 2D * Math.Sqrt(3D));
            shortside = hexside_F / 2;
            this.SW = new HexPoint();
            switch (efirstpoint)
            {
                case eFirstPoint.SW:
                    this.SW.X = x;
                    this.SW.Y = y;
                    break;
                case eFirstPoint.SE:
                    this.SW.X = x - side;
                    this.SW.Y = y;
                    break;
                case eFirstPoint.E:
                    this.SW.X = x - side - shortside;
                    this.SW.Y = y + longside;
                    break;
                case eFirstPoint.NE:
                    this.SW.X = x - side;
                    this.SW.Y = y + (2 * longside);
                    break;
                case eFirstPoint.NW:
                    this.SW.X = x;
                    this.SW.Y = y + (2 * longside);
                    break;
                case eFirstPoint.W:
                    this.SW.X = x + shortside;
                    this.SW.Y = y + longside;
                    break;
            }

            this.HexSideLength = side;
            SetPoints();
            SetSides();
        }

        public Hexagon(float x, float y, float side, eFirstPoint efirstpoint)
        {
            HexInit(x, y, side, efirstpoint);
        }
        public HexPoint NW { get; private set; }
        public HexPoint NE { get; private set; }
        public HexPoint E { get; private set; }
        public HexPoint SE { get; private set; }
        public HexPoint SW { get; private set; }
        public HexPoint W { get; private set; }

        public HexSide NSide { get; private set; }
        public HexSide NWSide { get; private set; }
        public HexSide SWSide { get; private set; }
        public HexSide SSide { get; private set; }
        public HexSide SESide { get; private set; }
        public HexSide NESide { get; private set; }

        public int Height { get; private set; }

        public bool Selected { get; set; }
        public bool Hilighted { get; set; }

        public Color Color {
            get
            {
                        
            switch (this.Height)
            {
                case 1:
                    return Color.White;
                case 2:
                    return Color.NavajoWhite;
                case 3:
                    return Color.Moccasin;
                case 4:
                    return Color.Tan;
                case 5:
                    return Color.BurlyWood;
                case 6:
                    return Color.SandyBrown;
                case 7:
                    return Color.Peru;
                case 8:
                    return Color.Chocolate;
                case 9:
                    return Color.Sienna;
                case 10:
                    return Color.SaddleBrown;
            }
            return Color.Red;
        }
            private set
            {
            }

}
        public List<HexSide> HexSides
        {
            get
            {
                if (this.hexsides.Count != 6)
                {
                    this.hexsides.Clear();
                    this.hexsides.Add(this.SSide);
                    this.hexsides.Add(this.SWSide);
                    this.hexsides.Add(this.SESide);
                    this.hexsides.Add(this.NSide);
                    this.hexsides.Add(this.NESide);
                    this.hexsides.Add(this.NWSide);
                }
                return this.hexsides;
            }
            private set { }
        }


        public float HexSideLength { get; private set; }

        private void SetSides()
        {
            HexSide hs;

            hs = new HexSide(this.NW, this.NE);
            this.NSide = hs;

            hs = new HexSide(this.NE, this.E);
            this.NESide = hs;

            hs = new HexSide(this.E, this.SE);
            this.SESide = hs;

            hs = new HexSide(this.SW, this.SE);
            this.SSide = hs;

            hs = new HexSide(this.SW, this.W);
            this.SWSide = hs;

            hs = new HexSide(this.W, this.NW);
            this.NWSide = hs;



        }

        private void SetPoints()
        {



            this.SE = new HexPoint(this.SW.X + this.HexSideLength, this.SW.Y);
            this.W = new HexPoint(this.SW.X - shortside, this.SW.Y - longside);
            this.E = new HexPoint(this.SE.X + shortside, this.SE.Y - longside);
            this.NW = new HexPoint(this.SW.X, this.SW.Y - (longside * 2));
            this.NE = new HexPoint(this.SE.X, this.SE.Y - (longside * 2));

            this.DrawHexPoints = new List<HexPoint>();
            this.DrawPointFs = new List<PointF>();


            this.DrawHexPoints.Add(this.SW);
            this.DrawHexPoints.Add(this.SE);
            this.DrawHexPoints.Add(this.E);
            this.DrawHexPoints.Add(this.NE);
            this.DrawHexPoints.Add(this.NW);
            this.DrawHexPoints.Add(this.W);

            foreach (HexPoint hp in this.DrawHexPoints)
            {
                this.DrawPointFs.Add(ConvertHexPointToPointF(hp));
            }
        }

        private PointF ConvertHexPointToPointF(HexPoint hp)
        {
            PointF result = new PointF(hp.X, hp.Y);
            return result;
        }

        public List<HexPoint> DrawHexPoints { get; private set; }
        public List<PointF> DrawPointFs { get; private set; }

        public void SetHeight(int h)
        {
            this.Height = h;
        }
        public HexPoint Center
        {

            get
            {
                HexPoint result = new HexPoint(0, 0);
                if (centerpoint == null)
                {

                    float centerX = Math.Abs(this.NE.X + this.SW.X) / 2F;
                    float centerY = Math.Abs(this.NE.Y + this.SW.Y) / 2F;
                    result = new HexPoint(centerX, centerY);
                }
                else
                {
                    result = centerpoint;
                }

                return result;
            }
            private set { }


        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Hexagon hex = obj as Hexagon;
            if ((System.Object)hex == null)
            {
                return false;
            }
            if (this.Center == hex.Center && this.HexSideLength == hex.HexSideLength)
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int result;
            int x = (int)(this.Center.X);
            int y = (int)(this.Center.Y);
            int s = (int)this.HexSideLength;
            result = x * 1000 + y * 100+s;
            return result;
        }
    }
}
