using UnityEngine;

[CreateAssetMenu()]
public class TriggerEvent : ScriptableObject
{
    public virtual void CustomAwake(GameObject pObject) 
    { Debug.Log("CustomAwake - Trigger: " + name); }

    public virtual void CustomEnter(Collider other)
    { Debug.Log("CustomEnter - Trigger" + name); }

    public virtual void CustomStay(Collider other)
    { Debug.Log("CustomStay - Trigger" + name); }

    public virtual void CustomExit(Collider other)
    { Debug.Log("CustomExit - Trigger" + name); }
}
