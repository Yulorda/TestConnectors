using System.Collections.Generic;
using UnityEngine;

//TODO Rename
public class PrefabFactory<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;

    public IEnumerable<T> Create(int count)
    {
        List<T> gameObjects = new List<T>();
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(prefab, transform);
            gameObjects.Add(go);
        }

        return gameObjects;
    }

    public T Create()
    {
        var go = Instantiate(prefab, transform);
        return go;
    }
}
