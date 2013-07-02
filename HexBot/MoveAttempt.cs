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

        public MoveAttempt(eMoveDirection direction, int currentheight, int targetheight)
        {
            this.Direction = direction;
            this.CurrentHeight = currentheight;
            this.TargetHeight = targetheight;
        }
        
        
        public eMoveDirection Direction{get;private set;}
        public int CurrentHeight{get;private set;}
        public int TargetHeight { get; private set; }

    }
}
