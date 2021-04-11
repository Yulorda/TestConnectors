using UnityEngine;
using UnityEngine.UI;

namespace MovingConnector
{
    public class CoordinatesPresenter : MonoBehaviour
    {
        [SerializeField]
        private Text coordinates;

        public void Inject(Connector connector)
        {
            connector.OnMoving += OnMoving;
            connector.OnDestroy += OnDestroyConnector;
            OnMoving(connector);
        }

        private void OnMoving(Connector movingConnector)
        {
            coordinates.text = movingConnector.GetConnectorPosition().ToString();
        }

        private void OnDestroyConnector(Connector connector)
        {
            connector.OnMoving -= OnMoving;
            connector.OnDestroy -= OnDestroyConnector;
            DestroyImmediate(this.gameObject);
        }
    }
}