using System.Collections.Generic;
using UnityEngine;

//TODO Rename
public class PrefabFactory<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private Main main;

    [SerializeField]
    private T prefab;

    [SerializeField]
    private int prefabCount;

    private List<T> gameObjects = new List<T>();

    [ContextMenu("Create")]
    public IEnumerable<T> Create()
    {
        for (int i = 0; i < prefabCount; i++)
        {
            var go = Instantiate(prefab, transform);
            go.transform.position = GetCoodinates(main.Radius);
            gameObjects.Add(go);
        }

        return gameObjects;
    }

    private Vector3 GetCoodinates(float radius)
    {
        if (radius < 0)
        {
            return Vector3.zero;
        }
        else
        {
            //return Random.insideUnitCircle * radius;
            float a = Random.Range(0f, 1f) * 2 * Mathf.PI;
            float r = radius * Mathf.Sqrt(Random.Range(0f, 1f));

            var x = r * Mathf.Cos(a);
            var z = r * Mathf.Sin(a);

            return new Vector3(x, 0, z);
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        foreach (var go in gameObjects)
        {
            DestroyImmediate(go);
        }
        gameObjects.Clear();
    }
}
