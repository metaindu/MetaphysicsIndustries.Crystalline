using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
    public partial class RTree
    {
        private Dictionary<Entity,EntityRNodeAdapter> _adaptersByEntity = new Dictionary<Entity,EntityRNodeAdapter>();
        protected Dictionary<Entity,EntityRNodeAdapter> AdaptersByEntity
        {
            get { return _adaptersByEntity; }
        }


        private RNode _root;
        protected RNode Root
        {
            get { return _root; }
            set { _root = value; }
        }

        public void Add(Entity entity)
        {
            if (entity == null) { throw new ArgumentNullException("item"); }

            entity.BoundingBoxChanged += new EventHandler(Entity_BoundingBoxChanged);

            Add(new EntityRNodeAdapter(entity));
        }

        protected void Entity_BoundingBoxChanged(object sender, EventArgs e)
        {
            Entity ent = (Entity)sender;

            RNode node = AdaptersByEntity[ent];

            Remove(node);
            Add(node);
        }

        protected void Remove(RNode node)
        {
            if (Root == node)
            {

            }
        }

        protected void Add(RNode node)
        {
            if (Root == null)
            {
                Root = node;
            }
            else
            {
                RNode current = Root;

                while (!current.IsLeafContainer)
                {
                }
            }
        }

        public void Remove(Entity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            RNode node = AdaptersByEntity[entity];
            entity.BoundingBoxChanged -= Entity_BoundingBoxChanged;
            Remove(node);
        }

        public Entity[] GetAllEntitiesOverlapping(RectangleF reect)
        {
            throw new NotImplementedException();
        }

        public Entity[] GetAllEntitiesWithin(RectangleF rect)
        {
            throw new NotImplementedException();
        }
    }
}
