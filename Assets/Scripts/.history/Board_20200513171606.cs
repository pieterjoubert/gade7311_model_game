using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public enum Owner
    {
        EMPTY,
        RED,
        BLUE
    }

    public class Board
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Owner[] Walls { get; }
        public Owner[,] Boxes { get; }

        public int NumWalls {get; set;} 
        public int NumCubes {get; set;} 
        public Board(int height, int width)
        {
            Height = height;
            Width = width;
            NumWalls  = 0;
            for(int i = 0; i < (Height * 2) - 1; i++)
            {
                NumWalls += i % 2 == 0 ? Width - 1 : Width;
            }

            NumCubes = (Height - 1) * (Width - 1);

            Walls = new Owner[NumWalls];
            Boxes = new Owner[Width - 1, Height - 1];
            
            for (int i = 0; i < NumWalls; i++)
            {
                Walls[i] = Owner.EMPTY; 
            }

            for(int x = 0; x < Width - 1; x++)
            {
                for(int z = 0; z < Width - 1; z++)
                {
                    Boxes[x, z] = Owner.EMPTY; //Testing with red first
                }
            }

        }

        public string ToString()
        {
            string temp = "";
            bool horizontal = true;
            int i = 0;
            foreach(Owner o in Walls)
            {
                temp += (int)o;
                i++;
                if(horizontal == true && i == Width - 1)
                {
                    temp += "\n";
                    i = 0;
                    horizontal = false;
                }
                else if (horizontal == false && i == Width)
                {
                    temp += "\n";
                    i = 0;
                    horizontal = true;
                }
            }
            
            return temp;
        }

        public bool Place(Owner p, int x, int y)
        {
            if(Walls[GetPosition(x, y)] == Owner.EMPTY)
            {
                Walls[GetPosition(x, y)] = p;
                return true;
            }
            return false;
        }
        public bool ClosedBox(Owner p, int x, int y)
        {
            bool closed = false;
            if (y % 2 == 0) //horizontal wall
            {
                if ((GetPosition(x, y + 1) != -1 &&
                    GetPosition(x, y + 2) != -1 &&
                    GetPosition(x + 1, y + 1) != -1) &
                    (Walls[GetPosition(x, y + 1)] != Owner.EMPTY &&
                    Walls[GetPosition(x, y + 2)] != Owner.EMPTY &&
                    Walls[GetPosition(x + 1, y + 1)] != Owner.EMPTY))//box above
                {
                    if(x + 1 < Width && y / 2 < Height - 1)
                        Boxes[x, y / 2] = p;
                    closed = true;
                }
                if (Walls[GetPosition(x, y - 1)] != Owner.EMPTY &&
                    Walls[GetPosition(x, y - 2)] != Owner.EMPTY &&
                    Walls[GetPosition(x + 1, y - 1)] != Owner.EMPTY)//box below
                {
                    Boxes[x, (y / 2) - 1] = p;
                    closed = true;
                }
            }
            else //vertical wall
            {
                if (Walls[GetPosition(x - 1, y + 1)] != Owner.EMPTY &&
                    Walls[GetPosition(x - 1, y - 1)] != Owner.EMPTY &&
                    Walls[GetPosition(x - 1, y)] != Owner.EMPTY) //box to the left
                {
                    if(x - 1 > 0 && y / 2 < Height - 1)
                        Boxes[x - 1, y / 2] = p;
                    closed = true;
                }
                if (Walls[GetPosition(x, y + 1)] != Owner.EMPTY &&
                    Walls[GetPosition(x, y - 1)] != Owner.EMPTY &&
                    Walls[GetPosition(x + 1, y)] != Owner.EMPTY) //box to the right
                {
                    Boxes[x, (int)Math.Ceiling((double)(y - 1) / 2)] = p;
                    closed = true;
                }
            }
            return closed;
        }

        private int GetPosition(int x, int y)
        {
            int position = 0;
            for(int i = 0; i < y; i++)
            {
                position += i % 2 == 0 ? Width - 1 : Width;
            }
            position += x;
            Debug.Log("Calculate position: " + position + " for: " + x + "," + y);
            if (position >= NumWalls)
            { 
                position = -1;
                Debug.Log("Outside Normal Constraints for: " + x + "," + y);

            }
            if (position < 0)
            {
                position = -1;
                Debug.Log("Outside Normal Constraints for: " + x + "," + y);
            }
            return position;
        }


        public Owner Get(int x, int y)
        {
            return Walls[GetPosition(x,y)];
        }

        public double EvaluationFunction()
        {
            return 0.0;
        }
    }
}
