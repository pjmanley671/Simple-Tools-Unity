using UnityEngine;

[CreateAssetMenu(menuName = "PhysicsTrigger")]
public class PhysicsTrigger : TriggerEvent
{
    [SerializeField]
    private ForcemMode _mode;
    
    [SerializeField]
    private Vector3 _direction;

    [SerializeField]
    private bool _enter, _stay, _exit;

    public override void CustomAwake(GameObject pObject) 
    { base.CustomAwake();
        if(pObject.GetComponent<RigidBody>() == null)
            pObject.AddComponent<RigidBody>(); 
    }

    public override void CustomEnter(Collider other)
    { 
        if(_enter)
        {
            base.CustomEnter();
        }
    }

    public override void CustomStay(Collider other)
    { 
        if(_stay)
        {
            base.CustomStay();
        }
    }

    public override void CustomExit(Collider other)
    { 
        if(_exit)
        {
            base.CustomExit();
        }
    }
}