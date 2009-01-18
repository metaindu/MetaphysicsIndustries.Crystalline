using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MetaphysicsIndustries.Collections;
using System.Drawing;

namespace MetaphysicsIndustries.Crystalline
{
    public abstract class ConnectionEngine<TFrom, TTo, TConduit> : Functionality
        where TFrom : Element//, IConnector<TFrom, TTo, TConduit>             //change this to ": Entity"
        where TTo : Element//, IConnectee<TFrom, TTo, TConduit>               //change this to ": Entity"
        where TConduit : Path//, IConnectionConduit<TFrom, TTo, TConduit>     //change this to ": Entity"
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

        public override void ProcessMouseMove(MouseEventArgs e)
        {
            if (_connectionSource != null)
            {
                PointF connectionPoint = GetConnectionPoint();

                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.LastMouseMoveInDocument);
                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.DocumentSpaceFromClientSpace(e.Location));

                InvalidateConnectionSelection();

                _connectionTargetCandidate = null;
                Set<Element> elementsUnderCursor = Control.GetElementsAtPoint(Control.DocumentSpaceFromClientSpace(e.Location));
                if (elementsUnderCursor != null && elementsUnderCursor.Count > 0)
                {
                    foreach (Element element in elementsUnderCursor)
                    {
                        if (element != _connectionSource)
                        {
                            _connectionTargetCandidate = element as TTo;
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
                Control.InvalidateRectFromElement(_connectionSource);
            }
            if (_connectionTargetCandidate != null)
            {
                Control.InvalidateRectFromElement(_connectionTargetCandidate);
            }
            if (_connectionPath != null)
            {
                Control.InvalidateRectFromPath(_connectionPath);
            }
        }

        private PointF GetConnectionPoint()
        {
            return _connectionSource.GetOutboundConnectionPoint(_connectionPath);
        }

        public override void ProcessMouseUp(MouseEventArgs e)
        {
            if (_connectionSource != null)
            {
                PointF connectionPoint = GetConnectionPoint();

                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.DocumentSpaceFromClientSpace(e.Location));
                Control.InvalidateRectFromPointsInDocument(connectionPoint, Control.LastMouseMoveInDocument);

                InvalidateConnectionSelection();

                if (_connectionTargetCandidate != null)
                {
                    _connectionPath.To = _connectionTargetCandidate;
                    Control.InvalidateRectFromPath(_connectionPath);
                    Control.RoutePath(_connectionPath);
                }
                else
                {
                    Control.InvalidateRectFromPath(_connectionPath);
                    _connectionPath.From = null;
                    Control.RemovePath(_connectionPath);
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
                PointF connectionPoint = _connectionSource.GetOutboundConnectionPoint(_connectionPath);

                connectionPoint = Control.ClientSpaceFromDocumentSpace(connectionPoint);

                PointF cursor = Control.PointToClient(System.Windows.Forms.Control.MousePosition);

                e.Graphics.DrawLine(_connectionPen, connectionPoint, cursor);
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

        public void InitiateConnection(TFrom connectionSource)
        {
            if (connectionSource == null) { throw new ArgumentNullException("connectionSource"); }

            _connectionSource = connectionSource;

            TConduit path = new TConduit();
            Control.AddPath(path);
            path.From = _connectionSource;
            _connectionPath = path;
        }

    }
}
