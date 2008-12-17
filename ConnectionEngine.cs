using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class ConnectionEngine<TFrom, TTo, TConduit> : Functionality
        where TFrom : Entity, IConnector<TFrom, TTo, TConduit>
        where TTo : Entity, IConnectee<TFrom, TTo, TConduit>
        where TConduit : Entity, IConnectionConduit<TFrom, TTo, TConduit>
    {
        public ConnectionEngine(CrystallineControl control)
            : base(control)
        {
        }

        public abstract void MakeConnection(TFrom from, TTo to);
        public abstract void MakeConnectionUsingConduit(TFrom from, TTo to, TConduit conduit);
    }
}
