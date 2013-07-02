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

        public void SetCurrentHexagon(Hexagon currenthex)
        {
            this.CurrentHexagon = currenthex;
        }

        public Robot(int upjump, int downjump, int vision, float toprowYvalue)
        {
            SetBaseRobot(upjump, downjump, vision);
            
            
            //this.CurrentHex = startingHex;
            toprowY = toprowYvalue;

            //What do we do with toprowY?
            /*
            toprowY = (from h in this.HexWorld
                        select h.NE.Y).Min();
             * */

            //Not read to start the journey yet.
            BeginJourney();
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

        
       
    }
}
