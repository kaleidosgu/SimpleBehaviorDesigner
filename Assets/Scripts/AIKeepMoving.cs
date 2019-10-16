using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AIKeepMoving : Action
{
    private Rigidbody2D m_rigidBody;
    private AIAttributeComponent m_attr;
    public override TaskStatus OnUpdate()
    {
        Vector3 vecPosTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        m_attr.currentTime += Time.deltaTime;
        m_rigidBody.AddRelativeForce(Vector3.right * m_attr.ForceValue);
        if (m_attr.currentTime >= m_attr.KeepMovingTime)
        {
            return TaskStatus.Failure;
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
