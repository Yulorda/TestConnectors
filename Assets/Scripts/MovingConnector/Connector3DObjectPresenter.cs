using UnityEngine;

namespace MovingConnector
{
    public class Connector3DObjectPresenter : MonoBehaviour
    {
        public PlatformPresenter connectorPlatform;
        public ConnectionPointPresenter connectorPoint;

        public void Inject(Connector connector)
        {
            connectorPlatform.Inject(connector);
            connectorPoint.Inject(connector);
        }
    }
}