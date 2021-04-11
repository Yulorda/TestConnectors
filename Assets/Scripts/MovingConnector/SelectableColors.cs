using UnityEngine;

namespace MovingConnector
{
    [CreateAssetMenu(fileName = "ConnectorColors", menuName = "ScriptableObjects/ConnectorColors", order = 1)]
    public class SelectableColors : ScriptableObject
    {
        public Color selectedColor;
        public Color whenSelectAnotherElementColor;
    }
}