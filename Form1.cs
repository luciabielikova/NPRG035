namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        string slovo = "kitty";
        private FlowLayoutPanel flp;
        private Panel scorePanel;
        private Label scoreLabel;
        private int score = 500;


        private Panel klavesnicaPanel;
        private Dictionary<char, Button> klavesnicaPismena;



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
            flp.Size = new Size(dlzkaSlova * 50 + dlzkaSlova * 10 + 50, pocetPokusov * 55 + 25);
            this.Controls.Add(flp);

            InitializeScorePanel();
            InitializeKlavesnica();
            this.Size = new Size(flp.Width + scorePanel.Width + 100, flp.Height + 120 + klavesnicaPanel.Height);

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
                        textBox.ReadOnly = true;
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
                flp.Controls["textBox_0"].Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            label1.Visible = false;
            labelresponseinput.Visible = false;
            button1.Visible = false;
            //slovo = getWord();
            novePolicka((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            //InitializeKlavesnica();

        }

        private void submitBTN_Click(object sender, EventArgs e)
        {
            Button tlacitko = sender as Button;
            int poradieSubmitTlacitka = int.Parse(tlacitko.Name[10..]);
            int dlzkaSlova = (int)numericUpDown2.Value;
            bool allCorrect = true;
            int i = 0;
            for (i = poradieSubmitTlacitka * dlzkaSlova; i < (poradieSubmitTlacitka + 1) * dlzkaSlova; i++)
            {
                TextBox textBox = flp.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBox != null)
                {
                    if (textBox.Text[0] == slovo[i % dlzkaSlova])
                    {
                        textBox.BackColor = Color.Green;
                        textBox.ForeColor = Color.White;
                        klavesnicaPismena[textBox.Text[0]].BackColor = Color.Green;
                        klavesnicaPismena[textBox.Text[0]].ForeColor = Color.White;
                        UpdateScore(2);

                    }
                    else if (slovo.Contains(textBox.Text[0]))
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gold;
                        textBox.ForeColor = Color.White;
                        klavesnicaPismena[textBox.Text[0]].BackColor = Color.Gold;
                        klavesnicaPismena[textBox.Text[0]].ForeColor = Color.White;

                    }
                    else
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gray;
                        textBox.ForeColor = Color.White;
                        klavesnicaPismena[textBox.Text[0]].BackColor = Color.Gray;
                        klavesnicaPismena[textBox.Text[0]].ForeColor = Color.White;

                        UpdateScore(-3);

                    }
                    textBox.ReadOnly = true;
                }
            }
            if (poradieSubmitTlacitka + 1 != (int)numericUpDown1.Value)
            {
                TextBox textBox = flp.Controls["textBox_" + ((poradieSubmitTlacitka + 1) * dlzkaSlova).ToString()] as TextBox;
                textBox.ReadOnly = false;
                textBox.Focus();
            }
            tlacitko.Enabled = true;


            if (allCorrect || (poradieSubmitTlacitka + 1 == (int)numericUpDown1.Value))
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
            if (nextIndex % (int)numericUpDown2.Value == 0)
            {
                //nic nerob
            }
            else if (nextIndex < flp.Controls.Count && flp.Controls["textBox_" + nextIndex.ToString()] is TextBox nextTextBox)
            {
                nextTextBox.ReadOnly = false;
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
            scorePanel.Size = new Size(180, flp.Height);
            scorePanel.BorderStyle = BorderStyle.FixedSingle;

            scoreLabel = new Label();
            scoreLabel.Text = "Score: " + score.ToString();
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

        private void InitializeKlavesnica()
        {
            klavesnicaPanel = new Panel();
            klavesnicaPanel.Location = new Point(30, flp.Bottom + 20);
            klavesnicaPanel.Size = new Size(500, 150);
            this.Controls.Add(klavesnicaPanel);

            klavesnicaPismena = new Dictionary<char, Button>();
            string abeceda = "qwertyuiopasdfghjklzxcvbnm";

            int x = 10, y = 10;
            foreach (char pismeno in abeceda)
            {
                Button tlacitkoPismeno = new Button();
                tlacitkoPismeno.Text = pismeno.ToString();
                tlacitkoPismeno.Width = 40;
                tlacitkoPismeno.Height = 40;
                tlacitkoPismeno.Location = new Point(x, y);
                tlacitkoPismeno.Font = new Font("Arial", 14, FontStyle.Bold);
                klavesnicaPanel.Controls.Add(tlacitkoPismeno);

                klavesnicaPismena[pismeno] = tlacitkoPismeno;

                x += 45;
                if (x + 45 > klavesnicaPanel.Width)
                {
                    x = 10;
                    y += 45;
                }
            }
        }

        

        private string getWord()
        {
            string wordLength = numericUpDown2.Value.ToString();
            List<string> words = new List<string>();
            string filename = "words/" + wordLength + ".txt";
            using (StreamReader sr = new StreamReader(filename))
            {
                while (sr.Peek() >= 0)
                {
                    words.Add(sr.ReadLine());
                }
            }
            Random random = new Random();

            int indexOfRandomNumber = random.Next(1, words.Count);
            return words[indexOfRandomNumber];
        }
    }
}