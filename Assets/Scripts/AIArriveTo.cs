
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Action")]
class AIArriveTo : Action
{
    public SharedVector3 SharedVecDest;

    public float DiffDistance;
    public bool ProcessSucess;
    public Transform test;

    private Rigidbody2D m_rigidBody;
    private AIAttributeComponent m_attr;
    public override TaskStatus OnUpdate()
    {
        test.position = SharedVecDest.Value;
        Vector2 vecDirToTarget = SharedVecDest.Value - transform.position;

        vecDirToTarget.Normalize();

        float valRotation = Vector3.Cross(transform.right, vecDirToTarget).z;

        float fDistance = Vector2.Distance(transform.position, SharedVecDest.Value);

        if(fDistance <= DiffDistance)
        {
            return TaskStatus.Failure;
        }
        if (Mathf.Abs(m_rigidBody.velocity.x) < m_attr.LimitSpeed && Mathf.Abs(m_rigidBody.velocity.y) < m_attr.LimitSpeed)
        {
            m_rigidBody.AddRelativeForce(Vector3.right * m_attr.ForceValue);
        }
        else if ((m_rigidBody.velocity.x > 0 && vecDirToTarget.x < 0 || m_rigidBody.velocity.x < 0 && vecDirToTarget.x > 0)
            || (m_rigidBody.velocity.y > 0 && vecDirToTarget.y < 0 || m_rigidBody.velocity.y < 0 && vecDirToTarget.y > 0))
        {
            m_rigidBody.AddRelativeForce(Vector3.right * m_attr.ForceValue);
        }
        else
        {
            //速度上限到了，就不用更新力度。
        }
        m_rigidBody.angularVelocity = m_attr.rotatingSpeed * valRotation;
        return TaskStatus.Running;
    }

    public override void OnAwake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody2D>();
        m_attr = transform.GetComponent<AIAttributeComponent>();
    }

}