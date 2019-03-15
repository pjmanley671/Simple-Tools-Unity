using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public CollisionEvent[] collisions;

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collisions.Length; i++)
            collisions[i].CustomEnter(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collisions.Length; i++)
            collisions[i].CustomStay(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < collisions.Length; i++)
            collisions[i].CustomExit(collision);
    }
}
