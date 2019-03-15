using UnityEngine;

public class TriggerHandler : MonoBehaviour 
{
    public TriggerEvent[] triggerEvents;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < triggerEvents.Length; i++)
            triggerEvents[i].CustomEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < triggerEvents.Length; i++)
            triggerEvents[i].CustomStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < triggerEvents.Length; i++)
            triggerEvents[i].CustomExit(other);
    }
}
