namespace WinFormsApp1
{
    //todo komentare, vizual aj uvodnej stranky,dat kod z designera sem, potencialne nejaka animacia
    
    
    public partial class Form1 : Form
    {
        string word = "kitty";
        private string alphabet = "qwertyuiopasdfghjklzxcvbnm";
        private FlowLayoutPanel flowLayoutPanel;
        private Panel scorePanel;
        private Label scoreLabel;
        private Label highestScoreLabel;
        private int score = 500;

        private Panel keyboardPanel;
        private Dictionary<char, Button> keyboardButtons;
        private Dictionary<char, int> letterCount = new Dictionary<char, int>();

        private int NumberOfWordLetters;
        private int NumberOfAttempts;

        private Size textBoxSize = new Size(50, 50);
        private Size buttonSize = new Size(40, 40);
        private Size spaceBetweenTextBoxes = new Size(8, 5);
        private Label cheekyLabel;
        private string scoreFilePath = "highscore.txt";
        private int highScore = 0;

       
        private string[] cheekyLabels = { 
            "Close, but no cigar!", 
            "Not everyone can be a winner... but still.",
            "Well, somebody had to lose!","It’s not the end of the world... just your streak.",
            "A+ for effort! Just kidding, it's a solid D.",
            "Winning isn’t everything. But it’s better than this.",
            "Good news: you're consistent! Bad news: at losing.",
            "That was... something. Not winning, but something.",
            "It's all about the fun, right? No? Oh.",
            "Well, that’s one way to not win!",
            "Maybe next time... or the time after that.",
            "Participation trophy? Nah, not even that.",
            "It’s the journey that counts. Except this one leads nowhere.",
            "You miss 100% of the shots you take... like, seriously.",
            "Better luck next time... and by next time, I mean a miracle.",
            "Don’t quit your day job... especially if it's not guessing.",
            "Almost had it! (Not really, but it’s nice to say.)",
            "Hey, at least you’re trying! That’s something, right?",
            "Winning isn’t everything. Losing, on the other hand, seems to be.",
            "It’s not about winners or losers, but... you lost.",
            "Close! Like, really, really far from it, but close!",
            "That guess was… bold. Wrong, but bold.",
            "Don't worry, even champions have bad days. Today is just one of yours.",
            "You gave it your best shot! Unfortunately, your best wasn't good enough.",
            "You win some, you lose some… and then there's this.",
            "Not bad. Not good either, but not bad!",
            "You tried! Which is more than can be said for that guess.",
            "Remember, it's just a game. A game you're currently losing.",
            "Well, someone’s gotta be last, right?",
            "Well, that’s one way to not win!",
            "Maybe next time... or the time after that.",
            "Participation trophy? Nah, not even that.",
            "It’s the journey that counts. Except this one leads nowhere.",
            "You miss 100% of the shots you take... like, seriously.",
            "Better luck next time... and by next time, I mean a miracle.",
            "Don’t quit your day job... especially if it's not guessing.",
            "Almost had it! (Not really, but it’s nice to say.)",
            "Hey, at least you’re trying! That’s something, right?",
            "Winning isn’t everything. Losing, on the other hand, seems to be.",
        };

        public Form1()
        {
            InitializeComponent();
            NumberOfWordLetters = (int)numericUpDown2.Value;
            NumberOfAttempts = (int)numericUpDown1.Value;
            FillDictionary(word); // This will be removed later
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHighScore();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            NumberOfWordLetters = (int)numericUpDown2.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            NumberOfAttempts = (int)numericUpDown1.Value;
        }

        private void PlacePanels()
        {
            if (keyboardPanel.Width > flowLayoutPanel.Width)
            {
                flowLayoutPanel.Location = new Point(keyboardPanel.Width / 2 + keyboardPanel.Location.X - flowLayoutPanel.Width / 2, 30);
            }
            else
            {
                keyboardPanel.Location = new Point(flowLayoutPanel.Width / 2 + flowLayoutPanel.Location.X - keyboardPanel.Width / 2 + 30, flowLayoutPanel.Bottom + 20);
            }
        }


