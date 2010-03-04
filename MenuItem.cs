using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class MenuItem : Entity
    {
        public MenuItem()
        {
            _menuItems = new MenuItemMenuItemParentChildrenCollection(this);
        }

        public override void Render(Graphics g, Pen pen, Brush brush, Font font)
        {
        }

        public override RectangleV GetBoundingBox()
        {
            return new RectangleV();
        }

        public void Click(CrystallineControl control)
        {
            try
            {
                InternalClick(control);
            }
            catch (Exception ee)
            {
                control.ReportException(ee);
            }
        }

        protected virtual void InternalClick(CrystallineControl control)
        {
        }

        private string _text;
        public virtual string Text
        {
            get { return _text; }
            protected set { _text = value; }
        }

        private bool _isChecked;
        public virtual bool IsChecked
        {
            get { return _isChecked; }
            protected set { _isChecked = value; }
        }

        private bool _isEnabled;
        public virtual bool IsEnabled
        {
            get { return _isEnabled; }
            protected set { _isEnabled = value; }
        }

        //clone?

        private MenuItemMenuItemParentChildrenCollection _menuItems;
        public MenuItemMenuItemParentChildrenCollection MenuItems
        {
            get { return _menuItems; }
        }

        private MenuItem _parentMenuItem;
        public MenuItem ParentMenuItem
        {
            get { return _parentMenuItem; }
            set
            {
                if (value != _parentMenuItem)
                {
                    if (_parentMenuItem != null)
                    {
                        _parentMenuItem.MenuItems.Remove(this);
                    }

                    _parentMenuItem = value;

                    if (_parentMenuItem != null)
                    {
                        _parentMenuItem.MenuItems.Add(this);
                    }
                }
            }
        }

    }
}
