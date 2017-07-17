namespace MineSweeper
{
    partial class MineSweeper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_newgame = new System.Windows.Forms.Button();
            this.b_easy = new System.Windows.Forms.Button();
            this.b_medium = new System.Windows.Forms.Button();
            this.b_hard = new System.Windows.Forms.Button();
            this.l_level = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // b_newgame
            // 
            this.b_newgame.Location = new System.Drawing.Point(110, 147);
            this.b_newgame.Name = "b_newgame";
            this.b_newgame.Size = new System.Drawing.Size(98, 28);
            this.b_newgame.TabIndex = 0;
            this.b_newgame.Text = "New Game";
            this.b_newgame.UseVisualStyleBackColor = true;
            this.b_newgame.Click += new System.EventHandler(this.button1_Click);
            // 
            // b_easy
            // 
            this.b_easy.Location = new System.Drawing.Point(12, 118);
            this.b_easy.Name = "b_easy";
            this.b_easy.Size = new System.Drawing.Size(75, 23);
            this.b_easy.TabIndex = 1;
            this.b_easy.Text = "Easy";
            this.b_easy.UseVisualStyleBackColor = true;
            this.b_easy.Click += new System.EventHandler(this.button2_Click);
            // 
            // b_medium
            // 
            this.b_medium.Location = new System.Drawing.Point(94, 118);
            this.b_medium.Name = "b_medium";
            this.b_medium.Size = new System.Drawing.Size(75, 23);
            this.b_medium.TabIndex = 2;
            this.b_medium.Text = "Medium";
            this.b_medium.UseVisualStyleBackColor = true;
            this.b_medium.Click += new System.EventHandler(this.button3_Click);
            // 
            // b_hard
            // 
            this.b_hard.Location = new System.Drawing.Point(175, 118);
            this.b_hard.Name = "b_hard";
            this.b_hard.Size = new System.Drawing.Size(75, 23);
            this.b_hard.TabIndex = 3;
            this.b_hard.Text = "Hard";
            this.b_hard.UseVisualStyleBackColor = true;
            this.b_hard.Click += new System.EventHandler(this.button4_Click);
            // 
            // l_level
            // 
            this.l_level.AutoSize = true;
            this.l_level.Location = new System.Drawing.Point(269, 121);
            this.l_level.Name = "l_level";
            this.l_level.Size = new System.Drawing.Size(39, 17);
            this.l_level.TabIndex = 4;
            this.l_level.Text = "Easy";
            this.l_level.Click += new System.EventHandler(this.label1_Click);
            // 
            // MineSweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 269);
            this.Controls.Add(this.l_level);
            this.Controls.Add(this.b_hard);
            this.Controls.Add(this.b_medium);
            this.Controls.Add(this.b_easy);
            this.Controls.Add(this.b_newgame);
            this.Name = "MineSweeper";
            this.Text = "MineSweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_newgame;
        private System.Windows.Forms.Button b_easy;
        private System.Windows.Forms.Button b_medium;
        private System.Windows.Forms.Button b_hard;
        private System.Windows.Forms.Label l_level;
    }
}

