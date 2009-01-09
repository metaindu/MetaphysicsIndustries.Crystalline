using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class Functionality
    {
        public Functionality(CrystallineEngine engine)
        {
            if (engine == null) { throw new ArgumentNullException("engine"); }

            //ParentCrystallineControl = control;
            ParentCrystallineEngine = engine;
        }

        protected Functionality()
        {
            if (!(this is CrystallineEngine))
            {
                throw new InvalidOperationException("Parameterless constructor can only be accessed by CrystallineEngine");
            }
        }

        public virtual CrystallineControl Control
        {
            get
            {
                if (ParentCrystallineEngine != null)
                {
                    return ParentCrystallineEngine.Control;
                }
                else
                {
                    return null;
                }
            }
        }

        //private CrystallineControl _parentCrystallineControl;
        //public CrystallineControl ParentCrystallineControl
        //{
        //    get { return _parentCrystallineControl; }
        //    set
        //    {
        //        if (value != _parentCrystallineControl)
        //        {
        //            if (_parentCrystallineControl != null)
        //            {
        //                _parentCrystallineControl.Functionalities.Remove(this);
        //            }

        //            _parentCrystallineControl = value;

        //            if (_parentCrystallineControl != null)
        //            {
        //                _parentCrystallineControl.Functionalities.Add(this);
        //            }
        //        }
        //    }
        //}
        private CrystallineEngine _parentCrystallineEngine;
        public virtual CrystallineEngine ParentCrystallineEngine
        {
            get { return _parentCrystallineEngine; }
            set
            {
                if (value != _parentCrystallineEngine)
                {
                    if (_parentCrystallineEngine != null)
                    {
                        _parentCrystallineEngine.Functionalities.Remove(this);
                    }

                    _parentCrystallineEngine = value;

                    if (_parentCrystallineEngine != null)
                    {
                        _parentCrystallineEngine.Functionalities.Add(this);
                    }
                }
            }
        }


        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
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

        public virtual void ProcesssDoubleClick(EventArgs e)
        {
        }



        public virtual void ProcessLoad(EventArgs e)
        {
        }

        public virtual void ProcessPaint(PaintEventArgs e)
        {
        }
    }
}
