﻿using System;
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
        //List<Hexagon> hexagons = new List<Hexagon>();

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
            foreach (var h in hexworld.Tiles)
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
                foreach (var h in hexworld.Tiles)
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
            var hexselected = (from hx in hexworld.Tiles
                      where hx.Selected
                      select hx).FirstOrDefault();
           
            hexworld.Tiles.ForEach(h => h.Selected = false);

            /*
             * The hextarget linq code is to find out which
             * hexagon the user clicked on.  It's possible to click
             * between hexagons, which results in a DNE even though
             * the user will think it should work or give something
             * more description.
             * 
             * No point in having this once the robot takes over movements.
             * */
            var hextarget = (from h in hexworld.Tiles
                       where h.NW.X <= e.X && h.NE.X >= e.X
                       && h.NW.Y <= e.Y && h.SW.Y >= e.Y
                       select h).FirstOrDefault();
            
            MoveResult moveresult = new MoveResult(MoveResult.eMoveResult.DNE, "");

            if (hextarget != null)
            {
                moveresult = HexUtils.isMoveAllowed(hexselected, hextarget,jumpup,jumpdown);
                if (moveresult.MoveResultStatus== MoveResult.eMoveResult.Success)
                {
                    if (hexselected != null)
                    {
                        hexselected.Selected = false;
                    }
                    hextarget.Selected = true;
                }
                else
                {
                    if (hexselected != null)
                    {
                        hexselected.Selected = true;
                        hextarget.Selected = false;
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

        private void btnGoBot_Click(object sender, EventArgs e)
        {
            Robot robot = hexworld.Robots[0];
        }


       

        
    }
}
