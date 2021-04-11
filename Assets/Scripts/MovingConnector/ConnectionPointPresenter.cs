using UnityEngine;

namespace MovingConnector
{
    public class ConnectionPointPresenter : MonoBehaviour
    {
        public Connector connector;

        [SerializeField]
        EmissionHighlighter highlighter;

        public void Inject(Connector connector)
        {
            this.connector = connector;

            connector.OnConnectionPointSelect += Select;
            connector.OnConnectionPointUnSelect += UnSelect;
            connector.OnDestroy += OnDestroyConnector;
        }
       
        private void OnMouseDown()
        {
            Debug.Log("PointerDown");
            connector.ConnectionPointPointerClick();
        }

        private void OnMouseExit()
        {
            if (Input.GetMouseButton(0) && connector.IsSelected)
            {
                Debug.Log("ConnectionPointRetention");
                connector.ConnectionPointPonterDrag();
            }
        }

        private void UnSelect()
        {
            highlighter.Highlight(false);
        }

        private void Select(Color color)
        {
            highlighter.color = color;
            highlighter.Highlight(true);
        }

        private void OnDestroyConnector(Connector obj)
        {
            connector.OnConnectionPointSelect -= Select;
            connector.OnConnectionPointUnSelect -= UnSelect;
            connector.OnDestroy -= OnDestroyConnector;
        }
    }
}