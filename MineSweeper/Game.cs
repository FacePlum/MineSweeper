using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace MineSweeper
{
    class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

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

        public Game(int bombs, int width,int height, MineSweeper ms) 
        {
            timer.Start();

            fieldsUncovered = 0;
            level = 0;

            bombsCount = bombs;
            mapWidth = width;
            mapHeight = height;

            bombMap = new bool[mapWidth, mapHeight];
            fillMapWithValue(false, bombMap);

            exploredMap = new bool[mapWidth, mapHeight];
            fillMapWithValue(false, exploredMap);

            fillRandomBombs();

            bombsNear = new byte[mapWidth, mapHeight];
            setNearValues();

            this.ms = ms;
        }

        public void setLevel(byte level) // difficulty level
        {
            this.level = level;
        }

        public bool[,] getMap() // returns map with bomb placement
        {
            return bombMap;
        }

        public int getMapWidth()    // returns game width in blocks
        {
            return mapWidth;
        }
        public int getMapHeight() // returns game height in blocks
        {
            return mapHeight;
        }
        public double getElapsedSeconds()   // returns elapsed seconds since the game began
        {
            double elapsedSeconds = (double)timer.ElapsedMilliseconds / (double)1000;
            return elapsedSeconds;
        }

        public int getBombsCount() // returns total number of bombs on map
        {
            return bombsCount;
        }
        public int getBombsNear(int x, int y) // returns the quantity of bombs that surround a field
        {
            return bombsNear[x, y];
        }
        public int getUncoveredFields() // returns the quantity of uncovered fields
        {
            return fieldsUncovered;
        }
        public void uncoverBFS(int xstart, int ystart) // uses BFS algorithm to uncover all "0" fields near "0" field player has chosen
        {
            Queue<Point> toCheck = new Queue<Point>();
            toCheck.Enqueue(new Point(xstart, ystart));
            exploredMap[xstart, ystart] = true;
            bool up, down, left, right;
            up = down = left = right = true;

            while(toCheck.Count != 0)
            {
                Point nowChecked = toCheck.Dequeue();
                
                int x = nowChecked.x;
                int y = nowChecked.y;

                ms.printZero(x, y);
                fieldsUncovered++;

                if (y == 0)
                    up = false;
                if ((y + 1) / mapHeight == 1)
                    down = false;
                if (x % mapWidth == 0)
                    left = false;
                if (x % (mapWidth) == mapWidth-1)
                    right = false;

                if (up && !exploredMap[x,y-1] && bombsNear[x, y - 1] == 0){ 
                    toCheck.Enqueue(new Point(x, y-1));
                    exploredMap[x, y-1] = true;
                }
                if (up && left && !exploredMap[x - 1, y - 1] && bombsNear[x - 1, y - 1] == 0){
                    toCheck.Enqueue(new Point(x - 1, y - 1));
                    exploredMap[x - 1, y - 1] = true;
                }
                if (up && right && !exploredMap[x + 1, y - 1] && bombsNear[x + 1, y - 1] == 0){
                    toCheck.Enqueue(new Point(x + 1, y - 1));
                    exploredMap[x + 1, y - 1] = true;
                }
                if (left && !exploredMap[x-1,y] && bombsNear[x - 1, y] == 0){
                    toCheck.Enqueue(new Point(x - 1, y));
                    exploredMap[x - 1, y] = true;
                }
                if (right && !exploredMap[x + 1, y] && bombsNear[x + 1, y] == 0){
                    toCheck.Enqueue(new Point(x + 1, y));
                    exploredMap[x + 1, y] = true;
                }
                if (down && left && !exploredMap[x - 1, y + 1] && bombsNear[x - 1, y + 1] == 0){
                    toCheck.Enqueue(new Point(x - 1, y + 1));
                    exploredMap[x - 1, y + 1] = true;
                }
                if (down && right && !exploredMap[x + 1, y + 1] && bombsNear[x + 1, y + 1] == 0){
                    toCheck.Enqueue(new Point(x + 1, y + 1));
                    exploredMap[x + 1, y + 1] = true;
                }
                if (down && !exploredMap[x, y + 1] && bombsNear[x, y + 1] == 0){
                    toCheck.Enqueue(new Point(x, y + 1));
                    exploredMap[x, y + 1] = true;
                }
                up = down = left = right = true;
            }
        }

        void fillRandomBombs() // fills map with bombs on random places
        {
            ArrayList freePositions = new ArrayList();
            for(int i = 0; i < mapWidth * mapHeight;i++)
                freePositions.Add(i);

            for(int i = 0; i < bombsCount; i++){
                int nextRan = ran.Next() % (mapWidth * mapHeight - i);
                int chosenPosition = (int)freePositions[nextRan];
                int xpos = chosenPosition % mapWidth;
                int ypos = chosenPosition / mapWidth;
                bombMap[xpos, ypos] = true;
                freePositions.RemoveAt(nextRan);
            }
        }

        void fillMapWithValue(bool val, bool[,] map)   
        {
            for (int x = 0; x <mapWidth ; x++)
                for (int y = 0; y < mapHeight; y++)
                    map[x, y] = val;
        }

        public void checkField(int x, int y) // checks the field player has chosen and does action connected with it
        {
            if (exploredMap[x, y] == false && bombMap[x, y] == true) { // lose - player stepped on a bomb
                ms.endGame();
                MessageBox.Show("Game Over!");
            }
            else if(exploredMap[x, y] == false && bombMap[x, y] == false) { // player stepped on an empty field
                exploredMap[x, y] = true;
                fieldsUncovered++;
                ms.printField(x, y);
            }
        }

        void setNearValues()    // Fills every field with quantity of bombs that are surrounding that field
        {
            //cleanup
            for (int x = 0; x < mapWidth; x++)
                for (int y = 0; y < mapHeight; y++)
                    bombsNear[x, y] = 0;
            //cleanup

            for (int x = 0; x < mapWidth; x++){
                for(int y = 0; y < mapHeight; y++){
                    bool up, down, left, right;
                    up = down = right = left = true;
                    if(bombMap[x,y]) { // setting also values for fields with bombs, although unnecessary, it would take more time to exclude them
                        if (y == 0)
                            up = false;
                        if ((y + 1) / mapHeight == 1)
                            down = false;
                        if (x % mapWidth == 0)
                            left = false;
                        if (x % (mapWidth) == mapWidth - 1)
                            right = false;

                        if (up)
                            bombsNear[x, y-1]++;
                        if (down)
                            bombsNear[x, y+1]++;
                        if (left)
                            bombsNear[x-1, y]++;
                        if (right)
                            bombsNear[x + 1, y]++;
                        if (up && left)
                            bombsNear[x - 1, y - 1]++;
                        if (up && right)
                            bombsNear[x + 1, y - 1]++;
                        if (down && left)
                            bombsNear[x - 1, y + 1]++;
                        if (down && right)
                            bombsNear[x + 1, y + 1]++;
                    }
                }
            }

        }

        public bool visited(int x, int y) // checks whether given field was visited(if it's uncovered)
        {
            return exploredMap[x, y];
        }
    } // class
} // namespace
