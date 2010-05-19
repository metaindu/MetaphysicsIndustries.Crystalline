using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Crystalline;
using System.IO;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineAppForm<T> : Form
        where T : CrystallineControl, new()
    {
        //file menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileHasChanged)
            {
                DialogResult r = MessageBox.Show(this, "The diagram has changed.\r\n\r\nDo you want to save the changes?", AppTitle, MessageBoxButtons.YesNoCancel);

                if (r == DialogResult.Cancel)
                {
                    return;
                }
                else if (r == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(CurrentFilename))
                    {
                        if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                        {
                            return;
                        }
                        CurrentFilename = saveFileDialog1.FileName;
                    }

                    SaveFile(CurrentFilename);
                }
            }

            CurrentFilename = string.Empty;
            FileHasChanged = false;
            CrystallineControl.ResetContent();
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileHasChanged)
            {
                DialogResult r = MessageBox.Show(this, "The diagram has changed.\r\n\r\nDo you want to save the changes?", AppTitle, MessageBoxButtons.YesNoCancel);

                if (r == DialogResult.Cancel)
                {
                    return;
                }
                else if (r == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(CurrentFilename))
                    {
                        if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                        {
                            return;
                        }
                        CurrentFilename = saveFileDialog1.FileName;
                    }

                    SaveFile(CurrentFilename);
                }
            }

            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            OpenFile(openFileDialog1.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_Click();
        }

        private void Save_Click()
        {
            if (string.IsNullOrEmpty(CurrentFilename))
            {
                if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                CurrentFilename = saveFileDialog1.FileName;
            }

            SaveFile(CurrentFilename);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            CurrentFilename = saveFileDialog1.FileName;

            SaveFile(CurrentFilename);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileHasChanged)
            {
                DialogResult r = MessageBox.Show(this, "The diagram has changed.\r\n\r\nDo you want to save the changes?", AppTitle, MessageBoxButtons.YesNoCancel);

                if (r == DialogResult.Cancel)
                {
                    return;
                }
                else if (r == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(CurrentFilename))
                    {
                        if (saveFileDialog1.ShowDialog(this) != DialogResult.OK)
                        {
                            return;
                        }
                        CurrentFilename = saveFileDialog1.FileName;
                    }

                    SaveFile(CurrentFilename);
                }
            }

            CrystallineControl.ResetContent();

            this.Close();
        }

        //edit menu
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //tools menu
        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //help menu
        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
