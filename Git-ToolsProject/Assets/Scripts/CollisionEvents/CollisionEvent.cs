using UnityEngine;

[CreateAssetMenu()]
public class CollisionEvent : ScriptableObject
{
    // Sets the base calls for all CollsionEvents.
    public virtual void CustomEnter(Collision collision)
    { Debug.Log("<color=blue>Collision</color> - CustomEnter: " + name); }

    public virtual void CustomStay(Collision collision)
    { Debug.Log("<color=blue>Collision</color> - CustomStay: " + name); }

    public virtual void CustomExit(Collision collision)
    { Debug.Log("<color=blue>Collision</color> - CustomExit: " + name); }
}
