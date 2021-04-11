using UnityEngine;

namespace MovingConnector
{
    public class PlatformPresenter : MonoBehaviour
    {
        Connector connector;

        [SerializeField]
        EmissionHighlighter highlighter;

        private Vector3 distance;

        public void Inject(Connector connector)
        {
            this.connector = connector;
        }

        private void OnMouseUp()
        {
            highlighter.Highlight(false);
        }

        private void OnMouseDown()
        {
            highlighter.Highlight(true);
            var position = connector.GetPosition();
            distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(position).z)) - position;
        }

        private void OnMouseDrag()
        {
            var position = connector.GetPosition();
            Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(position);
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
            connector.SetPosition(new Vector3(pos_move.x - distance.x, position.y, pos_move.z - distance.z));
        }
    }
}
