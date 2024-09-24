//Wordle
//Lucia Bieliková, 3. ročník
//zimný semester 2023
//Programování v jazyku C#, NPRG035


using System.Drawing.Drawing2D;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // The word to be guessed in the game
        string word = "";

        // A string containing all alphabet letters for use in the virtual keyboard
        private string alphabet = "qwertyuiopasdfghjklzxcvbnm";

        // A panel for placing the word guess text boxes
        private FlowLayoutPanel flowLayoutPanel;

        // A panel for displaying score information
        private Panel scorePanel;

        // A label that displays the player's current score
        private Label scoreLabel;

        // A label that displays the highest score achieved by any player
        private Label highestScoreLabel;

        // The player's current score, initialized at 500
        private int score = 500;

        // A panel that contains the virtual keyboard
        private Panel keyboardPanel;

        // A dictionary to associate each alphabet letter with its corresponding button on the virtual keyboard
        private Dictionary<char, Button> keyboardButtons;

        // A dictionary that tracks how many times each letter appears in the word
        private Dictionary<char, int> letterCount = new Dictionary<char, int>();

        // The number of letters in the word to be guessed
        private int NumberOfWordLetters;

        // The number of attempts allowed to guess the word
        private int NumberOfAttempts;

        // Size of the text boxes (each representing a letter in the word)
        private Size textBoxSize = new Size(50, 50);

        // Size of the buttons in the virtual keyboard
        private Size buttonSize = new Size(45, 45);

        // The space between text boxes in the flow layout
        private Size spaceBetweenTextBoxes = new Size(8, 5);

        // A label that displays cheeky or humorous feedback after an incorrect guess
        private Label cheekyLabel;

        // Path to the file that stores the highest score
        private string scoreFilePath = "highscore.txt";

        // The highest score achieved, initially set to 0
        private int highScore = 0;


        // An array of cheeky feedback messages shown when a guess is incorrect
        private string[] cheekyLabels = {
            "Close, but no cigar!",
            "Not everyone can be a winner... but still.",
            "Well, somebody had to lose!",
            "It’s not the end of the world... just your streak.",
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

        /// <summary>
        /// Initializes a new instance of the Form1 class.
        /// This constructor sets up the initial state of the form, 
        /// including the default values for NumberOfWordLetters and NumberOfAttempts 
        /// based on the values from the numeric up-down controls. 
        /// It also calls the FillDictionary method to populate the letter count dictionary.
        /// Note: FillDictionary will be removed later as indicated.
        /// </summary>
        public Form1()
        {
            // Initializes the form's components (UI elements, settings, etc.)
            InitializeComponent();

            // Set the default number of letters in a word based on the wordLen control's current value.
            NumberOfWordLetters = (int)wordLen.Value;

            // Set the default number of attempts based on the attemptsNr control's current value.
            NumberOfAttempts = (int)attemptsNr.Value;

            // Populate the letter count dictionary with the word from the game.
            // This line will be removed later; it is here for initial testing or setup purposes.
            FillDictionary(word); // This will be removed later
        }

        /// <summary>
        /// Initializes the components for the form.
        /// This method manually creates all the controls for the form, sets their properties, and adds them to the form.
        /// It includes input fields for the number of tries and the length of the word, a button to start the game, and their respective labels.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelresponseinput = new System.Windows.Forms.Label();
            this.attemptsNr = new System.Windows.Forms.NumericUpDown();
            this.wordLen = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.attemptsNr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wordLen)).BeginInit();
            this.SuspendLayout();
            // 
            // labelresponseinput
            // 
            this.labelresponseinput.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelresponseinput.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.labelresponseinput.Location = new System.Drawing.Point(100, 212);
            this.labelresponseinput.Name = "labelresponseinput";
            this.labelresponseinput.Size = new System.Drawing.Size(255, 24);
            this.labelresponseinput.TabIndex = 1;
            this.labelresponseinput.Text = "Number of Attempts:";
            // 
            // attemptsNr
            // 
            this.attemptsNr.Location = new System.Drawing.Point(412, 212);
            this.attemptsNr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.attemptsNr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.attemptsNr.Name = "attemptsNr";
            this.attemptsNr.Size = new System.Drawing.Size(83, 27);
            this.attemptsNr.TabIndex = 2;
            this.attemptsNr.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.attemptsNr.ValueChanged += new System.EventHandler(this.attemptsNr_ValueChanged);
            // 
            // wordLen
            // 
            this.wordLen.Location = new System.Drawing.Point(412, 143);
            this.wordLen.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.wordLen.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.wordLen.Name = "wordLen";
            this.wordLen.Size = new System.Drawing.Size(83, 27);
            this.wordLen.TabIndex = 3;
            this.wordLen.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.wordLen.ValueChanged += new System.EventHandler(this.wordLen_ValueChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Location = new System.Drawing.Point(100, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Word Length:";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(280, 320);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(160, 45);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start Game!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startGame);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(205)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(688, 484);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wordLen);
            this.Controls.Add(this.attemptsNr);
            this.Controls.Add(this.labelresponseinput);
            this.Name = "Form1";
            this.Text = "Wordle";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.attemptsNr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wordLen)).EndInit();
            this.ResumeLayout(false);

        }





        /// <summary>
        /// Event handler for the Load event of the Form1.
        /// This method is called when the form is first loaded. 
        /// It loads the high score from the score file to display 
        /// the highest score achieved in previous games.
        /// </summary>
        /// <param name="sender">The source of the event, typically the form itself.</param>
        /// <param name="e">Event arguments containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load the highest score from the persistent storage when the form loads.
            LoadHighScore();
            CustomizeStartButton();
            CustomizeLabels();
            CustomizeControls();

        }

        /// <summary>
        /// Customizes the appearance of a NumericUpDown control.
        /// </summary>
        /// <param name="numericUpDown">The NumericUpDown control to customize.</param>
        private void CustomizeNumericUpDown(NumericUpDown numericUpDown)
        {
            // Set background color for a soft and pleasant appearance
            numericUpDown.BackColor = Color.FromArgb(198, 225, 233);
            // Set font style and size for a cute, friendly look
            numericUpDown.Font = new Font("Comic Sans MS", 23, FontStyle.Bold);
            numericUpDown.ForeColor = Color.White; // Set text color to white for contrast

            // Remove the default border style for a more modern look
            numericUpDown.BorderStyle = BorderStyle.None;
            numericUpDown.Size = new Size(140, 100); // Set size for better user interaction
        }

        /// <summary>
        /// Customizes the appearance of the start button.
        /// </summary>
        private void CustomizeStartButton()
        {
            // Set the button text with a cute emoji to enhance visual appeal
            this.startButton.Text = "💖 Start";
            // Set a light pink background color for the button
            this.startButton.BackColor = Color.FromArgb(255, 182, 193);
            this.startButton.Font = new Font("Comic Sans MS", 14, FontStyle.Bold); // Use a playful font
            this.startButton.ForeColor = Color.White; // Set white text for contrast

            // Create rounded corners for the button using a custom region
            GraphicsPath buttonPath = new GraphicsPath();
            buttonPath.AddArc(0, 0, 20, 20, 180, 90); // Top-left corner
            buttonPath.AddArc(this.startButton.Width - 20, 0, 20, 20, 270, 90); // Top-right corner
            buttonPath.AddArc(this.startButton.Width - 20, this.startButton.Height - 20, 20, 20, 0, 90); // Bottom-right corner
            buttonPath.AddArc(0, this.startButton.Height - 20, 20, 20, 90, 90); // Bottom-left corner
            buttonPath.CloseFigure(); // Close the path to form a complete shape
            this.startButton.Region = new Region(buttonPath); // Apply the rounded region to the button

            // Add hover effect for better user interaction
            this.startButton.MouseEnter += (s, e) =>
            {
                this.startButton.BackColor = Color.FromArgb(255, 105, 180); // Change to hot pink on hover
            };

            this.startButton.MouseLeave += (s, e) =>
            {
                this.startButton.BackColor = Color.FromArgb(255, 182, 193); // Return to light pink when not hovered
            };
        }

        /// <summary>
        /// Customizes the appearance of a Label control.
        /// </summary>
        /// <param name="label">The Label control to customize.</param>
        private void CustomizeLabel(Label label)
        {
            // Automatically adjust the size of the label based on its content
            label.AutoSize = true;
            label.Font = new Font("Comic Sans MS", 16, FontStyle.Bold); // Use a playful font
            label.ForeColor = Color.White; // Set text color to white for good contrast
            label.Padding = new Padding(10); // Add padding for a softer appearance
            label.BorderStyle = BorderStyle.None; // Use no border for a clean look
            label.FlatStyle = FlatStyle.Flat; // Set flat style for a modern appearance
        }

        /// <summary>
        /// Customizes all the necessary controls for the application.
        /// </summary>
        private void CustomizeControls()
        {
            CustomizeNumericUpDown(attemptsNr); // Customize the attempts number input
            CustomizeNumericUpDown(wordLen); // Customize the word length input
        }

        /// <summary>
        /// Customizes the labels used in the application.
        /// </summary>
        private void CustomizeLabels()
        {
            CustomizeLabel(labelresponseinput); // Customize the label for the number of attempts
            CustomizeLabel(label1); // Customize the label for the word length
        }


        /// <summary>
        /// Event handler for the ValueChanged event of wordLen.
        /// Updates the NumberOfWordLetters variable based on the selected value from the numeric up-down control.
        /// This determines the length of the word to be guessed in the game.
        /// </summary>
        /// <param name="sender">The source of the event, typically the numericUpDown control.</param>
        /// <param name="e">Event arguments that contain the event data.</param>
        private void wordLen_ValueChanged(object sender, EventArgs e)
        {
            // Update the NumberOfWordLetters to the currently selected value in wordLen.
            NumberOfWordLetters = (int)wordLen.Value;
        }

        /// <summary>
        /// Event handler for the ValueChanged event of attemptsNr.
        /// Updates the NumberOfAttempts variable based on the selected value from the numeric up-down control.
        /// This determines how many attempts the player has to guess the word in the game.
        /// </summary>
        /// <param name="sender">The source of the event, typically the numericUpDown control.</param>
        /// <param name="e">Event arguments that contain the event data.</param>
        private void attemptsNr_ValueChanged(object sender, EventArgs e)
        {
            // Update the NumberOfAttempts to the currently selected value in attemptsNr.
            NumberOfAttempts = (int)attemptsNr.Value;
        }


        /// <summary>
        /// Adjusts the position of the keyboard panel and the flow layout panel based on their respective widths.
        /// This ensures that both panels are centered relative to each other within the form's layout.
        /// </summary>
        private void PlacePanels()
        {
            // Check if the keyboard panel's width is greater than the flow layout panel's width.
            if (keyboardPanel.Width > flowLayoutPanel.Width)
            {
                // Center the flow layout panel horizontally beneath the keyboard panel.
                flowLayoutPanel.Location = new Point(
                    keyboardPanel.Width / 2 + keyboardPanel.Location.X - flowLayoutPanel.Width / 2, // Calculate new X position for centering.
                    30 // Set a fixed Y position for the flow layout panel.
                );
            }
            else
            {
                // Center the keyboard panel horizontally beneath the flow layout panel.
                keyboardPanel.Location = new Point(
                    flowLayoutPanel.Width / 2 + flowLayoutPanel.Location.X - keyboardPanel.Width / 2 + 30, // Calculate new X position for centering with additional padding.
                    flowLayoutPanel.Bottom + 20 // Position the keyboard panel below the flow layout panel with some space.
                );
            }
        }

        /// <summary>
        /// Creates the necessary layout, text boxes, and submit buttons for the word guessing game.
        /// </summary>
        /// <param name="attempts">The number of attempts allowed for the player.</param>
        /// <param name="wordLength">The length of the word to be guessed.</param>
        private void CreateTextBoxes(int attempts, int wordLength)
        {
            InitializeLayout(wordLength, attempts);
            for (int j = 0; j < attempts; j++)
            {
                CreateAttemptTextBoxes(j, wordLength);  // Create TextBoxes for this attempt
                CreateSubmitButton(j);  // Create the submit button for this attempt
            }

            SetInitialFocus();  // Set focus on the first TextBox for user input
        }

        /// <summary>
        /// Initializes the layout of the flow layout panel and other UI elements like the keyboard and score panel.
        /// Sets the form size based on the largest panel.
        /// </summary>
        /// <param name="wordLength">The length of the word to be guessed.</param>
        /// <param name="attempts">The number of attempts allowed for the player.</param>
        private void InitializeLayout(int wordLength, int attempts)
        {
            // Initialize flowLayoutPanel
            flowLayoutPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                BorderStyle = BorderStyle.None,
                Size = new Size((wordLength + 1) * (textBoxSize.Width + spaceBetweenTextBoxes.Width),
                                (attempts + 1) * (textBoxSize.Height + spaceBetweenTextBoxes.Height)),
                Location = new Point(30, 30)
            };
            this.Controls.Add(flowLayoutPanel);

            InitializeKeyboard();  // Initialize the virtual keyboard panel
            PlacePanels();         // Adjust and place other panels if needed
            InitializeScorePanel(); // Initialize the score display panel

            // Set form size based on the larger panel between keyboard and flow layout
            this.Width = Math.Max(keyboardPanel.Width, flowLayoutPanel.Width) + 150 + scorePanel.Width;
            this.Height = flowLayoutPanel.Height + 120 + keyboardPanel.Height;
        }

        /// <summary>
        /// Creates a row of text boxes for a specific attempt.
        /// Each text box is initialized with its properties such as size, font, and event handlers.
        /// </summary>
        /// <param name="attemptIndex">The index of the current attempt (row) being created.</param>
        /// <param name="wordLength">The number of text boxes (word length) to be created for this attempt.</param>
        private void CreateAttemptTextBoxes(int attemptIndex, int wordLength)
        {
            for (int i = attemptIndex * wordLength; i < attemptIndex * wordLength + wordLength; i++)
            {
                TextBox textBox = new TextBox
                {
                    Name = "textBox_" + i.ToString(),
                    Size = textBoxSize,
                    BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    MaxLength = 1, // Single character input
                    Font = new Font("Comic Sans MS", 20, FontStyle.Bold),
                    TextAlign = HorizontalAlignment.Center
                };

                // Make all text boxes read-only except the first one in the first row
                if (i != 0)
                {
                    textBox.ReadOnly = true;
                }

                // Attach event handler for when the text changes in the box
                textBox.TextChanged += new EventHandler(textBox_TextChanged);

                // Add the text box to the panel
                flowLayoutPanel.Controls.Add(textBox);
            }
        }

        /// <summary>
        /// Creates a submit button for a specific attempt, setting its properties such as size, color, and event handler.
        /// </summary>
        /// <param name="attemptIndex">The index of the current attempt (row) being created.</param>
        private void CreateSubmitButton(int attemptIndex)
        {
            Button submitButton = new Button
            {
                Name = "submitBTN_" + attemptIndex.ToString(),
                Size = textBoxSize,
                BackColor = Color.LightGreen,
                Font = new Font("Comic Sans MS", 12, FontStyle.Bold),
                Text = "OK",
                Enabled = false // Button is disabled initially until text input is valid
            };

            // Attach click event to handle submission logic
            submitButton.Click += new System.EventHandler(submitButton_Click);

            // Add the submit button to the flow layout panel
            flowLayoutPanel.Controls.Add(submitButton);
        }

        /// <summary>
        /// Sets the initial focus on the first text box in the flow layout for immediate user input.
        /// </summary>
        private void SetInitialFocus()
        {
            // Set focus on the first text box to allow immediate input from the user
            flowLayoutPanel.Controls["textBox_0"].Focus();
        }


        /// <summary>
        /// Initiates the game by hiding the game settings input controls and creating 
        /// the necessary TextBoxes for the player to input their guesses.
        /// </summary>
        /// <param name="sender">The source of the event, typically the button that was clicked.</param>
        /// <param name="e">Event arguments containing data related to the event.</param>
        private void startGame(object sender, EventArgs e)
        {
            // Hide the input controls for game settings.
            word = GetWord();
            attemptsNr.Visible = false;
            wordLen.Visible = false;
            label1.Visible = false;
            labelresponseinput.Visible = false;
            startButton.Visible = false;
            CreateTextBoxes(NumberOfAttempts, NumberOfWordLetters); // Create input fields for the game.
        }



        /// <summary>
        /// Handles the click event for the submit button.
        /// Extracts the word from the TextBoxes, validates it, processes the submission, and checks if the game has ended.
        /// </summary>
        /// <param name="sender">The button that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void submitButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int submitButtonIndex = int.Parse(button.Name[10..]);
            string wordToCheck = ExtractWord(submitButtonIndex);

            if (IsValidWord(wordToCheck))
            {
                HandleSubmitAttempt(submitButtonIndex, wordToCheck, button);
            }
            else
            {
                cheekyLabel.Text = "That word is a little questionable. How about giving it another shot with a valid one?";
                return;
            }

            CheckGameEnd(submitButtonIndex, button);
        }

        /// <summary>
        /// Extracts the word guessed by the player based on the current submission index.
        /// </summary>
        /// <param name="submitButtonIndex">The index of the submit button pressed, determining which word to extract.</param>
        /// <returns>The guessed word as a string.</returns>
        private string ExtractWord(int submitButtonIndex)
        {
            string wordToCheck = "";
            for (int i = submitButtonIndex * NumberOfWordLetters; i < (submitButtonIndex + 1) * NumberOfWordLetters; i++)
            {
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;
                wordToCheck += textBox.Text[0];
            }
            return wordToCheck;
        }

        /// <summary>
        /// Validates if the word entered by the player is part of the game's accepted word list.
        /// </summary>
        /// <param name="wordToCheck">The word to validate.</param>
        /// <returns>True if the word is valid, otherwise false.</returns>
        private bool IsValidWord(string wordToCheck)
        {
            return checkValidWord(wordToCheck);
        }

        /// <summary>
        /// Handles the logic for processing the player's submission, including updating TextBox colors,
        /// keyboard button states, and adjusting the score. It also sets the next TextBox active, if applicable.
        /// </summary>
        /// <param name="submitButtonIndex">The index of the submission being processed.</param>
        /// <param name="wordToCheck">The word guessed by the player.</param>
        /// <param name="button">The submit button that was clicked.</param>
        private void HandleSubmitAttempt(int submitButtonIndex, string wordToCheck, Button button)
        {
            bool allCorrect = true;

            for (int i = submitButtonIndex * NumberOfWordLetters; i < (submitButtonIndex + 1) * NumberOfWordLetters; i++)
            {
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBox != null)
                {
                    UpdateTextBoxAppearance(textBox, i, ref allCorrect);
                }
            }

            if (submitButtonIndex + 1 != NumberOfAttempts)
            {
                TextBox nextTextBox = flowLayoutPanel.Controls["textBox_" + ((submitButtonIndex + 1) * NumberOfWordLetters).ToString()] as TextBox;
                nextTextBox.ReadOnly = false;
                nextTextBox.Focus();
            }

            button.Enabled = false;

            if (allCorrect)
            {
                ShowPlayAgainDialog(true);
            }
        }

        /// <summary>
        /// Updates the appearance of the TextBox and the corresponding keyboard button based on the player's guess.
        /// Changes the background color of the TextBox and keyboard button based on whether the guess was correct, partially correct, or incorrect.
        /// Also updates the player's score accordingly.
        /// </summary>
        /// <param name="textBox">The TextBox being updated.</param>
        /// <param name="index">The current index of the letter being evaluated in the word.</param>
        /// <param name="allCorrect">Reference to a boolean flag indicating if all guesses so far are correct.</param>
        private void UpdateTextBoxAppearance(TextBox textBox, int index, ref bool allCorrect)
        {
            char guessedLetter = textBox.Text[0];
            if (guessedLetter == word[index % NumberOfWordLetters])
            {
                textBox.BackColor = Color.Green;
                textBox.ForeColor = Color.White;
                keyboardButtons[guessedLetter].BackColor = Color.Green;
                keyboardButtons[guessedLetter].ForeColor = Color.White;
                UpdateScore(2);
            }
            else if (word.Contains(guessedLetter))
            {
                allCorrect = false;
                textBox.BackColor = Color.Gold;
                textBox.ForeColor = Color.White;
                if (keyboardButtons[guessedLetter].BackColor != Color.Green)
                {
                    keyboardButtons[guessedLetter].BackColor = Color.Gold;
                    keyboardButtons[guessedLetter].ForeColor = Color.White;
                }
            }
            else
            {
                allCorrect = false;
                textBox.BackColor = Color.Gray;
                textBox.ForeColor = Color.White;
                keyboardButtons[guessedLetter].BackColor = Color.Gray;
                keyboardButtons[guessedLetter].ForeColor = Color.White;
                UpdateScore(-3);
            }

            textBox.ReadOnly = true;
        }

        /// <summary>
        /// Checks if the game has ended, either because the player guessed the word correctly or because all attempts have been used.
        /// If the game is still ongoing, it provides feedback to the player.
        /// </summary>
        /// <param name="submitButtonIndex">The index of the current submission attempt.</param>
        /// <param name="button">The submit button that was clicked.</param>
        private void CheckGameEnd(int submitButtonIndex, Button button)
        {
            if (submitButtonIndex + 1 == NumberOfAttempts)
            {
                cheekyLabel.Text = "Better luck next time!";
                ShowPlayAgainDialog(false);
            }
            else
            {
                ProvideCheekyFeedback();
            }
        }

        /// <summary>
        /// Displays a dialog box prompting the player to either play again or exit the game.
        /// If the player chooses to play again, the game restarts. Otherwise, the application exits.
        /// </summary>
        /// <param name="isWin">Boolean flag indicating whether the player has won or lost the game.</param>
        private void ShowPlayAgainDialog(bool isWin)
        {
            DialogResult result = MessageBox.Show(isWin ? "Play again?" : "Try again?", "The End", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        /// <summary>
        /// Provides the player with a random "cheeky" feedback message from the `cheekyLabels` array.
        /// </summary>
        private void ProvideCheekyFeedback()
        {
            Random random = new Random();
            cheekyLabel.Text = cheekyLabels[random.Next(0, cheekyLabels.Length)];
        }

        /// <summary>
        /// Handles the TextChanged event for each TextBox in the game.
        /// 
        /// This method is triggered whenever the text in a TextBox changes. It performs 
        /// several key functions: 
        /// 1. Validates the input to ensure it contains an alphabet character from the 
        ///    predefined alphabet.
        /// 2. Moves focus to the next TextBox if the current one is filled.
        /// 3. Checks if all TextBoxes in the current submission set are filled, 
        ///    enabling or disabling the corresponding submit button accordingly.
        /// </summary>
        /// <param name="sender">The source of the event, typically the TextBox that triggered the event.</param>
        /// <param name="e">An event argument containing data related to the text change event.</param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            // Split the TextBox's name to determine its index.
            string[] nameParts = currentTextBox.Name.Split('_');
            int currentIndex = int.Parse(nameParts[1]);

            // If the current TextBox is empty, exit the method.
            if (string.IsNullOrEmpty(currentTextBox.Text))
            {
                return;
            }

            // Validate that the entered text is a single character from the alphabet.
            if (!alphabet.Contains(currentTextBox.Text))
            {
                currentTextBox.Text = ""; // Clear the TextBox if invalid.
                return;
            }

            int nextIndex = currentIndex + 1;

            // Move focus to the next TextBox if it exists and is not at the end of the line.
            if (nextIndex % NumberOfWordLetters == 0)
            {
                // Do nothing if at the end of a word.
            }
            else if (nextIndex < flowLayoutPanel.Controls.Count && flowLayoutPanel.Controls["textBox_" + nextIndex.ToString()] is TextBox nextTextBox)
            {
                nextTextBox.ReadOnly = false; // Make the next TextBox editable.
                nextTextBox.Focus(); // Set focus to the next TextBox.
            }

            // Determine the index of the submit button corresponding to the current TextBox.
            int submitButtonIndex = currentIndex / NumberOfWordLetters;

            // Check if all TextBoxes for the current submission are filled.
            bool allForSubmitFilled = true;
            for (int i = submitButtonIndex * NumberOfWordLetters; i < (submitButtonIndex + 1) * NumberOfWordLetters; i++)
            {
                TextBox textBoxToCheck = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;
                if (textBoxToCheck != null && string.IsNullOrEmpty(textBoxToCheck.Text))
                {
                    allForSubmitFilled = false; // Mark as not filled if any TextBox is empty.
                    break;
                }
            }

            // Enable or disable the submit button based on the filled status of TextBoxes.
            Button submitBtn = flowLayoutPanel.Controls["submitBTN_" + submitButtonIndex.ToString()] as Button;
            if (submitBtn != null)
            {
                submitBtn.Enabled = allForSubmitFilled; // Update submit button state.
            }
        }



        /// <summary>
        /// Initializes and configures the score panel that displays the player's current score,
        /// highest score, and includes a button to start a new game. The panel is positioned 
        /// dynamically based on the layout of other controls in the form.
        ///
        /// Steps:
        /// 1. Creates a new panel (`scorePanel`) to contain score information and related controls.
        /// 2. Sets the location and size of the panel based on the positions of the keyboard and flow layout panels.
        /// 3. Configures the appearance of the score label to show the current score, including font style and size.
        /// 4. Adds the score label to the score panel.
        /// 5. Creates and configures a label to display the highest score, setting its position relative to the score label.
        /// 6. Adds the highest score label to the score panel.
        /// 7. Creates a button for starting a new game, setting its size, location, and appearance.
        /// 8. Attaches an event handler to the button's click event to trigger the `NewGameButton_Click` method.
        /// 9. Adds the new game button to the score panel.
        /// 10. Initializes a cheeky label for displaying playful messages, configuring its appearance and size.
        /// 11. Adds the cheeky label to the score panel.
        /// 12. Finally, adds the score panel to the form's controls for visibility in the UI.
        /// </summary>
        private void InitializeScorePanel()
        {
            scorePanel = new Panel();
            scorePanel.Location = new Point((keyboardPanel.Width > flowLayoutPanel.Width ? keyboardPanel.Right : flowLayoutPanel.Right) + 20, 30);
            scorePanel.Size = new Size(180, flowLayoutPanel.Height + keyboardPanel.Height);
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            scorePanel.BackColor = Color.FromArgb(255, 182, 193);

            scoreLabel = new Label();
            scoreLabel.Text = "Score: " + score.ToString();
            scoreLabel.Font = new Font("Comic Sans MS", 16, FontStyle.Bold);
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.ForeColor = Color.White;

            scorePanel.Controls.Add(scoreLabel);


            highestScoreLabel = new Label();
            highestScoreLabel.Text = "Highest score: " + highScore.ToString();
            highestScoreLabel.Font = new Font("Comic Sans MS", 10, FontStyle.Bold);
            highestScoreLabel.Location = new Point(10, scoreLabel.Bottom + 20);
            highestScoreLabel.AutoSize = true;
            highestScoreLabel.ForeColor = Color.White;

            scorePanel.Controls.Add(this.highestScoreLabel);

            Button newGameButton = new Button();
            newGameButton.Text = "New Game";
            newGameButton.Size = new Size(150, 40);
            newGameButton.Location = new Point(15, highestScoreLabel.Bottom + 20);
            newGameButton.BackColor = Color.FromArgb(198, 225, 233);
            newGameButton.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);

            newGameButton.Click += new EventHandler(NewGameButton_Click);
            scorePanel.Controls.Add(newGameButton);

            cheekyLabel = new Label();
            cheekyLabel.Text = "";
            cheekyLabel.Font = new Font("Comic Sans MS", 12, FontStyle.Italic);
            cheekyLabel.ForeColor = Color.DarkRed;
            cheekyLabel.AutoSize = false;

            cheekyLabel.Size = new Size(150, 300);
            cheekyLabel.Location = new Point(15, newGameButton.Bottom + 20);
            cheekyLabel.TextAlign = ContentAlignment.MiddleCenter;
            scorePanel.Controls.Add(cheekyLabel);

            this.Controls.Add(scorePanel);
        }

        /// <summary>
        /// Handles the click event for the "New Game" button.
        /// 
        /// This method prompts the user with a confirmation dialog asking whether they 
        /// would like to start a new game. If the user confirms by selecting "Yes", 
        /// the game is restarted by calling the <see cref="RestartGame"/> method.
        /// </summary>
        /// <param name="sender">The source of the event, typically the "New Game" button.</param>
        /// <param name="e">An event argument containing data related to the button click event.</param>

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
        }

        /// <summary>
        /// Updates the player's score based on a specified change value.
        /// 
        /// This method adjusts the current score by the provided `change` parameter,
        /// which can be positive (for earning points) or negative (for losing points).
        /// After updating the score, it refreshes the displayed score on the score label.
        /// 
        /// If the updated score exceeds the current high score, it also updates the high score
        /// and refreshes the corresponding label to reflect the new highest score.
        /// </summary>
        /// <param name="change">The amount to change the score by. Can be positive or negative.</param>
        private void UpdateScore(int change)
        {
            score += change;
            scoreLabel.Text = "Score: " + score;

            if (score > highScore)
            {
                highScore = score;
                highestScoreLabel.Text = "Highest Score: " + highScore;
            }
        }



        /// <summary>
        /// Restarts the game by resetting the UI components and initializing the game state.
        /// 
        /// This method clears all existing controls from the main form, effectively resetting the interface.
        /// It then reinitializes the form components to their default state using the `InitializeComponent` method.
        /// 
        /// After reinitialization, it makes certain controls (such as numeric up-downs, labels, and buttons) visible again
        /// so the player can set the parameters for a new game session.
        /// 
        /// Additionally, the player's score is reset to the starting value of 500, allowing them to begin fresh.
        /// </summary>
        private void RestartGame()
        {
            this.Controls.Clear();
            InitializeComponent();
            CustomizeStartButton();
            LoadHighScore();
            CustomizeLabels();
            CustomizeControls();
            attemptsNr.Visible = true;
            wordLen.Visible = true;
            label1.Visible = true;
            labelresponseinput.Visible = true;
            startButton.Visible = true;

            score = 500;
        }

        /// <summary>
        /// Initializes and configures the on-screen keyboard for the game, allowing users to interact
        /// with letters and perform actions like backspacing. This method sets up the keyboard panel,
        /// creates buttons for each letter in the alphabet, and arranges them in a user-friendly layout.
        ///
        /// Steps:
        /// 1. Enables key preview for the form to capture keyboard events.
        /// 2. Subscribes to the `KeyDown` event to handle key presses via `Form1_KeyDown`.
        /// 3. Creates a new panel (`keyboardPanel`) for displaying the keyboard, setting its location and size.
        /// 4. Adds the keyboard panel to the form's controls for visibility.
        /// 5. Initializes a dictionary (`keyboardButtons`) to map characters to their respective buttons.
        /// 6. Sets initial x and y coordinates for button placement, and an index `i` for row management.
        /// 7. Iterates through each letter in the `alphabet` string to create a button for each letter:
        ///    - Configures the button properties, including text, size, location, and font.
        ///    - Attaches a click event handler to each button to call `Keyboard_Click` with the button reference.
        ///    - Adds the button to the keyboard panel and updates the dictionary to link the letter with its button.
        ///    - Adjusts the position of buttons to wrap to the next row if the current row exceeds the panel width.
        /// 8. Creates a backspace button, configuring its text, location, height, and font.
        /// 9. Adds the backspace button to the keyboard panel.
        /// 10. Attaches an event handler to the backspace button's click event to call `Backspace_Click`.
        /// </summary>
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
                letterButton.Font = new Font("Comic Sans MS", 14, FontStyle.Bold);
                letterButton.Padding = new Padding(0, 0, 0, 0);
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
            backspaceButton.Height = buttonSize.Height;
            backspaceButton.Font = new Font("Comic Sans MS", 14, FontStyle.Bold);
            keyboardPanel.Controls.Add(backspaceButton);

            backspaceButton.Click += new System.EventHandler(Backspace_Click);
        }

        /// <summary>
        /// Handles the click event for the backspace button. This method clears the text in the most 
        /// recently used editable text box within the flow layout panel, simulating a backspace action.
        /// 
        /// Steps:
        /// 1. Iterates through the controls in the `flowLayoutPanel` in reverse order to find the most 
        ///    recent editable TextBox.
        /// 2. Checks if the current control is a TextBox and if it is not read-only.
        /// 3. Parses the TextBox's index from its name to determine its position in the sequence.
        /// 4. If the TextBox is at the start of a new word (index is a multiple of `NumberOfWordLetters`),
        ///    clears its text and sets focus to it, then exits the loop to prevent further action.
        /// 5. If the TextBox is empty, sets it to read-only and continues to the next TextBox.
        /// 6. If the TextBox has text, clears its content, sets focus to it, and exits the loop.
        /// </summary>
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

        /// <summary>
        /// Handles the click event for the keyboard buttons. This method assigns the letter from the 
        /// clicked button to the first editable text box in the flow layout panel that is currently empty.
        /// 
        /// Steps:
        /// 1. Retrieves the letter from the text of the clicked button.
        /// 2. Iterates through each control in the `flowLayoutPanel` to find an editable TextBox.
        /// 3. Checks if the control is a TextBox, is not read-only, and is currently empty.
        /// 4. If such a TextBox is found, assigns the letter to its text property.
        /// 5. The loop is exited after assigning the letter to ensure that only the first empty TextBox 
        ///    is updated, preventing overwriting any existing input.
        /// </summary>
        /// <param name="clickedButton">The button that was clicked, representing a letter.</param>
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

        /// <summary>
        /// Populates the `letterCount` dictionary with the frequency of each character in the provided word.
        /// 
        /// This method iterates over each character in the input word and updates the count in the `letterCount` dictionary:
        /// - If the character is already a key in the dictionary, its count is incremented by one.
        /// - If the character is not present in the dictionary, it is added with an initial count of one.
        /// 
        /// This allows for tracking how many times each letter appears in the word, which can be useful for game logic,
        /// such as checking user inputs against the correct word.
        /// </summary>
        /// <param name="word">The word from which to count letter frequencies.</param>
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


        /// <summary>
        /// Retrieves a random word of a specified length from a text file and populates the letter count dictionary.
        /// 
        /// The method works as follows:
        /// 1. It reads the selected word length from a numeric input (wordLen).
        /// 2. It constructs the filename based on the specified word length, expecting a text file containing words of that length.
        /// 3. It reads all lines from the specified text file and stores them in a list.
        /// 4. A random number is generated to select an index from the list of words.
        /// 5. The randomly selected word is then used to populate the `letterCount` dictionary using the `FillDictionary` method.
        /// 
        /// This method assumes that the text file is located in a "words" directory and that it contains at least one word.
        /// If the file does not exist or is empty, an exception may occur.
        /// </summary>
        /// <returns>A randomly selected word from the corresponding text file.</returns>
        private string GetWord()
        {
            string wordLength = wordLen.Value.ToString();
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


        /// <summary>
        /// Checks if the given word is valid by comparing it against a list of words stored in a text file.
        /// </summary>
        /// <param name="wordToCheck">The word that needs to be validated.</param>
        /// <returns>
        /// Returns true if the word exists in the specified word list file; otherwise, returns false.
        /// </returns>
        private bool checkValidWord(string wordToCheck)
        {
            string wordLength = wordLen.Value.ToString();
            //List<string> words = new List<string>();
            string filename = "words/" + wordLength + ".txt";
            using (StreamReader sr = new StreamReader(filename))
            {
                while (sr.Peek() >= 0)
                {
                    if (sr.ReadLine() == wordToCheck) return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Handles key press events for the main form, allowing for interaction with the game using keyboard inputs.
        /// 
        /// This method captures two specific key presses:
        /// 1. **Enter Key**:
        ///    - When the Enter key is pressed, the method iterates through the controls in the `flowLayoutPanel`.
        ///    - It looks for the first enabled submit button and triggers its click event using `PerformClick()`.
        ///    - This simulates the user clicking the submit button, allowing for form submission without a mouse.
        ///    - The key event is marked as handled to prevent further processing of the Enter key.
        ///    
        /// 2. **Backspace Key**:
        ///    - When the Backspace key is pressed, the method iterates through the controls in reverse order (from last to first).
        ///    - It checks for any editable `TextBox` controls (not read-only).
        ///    - If the current `TextBox` is empty, it sets it to read-only and continues to the next control.
        ///    - If the `TextBox` is not empty, it clears its text and sets focus back to it.
        ///    - If the `TextBox` is at the start of a word (index is a multiple of `NumberOfWordLetters`), it clears its content before focusing on it.
        /// 
        /// This method enhances user experience by allowing keyboard interactions for submission and text deletion.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The key event arguments containing information about the key event.</param>
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

        /// <summary>
        /// Loads the highest score from a file when the game starts or resets.
        /// 
        /// This method checks if the score file exists. If it does, it reads the content of the file,
        /// attempts to parse it as an integer, and assigns it to the `highScore` variable if successful.
        /// 
        /// If the file does not exist or the content cannot be parsed, the `highScore` remains unchanged.
        /// </summary>
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

        /// <summary>
        /// Saves the current score as the highest score if it exceeds the previously recorded high score.
        /// 
        /// This method compares the given score with the existing `highScore`. If the given score is higher,
        /// it updates the `highScore` variable and writes the new high score to the score file, 
        /// ensuring the score persists across game sessions.
        /// 
        /// This helps maintain a competitive element by allowing players to track their best performances.
        /// </summary>
        /// <param name="score">The current score to be checked against the high score.</param>
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