        private void CreateTextBoxes(int attempts, int wordLength)
        {
            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.BorderStyle = BorderStyle.None;
            flowLayoutPanel.Size = new Size((wordLength + 1) * (textBoxSize.Width + spaceBetweenTextBoxes.Width), (attempts + 1) * (textBoxSize.Height + spaceBetweenTextBoxes.Height));
            flowLayoutPanel.Location = new Point(30, 30);
            this.Controls.Add(flowLayoutPanel);

            InitializeKeyboard();

            PlacePanels();

            InitializeScorePanel();
            this.Width = (keyboardPanel.Width > flowLayoutPanel.Width ? keyboardPanel.Width : flowLayoutPanel.Width) + 150 + scorePanel.Width;
            this.Height = flowLayoutPanel.Height + 120 + keyboardPanel.Height;

            for (int j = 0; j < attempts; j++)
            {
                for (int i = j * wordLength; i < j * wordLength + wordLength; i++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = "textBox_" + i.ToString();
                    textBox.Size = textBoxSize;
                    textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    textBox.MaxLength = 1;
                    textBox.Font = new Font("Arial", 20, FontStyle.Bold);
                    textBox.TextAlign = HorizontalAlignment.Center;

                    if (i != 0)
                    {
                        textBox.ReadOnly = true;
                    }

                    textBox.TextChanged += new EventHandler(textBox_TextChanged);

                    flowLayoutPanel.Controls.Add(textBox);
                }

                Button submitButton = new Button();
                submitButton.Name = "submitBTN_" + j.ToString();
                submitButton.Size = textBoxSize;
                submitButton.BackColor = Color.LightGreen;
                submitButton.Font = new Font("Arial", 12, FontStyle.Bold);
                submitButton.Text = "OK";
                submitButton.Click += new System.EventHandler(submitButton_Click);

                submitButton.Enabled = false;

                flowLayoutPanel.Controls.Add(submitButton);
                flowLayoutPanel.Controls["textBox_0"].Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            label1.Visible = false;
            labelresponseinput.Visible = false;
            button1.Visible = false;
            //word = GetWord();
            CreateTextBoxes(NumberOfAttempts, NumberOfWordLetters);
            //InitializeKeyboard();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int submitButtonIndex = int.Parse(button.Name[10..]);
            int wordLength = NumberOfWordLetters;
            bool allCorrect = true;
            int i = 0;
            for (i = submitButtonIndex * wordLength; i < (submitButtonIndex + 1) * wordLength; i++)
            {
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBox != null)
                {
                    if (textBox.Text[0] == word[i % wordLength])
                    {
                        textBox.BackColor = Color.Green;
                        textBox.ForeColor = Color.White;

                        keyboardButtons[textBox.Text[0]].BackColor = Color.Green;
                        keyboardButtons[textBox.Text[0]].ForeColor = Color.White;
                        UpdateScore(2);
                    }
                    else if (word.Contains(textBox.Text[0]))
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gold;
                        textBox.ForeColor = Color.White;
                        if (keyboardButtons[textBox.Text[0]].BackColor != Color.Green)
                        {
                            keyboardButtons[textBox.Text[0]].BackColor = Color.Gold;
                            keyboardButtons[textBox.Text[0]].ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gray;
                        textBox.ForeColor = Color.White;
                        keyboardButtons[textBox.Text[0]].BackColor = Color.Gray;
                        keyboardButtons[textBox.Text[0]].ForeColor = Color.White;

                        UpdateScore(-3);
                    }
                    textBox.ReadOnly = true;
                }
            }
            if (submitButtonIndex + 1 != NumberOfAttempts)
            {
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + ((submitButtonIndex + 1) * wordLength).ToString()] as TextBox;
                textBox.ReadOnly = false;
                textBox.Focus();
            }
            button.Enabled = false;

            if (allCorrect)
            {
                DialogResult result = MessageBox.Show("Play again?", "The End", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                SaveHighScore(score);
                if (result == DialogResult.Yes)
                {
                    RestartGame();
                }
                else
                {
                    Application.Exit();
                }
            }
            else if (submitButtonIndex + 1 == NumberOfAttempts)
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
            else
            {
                Random random = new Random();

                cheekyLabel.Text = cheekyLabels[random.Next(0, cheekyLabels.Length)];
            }
        }


