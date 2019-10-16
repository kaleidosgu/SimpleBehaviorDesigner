
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Action")]
class BAForceRotate : Action
{
    private Rigidbody2D m_rigidBody;
    private AIAttributeComponent m_attr;
    public override TaskStatus OnUpdate()
    {
        Vector3 vecPosTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,10));
        //if (m_transCenter != null)
        {
            Debug.Log(string.Format("x[{0}],y[{1}]", vecPosTarget.x, vecPosTarget.y));
            Vector2 vecDirToTarget = vecPosTarget - transform.position;

            vecDirToTarget.Normalize();

            float valRotation = Vector3.Cross(transform.right, vecDirToTarget).z;

            float fDistance = Vector2.Distance(transform.position, vecPosTarget);

            float fMoveForce = m_attr.ForceValue;
            if (fDistance >= m_attr.LimitDistance)
            {
                fMoveForce = m_attr.PowerForceValue;
            }
            if (Mathf.Abs(m_rigidBody.velocity.x) < m_attr.LimitSpeed && Mathf.Abs(m_rigidBody.velocity.y) < m_attr.LimitSpeed)
            {
                m_rigidBody.AddRelativeForce(Vector3.right * fMoveForce);
            }
            else if ((m_rigidBody.velocity.x > 0 && vecDirToTarget.x < 0 || m_rigidBody.velocity.x < 0 && vecDirToTarget.x > 0)
                || (m_rigidBody.velocity.y > 0 && vecDirToTarget.y < 0 || m_rigidBody.velocity.y < 0 && vecDirToTarget.y > 0))
            {
                m_rigidBody.AddRelativeForce(Vector3.right * fMoveForce);
            }
            else
            {
                //速度上限到了，就不用更新力度。
            }
            m_rigidBody.angularVelocity = m_attr.rotatingSpeed * valRotation;

        }
        return TaskStatus.Running;
    }

    public override void OnAwake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody2D>();
        m_attr = transform.GetComponent<AIAttributeComponent>();
    }

    public override void OnReset()
    {
    }
    public override void OnStart()
    {
    }
}