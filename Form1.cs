﻿namespace WinFormsApp1
{
    //todo vizual aj uvodnej stranky,dat kod z designera sem, potencialne nejaka animacia


public partial class Form1 : Form
{
        // The word to be guessed in the game
        string word = "kitty";

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
        private Size buttonSize = new Size(40, 40);

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

            // Set the default number of letters in a word based on the numericUpDown2 control's current value.
            NumberOfWordLetters = (int)numericUpDown2.Value;

            // Set the default number of attempts based on the numericUpDown1 control's current value.
            NumberOfAttempts = (int)numericUpDown1.Value;

            // Populate the letter count dictionary with the word from the game.
            // This line will be removed later; it is here for initial testing or setup purposes.
            FillDictionary(word); // This will be removed later
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
        }


        /// <summary>
        /// Event handler for the ValueChanged event of numericUpDown2.
        /// Updates the NumberOfWordLetters variable based on the selected value from the numeric up-down control.
        /// This determines the length of the word to be guessed in the game.
        /// </summary>
        /// <param name="sender">The source of the event, typically the numericUpDown control.</param>
        /// <param name="e">Event arguments that contain the event data.</param>
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            // Update the NumberOfWordLetters to the currently selected value in numericUpDown2.
            NumberOfWordLetters = (int)numericUpDown2.Value;
        }

        /// <summary>
        /// Event handler for the ValueChanged event of numericUpDown1.
        /// Updates the NumberOfAttempts variable based on the selected value from the numeric up-down control.
        /// This determines how many attempts the player has to guess the word in the game.
        /// </summary>
        /// <param name="sender">The source of the event, typically the numericUpDown control.</param>
        /// <param name="e">Event arguments that contain the event data.</param>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Update the NumberOfAttempts to the currently selected value in numericUpDown1.
            NumberOfAttempts = (int)numericUpDown1.Value;
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
        /// Initializes a flow layout panel containing text boxes for user input and submit buttons
        /// for each attempt in the guessing game.
        /// </summary>
        /// <param name="attempts">The number of attempts allowed for the player to guess the word.</param>
        /// <param name="wordLength">The length of the word that the player is trying to guess.</param>
        private void CreateTextBoxes(int attempts, int wordLength)
        {
            // Initialize the flowLayoutPanel to hold text boxes and buttons for user input.
            flowLayoutPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight, // Arrange controls horizontally.
                BorderStyle = BorderStyle.None, // No border for the panel.
                Size = new Size((wordLength + 1) * (textBoxSize.Width + spaceBetweenTextBoxes.Width), // Calculate total width based on word length and spacing.
                                (attempts + 1) * (textBoxSize.Height + spaceBetweenTextBoxes.Height)), // Calculate total height based on number of attempts and spacing.
                Location = new Point(30, 30) // Position the panel within the form.
            };
            this.Controls.Add(flowLayoutPanel); // Add the flow layout panel to the form.

            // Initialize the virtual keyboard for user input.
            InitializeKeyboard();

            // Adjust the layout of additional panels, if necessary.
            PlacePanels();

            // Initialize the score display panel.
            InitializeScorePanel();

            // Set the form size based on the largest panel (keyboard or flow layout) plus additional padding for score panel and layout.
            this.Width = (keyboardPanel.Width > flowLayoutPanel.Width ? keyboardPanel.Width : flowLayoutPanel.Width) + 150 + scorePanel.Width;
            this.Height = flowLayoutPanel.Height + 120 + keyboardPanel.Height; // Adjust height based on flow layout and keyboard.

            // Create text boxes for each attempt.
            for (int j = 0; j < attempts; j++)
            {
                // Loop to create text boxes for each character of the word in the current attempt.
                for (int i = j * wordLength; i < j * wordLength + wordLength; i++)
                {
                    TextBox textBox = new TextBox
                    {
                        Name = "textBox_" + i.ToString(), // Unique name for each text box based on its position.
                        Size = textBoxSize, // Set size of the text box.
                        BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle, // Single border style.
                        MaxLength = 1, // Limit input to one character.
                        Font = new Font("Arial", 20, FontStyle.Bold), // Set font for the text box.
                        TextAlign = HorizontalAlignment.Center // Center-align the text within the text box.
                    };

                    // Make all text boxes read-only except the first one in each row.
                    if (i != 0)
                    {
                        textBox.ReadOnly = true; // Only the first text box in the row is editable.
                    }

                    // Attach an event handler to monitor text changes in the text box.
                    textBox.TextChanged += new EventHandler(textBox_TextChanged);

                    // Add the text box to the flow layout panel.
                    flowLayoutPanel.Controls.Add(textBox);
                }

                // Create and configure the submit button for the current attempt.
                Button submitButton = new Button
                {
                    Name = "submitBTN_" + j.ToString(), // Unique name for the submit button.
                    Size = textBoxSize, // Set the size of the submit button.
                    BackColor = Color.LightGreen, // Set the button's background color.
                    Font = new Font("Arial", 12, FontStyle.Bold), // Set font properties for the button.
                    Text = "OK", // Button text.
                    Enabled = false // Initially disabled until input is valid.
                };

                // Attach click event to handle submission of the text boxes when the button is clicked.
                submitButton.Click += new System.EventHandler(submitButton_Click);

                // Add the submit button to the flow layout panel.
                flowLayoutPanel.Controls.Add(submitButton);

                // Set focus to the first text box to allow immediate input from the user.
                flowLayoutPanel.Controls["textBox_0"].Focus();
            }
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
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            label1.Visible = false;
            labelresponseinput.Visible = false;
            button1.Visible = false;
            CreateTextBoxes(NumberOfAttempts, NumberOfWordLetters); // Create input fields for the game.
        }



        /// <summary>
        /// Handles the click event for the submit button. This method processes the user's input,
        /// checks each letter against the target word, and updates the UI accordingly.
        /// 
        /// Steps:
        /// 1. Retrieves the index of the button that was clicked to determine which attempt is being made.
        /// 2. Iterates through the corresponding text boxes to compare user input with the target word.
        /// 3. Updates the text box colors based on the correctness of each letter:
        ///    - Green for correct letters in the right position.
        ///    - Gold for letters that are correct but in the wrong position.
        ///    - Gray for incorrect letters not in the word.
        /// 4. Updates the keyboard button colors to reflect the same status as the text boxes.
        /// 5. Keeps track of the overall correctness of the guess using the `allCorrect` flag.
        /// 6. If the guess is correct, prompts the user to play again; if all attempts are used, shows a different message.
        /// 7. Resets the game state if the user chooses to play again, or exits the application if they decline.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event data.</param>
        private void submitButton_Click(object sender, EventArgs e)
        {
            // Cast the sender to a Button to access its properties.
            Button button = sender as Button;

            // Extract the index from the button's name to determine which submission set it corresponds to.
            int submitButtonIndex = int.Parse(button.Name[10..]);
            int wordLength = NumberOfWordLetters; // The length of the target word.
            bool allCorrect = true; // Flag to track if all letters were guessed correctly.

            // Iterate through the TextBoxes corresponding to the current submission.
            for (int i = submitButtonIndex * wordLength; i < (submitButtonIndex + 1) * wordLength; i++)
            {
                // Find the TextBox for the current index.
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + i.ToString()] as TextBox;

                if (textBox != null)
                {
                    // Check if the player's input matches the corresponding character in the word.
                    if (textBox.Text[0] == word[i % wordLength])
                    {
                        // Correct guess: Update the TextBox appearance to indicate success.
                        textBox.BackColor = Color.Green;
                        textBox.ForeColor = Color.White;

                        // Update the corresponding keyboard button to reflect the correct guess.
                        keyboardButtons[textBox.Text[0]].BackColor = Color.Green;
                        keyboardButtons[textBox.Text[0]].ForeColor = Color.White;

                        // Update the player's score positively.
                        UpdateScore(2);
                    }
                    // Check if the guessed letter is in the word but not in the correct position.
                    else if (word.Contains(textBox.Text[0]))
                    {
                        allCorrect = false; // Set flag to false since not all letters are correct.
                        textBox.BackColor = Color.Gold; // Indicate a correct letter in the wrong position.
                        textBox.ForeColor = Color.White;

                        // Update the keyboard button to reflect the presence of the letter.
                        if (keyboardButtons[textBox.Text[0]].BackColor != Color.Green)
                        {
                            keyboardButtons[textBox.Text[0]].BackColor = Color.Gold;
                            keyboardButtons[textBox.Text[0]].ForeColor = Color.White;
                        }
                    }
                    // Incorrect guess: Update the TextBox and keyboard button to indicate failure.
                    else
                    {
                        allCorrect = false; // Not all letters are correct.
                        textBox.BackColor = Color.Gray; // Mark as incorrect.
                        textBox.ForeColor = Color.White;

                        keyboardButtons[textBox.Text[0]].BackColor = Color.Gray; // Update keyboard button.
                        keyboardButtons[textBox.Text[0]].ForeColor = Color.White;

                        // Deduct points from the player's score.
                        UpdateScore(-3);
                    }
                    // Set the TextBox to read-only to prevent further editing.
                    textBox.ReadOnly = true;
                }
            }

            // Enable the next TextBox for user input if there are attempts remaining.
            if (submitButtonIndex + 1 != NumberOfAttempts)
            {
                TextBox textBox = flowLayoutPanel.Controls["textBox_" + ((submitButtonIndex + 1) * wordLength).ToString()] as TextBox;
                textBox.ReadOnly = false; // Make the next TextBox editable.
                textBox.Focus(); // Move focus to the next TextBox.
            }

            // Disable the current submit button to prevent repeated submissions.
            button.Enabled = false;

            // Check if all letters were guessed correctly.
            if (allCorrect)
            {
                // If all guesses are correct, prompt the user to play again.
                DialogResult result = MessageBox.Show("Play again?", "The End", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                SaveHighScore(score); // Save the current score if it's a new high score.

                // Restart the game or exit based on user response.
                if (result == DialogResult.Yes)
                {
                    RestartGame();
                }
                else
                {
                    Application.Exit(); // Exit the application if user chooses not to play again.
                }
            }
            // If this was the last attempt, inform the player and prompt to play again.
            else if (submitButtonIndex + 1 == NumberOfAttempts)
            {
                if (!allCorrect)
                {
                    cheekyLabel.Text = "Better luck next time!"; // Provide feedback on failure.
                }

                // Ask the user if they want to play again.
                DialogResult result = MessageBox.Show("Play again?", "The End", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Restart or exit based on user choice.
                if (result == DialogResult.Yes)
                {
                    RestartGame();
                }
                else
                {
                    Application.Exit();
                }
            }
            // Provide a random cheeky label if the game is still ongoing and not all guesses are correct.
            else
            {
                Random random = new Random();
                cheekyLabel.Text = cheekyLabels[random.Next(0, cheekyLabels.Length)];
            }
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

            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            label1.Visible = true;
            labelresponseinput.Visible = true;
            button1.Visible = true;

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
        /// 1. It reads the selected word length from a numeric input (numericUpDown2).
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