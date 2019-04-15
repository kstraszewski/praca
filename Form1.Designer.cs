namespace demowinformcs1
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(332, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnStart);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 115);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(332, 48);
            this.button2.TabIndex = 1;
            this.button2.Text = "Snap";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnSnap);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(24, 202);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(328, 33);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.OnSelectResolution);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(24, 290);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(185, 29);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Auto Exposure";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(24, 388);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(332, 90);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.ValueChanged += new System.EventHandler(this.OnExpoValueChange);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 336);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Expo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(364, 83);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1336, 940);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 458);
            this.button3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(332, 73);
            this.button3.TabIndex = 8;
            this.button3.Text = "White Balance One Push";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnWhiteBalanceOnePush);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 551);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(328, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Temp";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(19, 647);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tint";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(15, 582);
            this.trackBar2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(332, 90);
            this.trackBar2.TabIndex = 11;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar2.ValueChanged += new System.EventHandler(this.OnTempTintChanged);
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(15, 678);
            this.trackBar3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(332, 90);
            this.trackBar3.TabIndex = 12;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar3.ValueChanged += new System.EventHandler(this.OnTempTintChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 731);
            this.button4.Margin = new System.Windows.Forms.Padding(6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(332, 48);
            this.button4.TabIndex = 13;
            this.button4.Text = "DOWN";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(24, 806);
            this.button5.Margin = new System.Windows.Forms.Padding(6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(332, 48);
            this.button5.TabIndex = 14;
            this.button5.Text = "STOP";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(24, 875);
            this.button6.Margin = new System.Windows.Forms.Padding(6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(332, 48);
            this.button6.TabIndex = 15;
            this.button6.Text = "UP";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(368, 37);
            this.result.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(332, 25);
            this.result.TabIndex = 16;
            this.result.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1722, 1076);
            this.Controls.Add(this.result);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MinimumSize = new System.Drawing.Size(1254, 923);
            this.Name = "Form1";
            this.Text = "demowinformcs1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label result;
    }
}

