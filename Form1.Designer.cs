namespace WinFormsApp1
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelresponseinput = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();

            // 
            // labelresponseinput
            // 
            this.labelresponseinput.AutoSize = true;
            this.labelresponseinput.Location = new System.Drawing.Point(200, 220);
            this.labelresponseinput.Name = "labelresponseinput";
            this.labelresponseinput.Size = new System.Drawing.Size(104, 20);
            this.labelresponseinput.TabIndex = 1;
            this.labelresponseinput.Text = "Počet pokusov";
            this.labelresponseinput.Font = new Font("Arial", 12, FontStyle.Bold);
            this.labelresponseinput.ForeColor = Color.DarkSlateBlue;

            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(370, 220);
            this.numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(83, 27);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] { 6, 0, 0, 0 });
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);

            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(370, 150);
            this.numericUpDown2.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            this.numericUpDown2.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(83, 27);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] { 5, 0, 0, 0 });
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Dĺžka slova";
            this.label1.Font = new Font("Arial", 12, FontStyle.Bold);
            this.label1.ForeColor = Color.DarkSlateBlue;

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(496, 373);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 5;
            this.button1.Text = "GO!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.BackColor = Color.LightSkyBlue;
            this.button1.Font = new Font("Arial", 12, FontStyle.Bold);
            this.button1.Click += new System.EventHandler(this.startGame);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 484);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.labelresponseinput);
            this.Name = "Form1";
            this.Text = "Wordle";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private Label labelresponseinput;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label1;
        private Button button1;
    }
}
