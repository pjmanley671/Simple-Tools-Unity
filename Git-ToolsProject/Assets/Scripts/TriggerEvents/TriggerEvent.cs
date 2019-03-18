using UnityEngine;

[CreateAssetMenu()]
public class TriggerEvent : ScriptableObject
{
    public virtual void CustomEnter(Collider other)
    { Debug.Log("CustomEnter - Trigger"); }

    public virtual void CustomStay(Collider other)
    { Debug.Log("CustomStay - Trigger"); }

    public virtual void CustomExit(Collider other)
    { Debug.Log("CustomExit - Trigger"); }
}
