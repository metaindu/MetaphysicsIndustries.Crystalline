using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
    [Serializable]
    public abstract class Entity
    {
        public abstract void Render(Graphics g, Pen pen, Brush brush, Font font);//InternalRender and try/catch?

        [NonSerialized]
        private CrystallineControl _parentCrystallineControl;
        public virtual CrystallineControl ParentCrystallineControl
        {
            get { return _parentCrystallineControl; }
            set
            {
                if (value != ParentCrystallineControl)
                {
                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Entities.Remove(this);
                    }

                    SetParentCrystallineControl(value);

                    if (ParentCrystallineControl != null)
                    {
                        ParentCrystallineControl.Entities.Add(this);
                    }
                }
            }
        }

        protected virtual void SetParentCrystallineControl(CrystallineControl value)
        {
            _parentCrystallineControl = value;
        }

        public abstract RectangleF GetBoundingBox();

        public event EventHandler BoundingBoxChanged;

        protected virtual void OnBoundingBoxChanged()
        {
            if (BoundingBoxChanged != null)
            {
                EventArgs e = new EventArgs();

                BoundingBoxChanged(this, e);
            }
        }
    }
}
