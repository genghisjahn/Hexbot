using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class Robot
    {
        private float toprowY = 0;
        private int serialNumber = 0;

        public Robot() { }
        public Robot(int upjump, int downjump, int vision)
        {
            SetBaseRobot(upjump, downjump, vision);
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

        public Robot(int upjump, int downjump, int vision, float toprowYvalue,Hexagon startingHex)
        {
            SetBaseRobot(upjump, downjump, vision);
            
            
            this.CurrentHex = startingHex;
            toprowY = toprowYvalue;

            //What do we do with toprowY?
            /*
            toprowY = (from h in this.HexWorld
                        select h.NE.Y).Min();
             * */
            BeginJourney();
        }

        public int UpJump { get; private set; }
        public int DownJump { get; private set; }
        public int Vision { get; private set; }
        
        public Hexagon CurrentHex { get; private set; }
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

        public void BeginJourney()
        {
            Move();
        }
        private void Move(){
            if (this.CurrentHex.NE.Y == toprowY)
            {
                string temp = "";
            }
            else
            {
                //Get a list of hexs that we can move to
                //The further north the better, the higher the better
                //Select one at random and move there.
                //Raise An Event;
                //Try again.
            }
        }

       
        private int AdjacentMovesAllowed(Hexagon h)
        {
            int result = 0;
            foreach (HexSide hs in h.HexSides)
            {
                var aHex = from h1 in this.HexWorld
                           where h1.HexSides.Contains(hs)
                           && !h1.Equals(h)
                           select h1;

                if (aHex.Count() == 1)
                {
                    Hexagon testHex = aHex.First();
                    MoveResult mr = (HexUtils.isMoveAllowed(h, testHex, this.UpJump, this.DownJump));
                    if (mr.MoveResultStatus == MoveResult.eMoveResult.Success)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
