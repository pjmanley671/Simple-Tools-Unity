using UnityEngine;

[CreateAssetMenu()]
public class DestroyThisEvent : CollisionEvent
{
    public override void CustomEnter(Collision collision)
    {
        //base.CustomEnter(collision);
        //Debug.Log("<color=red>Destroying: </color>" + collision.contacts[0].thisCollider);
        Destroy(collision.contacts[0].thisCollider.gameObject);
    }
}
