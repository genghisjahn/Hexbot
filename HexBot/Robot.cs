using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace HexBot
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

    public class Robot:IWorldObject
    {
        

        private float toprowY = 0;
        private int serialNumber = 0;
        private void ReceiveCameraInput(List<Hexagon> hexes)
        {
            var OrderByPreference = (from h in hexes
                                     orderby h.Center.Y ascending, h.Height descending
                                     select h).ToList();
            CycleThroughMoves(OrderByPreference);
            //Attempt to move in order of preference
            //Until a move is successful 
            //or all moves are tried, in which case
            //the bot is stuck.
        }
        private void CycleThroughMoves(List<Hexagon> hexes)
        {
            HexUtils.eMoveDirection direction;
            foreach (var hex in hexes)
            {
                direction = GetDirectionOfAdjancentHex(hex);
                TryMoveEventArgs tmargs = new TryMoveEventArgs();
                tmargs.Direction = direction;
                if (this.TryMove != null)
                {
                    this.TryMove(this, tmargs);
                }    
            }
        }

        private HexUtils.eMoveDirection GetDirectionOfAdjancentHex(Hexagon hex)
        {
            Hexagon chex = this.CurrentHexagon;
            HexUtils.eMoveDirection direction;
            if (hex.SE == chex.NE && hex.SW == chex.NW)
            {
                direction = HexUtils.eMoveDirection.N;
                return direction;
            }
            if (hex.W == chex.NE && hex.SW == chex.E)
            {
                direction = HexUtils.eMoveDirection.NE;
                return direction;
            }
            if (hex.NW == chex.SW && hex.W == chex.E)
            {
                direction = HexUtils.eMoveDirection.SE;
                return direction;

            } 
            if (hex.NW == chex.SW && hex.NE == chex.SE)
            {
                direction = HexUtils.eMoveDirection.S;
                return direction;
            }
            if (hex.NE == chex.W && hex.E == chex.SW)
            {
                direction = HexUtils.eMoveDirection.SW;
                return direction;
            }
            if (hex.SE == chex.W && hex.E == chex.NW)
            {
                direction = HexUtils.eMoveDirection.NW;
                return direction;
            }
            return HexUtils.eMoveDirection.DNE;
            
        }


        public void PowerOn()
        {
            
            LookAroundEventArgs args =new LookAroundEventArgs();
            args.Radius=1;
            if (this.LookAround != null)
            {
                this.LookAround(this, args);
            }
        }

      

        public Robot() { }
        public Robot(int upjump, int downjump, int vision)
        {
            SetBaseRobot(upjump, downjump, vision);
        }

        public event EventHandler LookAround;
        public event EventHandler TryMove;
        public void OnLookAround(List<Hexagon> hexes)
        {
            ReceiveCameraInput(hexes);
        }
        public void OnTryMove(MoveResult mresult, Hexagon newhex)
        {
            var temp = "";
        }
        
        
        public int SerialNumber { get; private set; }
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
