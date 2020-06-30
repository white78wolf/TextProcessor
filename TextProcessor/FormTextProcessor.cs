using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TextProcessor
{
    public partial class FormTextProcessor : Form
    {
        public FormTextProcessor()
        {
            InitializeComponent();

            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openFileDialog.FileName = "";
            saveFileDialog.FileName = "";
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {            
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
            }
            catch
            {
                _ = MessageBox.Show("Ошибка при открытии файла", "Ошибка!");
            }
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.FileName == "" || openFileDialog.FileName == null)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                        return;
                    File.WriteAllText(saveFileDialog.FileName, richTextBox.Text, Encoding.UTF8);
                }                    
                else
                    File.WriteAllText(openFileDialog.FileName, richTextBox.Text, Encoding.UTF8);

                MessageBox.Show("Файл сохранён");
            }
            catch
            {
                _ = MessageBox.Show("Ошибка при сохранении файла", "Ошибка!");
            }
        }

        private void ToolStripMenuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                File.WriteAllText(saveFileDialog.FileName, richTextBox.Text, Encoding.UTF8);
                MessageBox.Show("Файл сохранён");
            }
            catch
            {
                _ = MessageBox.Show("Ошибка при сохранении файла", "Ошибка!");
            }
        }        

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripMenuItemInsertTimeStamp_Click(object sender, EventArgs e)
        {
            var selectionStart = richTextBox.SelectionStart + DateTime.Now.ToString().Length;
            richTextBox.Text = richTextBox.Text.Insert(richTextBox.SelectionStart, DateTime.Now.ToString());
            richTextBox.SelectionStart = selectionStart;
        }

        private void ToolStripMenuItem_ClickPatternToReplace(object sender, EventArgs e)
        {
            FormPatternToReplace formToReplace = new FormPatternToReplace(this);
            formToReplace.Show();
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            _ = MessageBox.Show("Программа предназначена\nдля изучения основ работы с Windows Forms.\n" +
                "Участник клуба Формулистов\nЦепков А. М. © 2020 год.", "О программе:");
        }                
    }
}
