using UnityEngine;

[CreateAssetMenu()]
public class CollisionEvent : ScriptableObject
{
    // Sets the base calls for all CollsionEvents.
    public virtual void CustomEnter(Collision collision)
    { Debug.Log("CustomEnter - Collision"); }

    public virtual void CustomStay(Collision collision)
    { Debug.Log("CustomStay - Collision"); }

    public virtual void CustomExit(Collision collision)
    { Debug.Log("CustomExit - Collision"); }
}
