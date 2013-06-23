using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class MoveResult
    {
        public enum eMoveResult
        {
            Success = 1,
            TooHigh = 2,
            TooLow = 3,
            NoBorder = 4,
            DNE = 5
        }
        public eMoveResult MoveResultStatus { get;  set; }
        public string ResultMessage { get;  set; }
        public MoveResult(eMoveResult emoveresult, string resultmessage)
        {
            this.MoveResultStatus = emoveresult;
            this.ResultMessage = resultmessage;
        }
    }
}
