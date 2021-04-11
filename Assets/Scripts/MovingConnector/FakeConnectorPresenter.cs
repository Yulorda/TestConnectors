using UnityEngine;

namespace MovingConnector
{
    public class FakeConnectorPresenter : MonoBehaviour
    {
        public SelectedPointer selectedPointer;
        public FollowMouseConnectorMover followMouse;

        public void Inject(Connector connector, ConnectorsSelectableGroup selectableGroup, SelectableColors colors)
        {
            selectedPointer.Inject(connector, selectableGroup, colors);
            followMouse.Inject(connector);
        }
    }
}