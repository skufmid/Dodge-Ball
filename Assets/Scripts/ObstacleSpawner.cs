using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float interval = 3f;
    public float spawnHeight = 10f;
    public Vector2 areaSize = new Vector2(10f, 10f);
    public float sphereScale = 1f;
    public Material sphereMaterial;
    public float lifetime = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), interval, interval);
    }

    private void Spawn()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            CancelInvoke(nameof(Spawn));
            return;
        }

        Vector3 position = new Vector3(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            spawnHeight,
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f));

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "Obstacle";
        sphere.tag = "Obstacle";
        sphere.transform.position = position;
        sphere.transform.localScale = Vector3.one * sphereScale;

        if (sphereMaterial != null)
        {
            sphere.GetComponent<Renderer>().material = sphereMaterial;
        }

        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        Destroy(sphere, lifetime);
    }
}