        private void submittButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int submitButtonIndex = int.Parse(button.Name[10..]);
            int wordLength = NumberOfWordLetters;
            bool allCorrect = true;
            int i = 0;
            for (i = submitButtonIndex * wordLength; i < (submitButtonIndex + 1) * wordLength; i++)
            {
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBox != null)
                {
                    if (textBox.Text[0] == word[i % wordLength])
                    {
                        textBox.BackColor = Color.Green;
                        textBox.ForeColor = Color.White;
                        keyboardButtons[textBox.Text[0]].BackColor = Color.Green;
                        keyboardButtons[textBox.Text[0]].ForeColor = Color.White;
                        UpdateScore(2);
                    }
                    else if (word.Contains(textBox.Text[0]))
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gold;
                        textBox.ForeColor = Color.White;
                        if (keyboardButtons[textBox.Text[0]].BackColor != Color.Green)
                        {
                            keyboardButtons[textBox.Text[0]].BackColor = Color.Gold;
                            keyboardButtons[textBox.Text[0]].ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        allCorrect = false;
                        textBox.BackColor = Color.Gray;
                        textBox.ForeColor = Color.White;
                        keyboardButtons[textBox.Text[0]].BackColor = Color.Gray;
                        keyboardButtons[textBox.Text[0]].ForeColor = Color.White;
                        UpdateScore(-3);
                    }
                    textBox.ReadOnly = true;
                }
            }

            if (allCorrect || (submitButtonIndex + 1 == NumberOfAttempts))
            {
                if (!allCorrect)
                {
                    cheekyLabel.Text = "Better luck next time!";
                }

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
            else
            {
                Random random = new Random();

                cheekyLabel.Text = cheekyLabels[random.Next(0, cheekyLabels.Length)];
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
            if (!alphabet.Contains(currentTextBox.Text))
            {
                currentTextBox.Text = "";
                return;
            }

            int nextIndex = currentIndex + 1;
            if (nextIndex % NumberOfWordLetters == 0)
            {
                // Do nothing
            }
            else if (nextIndex < flowLayoutPanel.Controls.Count && flowLayoutPanel.Controls["textBox_" + nextIndex.ToString()] is TextBox nextTextBox)
            {
                nextTextBox.ReadOnly = false;
                nextTextBox.Focus();
            }

            int submitButtonIndex = currentIndex / NumberOfWordLetters;

            bool allForSubmitFilled = true;
            for (int i = submitButtonIndex * NumberOfWordLetters; i < (submitButtonIndex + 1) * NumberOfWordLetters; i++)
            {
                TextBox textBoxToCheck = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBoxToCheck != null && string.IsNullOrEmpty(textBoxToCheck.Text))
                {
                    allForSubmitFilled = false;
                    break;
                }
            }

            Button submitBtn = flowLayoutPanel.Controls["submitBTN_" + submitButtonIndex.ToString()] as Button;
            if (submitBtn != null)
            {
                submitBtn.Enabled = allForSubmitFilled;
            }
        }

        private void InitializeScorePanel()
        {
            scorePanel = new Panel();
            scorePanel.Location = new Point((keyboardPanel.Width > flowLayoutPanel.Width ? keyboardPanel.Right : flowLayoutPanel.Right) + 20, 30);
            scorePanel.Size = new Size(180, flowLayoutPanel.Height + keyboardPanel.Height);
            scorePanel.BorderStyle = BorderStyle.FixedSingle;

            scoreLabel = new Label();
            scoreLabel.Text = "Score: " + score.ToString();
            scoreLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.AutoSize = true;

            scorePanel.Controls.Add(scoreLabel);


            highestScoreLabel = new Label();
            highestScoreLabel.Text = "Highest score: " + highScore.ToString();
            highestScoreLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            highestScoreLabel.Location = new Point(10, scoreLabel.Bottom + 20);
            highestScoreLabel.AutoSize = true;
            scorePanel.Controls.Add(this.highestScoreLabel);

            Button newGameButton = new Button();
            newGameButton.Text = "New Game";
            newGameButton.Size = new Size(150, 40);
            newGameButton.Location = new Point(15, highestScoreLabel.Bottom + 20);
            newGameButton.BackColor = Color.LightSkyBlue;
            newGameButton.Font = new Font("Arial", 12, FontStyle.Bold);

            newGameButton.Click += new EventHandler(NewGameButton_Click);
            scorePanel.Controls.Add(newGameButton);

            cheekyLabel = new Label();
            cheekyLabel.Text = ""; 
            cheekyLabel.Font = new Font("Arial", 12, FontStyle.Italic);
            cheekyLabel.ForeColor = Color.DarkRed;
            cheekyLabel.AutoSize = false; 

            cheekyLabel.Size = new Size(150, 150); 
            cheekyLabel.Location = new Point(15, newGameButton.Bottom + 20); 

            scorePanel.Controls.Add(cheekyLabel);

            this.Controls.Add(scorePanel);
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
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

            score = 500;
        }

