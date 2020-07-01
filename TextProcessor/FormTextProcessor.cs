using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace TextProcessor
{
    public partial class FormTextProcessor : Form
    {
        static string regKeyName = "Software\\TextProcessor";
        string lastDocument = "";        
        public FormTextProcessor()
        {
            InitializeComponent();

            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openFileDialog.FileName = "";
            saveFileDialog.FileName = "";

            RegistryKey rk = null;
            float fontSize;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    if (rk.GetValue("LastDocument") != null && rk.GetValue("LastDocumentFontSize") != null)
                    {
                        fontSize = Convert.ToSingle(rk.GetValue("LastDocumentFontSize"));
                        lastDocument = (string)rk.GetValue("LastDocument");
                        richTextBox.Text = File.ReadAllText(lastDocument, Encoding.UTF8);
                        comboBox.SelectedItem = (string)rk.GetValue("LastDocumentFontSize");
                        richTextBox.Font = new Font(richTextBox.Font.Name, fontSize);
                    }
                    else
                    {
                        lastDocument = "";
                        richTextBox.Font = new Font(richTextBox.Font.Name, 7);
                    }                   
                }
            }
            finally
            {
                if (rk != null) rk.Close();
            }            
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {            
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {                
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                lastDocument = openFileDialog.FileName;
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
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;

                rk.SetValue("LastDocument", lastDocument);
                rk.SetValue("LastDocumentFontSize", richTextBox.Font.Size);
            }
            finally
            {
                if (rk != null) rk.Close();
            }

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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox.Font = new Font(richTextBox.Font.Name, Convert.ToSingle(comboBox.SelectedItem));
        }
    }    
}
