
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
    private float m_widthCollider;
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
            m_attr.currentTime = 0.0f;
            VecShared.Value = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            return TaskStatus.Failure;
        }

        PolygonCollider2D _pol = _hit2d.transform.GetComponent<PolygonCollider2D>();

        float fLeftLength = _hit2d.point.x - _pol.bounds.min.x;
        float fRightLength = _pol.bounds.max.x - _hit2d.point.x;

        bool bLeft = false;
        bool bRight = false;
        bool bUp = false;
        bool bDown = false;
        if ( fLeftLength < fRightLength )
        {
            bLeft = true;
        }
        else
        {
            bRight = true;
        }

        float fDownLength = _hit2d.point.y - _pol.bounds.min.y;
        float fUpLength = _pol.bounds.max.y - _hit2d.point.y;
        if (fUpLength < fDownLength)
        {
            bUp = true;
        }
        else
        {
            bDown = true;
        }

        if( bLeft && bUp )
        {
            if( transform.position.y > _hit2d.point.y )
            {
                bUp = false;
            }
            else
            {
                bLeft = false;
            }
        }
        else if( bLeft && bDown )
        {
            if (transform.position.y < _hit2d.point.y)
            {
                bDown = false;
            }
            else
            {
                bLeft = false;
            }
        }
        else if (bRight && bUp)
        {
            if (transform.position.y > _hit2d.point.y)
            {
                bUp = false;
            }
            else
            {
                bRight = false;
            }
        }
        else if (bRight && bDown)
        {
            if (transform.position.y < _hit2d.point.y)
            {
                bDown = false;
            }
            else
            {
                bRight = false;
            }
        }
        Vector3 vecHitObjPos = _hit2d.transform.position;
        if ( bUp )
        {
            VecShared.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.center.x, _pol.bounds.max.y + m_heightCollider, vecHitObjPos.z);
        }
        else if( bDown )
        {
            VecShared.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.center.x, _pol.bounds.min.y - m_heightCollider, vecHitObjPos.z);
        }
        else if( bRight )
        {
            VecShared.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.max.x + m_widthCollider, _pol.bounds.center.y,vecHitObjPos.z);
        }
        else if( bLeft )
        {
            VecShared.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.max.x - m_widthCollider, _pol.bounds.center.y,vecHitObjPos.z);
        }

        //Vector3 vecPosUp = _hit2d.transform.position;
        //vecPosUp.Set(vecPosUp.x, _pol.bounds.max.y + m_heightCollider, vecPosUp.z);
        //Vector2 vecDirUp = (vecPosUp - transform.position).normalized;

        //Vector3 vecPosDown = _hit2d.transform.position;
        //vecPosDown.Set(vecPosDown.x, _pol.bounds.min.y - m_heightCollider, vecPosDown.z);
        //Vector2 vecDirDown = (vecPosDown - transform.position).normalized;

        //if(m_bReset == true)
        //{
        //    m_bReset = false;
        //    float angleUp = Mathf.Acos(Vector3.Dot(transform.right, vecDirUp)) * Mathf.Rad2Deg;
        //    float angleDown = Mathf.Acos(Vector3.Dot(transform.right, vecDirDown)) * Mathf.Rad2Deg;
        //    if (angleUp > angleDown)
        //    {
        //        VecShared.Value = VirtualObject.Value.transform.position = new Vector3(vecPosDown.x, vecPosDown.y, vecPosDown.z);
        //    }
        //    else
        //    {
        //        VecShared.Value = VirtualObject.Value.transform.position = new Vector3(vecPosUp.x, vecPosUp.y, vecPosUp.z);
        //    }
        //}
        return TaskStatus.Success;

    }

    public override void OnAwake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody2D>();
        m_attr = transform.GetComponent<AIAttributeComponent>();

        m_heightCollider = m_rigidBody.GetComponent<Collider2D>().bounds.size.y ;
        m_widthCollider = m_rigidBody.GetComponent<Collider2D>().bounds.size.x;
    }

    public override void OnReset()
    {
    }
    public override void OnStart()
    {
    }
}