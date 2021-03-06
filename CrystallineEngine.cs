using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MetaphysicsIndustries.Crystalline
{
    public class CrystallineEngine : Functionality
    {
        public CrystallineEngine(CrystallineControl control)
            : base()
        {
            if (control == null) { throw new ArgumentNullException("control"); }

            _control = control;

            _functionalities = new CrystallineEngineFunctionalityOrderedParentChildrenCollection(this);
        }

        public override CrystallineEngine ParentCrystallineEngine
        {
            get { return this; }
            set { }
        }

        private CrystallineControl _control;
        public override CrystallineControl Control
        {
            get { return _control; }
        }

        private CrystallineEngineFunctionalityOrderedParentChildrenCollection _functionalities;
        public CrystallineEngineFunctionalityOrderedParentChildrenCollection Functionalities
        {
            get { return _functionalities; }
        }

        public override void ProcessClick(EventArgs e)
        {
            foreach (Functionality f in Functionalities)
            {
                if (f.IsActive)
                {
                    f.ProcessClick(e);
                }
            }
        }

        public override void ProcessMouseDoubleClick(MouseEventArgs e)
        {
            foreach (Functionality f in Functionalities)
            {
                if (f.IsActive)
                {
                    f.ProcessMouseDoubleClick(e);
                }
            }
        }

        public override void ProcessMouseDown(MouseEventArgs e)
        {
            foreach (Functionality f in Functionalities)
            {
                if (f.IsActive)
                {
                    f.ProcessMouseDown(e);
                }
            }
        }

        public override void ProcessMouseMove(MouseEventArgs e)
        {
            foreach (Functionality f in Functionalities)
            {
                if (f.IsActive)
                {
                    f.ProcessMouseMove(e);
                }
            }
        }

        public override void ProcessMouseUp(MouseEventArgs e)
        {
            foreach (Functionality f in Functionalities)
            {
                if (f.IsActive)
                {
                    f.ProcessMouseUp(e);
                }
            }
        }

    }
}
