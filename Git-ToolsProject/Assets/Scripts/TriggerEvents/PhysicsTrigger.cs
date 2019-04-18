using UnityEngine;

[CreateAssetMenu(menuName = "PhysicsTrigger")]
public class PhysicsTrigger : TriggerEvent
{
    [SerializeField]
    private ForceMode _mode;
    
    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private bool _enter, _stay, _exit;

    public override void CustomAwake(GameObject pObject) 
    {
        base.CustomAwake(pObject);
    }

    public override void CustomEnter(Collider other)
    { 
        if(_enter)
        {
            base.CustomEnter(other);
            other.attachedRigidbody.AddForce(_direction, _mode);
        }
    }

    public override void CustomStay(Collider other)
    { 
        if(_stay)
        {
            base.CustomStay(other);
            other.attachedRigidbody.AddForce(_direction, _mode);
        }
    }

    public override void CustomExit(Collider other)
    { 
        if(_exit)
        {
            base.CustomExit(other);
            other.attachedRigidbody.AddForce(_direction, _mode);
        }
    }
}