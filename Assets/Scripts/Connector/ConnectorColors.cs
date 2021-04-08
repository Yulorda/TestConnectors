using UnityEngine;

[CreateAssetMenu(fileName = "ConnectorColors", menuName = "ScriptableObjects/ConnectorColors", order = 1)]
public class ConnectorColors : ScriptableObject
{
    public Color selectedColor;
    public Color whenSelectAnotherElementColor;
}