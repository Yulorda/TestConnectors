using System.Collections;
using UnityEngine;

namespace MovingConnector
{
    public class FollowMouseConnectorMover : MonoBehaviour
    {
        Connector connector;

        private Vector3 distance;

        public void Inject(Connector connector)
        {
            this.connector = connector;
        }

        public void StartFollow()
        {
            var position = connector.GetPosition();
            distance = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(StartFollowing());
        }

        private IEnumerator StartFollowing()
        {
            while (true)
            {
                yield return null;
                Following();

                if (Input.GetMouseButtonUp(0))
                {
                    EndFollow();
                    break;
                }

            }
        }

        private void Following()
        {
            connector.SetPosition(GetPosition());
        }

        public void EndFollow()
        {
            connector.ConnectionPointPointerDragEnd();
        }

        private Vector3 GetPosition()
        {
            var position = connector.GetPosition();
            Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(position);
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
            return new Vector3(pos_move.x, position.y, pos_move.z);
        }
    }

}