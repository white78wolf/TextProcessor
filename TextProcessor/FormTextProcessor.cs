﻿using System;
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
        string fontFamily = "Lucida Console";
        float fontSize = 10;
        public FormTextProcessor()
        {
            InitializeComponent();

            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openFileDialog.FileName = "";
            saveFileDialog.FileName = "";

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

            richTextBox.Text = File.ReadAllText(lastDocument, Encoding.UTF8);
            richTextBox.Font = new Font(fontFamily, fontSize);

            Text = lastDocument;

            comboBoxFontSize.SelectedItem = fontSize.ToString();
            comboBoxFontFamily.SelectedItem = fontFamily;
        }

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e)
        {            
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {                
                richTextBox.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                lastDocument = openFileDialog.FileName;
                Text = openFileDialog.FileName;
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
                Text = saveFileDialog.FileName;
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

        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox.Font = new Font(richTextBox.Font.Name, Convert.ToSingle(comboBoxFontSize.SelectedItem));
        }

        private void ComboBoxFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox.Font = new Font(Convert.ToString(comboBoxFontFamily.SelectedItem), richTextBox.Font.Size);
        }

        private void FormTextProcessor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Сохранить изменения?", "Уведомление", 
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
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
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }        
    }    
}
