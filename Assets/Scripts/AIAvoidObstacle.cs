
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskCategory("Custom/Action")]
class AIAvoidObstacle : Conditional
{
    public SharedGameObject VirtualObject;
    public SharedVector3 ShareDestPos;
    public SharedBool SharedBoolAimVO;

    private Rigidbody2D m_rigidBody;
    private AIAttributeComponent m_attr;
    private float m_ColliderHeight;
    private float m_ColliderWidth;
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
            m_attr.currentTime = 0.0f;
            ShareDestPos.Value = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            SharedBoolAimVO.Value = false;
            return TaskStatus.Failure;
        }

        PolygonCollider2D _pol = _hit2d.transform.GetComponent<PolygonCollider2D>();

        float fLengthWithLeft = _hit2d.point.x - _pol.bounds.min.x;
        float fLengthWithRight = _pol.bounds.max.x - _hit2d.point.x;

        bool bLeft = false;
        bool bRight = false;
        bool bUp = false;
        bool bDown = false;
        if ( fLengthWithLeft < fLengthWithRight )
        {
            bLeft = true;
        }
        else
        {
            bRight = true;
        }

        float fLengthWithDown = _hit2d.point.y - _pol.bounds.min.y;
        float fLengthWithUp = _pol.bounds.max.y - _hit2d.point.y;
        if (fLengthWithUp < fLengthWithDown)
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
            ShareDestPos.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.center.x, _pol.bounds.max.y + m_ColliderHeight, vecHitObjPos.z);
        }
        else if( bDown )
        {
            ShareDestPos.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.center.x, _pol.bounds.min.y - m_ColliderHeight, vecHitObjPos.z);
        }
        else if( bRight )
        {
            ShareDestPos.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.max.x + m_ColliderWidth, _pol.bounds.center.y,vecHitObjPos.z);
        }
        else if( bLeft )
        {
            ShareDestPos.Value = VirtualObject.Value.transform.position = new Vector3(_pol.bounds.min.x - m_ColliderWidth, _pol.bounds.center.y,vecHitObjPos.z);
        }
        SharedBoolAimVO.Value = true;
        return TaskStatus.Success;

    }

    public override void OnAwake()
    {
        m_rigidBody = transform.GetComponent<Rigidbody2D>();
        m_attr = transform.GetComponent<AIAttributeComponent>();

        m_ColliderHeight = m_rigidBody.GetComponent<Collider2D>().bounds.size.y ;
        m_ColliderWidth = m_rigidBody.GetComponent<Collider2D>().bounds.size.x;
    }

    public override void OnReset()
    {
    }
    public override void OnStart()
    {
    }
}