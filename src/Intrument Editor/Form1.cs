using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intrument_Editor
{
    public partial class IntrumentInterface : Form
    {
        //private static int originalSizeOfWindow = 107;
        private editor userInstrument = new StandardGuitar();
        public IntrumentInterface()
        {
            InitializeComponent();
            richTextBox1.AppendText(userInstrument.toFancyString());
            richTextBox3.AppendText("@");
        }

        private void Form1_Load(object sender, EventArgs e)        {

        }

        /**
         * Textbox where the user manipulates the tab
         */
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                if (radioButton1.Checked)
                {
                    userInstrument.setNewInstrumentTab(userInstrument.fromThisFancyStringToTab(richTextBox1.Text));
                    userInstrument.trim();
                    richTextBox1.Clear();
                    richTextBox1.AppendText(userInstrument.toFancyString());
                }
                else
                {
                    //userInstrument.setNewInstrumentTab();
                    userInstrument.moveNewNotesWithSpace(userInstrument.fromThisFancyStringToTab(richTextBox1.Text),richTextBox3.Text);
                    richTextBox1.Clear();
                    richTextBox1.AppendText(userInstrument.toFancyString());
                }
            }
            
        }

        /**
         * Adds a new line to thee tab
         */
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(richTextBox2.Text);
                userInstrument.addANewTabLine(number);
            } catch(System.FormatException)
            {
                userInstrument.addANewTabLine(0);
            }
            richTextBox1.Clear();
            richTextBox1.AppendText(userInstrument.toFancyString());
        }

        /**
         * Textbos that show position new line is being added
         */
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /**
         * Radio selected to show the user want to add notes
         */
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("#General: There must be a space between tab lines. If a new tab is" +
                " generated or you close the program, all progress is lost" +
                        "\n\n\n   Instructions:                                              " +
                        "\n\n- Insert: When Insert is selected, notes are shifted automatically, if there is " +
                        " a relevant note, it will be written in the next tab line. If there are no tab lines" +
                        ", a new one will be created for it" +
                        "\n\n- Shift: Will shift all notes is a tab line from the point of the marker given by the user" +
                        "\n\n- Add Line: Adds a tab line at a position given in the textbox" +
                        "\n\n- Remove Line: Removes a tab line at a position given in the textbox"+
                        "\n\n- Generate: Generates a blank tab of an instrument selected of a customly made instrument" +
                        "\n\n- Custom: Creates an instrument with notes divided by \"/\"");
        }

        /**
         * Term to start shift
         */
        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        /**
         * Position of the line to be removed
         */
        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(richTextBox4.Text);
                userInstrument.removeLine(number);
            }
            catch (System.FormatException)
            {
                userInstrument.removeLine(1);
            }
            richTextBox1.Clear();
            richTextBox1.AppendText(userInstrument.toFancyString());
        }

        /**
         * Radio selected to show that the user wanst to shift notes as they add new notes
         */
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings settingsWindows = new Settings();
            settingsWindows.Show();
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)//Generate button
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                if (radioButton3.Checked)//guitar
                {
                    userInstrument = new StandardGuitar();
                }
                else if (radioButton4.Checked)//Ukelele
                {
                    String[] tune = { "A", "E", "C", "G" };
                    userInstrument = new editor(4, tune);
                }
                else if (radioButton5.Checked)//Banjo
                {
                    String[] tune = { "d", "B", "g", "D", "G" };
                    userInstrument = new editor(5, tune);
                }
                else if (radioButton6.Checked)//Base
                {
                    String[] tune = { "E", "A", "D", "G" };
                    userInstrument = new editor(4, tune);
                }
                else//Custom
                {

                    String[] userText = textBox1.Text.Split('/');
                    String[] actualTuning = new String[userText.Length];
                    for (int i = userText.Length - 1; i >= 0; i--)
                    {
                        actualTuning[userText.Length - i - 1] = userText[i];
                    }
                    userInstrument = new editor(userText.Length, actualTuning);
                }
            }
            
            richTextBox1.Clear();
            richTextBox1.AppendText(userInstrument.toFancyString());
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//Guitar radio
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)//Ukelele radio
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)//Banjo radio
        {

        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)//Base radio
        {

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)//Custom radio
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//Custom instrument testbox
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Characters per line textbox
        {

        }

        private void label3_Click(object sender, EventArgs e)//"Settings"
        {

        }

        private void label4_Click(object sender, EventArgs e)//"Character per line"
        {

        }

        private void label2_Click(object sender, EventArgs e)//"1__________"
        {

        }

        private void label1_Click(object sender, EventArgs e)//"2_____________"
        {

        }

        
    }
}

