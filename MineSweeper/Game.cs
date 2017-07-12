using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MineSweeper
{
    class Game
    {
        private int bombsCount;
        private int mapWidth;
        private int mapHeight;
        private bool[,] bombMap;
        private bool[,] exploredMap;
        private byte[,] bombsNear; // toset 
        private byte level;  // 0 - easy 1-medium 2-hard
        private MineSweeper ms;
        private Random ran = new Random();
        private int fieldsUncovered;
        Stopwatch timer = new Stopwatch();

        public Game(int bombs, int width,int height, MineSweeper ms) // bombs < size^2 will not be checked as it's provided by programmer
        {
            timer.Start();

            fieldsUncovered = 0;
            level = 0;

            bombsCount = bombs;
            mapWidth = width;
            mapHeight = height;

            bombMap = new bool[mapHeight, mapWidth];
            fillMapWithValue(false, bombMap);

            exploredMap = new bool[mapHeight, mapWidth];
            fillMapWithValue(false, exploredMap);

            fillRandomBombs();

            bombsNear = new byte[mapHeight, mapWidth];
            setNearValues();

            this.ms = ms;
        }

        public void setLevel(byte level)
        {
            this.level = level;
        }

        public bool[,] getMap()
        {
            return bombMap;
        }

        public int getMapWidth()
        {
            return mapWidth;
        }
        public int getMapHeight()
        {
            return mapHeight;
        }
        public double getElapsedSeconds()
        {
            double elapsedSeconds = (double)timer.ElapsedMilliseconds / (double)1000;
            return elapsedSeconds;
        }

        public int getBombsCount()
        {
            return bombsCount;
        }
        public int getBombsNear(int i, int j)
        {
            return bombsNear[i, j];
        }
        public int getUncoveredFields()
        {
            return fieldsUncovered;
        }
        
        void fillRandomBombs()
        {
            ArrayList freePositions = new ArrayList();
            for(int i = 0; i < mapWidth * mapHeight;i++)
                freePositions.Add(i);

            for(int i = 0; i < bombsCount; i++){
                int nextRan = ran.Next() % (mapWidth * mapHeight - i);
                int chosenPosition = (int)freePositions[nextRan];
                freePositions.RemoveAt(nextRan);

                int xpos = chosenPosition % mapHeight;
                int ypos = chosenPosition / mapHeight;
                bombMap[xpos, ypos] = true;
            }
        }

        void fillMapWithValue(bool val, bool[,] map)
        {
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                    map[i, j] = val;
        }

        public void checkField(int i, int j)
        {
            if (exploredMap[i, j] == false && bombMap[i, j] == true) // lose - player stepped on a bomb
            {
                ms.endGame();
            }
            else if(exploredMap[i, j] == false && bombMap[i, j] == false) // empty field
            {
                exploredMap[i, j] = true;
                fieldsUncovered++;
                ms.printField(i, j);
            }
        }
        void setNearValues()    // on new game
        {
            //cleanup
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                    bombsNear[i, j] = 0;
            //cleanup

            for (int i = 0; i < mapHeight; i++)
            {
                for(int j = 0; j < mapWidth; j++)
                {
                    bool up, down, left, right;
                    up = down = right = left = true;
                    if(bombMap[i,j]) { // setting also values for bomb fields, although unnecessary, it would take more time to exclude them
                        if (i == 0)
                            up = false;
                        if ((i + 1) / mapHeight == 1)
                            down = false;
                        if (j % mapWidth == 0)
                            left = false;
                        if (j % (mapWidth - 1) == 0)
                            right = false;

                        if (up)
                            bombsNear[i - 1, j]++;
                        if (down)
                            bombsNear[i + 1, j]++;
                        if (left)
                            bombsNear[i, j - 1]++;
                        if (right)
                            bombsNear[i, j + 1]++;
                        if (up && left)
                            bombsNear[i - 1, j - 1]++;
                        if (up && right)
                            bombsNear[i - 1, j + 1]++;
                        if (down && left)
                            bombsNear[i + 1, j - 1]++;
                        if (down && right)
                            bombsNear[i + 1, j + 1]++;
                    }
                }
            }

        }

        public bool visited(int i, int j)
        {
            return exploredMap[i, j];
        }
    } // class
} // namespace
