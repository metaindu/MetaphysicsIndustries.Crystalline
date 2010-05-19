using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Collections;
using System.Drawing;
using MetaphysicsIndustries.Utilities;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class ConnectionEngine<TFrom, TTo, TConduit> : Functionality
        where TFrom : Element//, IConnector<TFrom, TTo, TConduit>             //eventually change this to ": Entity" ?
        where TTo : Element//, IConnectee<TFrom, TTo, TConduit>               //eventually change this to ": Entity" ?
        where TConduit : Path//, IConnectionConduit<TFrom, TTo, TConduit>     //eventually change this to ": Entity" ?
                            , new()
    {
        public ConnectionEngine(CrystallineEngine engine)
            : base(engine)
        {
        }

        public abstract void MakeConnection(TFrom from, TTo to);
        public abstract void MakeConnectionUsingConduit(TFrom from, TTo to, TConduit conduit);

        private TFrom _connectionSource;
        private TTo _connectionTargetCandidate;
        private TConduit _connectionPath;

        public bool IsConnecting
        {
            get { return _connectionSource != null; }
        }

        private bool _allowSelfConnections = false;
        public bool AllowSelfConnections
        {
            get { return _allowSelfConnections; }
            set { _allowSelfConnections = value; }
        }

        public override void ProcessMouseMove(MouseEventArgs e)
        {
            if (_connectionSource != null)
            {
                Vector connectionPoint = GetConnectionPoint();

                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.LastMouseMoveInDocument);
                Vector cursorLocationInDocument = Control.DocumentSpaceFromClientSpace(e.Location);
                Control.InvalidateRectFromPointsInDocument(connectionPoint, cursorLocationInDocument);

                InvalidateConnectionSelection();

                _connectionTargetCandidate = null;

                TTo[] entitiesUnderCursor = Control.GetEntitiesAtPointInDocument<TTo>(cursorLocationInDocument);

                if (entitiesUnderCursor != null && entitiesUnderCursor.Length > 0)
                {
                    foreach (TTo ent in entitiesUnderCursor)
                    {
                        if (ent != _connectionSource || AllowSelfConnections)
                        {
                            _connectionTargetCandidate = ent as TTo;
                            break;
                        }
                    }
                }

                InvalidateConnectionSelection();
            }
        }

        private void InvalidateConnectionSelection()
        {
            if (_connectionSource != null)
            {
                Control.InvalidateRectFromEntity(_connectionSource);
            }
            if (_connectionTargetCandidate != null)
            {
                Control.InvalidateRectFromEntity(_connectionTargetCandidate);
            }
            if (_connectionPath != null)
            {
                Control.InvalidateRectFromEntity(_connectionPath);
            }
        }

        private Vector GetConnectionPoint()
        {
            return _connectionSource.GetOutboundConnectionPoint(_connectionPath);
        }

        public override void ProcessMouseUp(MouseEventArgs e)
        {
            if (_connectionSource != null)
            {
                Vector connectionPoint = GetConnectionPoint();

                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.DocumentSpaceFromClientSpace(e.Location));
                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.LastMouseMoveInDocument);

                InvalidateConnectionSelection();

                if (_connectionTargetCandidate != null)
                {
                    _connectionPath.To = _connectionTargetCandidate;
                    Control.InvalidateRectFromEntity(_connectionPath);
                    Control.RoutePath(_connectionPath);
                }
                else
                {
                    Control.InvalidateRectFromEntity(_connectionPath);
                    _connectionPath.From = null;
                    Control.DisconnectAndRemoveEntity(_connectionPath);
                }

                _connectionPath = null;
                _connectionTargetCandidate = null;
                _connectionSource = null;
            }
        }

        Pen _connectionPen = new Pen(Color.Gold, 2);
        //Pen _connectionPen2 = new Pen(Color.Gold, 2);

        public override void ProcessPaint(PaintEventArgs e)
        {
            if (_connectionSource != null)
            {
                Vector connectionPointInDocument = _connectionSource.GetOutboundConnectionPoint(_connectionPath);

                Point connectionPointInClient = Control.ClientSpaceFromDocumentSpace(connectionPointInDocument);

                Vector cursorInDocument = Control.LastMouseMoveInDocument;
                    //Control.PointToClient(System.Windows.Forms.Control.MousePosition);

                e.Graphics.DrawLine(_connectionPen, connectionPointInDocument, cursorInDocument);

                if (Control.ShowDebugInfo)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("connectionPointInDocument = {{ {0}, {1} }}", connectionPointInDocument.X, connectionPointInDocument.Y);
                    sb.AppendLine();

                    e.Graphics.DrawString(sb.ToString(), Control.Font, Brushes.Green, 5, 360);
                }
            }
        }

        //Pen _connectionPen = new Pen(Color.Gold, 2);
        Pen _connectionPen2 = new Pen(Color.Gold, 2);

        public Pen ConnectionPen
        {
            get { return _connectionPen2; }
        }

        public Pen ChoosePenForElement(Element element)
        {
            if (element == _connectionSource)
            {
                return ConnectionPen;
            }
            else if (element == _connectionTargetCandidate)
            {
                return ConnectionPen;
            }

            return null;
        }

        public Pen ChoosePenForPath(Path path)
        {
            if (path == _connectionPath)
            {
                return _connectionPen;
            }

            return null;
        }

        public TConduit InitiateConnection(TFrom connectionSource)
        {
            if (connectionSource == null) { throw new ArgumentNullException("connectionSource"); }

            _connectionSource = connectionSource;

            TConduit path = new TConduit();
            Control.AddEntity(path);
            path.From = _connectionSource;
            _connectionPath = path;

            return path;
        }

    }
}
