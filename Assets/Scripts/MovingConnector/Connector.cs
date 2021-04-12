using System;
using UnityEngine;

namespace MovingConnector
{
    public class Connector : ISelectable<Color>
    {
        private bool isSelected;
        public bool IsSelected => isSelected;

        public event Action<Connector> OnMoving;

        public event Action<Connector> OnConnectionPointPointerClick;
        public event Action<Connector> OnConnectionPointPoinerDrag;
        public event Action<Connector> OnConnectionPointPointerDragEnd;

        public event Action<Color> OnConnectionPointSelect;
        public event Action OnConnectionPointUnSelect;

        public event Action<Connector> OnDestroy;

        Func<Vector3> getPosition;
        Action<Vector3> setPosition;
        Func<Vector3> getConnectorPosition;

        public Connector(Func<Vector3> getConnectorPosition, Func<Vector3> getPosition, Action<Vector3> setPosition)
        {
            this.getConnectorPosition = getConnectorPosition;
            this.getPosition = getPosition;
            this.setPosition = setPosition;
        }

        public Vector3 GetConnectorPosition()
        {
            if (getConnectorPosition != null)
            {
                return getConnectorPosition.Invoke();
            }
            else
            {
                return Vector3.zero;
            }
        }

        public Vector3 GetPosition()
        {
            if (getPosition != null)
            {
                return getPosition.Invoke();
            }
            else
            {
                return Vector3.zero;
            }
        }

        public void SetPosition(Vector3 position)
        {
            if (setPosition != null)
            {
                setPosition.Invoke(position);
                OnMoving?.Invoke(this);
            }
        }

        public void ConnectionPointPointerClick()
        {
            OnConnectionPointPointerClick?.Invoke(this);
        }

        public void ConnectionPointPonterDrag()
        {
            OnConnectionPointPoinerDrag?.Invoke(this);
        }

        public void ConnectionPointPointerDragEnd()
        {
            OnConnectionPointPointerDragEnd?.Invoke(this);
        }

        public void Select(Color value)
        {
            isSelected = true;
            OnConnectionPointSelect?.Invoke(value);
        }

        public void UnSelect()
        {
            isSelected = false;
            OnConnectionPointUnSelect?.Invoke();
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
    }
}

