using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Action")]

public class GroupArriteTo : Action
{
    public SharedGameObject VirtualObject;

    public SharedBool SharedBoolAimVO;

    private Rigidbody2D m_rigidBody;
    private GroupAttribute m_attr;
    public override TaskStatus OnUpdate()
    {
        Vector2 vecDirToTarget = m_attr.TargetFollow.position - transform.position;
        if( vecDirToTarget.magnitude < 5 )
        {
            int a = 0;
        }

        vecDirToTarget.Normalize();

        float valRotation = Vector3.Cross(transform.right, vecDirToTarget).z;
        float fMoveForce = m_attr.MoveForce;
        m_rigidBody.AddRelativeForce(Vector3.right * fMoveForce);
        m_rigidBody.angularVelocity = m_attr.rotatingSpeed * valRotation;
        return TaskStatus.Running;
    }

    public override void OnAwake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody2D>();
        m_attr = transform.GetComponent<GroupAttribute>();
    }

    public override void OnReset()
    {
    }
    public override void OnStart()
    {
    }
}
