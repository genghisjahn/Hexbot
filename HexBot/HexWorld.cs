using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class HexWorld
    {
        public string WorldName { get; private set; }
        public List<Hexagon> Tiles { get; private set; }
        public List<Robot> Robots { get; private set; }
        public HexWorld(string worldname,List<Hexagon> tiles)
        {
            this.WorldName = worldname;
            this.Tiles = tiles;
        }
        
        public void AddRobot(Robot robot)
        {
            if (this.Robots == null)
            {
                this.Robots = new List<Robot>();
            }
            this.Robots.Add(robot);
        }

    }
}