        private void InitializeKeyboard()
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            keyboardPanel = new Panel();
            keyboardPanel.Location = new Point(30, flowLayoutPanel.Bottom + 20);
            keyboardPanel.Size = new Size(470, 150);
            this.Controls.Add(keyboardPanel);

            keyboardButtons = new Dictionary<char, Button>();

            int x = 10, y = 10;
            int i = 1;
            foreach (char letter in alphabet)
            {
                Button letterButton = new Button();
                letterButton.Text = letter.ToString();
                letterButton.Size = buttonSize;
                letterButton.Location = new Point(x, y);
                letterButton.Font = new Font("Arial", 14, FontStyle.Bold);

                letterButton.Click += (s, e) => Keyboard_Click(letterButton);

                keyboardPanel.Controls.Add(letterButton);
                keyboardButtons[letter] = letterButton;

                x += 45;
                if (x + 45 > keyboardPanel.Width)
                {
                    x = 10 + i * 20;
                    y += 45;
                    i++;
                }
            }
            Button backspaceButton = new Button();

            backspaceButton.Text = "←";
            backspaceButton.ForeColor = Color.Black;
            backspaceButton.Location = new Point(x, y);
            backspaceButton.Height = 40;
            backspaceButton.Font = new Font("Arial", 14, FontStyle.Bold);
            keyboardPanel.Controls.Add(backspaceButton);

            backspaceButton.Click += new System.EventHandler(Backspace_Click);
        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            for (int i = flowLayoutPanel.Controls.Count - 1; i >= 0; i--)
            {
                if (flowLayoutPanel.Controls[i] is TextBox textBox && textBox.ReadOnly == false)
                {
                    int textBoxIndex = int.Parse(textBox.Name.Split('_')[1]);

                    if (textBoxIndex % NumberOfWordLetters == 0)
                    {
                        textBox.Text = string.Empty;
                        textBox.Focus();
                        break;
                    }

                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.ReadOnly = true;
                        continue;
                    }

                    textBox.Text = string.Empty;
                    textBox.Focus();
                    break;
                }
            }
        }


        private void Keyboard_Click(Button clickedButton)
        {
            string letter = clickedButton.Text;

            foreach (Control control in flowLayoutPanel.Controls)
            {
                if (control is TextBox textBox && !textBox.ReadOnly && string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = letter;
                    break;
                }
            }
        }

        private void FillDictionary(string word)
        {
            foreach (char ch in word)
            {
                if (letterCount.ContainsKey(ch))
                {
                    letterCount[ch]++;
                }
                else
                {
                    letterCount.Add(ch, 1);
                }
            }
        }

        private string GetWord()
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
            string word = words[indexOfRandomNumber];

            FillDictionary(word);

            return word;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (Control control in flowLayoutPanel.Controls)
                {
                    if (control is Button submitBtn && submitBtn.Enabled)
                    {
                        submitBtn.PerformClick();
                        break;
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Back)
            {
                for (int i = flowLayoutPanel.Controls.Count - 1; i >= 0; i--)
                {
                    if (flowLayoutPanel.Controls[i] is TextBox textBox && textBox.ReadOnly == false)
                    {
                        int textBoxIndex = int.Parse(textBox.Name.Split('_')[1]);

                        if (textBoxIndex % NumberOfWordLetters == 0)
                        {
                            textBox.Text = string.Empty;
                            textBox.Focus();
                            break;
                        }

                        if (string.IsNullOrEmpty(textBox.Text))
                        {
                            textBox.ReadOnly = true;
                            continue;
                        }

                        textBox.Text = string.Empty;
                        textBox.Focus();
                        break;
                    }
                }
            }
        }
        private void LoadHighScore()
        {
            if (File.Exists(scoreFilePath))
            {
                string scoreText = File.ReadAllText(scoreFilePath);
                if (int.TryParse(scoreText, out int score))
                {
                    highScore = score;
                }
            }
        }

        private void SaveHighScore(int score)
        {
            if (score > highScore)
            {
                highScore = score;
                File.WriteAllText(scoreFilePath, highScore.ToString());
            }
        }


    }
}