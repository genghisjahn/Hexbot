using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBot
{
    public class CompletedJourneyEventArgs:EventArgs
    {
        public string FinalMessage
        {
            get
            {
                return string.Format("Done! {0}", DateTime.Now.ToString());
            }
        }
    }
}
