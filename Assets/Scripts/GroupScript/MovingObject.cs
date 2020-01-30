using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float MoveForce;
    public float DistanceCheck;
    public float BorderVallue;
    public float DistanceArea;
    public float TimeToCheck;

    private float m_timeToCheck;
    private Vector2 m_vecInit;
    private bool m_bTurn;
    // Start is called before the first frame update
    void Start()
    {
        //transform.right = Vector3.up;
        m_vecInit = new Vector2(0, 0);
        transform.Rotate(Vector3.forward, Random.Range(-30,30));
    }

    private void Update()
    {
        //if(m_timeToCheck >= TimeToCheck)
        {
            //m_timeToCheck -= TimeToCheck;
            if (_checkHit() == true)
            {
                transform.Rotate(Vector3.forward, 20f);
            }
        }
        transform.position = transform.position + transform.right * MoveForce;
        borders();
        m_timeToCheck += Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        bool bCasted = _checkHit();
        Gizmos.color = Color.red;
        if (bCasted == false)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawLine(transform.position, transform.position + transform.right * DistanceCheck);
    }
    private bool _checkHit()
    {
        bool bCasted = false;
        RaycastHit2D[] _lstCasted = Physics2D.LinecastAll(transform.position, transform.position + transform.right * DistanceCheck);
        foreach (RaycastHit2D _cast in _lstCasted)
        {
            if (_cast.transform != transform)
            {
                bCasted = true;
                break;
            }
        }
        return bCasted;
    }
    void borders2()
    {
        if (transform.position.x < -BorderVallue)
        {
            transform.position = new Vector3(BorderVallue, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -BorderVallue)
        {
            transform.position = new Vector3(transform.position.x, BorderVallue, transform.position.z);
        }
        if (transform.position.x > BorderVallue)
        {
            transform.position = new Vector3(-BorderVallue, transform.position.y, transform.position.z);
        }
        if (transform.position.y > BorderVallue)
        {
            transform.position = new Vector3(transform.position.x, -BorderVallue, transform.position.z);
        }
    }

    void borders3()
    {
        if (transform.position.x < -BorderVallue)
        {
            transform.Rotate(Vector3.forward, 180);
        }
        else if (transform.position.y < -BorderVallue)
        {
            transform.Rotate(Vector3.forward, 180);
        }
        else if (transform.position.x > BorderVallue)
        {
            transform.Rotate(Vector3.forward, 180);
        }
        else if (transform.position.y > BorderVallue)
        {
            transform.Rotate(Vector3.forward, 180);
        }
    }
    void borders()
    {
        Vector2 vecPos = transform.position;
        float fDistance = Vector2.Distance(m_vecInit, vecPos);
        if(fDistance > DistanceArea)
        {
            if(m_bTurn == false)
            {
                transform.Rotate(Vector3.forward, 180);
                m_bTurn = true;
            }
        }
        else
        {
            //if (m_timeToCheck >= TimeToCheck)
            //{
            //    m_timeToCheck -= TimeToCheck;
            //    m_bTurn = false;
            //}
        }
        if( m_bTurn == true)
        {
            m_timeToCheck += Time.deltaTime;
            if (m_timeToCheck >= TimeToCheck)
            {
                m_timeToCheck -= TimeToCheck;
                m_bTurn = false;
            }
        }
    }
}
