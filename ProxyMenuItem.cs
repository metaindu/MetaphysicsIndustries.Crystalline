using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Crystalline
{
    public class ProxyMenuItem : MenuItem
    {
        private MenuItem _item;
        public MenuItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public override void Click()
        {
            if (Item != null)
            {
                Item.Click();
            }
        }

        public override bool IsChecked
        {
            get { return Item != null ? Item.IsChecked : false; }
            protected set { }
        }

        public override bool IsEnabled
        {
            get { return Item != null ? Item.IsEnabled : false; }
            protected set { }
        }

        public override string Text
        {
            get { return base.Text; }
            protected set { }
        }
    }
}
