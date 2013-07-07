using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace HexBot
{
    public class Robot:IWorldObject
    {
        public class LookAroundEventArgs : EventArgs
        {
            public int Radius { get; set; }
            public DateTime EventTime { get; set; }
        }

        public class TryMoveEventArgs : EventArgs
        {
            public HexUtils.eMoveDirection Direction { get; set; }
        }

        private float toprowY = 0;
        private int serialNumber = 0;

        public Robot() { }
        public Robot(int upjump, int downjump, int vision)
        {
            SetBaseRobot(upjump, downjump, vision);
        }
        public event EventHandler LookAround;
        public void OnLookAround()
        {
            EventHandler handler = LookAround;
            LookAroundEventArgs args = new LookAroundEventArgs();
            args.EventTime = DateTime.UtcNow;
            args.Radius = 1;
            
            if (null != handler) handler(this, args);
        }

        public event EventHandler TryMove;
        public void OnTryMove()
        {
            EventHandler handler = TryMove;
            TryMoveEventArgs args = new TryMoveEventArgs();
            args.Direction = HexUtils.eMoveDirection.N;
            if (null != handler) handler(this, EventArgs.Empty);
        }

        public void SetSerialNumber(int num)
        {
            if (this.serialNumber == 0)
            {
                if (num >0 )
                {
                    this.serialNumber = num;
                }
                else
                {
                    throw new Exception("Serial number must be greater than zero.");
                }
            }
            else
            {
                throw new Exception("Serial number can only be set one.");
            }
        }

        public Hexagon CurrentHexagon { get; private set; }

        public void SetCurrentHexagon(Hexagon currenthex)
        {
            this.CurrentHexagon = currenthex;
        }

        public Robot(int upjump, int downjump, int vision, float toprowYvalue)
        {
            SetBaseRobot(upjump, downjump, vision);
            toprowY = toprowYvalue;
        }

        public int UpJump { get; private set; }
        public int DownJump { get; private set; }
        public int Vision { get; private set; }
        
        public delegate void CompletedJourneyHandler(object sender, CompletedJourneyEventArgs e);
        public event CompletedJourneyHandler CompletedJourney;
        protected virtual void OnCompletedJourney(CompletedJourneyEventArgs e)
        {
            if (CompletedJourney != null)
            {
                CompletedJourney(this, e);
            }
        }
              
        private void SetBaseRobot(int upjump, int downjump, int vision)
        {
            this.UpJump = upjump;
            this.DownJump = downjump;
            this.Vision = vision;
        }

        public System.Drawing.Color GetColor()
        {
            return Color.Black;
        }

        public System.Drawing.SolidBrush GetShape()
        {
            int radius = 5;
            float x = this.CurrentHexagon.Center.X - radius;
            float y = this.CurrentHexagon.Center.Y - radius;
            float width = 2 * radius;
            float height = 2 * radius;
            SolidBrush selBrush = new SolidBrush(Color.Black);
            return selBrush;
        }
    }
}
