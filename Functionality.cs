using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class Functionality
    {
        public Functionality(CrystallineControl control)
        {
            ParentCrystallineControl = control;
        }

        private CrystallineControl _parentCrystallineControl;
        public CrystallineControl ParentCrystallineControl
        {
            get { return _parentCrystallineControl; }
            set
            {
                if (value != _parentCrystallineControl)
                {
                    if (_parentCrystallineControl != null)
                    {
                        _parentCrystallineControl.Functionalities.Remove(this);
                    }

                    _parentCrystallineControl = value;

                    if (_parentCrystallineControl != null)
                    {
                        _parentCrystallineControl.Functionalities.Add(this);
                    }
                }
            }
        }


        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }


        public virtual bool CanProcessMouseUp(MouseEventArgs e)
        {
            return false;
        }

        public virtual bool CanProcessMouseDoubleClick(MouseEventArgs e)
        {
            return false;
        }

        public virtual bool CanProcessMouseDown(MouseEventArgs e)
        {
            return false;
        }

        public virtual bool CanProcessMouseMove(MouseEventArgs e)
        {
            return false;
        }

        public virtual bool CanProcessClick(EventArgs e)
        {
            return false;
        }



        public virtual void ProcessMouseUp(MouseEventArgs e)
        {
        }

        public virtual void ProcessMouseDoubleClick(MouseEventArgs e)
        {
        }

        public virtual void ProcessMouseDown(MouseEventArgs e)
        {
        }

        public virtual void ProcessMouseMove(MouseEventArgs e)
        {
        }

        public virtual void ProcessClick(EventArgs e)
        {
        }
    }
}
