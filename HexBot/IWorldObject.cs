using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public interface IWorldObject
    {
        System.Drawing.Color GetColor();
        System.Drawing.SolidBrush GetShape();
        
    }
}
