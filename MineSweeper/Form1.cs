using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MineSweeper : Form
    {
        private Game game;
        private int fieldWidth;
        private int fieldHeight;
        private Square[,] imageMap;
        String startupPath = System.IO.Directory.GetCurrentDirectory();
        const String fieldImagePath = "/dicpic.png";
        const String hoverFieldImagePath = "/dicpichover.png";
        const String visitedFieldPath = "/field.png";
        const String bombPath = "/bomb.png";
        const String extension = ".png";
        const int margin = 70;
        int levelChosen;
        bool gameOver;

        public MineSweeper()
        {
            InitializeComponent();
            gameOver = false;
            levelChosen = 0;
        }

        private void button1_Click(object sender, EventArgs e) // new game button
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(startupPath + fieldImagePath);  // to determine dimensions
            fieldWidth = img.Width;

            // clean up block
            if (gameOver)
            {
                for (int x = 0; x < game.getMapWidth(); x++)
                {
                    for (int y = 0; y < game.getMapHeight(); y++)
                    {
                        this.Controls.Remove(imageMap[x, y]);
                    }
                }
            }
            // clean up block

            int bombs = 0;
            int X = 0;
            int Y = 0;

            if(levelChosen == 0){
                bombs = 10;
                X = 10;
                Y = 8;
            }
            else if(levelChosen == 1){
                bombs = 30;
                X = 12;
                Y = 10;
            }
            else if(levelChosen == 2){
                bombs = 70;
                X = 20;
                Y = 15;
            }

            game = new Game(bombs,X,Y, this);
            
            this.SetBounds(this.Left, this.Top, X *fieldWidth + 2* margin, Y * fieldWidth + 2 * margin + 30);

            imageMap = new Square[game.getMapWidth(),game.getMapHeight()];

            b_newgame.Visible = false;
            b_easy.Visible = false;
            b_medium.Visible = false;
            b_hard.Visible = false;
            l_level.Visible = false;

            printMap();
            gameOver = false;
        }
        public void printBomb(int x, int y)
        {
            imageMap[x, y].ImageLocation = startupPath + bombPath;
        }
        public void printField(int x, int y)
        {
            imageMap[x, y].ImageLocation = startupPath + "\\" + game.getBombsNear(x, y).ToString() + extension;
        }
        public void printZero(int x, int y)
        {
            imageMap[x, y].ImageLocation = startupPath + "\\0.png";
        }
        private void printMap()
        {
            bool[,] map = game.getMap();

            System.Drawing.Image img = System.Drawing.Image.FromFile(startupPath + fieldImagePath);  // to determine dimensions

            fieldWidth = img.Width;
            fieldHeight = img.Height;

            int height = game.getMapHeight();
            int width = game.getMapWidth();

            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    imageMap[x, y] = new Square();
                    imageMap[x, y].Image = img;
                    imageMap[x, y].Width = fieldWidth;
                    imageMap[x, y].Height = fieldHeight;
                    imageMap[x, y].Location = new System.Drawing.Point(margin + fieldWidth * x, margin + fieldHeight * y);
                    imageMap[x, y].MouseEnter += infield;
                    imageMap[x, y].MouseLeave += outfield;
                    imageMap[x, y].MouseClick += clickfield;
                    imageMap[x, y].x = x;
                    imageMap[x, y].y = y;
                    this.Controls.Add(imageMap[x, y]);
                }
            }
        }
        public void endGame()
        {
            uncoverMap();
            gameOver = true;

            b_newgame.Location = new System.Drawing.Point(100, 10);
            b_easy.Location = new System.Drawing.Point(10, 40);
            b_medium.Location = new System.Drawing.Point(70, 40);
            b_hard.Location = new System.Drawing.Point(130, 40);
            l_level.Location = new System.Drawing.Point(190, 43);

            b_newgame.Visible = true;
            b_newgame.Visible = true;
            b_easy.Visible = true;
            b_medium.Visible = true;
            b_hard.Visible = true;
            l_level.Visible = true; ;
            
        }
        public void uncoverMap()
        {
            bool[,] bombMap = game.getMap();
            int height = game.getMapHeight();
            int width = game.getMapWidth();

            for (int x = 0; x < width; x++){
                for (int y = 0; y < height; y++){
                    if (bombMap[x, y])
                        imageMap[x, y].ImageLocation = startupPath + bombPath;
                    else
                        imageMap[x, y].ImageLocation = startupPath + visitedFieldPath;
                }
            }
       }


        private void Form1_Load(object sender, EventArgs e)
        {
            // components are added either graphically or dynamically on event
        }
        private void clickfield(object sender, EventArgs e) // when clicking on a mine field
        {
            if (!gameOver){
                int x = ((Square)sender).x;
                int y = ((Square)sender).y;
                game.checkField(x, y);
                if (game.getBombsNear(x, y) == 0)
                    game.uncoverBFS(x, y);
            }
            int height = game.getMapHeight();
            int width = game.getMapWidth();
            if (height * width - game.getUncoveredFields() == game.getBombsCount() + 1){ // win
                MessageBox.Show("You won! Your game took " + game.getElapsedSeconds().ToString() + " seconds.");
                endGame();
                b_newgame.Show();
                uncoverMap();
            }
        }

        private void infield(object sender, EventArgs e) // on mouse in field
        {
            if(!game.visited(   ((Square)sender).x, ((Square)sender).y) && !gameOver    )
                ((Square)sender).ImageLocation = startupPath + hoverFieldImagePath;
        }

        private void outfield(object sender, EventArgs e) // on mouse out of field
        {
            if (!game.visited(  ((Square)sender).x, ((Square)sender).y) && !gameOver)
                ((Square)sender).ImageLocation = startupPath + fieldImagePath;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            levelChosen = 0;
            l_level.Text = "Easy";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            levelChosen = 1;
            l_level.Text = "Medium";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            levelChosen = 2;
            l_level.Text = "Hard";
        }
    }
}
