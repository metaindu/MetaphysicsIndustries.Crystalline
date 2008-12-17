using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Crystalline
{
    public interface IConnector<TFrom, TTo, TConduit>
        where TFrom : Entity, IConnector<TFrom, TTo, TConduit>
        where TTo : Entity, IConnectee<TFrom, TTo, TConduit>
        where TConduit : Entity, IConnectionConduit<TFrom, TTo, TConduit>
    {
    }
}
