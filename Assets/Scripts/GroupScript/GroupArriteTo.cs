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

        Vector3 vecCorss = Vector3.Cross(transform.right, vecDirToTarget);
        float valRotation = vecCorss.z;
        float fMoveForce = m_attr.MoveForce;

        //如果x,y速度小于限定速度，那么就加力。
        //if (Mathf.Abs(m_rigidBody.velocity.x) < m_attr.LimitSpeed && Mathf.Abs(m_rigidBody.velocity.y) < m_attr.LimitSpeed)
        //{
        //    m_rigidBody.AddRelativeForce(Vector3.right * m_attr.MoveForce);
        //}
        ////如果与速度与目标朝向是相反的，则需要加力。
        //else if ((m_rigidBody.velocity.x > 0 && vecDirToTarget.x < 0 || m_rigidBody.velocity.x < 0 && vecDirToTarget.x > 0)
        //    || (m_rigidBody.velocity.y > 0 && vecDirToTarget.y < 0 || m_rigidBody.velocity.y < 0 && vecDirToTarget.y > 0))
        //{
        //    m_rigidBody.AddRelativeForce(Vector3.right * m_attr.MoveForce);
        //}
        //else
        //{
        //    //速度上限到了，就不用更新力度。
        //}

        m_rigidBody.MovePosition(transform.position + transform.right * fMoveForce * Time.deltaTime);

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
