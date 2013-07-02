﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class HexWorld
    {
        private Random rand = new Random((int)DateTime.Now.Ticks);
        
        public string WorldName { get; private set; }
        public List<Hexagon> Tiles { get; private set; }
        public List<Robot> Robots { get; private set; }
        public HexWorld(string worldname,List<Hexagon> tiles)
        {
            this.WorldName = worldname;
            this.Tiles = tiles;
            
        }
        public HexWorld(string worldname)
        {
            this.WorldName = worldname;
            BuildWorld();
        }
        public void AddRobot(Robot robot)
        {
            if (this.Robots == null)
            {
                this.Robots = new List<Robot>();
            }
            this.Robots.Add(robot);
            
        }
        private Hexagon GetStartHexOnBottomRow()
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
    }
}
