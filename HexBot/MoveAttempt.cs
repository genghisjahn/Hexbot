using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class MoveAttempt
    {
        public enum eMoveDirection
        {
            N=1,
            NE=2,
            SE=3,
            S=4,
            SW=5,
            NW=6
        }

        public MoveAttempt( eMoveDirection direction,  Hexagon targethex)
        {
            this.Direction = direction;
           
            this.TargetHex = targethex;
        }
        
        
        public eMoveDirection Direction{get;private set;}
      
        public Hexagon TargetHex { get; private set; }

    }
}
