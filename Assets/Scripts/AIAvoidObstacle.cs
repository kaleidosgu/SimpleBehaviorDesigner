
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Action")]
class AIAvoidObstacle : Conditional
{
    public SharedGameObject VirtualObject;
    public SharedVector3 VecShared;
    private Rigidbody2D m_rigidBody;
    private AIAttributeComponent m_attr;
    private bool m_bHit;
    private bool m_bReset;
    private int m_nDot;
    private float m_heightCollider;
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
        bool bRes = false;
        if(bHit == false)
        {
            m_bReset = true;
            m_nDot = 0;
            m_attr.currentTime = 0.0f;
            bRes = true;
            VecShared.Value = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            return TaskStatus.Failure;
        }

        PolygonCollider2D _pol = _hit2d.transform.GetComponent<PolygonCollider2D>();

        Vector3 vecPosUp = _hit2d.transform.position;
        vecPosUp.Set(vecPosUp.x, _pol.bounds.max.y + m_heightCollider, vecPosUp.z);
        Vector2 vecDirUp = (vecPosUp - transform.position).normalized;

        Vector3 vecPosDown = _hit2d.transform.position;
        vecPosDown.Set(vecPosDown.x, _pol.bounds.min.y - m_heightCollider, vecPosDown.z);
        Vector2 vecDirDown = (vecPosDown - transform.position).normalized;

        Vector2 vecDirWithObstacle = (_hit2d.transform.position - transform.position).normalized;
        if(m_bReset == true)
        {
            m_bReset = false;
            float angleUp = Mathf.Acos(Vector3.Dot(transform.right, vecDirUp)) * Mathf.Rad2Deg;
            float angleDown = Mathf.Acos(Vector3.Dot(transform.right, vecDirDown)) * Mathf.Rad2Deg;
            if (angleUp > angleDown)
            {
                //m_nDot = -1;
                VecShared.Value = VirtualObject.Value.transform.position = new Vector3(vecPosDown.x, vecPosDown.y, vecPosDown.z);
            }
            else
            {
                //m_nDot = 1;
                VecShared.Value = VirtualObject.Value.transform.position = new Vector3(vecPosUp.x, vecPosUp.y, vecPosUp.z);
            }
        }
        return TaskStatus.Success;

    }

    public override void OnAwake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody2D>();
        m_attr = transform.GetComponent<AIAttributeComponent>();

        m_heightCollider = m_rigidBody.GetComponent<Collider2D>().bounds.size.y ;
    }

    public override void OnReset()
    {
    }
    public override void OnStart()
    {
    }
}