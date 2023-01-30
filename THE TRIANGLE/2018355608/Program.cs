/*

 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;                    //System.Drawing and System.Windows.Forms Using Directives are added in order to allow us to create a windows forms application
using System.Windows.Forms;

namespace _2018355608
{
    class Program
    {   //Declaration of these "Variables" on class level allows them to be accesible to all methods methods written on class level.
        static Button btnDraw = new Button();
        static NumericUpDown nudIterations = new NumericUpDown();
        static Panel pnlDraw = new Panel();
        static Point[] Triangle = new Point[3];      //Array of type point to hold the coordinates of the triangle on the panel
        static Point RandomPoint = new Point();
        static Random random = new Random();
        static void Main(string[] args)
        {
            Form frmChaos = new Form();

            //Form properties
            frmChaos.StartPosition = FormStartPosition.CenterScreen;
            frmChaos.MaximizeBox = frmChaos.MinimizeBox = false;
            frmChaos.Text = "Chaos Game";
            frmChaos.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmChaos.Width = 500;
            frmChaos.Height = 500;
            //End of form properties

            //Button properties
            btnDraw.Left = 8;
            btnDraw.Top = 425;
            btnDraw.Text = "&Start";
            btnDraw.Click += btnDraw_Click;
            frmChaos.Controls.Add(btnDraw);
            //End of button properties

            //Numeric Up-Down Properties
            nudIterations.Top = 425;
            nudIterations.Left = 350;
            nudIterations.Maximum = 100000;
            nudIterations.Minimum = 1000;
            nudIterations.Increment = 1000;
            frmChaos.Controls.Add(nudIterations);
            //End of numeric up-down properties

            //label properties
            Label lblIterations = new Label();
            lblIterations.Text = "Iterations";
            lblIterations.Top = 425;
            lblIterations.Left = 300;
            frmChaos.Controls.Add(lblIterations);
            //end of label properties

            //Panel Properties
            pnlDraw.Height = 400;
            pnlDraw.Width = 450;
            pnlDraw.Left = 17;
            pnlDraw.Top = 10;
            pnlDraw.BackColor = Color.White;
            frmChaos.Controls.Add(pnlDraw);
            //End of panel properties

            Application.Run(frmChaos);
        }
        static void SetPointLocations(int _pnlHeight)   //This method is used to set points to the array of points for the triangle.
        {
            Point pntMid = new Point(pnlDraw.Width / 2, _pnlHeight / 2); //This is used as a reference point in order for other points to be calculated.
            Triangle[0] = new Point(pntMid.X, pntMid.Y - (int)(_pnlHeight * (Math.Sqrt(3) / 2) / 2));  //The (Math.Sqrt(3) / 2) is the ratio used to calculate the height in relation to the width of the height in order to provide accurate scaling for the triangle
            Triangle[1] = new Point(pntMid.X - _pnlHeight / 2, pntMid.Y + (int)(_pnlHeight * (Math.Sqrt(3) / 2) / 2));
            Triangle[2] = new Point(pntMid.X + _pnlHeight / 2, pntMid.Y + (int)(_pnlHeight * (Math.Sqrt(3) / 2) / 2 ));
        }
        static void MoveToNextPoint()         //This method sets the rule for how the point should move itself.
        {
            int iNext = random.Next(0,3);
            RandomPoint.X = (RandomPoint.X + Triangle[iNext].X) / 2;        
            RandomPoint.Y = (RandomPoint.Y + Triangle[iNext].Y) / 2;
        }
        static void CreateSierpinski(Graphics _g)   // this method moves the point about in order to draw sierpinskis triangle
        {
            random = new Random();
            RandomPoint = new Point(Triangle[0].X, Triangle[0].Y);    //Acts as a starting point.
            for (int i = 0; i < nudIterations.Value; i++)
            {       
                MoveToNextPoint();
                _g.FillRectangle(Brushes.Red, RandomPoint.X, RandomPoint.Y, 1, 1);  //Draws point on panel
            }
        }
        static void btnDraw_Click(object sender, EventArgs e)
        {
            SetPointLocations(pnlDraw.Height);
            Graphics g = pnlDraw.CreateGraphics();
            g.Clear(Color.White);              //This clears the panel everytime the shape is drew, allowing the user the effect of changing the iterations of the points drawn.     
            btnDraw.Enabled = false;           //Disables Drawing button while simulation is running
            nudIterations.Enabled = false;     //Disables the numeric up-down while simulation is running.

            //Points to show location of triangle, the subtraction of 5 is merely for alignment purposes.
            g.FillEllipse(Brushes.Red, Triangle[0].X - 5, Triangle[0].Y - 5, 10, 10);
            g.FillEllipse(Brushes.Green, Triangle[1].X - 5, Triangle[1].Y - 5, 10, 10);
            g.FillEllipse(Brushes.Blue, Triangle[2].X - 5, Triangle[2].Y - 5, 10, 10);
            //End of points.

            CreateSierpinski(g);
            btnDraw.Enabled = true;             //Enables Drawing button after the simulation is completed
            nudIterations.Enabled = true;       //Enables numeric up-down after the simulation is completed.
        }
    }

    //References 
    /*
     * Numberphile video on YouTube: https://www.youtube.com/watch?v=kbKtFN71Lfs
     * Chaos Game Coding Challenge Part 1: https://www.youtube.com/watch?v=7gNzMtYo9n4
     * Chaos Game Coding Challenge Part 2: https://www.youtube.com/watch?v=A0NHGTggoOQ
     * Knowledge of the the game from: https://en.wikipedia.org/wiki/Chaos_game
     */
}
