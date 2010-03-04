using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Utilities;

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

        public abstract RectangleV GetBoundingBox();

        public event EventHandler BoundingBoxChanged;

        protected virtual void OnBoundingBoxChanged()
        {
            if (BoundingBoxChanged != null)
            {
                EventArgs e = new EventArgs();

                BoundingBoxChanged(this, e);
            }
        }

        protected void InvalidateRectInParentControlFromSelf()
        {
            if (ParentCrystallineControl != null)
            {
                ParentCrystallineControl.InvalidateRectFromEntity(this);
            }
        }

        public static RectangleV GetBoundingBoxFromEntities(Entity[] entities)
        {
            if (entities.Length > 0)
            {
                RectangleV rect = entities[0].GetBoundingBox();

                foreach (Entity ent in entities)
                {
                    rect = rect.Union(ent.GetBoundingBox());
                }

                return rect;
            }
            else
            {
                return new RectangleV();
            }
        }

        public static RectangleV GetBoundingBoxFromCenters(Entity[] entities)
        {
            if (entities.Length > 0)
            {
                //vec
                RectangleV rect =
                    new RectangleV(
                        MIMath.CalcCenterOfRectangle(entities[0].GetBoundingBox()),
                        new SizeF(0, 0));

                foreach (Entity ent in entities)
                {
                    Vector v = MIMath.CalcCenterOfRectangle(ent.GetBoundingBox());
                    rect = rect.Union(
                        new RectangleV(
                            v,
                            new SizeV(0, 0)));
                }

                return rect;
            }
            else
            {
                return new RectangleV();
            }
        }
    }
}
