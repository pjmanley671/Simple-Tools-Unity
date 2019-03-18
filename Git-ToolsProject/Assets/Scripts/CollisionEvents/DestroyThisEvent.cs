using UnityEngine;

[CreateAssetMenu()]
public class DestroyThisEvent : CollisionEvent
{
    public override void CustomEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log(collision.collider.gameObject.name);
            Destroy(collision.gameObject);
        }
    }
}
