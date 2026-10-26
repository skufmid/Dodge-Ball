using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string groundName = "Ground";
    public GameObject hitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == groundName)
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, collision.GetContact(0).point, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
