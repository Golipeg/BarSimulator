using UnityEngine;

public class ExampleUtility : MonoBehaviour
{
    //TODO DELETE AFTER CHANGED

    // Метод для генерации случайного вектора в указанном диапазоне
    public static Vector3 RandomVector(Vector3 min, Vector3 max)
    {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
}
