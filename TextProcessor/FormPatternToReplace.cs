using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextProcessor
{
    public partial class FormPatternToReplace : Form
    {
        FormTextProcessor mainForm;
        public FormPatternToReplace()
        {
            InitializeComponent();
        }

        public FormPatternToReplace(FormTextProcessor mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }        

        private void ButtonReplace_Click(object sender, EventArgs e)
        {
            if (textPattern.Text == "")
                return;
            mainForm.richTextBox.Text = mainForm.richTextBox.Text.Replace(textPattern.Text, textReplace.Text);
        }

        private void TextPattern_Leave(object sender, EventArgs e)
        {
            string find = textPattern.Text;

            if (find == "")
                return;

            if (mainForm.richTextBox.Text.Contains(find))
            {
                int i = 0;
                while (i <= mainForm.richTextBox.Text.Length - find.Length)
                {
                    i = mainForm.richTextBox.Text.IndexOf(find, i);
                    if (i < 0) break;
                    mainForm.richTextBox.SelectionStart = i;
                    mainForm.richTextBox.SelectionLength = find.Length;
                    mainForm.richTextBox.SelectionBackColor = Color.LightBlue;
                    i += find.Length;
                }
            }
        }        
    }
}
