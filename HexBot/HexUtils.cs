using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public static class HexUtils
    {
        public enum eMoveDirection
        {
            DNE=0,
            N = 1,
            NE = 2,
            SE = 3,
            S = 4,
            SW = 5,
            NW = 6,
        }

        public static void ClearAllHighlights(List<Hexagon> hexagons)
        {
            hexagons.ForEach(h => h.Hilighted = false);

        }

        public static MoveResult isMoveAllowed(Hexagon fromhex, Hexagon tohex,int jumpup,int jumpdown)
        {
            MoveResult result = new MoveResult(MoveResult.eMoveResult.DNE, "");
            bool isBorder = false;
            string notallowed = "";
            string isalllowed = "";
            if (fromhex == null)
            {
                result.MoveResultStatus = MoveResult.eMoveResult.Success;
                result.ResultMessage = "First move.";
            }
            else
            {
                notallowed = string.Format("Move from height {0} to {1} is NOT allowed!", fromhex.Height.ToString(), tohex.Height.ToString());
                isalllowed = string.Format("Move from height {0} to {1} is allowed.", fromhex.Height.ToString(), tohex.Height.ToString());
                foreach (var hs in fromhex.HexSides)
                {
                    if (tohex.HexSides.Contains(hs))
                    {
                        isBorder = true;
                        break;
                    }
                }
                if (isBorder)
                {
                    if (fromhex.Height == tohex.Height)
                    {
                        result.MoveResultStatus = MoveResult.eMoveResult.Success;
                        result.ResultMessage = isalllowed;
                    }
                    if (fromhex.Height < tohex.Height)
                    {
                        if (fromhex.Height + jumpup >= tohex.Height)
                        {
                            result.MoveResultStatus = MoveResult.eMoveResult.Success;
                            result.ResultMessage = isalllowed;

                        }
                        else
                        {
                            result.MoveResultStatus = MoveResult.eMoveResult.TooLow;
                            result.ResultMessage = notallowed;
                        }
                    }
                    if (fromhex.Height > tohex.Height)
                    {
                        if (fromhex.Height - tohex.Height <= jumpdown)
                        {
                            result.MoveResultStatus = MoveResult.eMoveResult.Success;
                            result.ResultMessage = isalllowed;
                        }
                        else
                        {
                            result.MoveResultStatus = MoveResult.eMoveResult.TooHigh;
                            result.ResultMessage = notallowed;
                        }
                    }
                }
            }

            return result;
        }
    }
}
