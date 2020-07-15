using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TextProcessor
{
    public partial class FormTextProcessor : Form
    {
        static string regKeyName = "Software\\TextProcessor";
        static string defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string lastDocument = defaultPath + "\\Новый документ.txt";
        string fontFamily = "Lucida Console";
        float fontSize = 10;

        public FormTextProcessor()
        {
            InitializeComponent();

            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openFileDialog.FileName = "";
            saveFileDialog.FileName = "Новый документ";

            RegistryKey rk = null;            
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    if (rk.GetValue("LastDocument") != null)
                        lastDocument = (string)rk.GetValue("LastDocument");

                    if (rk.GetValue("LastDocumentFontSize") != null)
                        fontSize = Convert.ToSingle(rk.GetValue("LastDocumentFontSize"));

                    if (rk.GetValue("LastDocumentFontFamily") != null)
                        fontFamily = (string)rk.GetValue("LastDocumentFontFamily");                    
                }
            }
            finally
            {
                if (rk != null) rk.Close();
            }

            try
            {
                if (lastDocument != defaultPath + "\\Новый документ.txt")
                    richTextBox.Text = File.ReadAllText(lastDocument, Encoding.UTF8);
            }
            catch
            {
                MessageBox.Show("Ошибка чтения файла", "Ошибка!");
                richTextBox.Text = "";
            }

            richTextBox.Font = new Font(fontFamily, fontSize);            
            Text = Path.GetFileNameWithoutExtension(lastDocument);
            comboBoxFontSize.SelectedItem = fontSize.ToString();
            comboBoxFontFamily.SelectedItem = fontFamily;
        }

        // Menu items group "File"
        private void ToolStripMenuItemNewDocument_Click(object sender, EventArgs e)
        {
            lastDocument = defaultPath + "\\Новый документ.txt";
            richTextBox.Text = "";
            Text = "Новый документ";
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {            
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {                
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                lastDocument = openFileDialog.FileName;                
                Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
            }
            catch
            {
                _ = MessageBox.Show("Ошибка при открытии файла", "Ошибка!");
            }
        }
        private void ToolStripMenuItemInsertTimeStamp_Click(object sender, EventArgs e)
        {
            var selectionStart = richTextBox.SelectionStart;
            richTextBox.Text = richTextBox.Text.Remove(selectionStart, richTextBox.SelectionLength);
            richTextBox.SelectionStart = selectionStart;

            selectionStart = richTextBox.SelectionStart + DateTime.Now.ToString().Length;
            richTextBox.Text = richTextBox.Text.Insert(richTextBox.SelectionStart, DateTime.Now.ToString());
            richTextBox.SelectionStart = selectionStart;
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            if (lastDocument == defaultPath + "\\Новый документ.txt")
            {
                if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    return;
                lastDocument = saveFileDialog.FileName;
                Text = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
            }

            try
            {
                File.WriteAllText(lastDocument, richTextBox.Text, Encoding.UTF8);
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
                richTextBox.Text = File.ReadAllText(saveFileDialog.FileName, Encoding.UTF8);
                Text = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                lastDocument = saveFileDialog.FileName;
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

        // Menu item "Find and Replace"
        private void ToolStripMenuItem_ClickPatternToReplace(object sender, EventArgs e)
        {
            FormPatternToReplace formToReplace = new FormPatternToReplace(this);
            formToReplace.Show();
        }

        // Menu item "About"
        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            _ = MessageBox.Show("Программа предназначена\nдля изучения основ работы с Windows Forms.\n" +
                "Участник клуба Формулистов\nЦепков А. М. © 2020 год.", "О программе:");
        }

        // Control "Set font size"
        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox.Font = new Font(richTextBox.Font.Name, Convert.ToSingle(comboBoxFontSize.SelectedItem));
        }

        // Control "Set font family"
        private void ComboBoxFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox.Font = new Font(Convert.ToString(comboBoxFontFamily.SelectedItem), richTextBox.Font.Size);
        }

        // Windows' closing button
        private void FormTextProcessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox.Modified)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Уведомление",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }

                    lastDocument = saveFileDialog.FileName;
                    File.WriteAllText(saveFileDialog.FileName, richTextBox.Text, Encoding.UTF8);
                }
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }

            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null) return;                

                rk.SetValue("LastDocument", lastDocument);
                rk.SetValue("LastDocumentFontSize", richTextBox.Font.Size);
                rk.SetValue("LastDocumentFontFamily", richTextBox.Font.FontFamily.Name);
            }
            finally
            {
                if (rk != null) rk.Close();
            }            
        }        
    }    
}
