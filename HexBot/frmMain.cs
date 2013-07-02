using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexBot
{
    public partial class frmMain : Form
    {
        List<Hexagon> hexagons = new List<Hexagon>();

        HexWorld hexworld = new HexWorld("Hex world.");
        
        int jumpup = 2;
        int jumpdown = 3;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            float toprowy = (from h in hexworld.Tiles
                             select h.NE.Y).Min();
            Robot robot = new Robot(2, 3, 2, toprowy);
            hexworld.AddRobot(robot);
            
            this.Refresh();
        }

        

        private void LogHexagons()
        {
            foreach (var h in hexagons)
            {
                this.txtLog.AppendText(h.Height.ToString());
                this.txtLog.AppendText(Environment.NewLine);
            }

        }
        private void DrawHex(PaintEventArgs e)
        {
            try
            {
                Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
                SolidBrush selBrush = new SolidBrush(Color.Black);
                SolidBrush hilightBrush = new SolidBrush(Color.Blue);
                Pen penH = new Pen(Color.Blue);
                foreach (var h in hexagons)
                {
                    SolidBrush brush = new SolidBrush(h.Color);
                    e.Graphics.FillPolygon(brush, h.DrawPointFs.ToArray());
                    e.Graphics.DrawPolygon(pen, h.DrawPointFs.ToArray());
                    
                    if (h.Hilighted)
                    {
                        DrawSelectCircle(h, e, hilightBrush);
                    }
                    if (h.Selected)
                    {
                        DrawSelectCircle(h, e, selBrush);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void DrawSelectCircle(Hexagon h, PaintEventArgs e,Brush brush)
        {
            //Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            int radius = 5;
            float x = h.Center.X - radius;
            float y = h.Center.Y - radius;
            float width = 2 * radius;
            float height = 2 * radius;
            //e.Graphics.DrawEllipse(pen, x, y, width, height);
            e.Graphics.FillEllipse(brush, x, y, width, height);
        }

        


        private void btnPaint_Click(object sender, EventArgs e)
        {
            LogHexagons();
        }



        private void pboxMain_Paint(object sender, PaintEventArgs e)
        {
            DrawHex(e);

        }

        private void pboxMain_MouseDown(object sender, MouseEventArgs e)
        {
            var h1 = (from hx in hexagons
                      where hx.Selected
                      select hx).FirstOrDefault();
            HexPoint hp = new HexPoint(e.X, e.Y);
            hexagons.ForEach(h => h.Selected = false);
            var hex = (from h in hexagons
                       where h.NW.X <= e.X && h.NE.X >= e.X
                       && h.NW.Y <= e.Y && h.SW.Y >= e.Y
                       select h).FirstOrDefault();
            MoveResult moveresult = new MoveResult(MoveResult.eMoveResult.DNE, "");

            if (hex != null)
            {
                moveresult = HexUtils.isMoveAllowed(h1, hex,jumpup,jumpdown);
                if (moveresult.MoveResultStatus== MoveResult.eMoveResult.Success)
                {
                    if (h1 != null)
                    {
                        h1.Selected = false;
                    }
                    hex.Selected = true;
                }
                else
                {
                    if (h1 != null)
                    {
                        h1.Selected = true;
                        hex.Selected = false;
                    }
                }
               
            }

            WriteLog(moveresult);

            this.Refresh();


        }

        private void WriteLog(MoveResult moveresult)
        {
            this.txtLog.AppendText(string.Format("{0} {1}",moveresult.MoveResultStatus.ToString(),moveresult.ResultMessage));
            this.txtLog.AppendText(System.Environment.NewLine);
        }

       

        
    }
}
