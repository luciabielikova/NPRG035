namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string slovo = "kitty";
        private FlowLayoutPanel flp;
        private Panel scorePanel;
        private Label scoreLabel;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged_2(object sender, EventArgs e)
        {
            decimal value = numericUpDown1.Value;
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            decimal value = numericUpDown2.Value;
        }

        private void novePolicka(int pocetPokusov, int dlzkaSlova)
        {
            flp = new FlowLayoutPanel();
            flp.FlowDirection = FlowDirection.LeftToRight;
            flp.BorderStyle = BorderStyle.Fixed3D;
            flp.Location = new Point(30, 30);
            flp.Size = new Size(dlzkaSlova * 50 + dlzkaSlova * 10 + 50, pocetPokusov * 55); 
            this.Controls.Add(flp);

            InitializeScorePanel();

            this.Size = new Size(flp.Width + scorePanel.Width + 100, flp.Height + 120);

            for (int j = 0; j < pocetPokusov; j++)
            {
                for (int i = j * dlzkaSlova; i < j * dlzkaSlova + dlzkaSlova; i++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = "textBox_" + i.ToString();
                    textBox.Width = 50;
                    textBox.Height = 50;
                    textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    textBox.MaxLength = 1;
                    textBox.Font = new Font("Arial", 20, FontStyle.Bold); 
                    textBox.TextAlign = HorizontalAlignment.Center; 

                    if (i != 0)
                    {
                        textBox.Enabled = false;
                    }

                    textBox.TextChanged += new EventHandler(textBox_TextChanged);

                    flp.Controls.Add(textBox);
                }
                Button submitBtn = new Button();
                submitBtn.Name = "submitBTN_" + j.ToString();
                submitBtn.Width = 50;
                submitBtn.Height = 50;
                submitBtn.BackColor = Color.LightGreen;
                submitBtn.Font = new Font("Arial", 12, FontStyle.Bold);
                submitBtn.Text = "OK";
                submitBtn.Click += new System.EventHandler(submitBTN_Click);

                submitBtn.Enabled = false;

                flp.Controls.Add(submitBtn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            label1.Visible = false;
            labelresponseinput.Visible = false;
            button1.Visible = false;
            novePolicka((int)numericUpDown1.Value, (int)numericUpDown2.Value);
        }

        private void submitBTN_Click(object sender, EventArgs e)
        {
            Button tlacitko = sender as Button;
            int poradieSubmitTlacitka = int.Parse(tlacitko.Name[10..]);
            int dlzkaSlova = (int)numericUpDown2.Value;
            bool allCorrect = true;

            for (int i = poradieSubmitTlacitka * dlzkaSlova; i < (poradieSubmitTlacitka + 1) * dlzkaSlova; i++)
            {
                TextBox textBox = flp.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBox != null)
                {
                    if (textBox.Text[0] == slovo[i % dlzkaSlova])
                    {
                        textBox.BackColor = Color.Green;  
                        UpdateScore(2);
                    }
                    else if (slovo.Contains(textBox.Text[0]))
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gold; 
                        UpdateScore(1); 
                    }
                    else
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gray;  
                    }

                    textBox.Enabled = false;
                }
            }

            tlacitko.Enabled = false;

            if (allCorrect)
            {
                DialogResult result = MessageBox.Show("Play again?", "The End", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    RestartGame();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            string[] nameParts = currentTextBox.Name.Split('_');
            int currentIndex = int.Parse(nameParts[1]);

            if (string.IsNullOrEmpty(currentTextBox.Text))
            {
                return;
            }

            int nextIndex = currentIndex + 1;
            if (nextIndex < flp.Controls.Count && flp.Controls["textBox_" + nextIndex.ToString()] is TextBox nextTextBox)
            {
                nextTextBox.Enabled = true;
                nextTextBox.Focus();
            }

            int poradieSubmitTlacitka = currentIndex / (int)numericUpDown2.Value;

            bool allForSubmitFilled = true;
            for (int i = poradieSubmitTlacitka * (int)numericUpDown2.Value; i < (poradieSubmitTlacitka + 1) * (int)numericUpDown2.Value; i++)
            {
                TextBox textBoxToCheck = flp.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBoxToCheck != null && string.IsNullOrEmpty(textBoxToCheck.Text))
                {
                    allForSubmitFilled = false;
                    break;
                }
            }

            Button submitBtn = flp.Controls["submitBTN_" + poradieSubmitTlacitka.ToString()] as Button;
            if (submitBtn != null)
            {
                submitBtn.Enabled = allForSubmitFilled;
            }
        }

        private void InitializeScorePanel()
        {
            scorePanel = new Panel();
            scorePanel.Location = new Point(flp.Right + 20, 30); 
            scorePanel.Size = new Size(150, flp.Height);
            scorePanel.BorderStyle = BorderStyle.FixedSingle;

            scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.AutoSize = true;

            scorePanel.Controls.Add(scoreLabel);
            this.Controls.Add(scorePanel);
        }

        private void UpdateScore(int points)
        {
            score += points;
            scoreLabel.Text = "Score: " + score.ToString();
        }

        private void RestartGame()
        {
            this.Controls.Clear();
            InitializeComponent();

            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            label1.Visible = true;
            labelresponseinput.Visible = true;
            button1.Visible = true;

            score = 0; 
        }
    }
}
