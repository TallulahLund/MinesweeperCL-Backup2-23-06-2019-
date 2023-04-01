using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MW_Proto_Game_CA
{
    class Program
    {
        /* to do:
         * a fish in the sea
         * accounts
         * game details
         * timer
         * flags and mines shown
         * blank square/0 distinction
         * print out letters instead of minuses
         * try use excel/access
         * 
        */



        // loop grid
        //public static char[,] Original = { { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' } };
        public static int[,] Original = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        //public static int[,] StartGrid = Original;
        public static int[,] StartGrid = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        public static int Flags = 0;//

        static string rl()
        {
            return Console.ReadLine();
        }
        static int r()
        {
            return Console.Read();
        }
        static ConsoleKeyInfo rk()
        {
            return Console.ReadKey();
        }
        static void wl(string s)
        {
            Console.WriteLine(s);
        }
        static void wli(int i)
        {
            Console.WriteLine(i);
        }
        static void w(string s)
        {
            Console.Write(s);
        }
        static void wi(int i)
        {
            Console.Write(i);
        }
        static void space()
        {
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // log on to account

            displayGridint(Original);
            Console.WriteLine();
            game();
            Console.ReadLine();
        }

        static void game()              // sort out kinks, does generate mines tho
        {
            int[,] Mines = { { 'z', 'z' }, { 'z', 'z' }, { 'z', 'z' }, { 'z', 'z' }, { 'z', 'z' } };// new char [2,5];
            Console.WriteLine("length - {0}", Mines.Length);
            for (int i = 0; i < 5; i++)
            {
                Mine m = new Mine();
                string z = m.createMine();
                string[] co = z.Split(',');
                int x = int.Parse(co[0]);
                int y = int.Parse(co[1]);
                Mines[i, 0] = x;
                Mines[i, 1] = y;
                //Console.WriteLine(Convert.ToChar(x));
                displayGridint(Mines);
                space();
                for (int j = 0; j < i; j++)
                {
                    if (Mines[j, 0] == x && Mines[j, 1] == y)
                        i--;
                }
            }
            displayGridint(Mines);
            // mines created


            int[,] EdgeT = { { 0, 1 }, { 0, 2 }, { 0, 3 } };            // write algorithm to create these for n x m grid.
            int[,] EdgeB = { { 4, 1 }, { 4, 2 }, { 4, 3 } };
            int[,] EdgeL = { { 1, 0 }, { 2, 0 }, { 3, 0 } };
            int[,] EdgeR = { { 1, 4 }, { 2, 4 }, { 3, 4 } };

            int[,] CornerTL = { { 0, 0 } };                     // one corner array?
            int[,] CornerTR = { { 0, 4 } };
            int[,] CornerBL = { { 4, 0 } };
            int[,] CornerBR = { { 4, 4 } };
            int[,] Corner = { { 0, 0 }, { 0, 4 }, { 4, 0 }, { 4, 4 } };

            // searching areas around mines
            /*for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Mines[i, 0] == Corner[i, 0] && Mines[i, 1] == Corner[i, 1])
                    {
                        CornerSS(i);
                    }
                    if (Mines[i, 0] == EdgeT[i, 0] && Mines[i, 1] == EdgeT[i, 1])
                    {
                        EdgeTSS(i);
                    }
                    if (Mines[i, 0] == EdgeB[i, 0] && Mines[i, 1] == EdgeB[i, 1])
                    {
                        EdgeBSS(i);
                    }
                    if (Mines[i, 0] == EdgeL[i, 0] && Mines[i, 1] == EdgeL[i, 1])
                    {
                        EdgeLSS(i);
                    }
                    if (Mines[i, 0] == EdgeR[i, 0] && Mines[i, 1] == EdgeR[i, 1])
                    {
                        EdgeRSS(i);
                    }
                }
            }*/


            for (int i = 0; i < 5; i++)
            {
                //for (int j = 0; j < i; j++)
                //{
                    if (Mines[i, 0] == 0)
                    {
                        if (Mines[i, 1] == 0)
                            CornerSS(0);
                        else if (Mines[i, 1] == 4)
                            CornerSS(1);
                        else
                            EdgeTSS(Mines[i, 1]);//
                    }
                    else if (Mines[i, 0] == 4)
                    {
                        if (Mines[i, 1] == 0)
                            CornerSS(2);
                        else if (Mines[i, 1] == 4)
                            CornerSS(3);
                        else
                            EdgeBSS(Mines[i, 1]);
                    }
                    else if (Mines[i, 1] == 0)
                        EdgeLSS(Mines[i, 0]);
                    else if (Mines[i, 1] == 4)
                        EdgeRSS(Mines[i, 0]);
                    else
                        MiddleSS(Mines[i, 0], Mines[i, 1]);
                //}
            }// checks what position type mine is then calls corresponding procedures which add numbers to surrounding squares

            for (int i = 0; i < 5; i++)
            {
                StartGrid[Mines[i, 0], Mines[i, 1]] = -1;
            }// placing mines on StartGrid

            space();
            displayGridint(StartGrid);
            space();
            displayGridint(Original);// Original is now StartGrid - fix! -> fixed
            // start grid created


            // figure out how to deal if a 0 is clicked
            // L for Left-click, R for Right-click, x and y for co-ordinates                // display numbered rows and columns on grid?
            // input
            wl("Enter L for left-click or R for right-click, followed by a space then the x coordinate (row), then another space, then y coordinate (column).");       // x and y axis switched?
            // while loop or functions?
            //ConsoleKeyInfo click;

            //
            List<string> Location0sList = new List<string>();
            ////string[] Location0sArray = Location0sList.ToArray();
            //

            bool go = true;
            while (go == true)
            {
                w("Mines Left - ");
                wli(5 - Flags);

                char click = Char.ToUpper(rk().KeyChar);// make x, y like this? or this just l[0]? - although realisticly once clicked can't unclick -> should check if ligit value i.e (L OR R) and react accordingly
                string[] l = rl().Split(' ');           // could automatically put spaces in?
                int x = int.Parse(l[1]);
                int y = int.Parse(l[2]);
                if (click == 'L')
                {
                    /*int x = r();
                    int y = r();
                    space();
                    wli(x);
                    wli(y);*/
                    // doesn't work

                    if (StartGrid[x, y] == -1)
                    {
                        Original[x, y] = -1;
                        displayGridint(Original);
                        wl("You have hit a mine, GAME OVER!");
                        go = false;
                    }
                    else if (StartGrid[x, y] > 0)
                    {
                        Original[x, y] = StartGrid[x, y];
                        displayGridint(Original);
                    }
                    else if (StartGrid[x, y] == 0)                  // Surronding0s called
                    {
                        // search for surounding 0s
                        Location0sList.Add(x +"-"+ y);
                        Surrounding0s(Location0sList, x, y);
                        displayGridint(Original);//
                    }

                    // add code for if not a mine is clicked
                }
                else if (click == 'R')
                {
                    if (Original[x, y] > 0)
                    {
                        // 0 not blank
                        wl("Not an empty square.");
                    }
                    else if (Original[x, y] == -2)                           // -2 means flag, -3 means ?
                    {
                        Original[x, y] = -3;
                        Flags -= 1;
                    }
                    else if (Original[x, y] == -3)
                    {
                        Original[x, y] = 0;
                    }
                    else
                    {
                        Original[x, y] = -2;
                        Flags += 1;// global, ?
                        // if (Flags > 5) {count goes to minus, not end of game}

                    }
                    displayGridint(Original);
                }
            }

            // game now over
            // add game details to user's file
            // ask if want another game and if yes reset, in menu?
            Reset();          // sets everything back to the beginning
            // reset in menu
        }// switch of both corners and edges.

        public static List<string> PoppedOff = new List<string>();

        static void Surrounding0s(List<string> Location0sList, int x, int y)////////////////////////////////////////////////////////////
        {
            // above
            if (x - 1 >= 0)// is sign right?
            {
                if (StartGrid[x - 1, y] > 0)
                    Original[x - 1, y] = StartGrid[x - 1, y];// displays number ( > 0 )  // [x,y]?
                else if (StartGrid[x - 1, y] == 0)
                {
                    // check if already on stack
                    // checks if not already popped off the stack
                    if (!PoppedOff.Contains(x - 1 + "-" + y) && !Location0sList.Contains(x - 1 + "-" + y))// check if correct and works
                    {
                        // push on stack
                        Location0sList.Add(x - 1 + "-" + y);
                        Surrounding0s(Location0sList, x - 1, y);
                    }
                }

                // left
                if (y - 1 >= 0)// is sign right?
                {
                    if (StartGrid[x - 1, y - 1] > 0)
                        Original[x - 1, y - 1] = StartGrid[x - 1, y - 1];
                    else if (StartGrid[x - 1, y - 1] == 0)
                    {
                        // check if already on stack
                        // checks if not already popped off the stack
                        if (!PoppedOff.Contains(x - 1 + "-" + (y - 1)) && !Location0sList.Contains(x - 1 + "-" + (y - 1)))// check if correct and works
                        {


                            // push on stack
                            Location0sList.Add(x - 1 + "-" + (y - 1));
                            Surrounding0s(Location0sList, x - 1, y - 1);
                        }
                    }
                }
                else// in 1st column
                {

                }

                // right
                if (y + 1 <= 4)
                {
                    if (StartGrid[x - 1, y + 1] > 0)
                        Original[x - 1, y + 1] = StartGrid[x - 1, y + 1];
                    else if (StartGrid[x - 1, y + 1] == 0)
                    {
                        // check if already on stack
                        // checks if not already popped off the stack
                        if (!PoppedOff.Contains(x - 1 + "-" + (y + 1)) && !Location0sList.Contains(x - 1 + "-" + (y + 1)))// check if correct and works
                        {


                            // push on stack
                            Location0sList.Add(x - 1 + "-" + (y + 1));
                            Surrounding0s(Location0sList, x - 1, y + 1);
                        }
                    }
                }// else // in last column
            }
            else// on top row
            {

            }

            // left
            if (y - 1 >= 0)// is sign right?
            {
                if (StartGrid[x, y - 1] > 0)
                    Original[x, y - 1] = StartGrid[x, y - 1];
                else if (StartGrid[x, y - 1] == 0)
                {
                    // check if already on stack
                    // checks if not already popped off the stack
                    if (!PoppedOff.Contains(x + "-" + (y - 1)) && !Location0sList.Contains(x + "-" + (y - 1)))// check if correct and works
                    {


                        // push on stack
                        Location0sList.Add(x + "-" + (y - 1));
                        Surrounding0s(Location0sList, x, y - 1);
                    }
                }
            }
            else// in 1st column
            {

            }

            // right
            if (y + 1 <= 4)
            {
                if (StartGrid[x, y + 1] > 0)
                    Original[x, y + 1] = StartGrid[x, y + 1];
                else if (StartGrid[x, y + 1] == 0)
                {
                    // check if already on stack
                    // checks if not already popped off the stack
                    if (!PoppedOff.Contains(x + "-" + (y + 1)) && !Location0sList.Contains(x + "-" + (y + 1)))// check if correct and works
                    {


                        // push on stack
                        Location0sList.Add(x + "-" + (y + 1));
                        Surrounding0s(Location0sList, x, y + 1);
                    }
                }
            }// else // in last column

            // under
            if (x + 1 <= 4)// is sign right?
            {
                if (StartGrid[x + 1, y] > 0)
                    Original[x + 1, y] = StartGrid[x + 1, y];// displays number ( > 0 )  // [x,y]?
                else if (StartGrid[x + 1, y] == 0)
                {
                    // check if already on stack
                    // checks if not already popped off the stack
                    if (!PoppedOff.Contains(x + 1 + "-" + y) && !Location0sList.Contains(x + 1 + "-" + y))// check if correct and works
                    {
                        // push on stack
                        Location0sList.Add(x + 1 + "-" + y);
                        Surrounding0s(Location0sList, x + 1, y);
                    }
                }

                // left
                if (y - 1 >= 0)// is sign right?
                {
                    if (StartGrid[x + 1, y - 1] > 0)
                        Original[x + 1, y - 1] = StartGrid[x + 1, y - 1];
                    else if (StartGrid[x + 1, y - 1] == 0)
                    {
                        // check if already on stack
                        // checks if not already popped off the stack
                        if (!PoppedOff.Contains(x + 1 + "-" + (y - 1)) && !Location0sList.Contains(x + 1 + "-" + (y - 1)))// check if correct and works
                        {


                            // push on stack
                            Location0sList.Add(x + 1 + "-" + (y - 1));
                            Surrounding0s(Location0sList, x + 1, y - 1);
                        }
                    }
                }
                else// in 1st column
                {

                }

                // right
                if (y + 1 <= 4)
                {
                    if (StartGrid[x + 1, y + 1] > 0)
                        Original[x + 1, y + 1] = StartGrid[x + 1, y + 1];
                    else if (StartGrid[x + 1, y + 1] == 0)
                    {
                        // check if already on stack
                        // checks if not already popped off the stack
                        if (!PoppedOff.Contains(x + 1 + "-" + (y + 1)) && !Location0sList.Contains(x + 1 + "-" + (y + 1)))// check if correct and works
                        {


                            // push on stack
                            Location0sList.Add(x + 1 + "-" + (y + 1));
                            Surrounding0s(Location0sList, x + 1, y + 1);
                        }
                    }
                }// else // in last column
            }
            /*if (y - 1 <= 0)
            {

            }
            else// in 1st coloumn
            {

            }*/
        }

                                                            // need to define mines as -1 after as a later on mine could be placed next to existing mine and +1 to its -1
        static void CornerSS(int i)
        {
            switch (i)                          // write algorithm
            {
                case 0:
                    StartGrid[0, 0] = -1;
                    StartGrid[0, 1] += 1;
                    StartGrid[1, 1] += 1;
                    StartGrid[1, 0] += 1;
                    break;
                case 1:
                    StartGrid[0, 4] = -1;
                    StartGrid[0, 3] += 1;
                    StartGrid[1, 3] += 1;
                    StartGrid[1, 4] += 1;
                    break;
                case 2:
                    StartGrid[4, 0] = -1;
                    StartGrid[3, 0] += 1;
                    StartGrid[3, 1] += 1;
                    StartGrid[4, 1] += 1;
                    break;
                case 3:
                    StartGrid[4, 4] = -1;
                    StartGrid[3, 4] += 1;
                    StartGrid[3, 3] += 1;
                    StartGrid[4, 3] += 1;
                    break;
            }
        }
        static void EdgeTSS(int i)
        {
            switch (i)
            {
                case 1:
                    StartGrid[0, 1] = -1;
                    StartGrid[0, 0] += 1;
                    StartGrid[1, 0] += 1;
                    StartGrid[1, 1] += 1;
                    StartGrid[1, 2] += 1;
                    StartGrid[0, 2] += 1;
                    break;
                case 2:
                    StartGrid[0, 2] = -1;
                    StartGrid[0, 1] += 1;
                    StartGrid[1, 1] += 1;
                    StartGrid[1, 2] += 1;
                    StartGrid[1, 3] += 1;
                    StartGrid[0, 3] += 1;
                    break;
                case 3:
                    StartGrid[0, 3] = -1;
                    StartGrid[0, 2] += 1;
                    StartGrid[1, 2] += 1;
                    StartGrid[1, 3] += 1;
                    StartGrid[1, 4] += 1;
                    StartGrid[0, 4] += 1;
                    break;

            }
        }
        static void EdgeBSS(int i)
        {
            switch (i)
            {
                case 1:
                    StartGrid[4, 1] = -1;
                    StartGrid[4, 0] += 1;
                    StartGrid[3, 0] += 1;
                    StartGrid[3, 1] += 1;
                    StartGrid[3, 2] += 1;
                    StartGrid[4, 2] += 1;
                    break;
                case 2:
                    StartGrid[4, 2] = -1;
                    StartGrid[4, 1] += 1;
                    StartGrid[3, 1] += 1;
                    StartGrid[3, 2] += 1;
                    StartGrid[3, 3] += 1;
                    StartGrid[4, 3] += 1;
                    break;
                case 3:
                    StartGrid[4, 3] = -1;
                    StartGrid[4, 2] += 1;
                    StartGrid[3, 2] += 1;
                    StartGrid[3, 3] += 1;
                    StartGrid[3, 4] += 1;
                    StartGrid[4, 4] += 1;
                    break;
            }
        }
        static void EdgeLSS(int i)
        {
            switch (i)
            {
                case 1:
                    StartGrid[1, 0] = -1;
                    StartGrid[0, 0] += 1;
                    StartGrid[0, 1] += 1;
                    StartGrid[1, 1] += 1;
                    StartGrid[2, 1] += 1;
                    StartGrid[2, 0] += 1;
                    break;
                case 2:
                    StartGrid[2, 0] = -1;
                    StartGrid[1, 0] += 1;
                    StartGrid[1, 1] += 1;
                    StartGrid[2, 1] += 1;
                    StartGrid[3, 1] += 1;
                    StartGrid[3, 0] += 1;
                    break;
                case 3:
                    StartGrid[3, 0] = -1;
                    StartGrid[2, 0] += 1;
                    StartGrid[2, 1] += 1;
                    StartGrid[3, 1] += 1;
                    StartGrid[4, 1] += 1;
                    StartGrid[4, 0] += 1;
                    break;

            }
        }
        static void EdgeRSS(int i)
        {
            switch (i)
            {
                case 1:
                    StartGrid[1, 4] = -1;
                    StartGrid[0, 4] += 1;
                    StartGrid[0, 3] += 1;
                    StartGrid[1, 3] += 1;
                    StartGrid[2, 3] += 1;
                    StartGrid[2, 4] += 1;
                    break;
                case 2:
                    StartGrid[2, 4] = -1;
                    StartGrid[1, 4] += 1;
                    StartGrid[1, 3] += 1;
                    StartGrid[2, 3] += 1;
                    StartGrid[3, 3] += 1;
                    StartGrid[3, 4] += 1;
                    break;
                case 3:
                    StartGrid[3, 4] = -1;
                    StartGrid[2, 4] += 1;
                    StartGrid[2, 3] += 1;
                    StartGrid[3, 3] += 1;
                    StartGrid[4, 3] += 1;
                    StartGrid[4, 4] += 1;
                    break;
            }
        }
        static void MiddleSS(int x, int y)
        {
            StartGrid[x, y] = -1;
            StartGrid[x, y - 1] += 1;
            StartGrid[x - 1, y - 1] += 1;
            StartGrid[x - 1, y] += 1;
            StartGrid[x - 1, y + 1] += 1;
            StartGrid[x, y + 1] += 1;
            StartGrid[x + 1, y + 1] += 1;
            StartGrid[x + 1, y] += 1;
            StartGrid[x + 1, y - 1] += 1;
        }
        

        //protected char[,] Original = { { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' }, { '0', '0', '0', '0', '0' } };

        static void displayGrid(char[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                    Console.Write("{0}   ", grid[i, j]);
                Console.WriteLine();
            }
        }
        static void displayGridint(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                    Console.Write("{0}\t", grid[i, j]);
                Console.WriteLine();
            }
        }


        static void Reset()
        {
            Original = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
            StartGrid = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
            Flags = 0;
            // reset global variables and timer
        }
    


        /*private int minecount = 5;

        private int countertimer = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            //timecount.BackColor = Color.DarkGray;
            countertimer++;
            if (countertimer >= 999)
                Timer.Stop();
            if (countertimer < 10)
                timecount.Text = "00" + countertimer.ToString();
            else if (countertimer < 100)
                timecount.Text = "0" + countertimer.ToString();
            else
                timecount.Text = countertimer.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timecount.BackColor = Color.DarkGray;
            countertimer++;
            if (countertimer >= 999)
                Timer.Stop();
            if (countertimer < 10)
                timecount.Text = "00" + countertimer.ToString();
            else if (countertimer < 100)
                timecount.Text = "0" + countertimer.ToString();
            else
                timecount.Text = countertimer.ToString();
        }*/
    }

    class Mine
    {
        protected char mine;

        public Mine()
        {/*
            Random rnd = new Random();
            //char[,] newgrid = Original;
            List<string> location = new List<string>();
            int x = rnd.Next(5);
            int y = rnd.Next(5);
            /// create mines compare location generated so that no one's picked twice, update new grid array with mines
            */
        }

        public string createMine()
        {
            Random rnd = new Random();
            //char[,] newgrid = Original;
            List<string> location = new List<string>();
            int x = rnd.Next(5);
            int y = rnd.Next(5);
            /// create mines compare location generated so that no one's picked twice, update new grid array with mines
            string z = x.ToString() + "," + y.ToString();
            return z;
        }
    }
}


// do error handling at the end.