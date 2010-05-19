using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Crystalline;
using MetaphysicsIndustries.Serialization;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class CrystallineAppForm<T> : Form
        where T : CrystallineControl, new()
    {
        private bool _fileHasChanged;
        public bool FileHasChanged
        {
            get { return _fileHasChanged; }
            protected set { _fileHasChanged = value; }
        }

        private string _currentFilename;
        public string CurrentFilename
        {
            get { return _currentFilename; }
            set
            {
                if (_currentFilename != value)
                {
                    _currentFilename = value;

                    Text = AppTitle + " - [" +
                        (string.IsNullOrEmpty(_currentFilename) ? "Untitled" : _currentFilename) +
                        (FileHasChanged ? "*" : string.Empty) + "]";
                }
            }
        }

        private void OpenFile(string filename)
        {
            try
            {
                Serializer ser = new Serializer();
                Entity[] entities = (Entity[])ser.Deserialize(filename);

                CrystallineControl.ResetContent();
                CrystallineControl.ImportEntities(entities);
                CurrentFilename = filename;
                FileHasChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was an error while trying to open \"" + filename + "\": \r\n" + ex.ToString());
            }
        }

        private void SaveFile(string filename)
        {
            try
            {
                Serializer ser = new Serializer();
                Entity[] entities = CrystallineControl.Entities.ToArray();
                ser.Serialize(filename, entities);

                CurrentFilename = filename;
                FileHasChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was an error while trying to save \"" + filename + "\": \r\n" + ex.ToString());
            }
        }
    }
}
