using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class UndoEvent
    {
        public abstract string Text { get; }

        public abstract void Undo();

        public abstract void Redo();
    }
}
