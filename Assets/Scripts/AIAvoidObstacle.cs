
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Action")]
class AIAvoidObstacle : Conditional
{
    private Rigidbody2D m_rigidBody;
    private AIAttributeComponent m_attr;
    private bool m_bHit;
    private bool m_bReset;
    private int m_nDot;
    public override TaskStatus OnUpdate()
    {
        bool bHit = false;
        RaycastHit2D _hit2d = Physics2D.Raycast(transform.position, transform.right, m_attr.DisOfCheckObstacle, m_attr.CheckLayerMask);
        if (_hit2d == true)
        {
            if ((_hit2d.transform.tag == "Obstacle") == true)
            {
                bHit = true;
            }
        }
        if(bHit == false)
        {
            m_bReset = true;
            m_nDot = 0;
            m_attr.KeepMovingTime = 0.0f;
            return TaskStatus.Failure;
        }

        Vector2 vecDirWithObstacle = (_hit2d.transform.position - transform.position).normalized;
        if(m_bReset == true)
        {
            m_bReset = false;
            float fDifDir = Vector3.Cross(transform.right, vecDirWithObstacle).z;
            if (fDifDir > 0)
            {
                m_nDot = 1;
            }
            else
            {
                m_nDot = -1;
            }
        }

        float changedDegree = m_attr.DiffAngle * m_nDot;
        Quaternion atAngle2nd = Quaternion.AngleAxis(changedDegree, new Vector3(0, 0, 1));
        Vector3 vecModified = atAngle2nd * transform.right;
        Vector3 vecNormal = Vector3.Cross(transform.right, vecModified);
        float valRotation = vecNormal.z;
        m_rigidBody.angularVelocity = m_attr.rotatingSpeed * valRotation;

        m_rigidBody.velocity = new Vector2();
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