using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Degrees per second around the Y axis
    public float speed = 90f;

    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
