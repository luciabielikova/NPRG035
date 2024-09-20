namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        string slovo = "kitty";
        private FlowLayoutPanel flp;

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
            flp.Size = new Size(dlzkaSlova * 40 + dlzkaSlova * 7 + 45, pocetPokusov * 40);
            this.Controls.Add(flp);
            this.Size = new Size(flp.Width + 80, flp.Height + 100);

            for (int j = 0; j < pocetPokusov; j++)
            {
                for (int i = j * dlzkaSlova; i < j * dlzkaSlova + dlzkaSlova; i++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = "textBox_" + i.ToString();
                    textBox.Width = 40;
                    textBox.Height = 50;
                    textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    textBox.MaxLength = 1;

                    if (i != 0)
                    {
                        textBox.Enabled = false;
                    }

                    textBox.TextChanged += new EventHandler(textBox_TextChanged);

                    flp.Controls.Add(textBox);
                }
                Button submitBtn = new Button();
                submitBtn.Name = "submitBTN_" + j.ToString();
                submitBtn.Width = 40;
                submitBtn.Height = 30;
                submitBtn.BackColor = Color.LightGreen;
                submitBtn.Click += new System.EventHandler(submitBTN_Click);

                submitBtn.Enabled = false;

                flp.Controls.Add(submitBtn);
            }
        }


        //mozem kontrolovat s celou databazou slov? bude to dlho trvat


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
            //tu potrebujem vybrat tie textboxy pred nim aby som to mohla skontrolovat

         
            foreach (Control ctrl in flp.Controls)
            {
                if (ctrl is TextBox textBox)//nieco take ze aby som musela stlacat tie submit buttons poporade
                {
                    string[] nameParts = textBox.Name.Split('_');
                    int cislo = int.Parse(nameParts[1]);

                    if (cislo < (1 + poradieSubmitTlacitka) * (int)numericUpDown2.Value)
                    {
                        foreach (char c in slovo)
                        {
                            if (textBox.Text[0] == c) textBox.BackColor = Color.Yellow;
                        }
                        if (textBox.Text[0] == slovo[cislo % (int)numericUpDown2.Value])
                        {
                            textBox.BackColor = Color.LightSeaGreen;//nejaka jemnejsia farba
                        }
                        textBox.Enabled = false;
                    }
                    tlacitko.Enabled = false;
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



    }
}