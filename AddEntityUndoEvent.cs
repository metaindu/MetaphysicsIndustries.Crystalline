using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Crystalline
{
    public class AddEntityUndoEvent : UndoEvent
    {
        public AddEntityUndoEvent(CrystallineControl control, Entity entity)
        {
            if (control == null) { throw new ArgumentNullException("control"); }
            if (entity == null) { throw new ArgumentNullException("entity"); }

            _control = control;
            _entity = entity;
        }

        private CrystallineControl _control;

        private Entity _entity;
        public Entity Entity
        {
            get { return _entity; }
        }

        public override string Text
        {
            get { return "Add " + Entity.EntityClassName; }
        }

        public override void Undo()
        {
            _control.Entities.Remove(Entity);
            //_control.DisconnectAndRemoveEntity(Entity); ?
        }

        public override void Redo()
        {
            _control.AddEntity(Entity);
        }
    }
}
