using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexBot;
namespace HexBot
{
    public class HexWorld
    {
        #region "Private variables"
        private Random rand = new Random((int)DateTime.Now.Ticks);
        #endregion

        #region "Public Properties"
        public string WorldName { get; private set; }
        public List<Hexagon> Tiles { get; private set; }
        public List<Robot> Robots { get; private set; }
        
        #endregion

        #region "constructors"
        public HexWorld(string worldname, List<Hexagon> tiles)
        {
            this.WorldName = worldname;
            this.Tiles = tiles;

        }
        public HexWorld(string worldname)
        {
            this.WorldName = worldname;
            BuildWorld();
        }
        #endregion

        #region "Public Methods"
        public void AddRobot(Robot robot)
        {
            if (this.Robots == null)
            {
                this.Robots = new List<Robot>();
            }
            robot.SetCurrentHexagon(GetStartHexOnBottomRow(robot));
            this.Robots.Add(robot);

        }

        public MoveResult TryMove(HexUtils.eMoveDirection direction, int index)
        {
            MoveResult result = new MoveResult(MoveResult.eMoveResult.DNE, ""); TryMove(direction, this.Robots[index]);
            return result;
        }

        public MoveResult TryMove(HexUtils.eMoveDirection direction, Robot robot)
        {
            MoveResult result = new MoveResult(MoveResult.eMoveResult.DNE,"");

            Hexagon targethex = GetAdjacentHexagaon(robot.CurrentHexagon, direction);

            if (targethex != null)
            {
                result = HexUtils.isMoveAllowed(robot.CurrentHexagon, targethex, robot.UpJump, robot.DownJump);
            }
            return result;
        }
        #endregion

        #region "Private Methods"
        private Hexagon GetAdjacentHexagaon(Hexagon hex, HexUtils.eMoveDirection direction)
        {
            Hexagon result;
            switch (direction)
            {
                case HexUtils.eMoveDirection.N:
                    result = (from h in this.Tiles
                              where hex.NSide == h.SSide
                              select h).FirstOrDefault();
                    break;
                case HexUtils.eMoveDirection.NE:
                    result = (from h in this.Tiles
                              where hex.NESide == h.SWSide
                              select h).FirstOrDefault();
                    break;
                case HexUtils.eMoveDirection.SE:
                    result = (from h in this.Tiles
                              where hex.SESide == h.NESide
                              select h).FirstOrDefault();
                    break;
                case HexUtils.eMoveDirection.S:
                    result = (from h in this.Tiles
                              where hex.SSide == h.NSide
                              select h).FirstOrDefault();
                    break;
                case HexUtils.eMoveDirection.SW:
                    result = (from h in this.Tiles
                              where hex.SWSide == h.NESide
                              select h).FirstOrDefault();
                    break;

                case HexUtils.eMoveDirection.NW:
                    result = (from h in this.Tiles
                              where hex.NWSide == h.SESide
                              select h).FirstOrDefault();
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }
        private Hexagon GetStartHexOnBottomRow(Robot robot)
        {
            Hexagon result = new Hexagon();

            var MaxY = (from h in this.Tiles
                        select h.SE.Y).Max();
            var bottomRow = (from h in this.Tiles
                             where h.SE.Y == MaxY
                             select h);

            this.Tiles.ForEach(hw => hw.Hilighted = false);


            List<Hexagon> BestHexes = new List<Hexagon>();
            int mostmoves = 0;

            foreach (Hexagon h in bottomRow)
            {
                int moves = AdjacentMovesAllowed(h, robot);
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
                result = BestHexes.First();

                //this.CurrentHex.Selected = true;
            }
            else if (BestHexes.Count() > 1)
            {
                Random rand = new Random();
                int pickone = rand.Next(1, BestHexes.Count());
                result = BestHexes[pickone - 1];
                //this.CurrentHex.Selected = true;
            }
            return result;
        }
        private int AdjacentMovesAllowed(Hexagon h, Robot robot)
        {
            int result = 0;
            foreach (HexSide hs in h.HexSides)
            {
                var aHex = from h1 in this.Tiles
                           where h1.HexSides.Contains(hs)
                           && !h1.Equals(h)
                           select h1;

                if (aHex.Count() == 1)
                {
                    Hexagon testHex = aHex.First();
                    MoveResult mr = (HexUtils.isMoveAllowed(h, testHex, robot.UpJump, robot.DownJump));
                    if (mr.MoveResultStatus == MoveResult.eMoveResult.Success)
                    {
                        result++;
                    }
                }
            }
            return result;
        }
        private void BuildWorld()
        {
            int side = 20;
            Hexagon rhex = new Hexagon(20, 50, side, Hexagon.eFirstPoint.SW);
            this.Tiles = new List<Hexagon>();
            Tiles.Add(rhex);
            int rows = 20;
            for (int r = 0; r < rows; r++)
            {
                for (int i = 0; i < 19; i++)
                {
                    Hexagon oHex = Tiles.Last();
                    if (i % 2 == 0)
                    {
                        Tiles.Add(new Hexagon(oHex.E.X, oHex.E.Y, side, Hexagon.eFirstPoint.NW));
                    }
                    else
                    {
                        Tiles.Add(new Hexagon(oHex.E.X, oHex.E.Y, side, Hexagon.eFirstPoint.SW));
                    }
                }
                if (r < rows - 1)
                {
                    Tiles.Add(new Hexagon(rhex.SW.X, rhex.SW.Y, side, Hexagon.eFirstPoint.NW));
                    rhex = Tiles.Last();
                }

            }
            var hseed = 5;
            foreach (var h in Tiles)
            {
                h.SetHeight(GetHeight(hseed));
                hseed = h.Height;
            }
        }
        private int GetHeight(int seed)
        {

            int hMod;
            int low = 1;
            int high = 10;
            if (seed > 3)
            {
                low = seed - 3;
            }
            if (seed < 8)
            {
                high = seed + 3;
            }
            hMod = rand.Next(low, high + 1);
            return hMod;
        }
        #endregion
    }
}
