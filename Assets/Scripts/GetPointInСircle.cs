using UnityEngine;

public static class GetPointInСircle
{
    public static Vector3 GetCoodinates(float radius)
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
}
