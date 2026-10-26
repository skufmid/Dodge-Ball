using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float interval = 1f;
    public float spawnHeight = 10f;
    public Vector2 areaSize = new Vector2(10f, 10f);
    public float sphereScale = 1f;
    public Material sphereMaterial;
    public float lifetime = 10f;

    // Every rampPeriod seconds the interval is multiplied by intervalMultiplier (no lower bound).
    // Spawning is capped at one sphere per frame.
    public float rampPeriod = 3f;
    public float intervalMultiplier = 0.5f;
    public GameObject hitEffectPrefab;

    private float currentInterval;
    private float spawnTimer;
    private float rampTimer;

    public float CurrentInterval => currentInterval;

    private void Start()
    {
        currentInterval = interval;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        rampTimer += Time.deltaTime;
        while (rampTimer >= rampPeriod)
        {
            rampTimer -= rampPeriod;
            currentInterval *= intervalMultiplier;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentInterval)
        {
            spawnTimer -= currentInterval;
            Spawn();
        }
    }

    private void Spawn()
    {
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

        Obstacle obstacle = sphere.AddComponent<Obstacle>();
        obstacle.hitEffect = hitEffectPrefab;

        Destroy(sphere, lifetime);
    }
}
