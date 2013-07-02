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

        public Robot(int upjump, int downjump, int vision, List<Hexagon> hexworld)
        {
            SetBaseRobot(upjump, downjump, vision);
            this.HexWorld = hexworld;
            SelectStartHexOnBottomRow();
            toprowY = (from h in this.HexWorld
                        select h.NE.Y).Min();
            BeginJourney();
        }

        public int UpJump { get; private set; }
        public int DownJump { get; private set; }
        public int Vision { get; private set; }
        public List<Hexagon> HexWorld { get; private set; }
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

        private void SelectStartHexOnBottomRow()
        {
            var MaxY = (from h in this.HexWorld
                        select h.SE.Y).Max();
            var bottomRow = (from h in this.HexWorld
                             where h.SE.Y == MaxY
                             select h);

            this.HexWorld.ForEach(hw => hw.Hilighted = false);


            List<Hexagon> BestHexes = new List<Hexagon>();
            int mostmoves = 0;

            foreach (Hexagon h in bottomRow)
            {
                int moves = AdjacentMovesAllowed(h);
                if (moves > 0)
                {
                    bool updateMostMoves = false;
                    if (moves > mostmoves)
                    {
                        BestHexes.Clear();
                        BestHexes.Add(h);
                        updateMostMoves = true;
                        mostmoves = moves;
                    }
                    if (moves == mostmoves)
                    {
                        BestHexes.Add(h);
                    }
                    if (updateMostMoves) { moves = mostmoves; }
                }
                
            }
            if (BestHexes.Count() == 1)
            {
                this.CurrentHex = BestHexes.First();
                this.CurrentHex.Selected = true;
            }
            else if (BestHexes.Count() > 1)
            {
                Random rand = new Random();
                int pickone = rand.Next(1, BestHexes.Count());
                this.CurrentHex = BestHexes[pickone - 1];
                this.CurrentHex.Selected = true;
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
