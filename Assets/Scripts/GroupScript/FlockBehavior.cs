using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehavior : MonoBehaviour
{
    public float MoveSpeed;
    public float desiredSeparation;
    public float MaxForce;
    public float neighbordist;
    public float BorderVallue;
    public Vector2 velocity;


    private Camera m_camera;
    private Vector2 m_vcAcceleration;
    // Start is called before the first frame update
    void Start()
    {
        //float fVal = 0.5f;
        //velocity = new Vector2(Random.Range(-fVal, fVal), Random.Range(-fVal, fVal));
        m_camera = Camera.main;
        m_vcAcceleration = new Vector2();
    }

    public void AddForce(Vector3 vecForce)
    {
        //transform.position = transform.position + vecForce;
        m_vcAcceleration.Set(m_vcAcceleration.x + vecForce.x, m_vcAcceleration.y + vecForce.y);
    }

    public Vector3 separate(List <FlockBehavior> lstFlock)
    {
        Vector2 steer = new Vector2();
        int nCount = 0;
        foreach(FlockBehavior _flock in lstFlock)
        {
            if(_flock != this )
            {
                float distance = Vector3.Distance(_flock.transform.position, transform.position);
                if( distance > 0 && distance < desiredSeparation )
                {
                    Vector2 vecDiff = transform.position - _flock.transform.position;
                    vecDiff.Normalize();
                    vecDiff = vecDiff / distance;
                    steer += vecDiff;
                    nCount++;
                }
            }
        }
        if( nCount > 0 )
        {
            steer = steer / nCount;
        }
        if( steer.magnitude > 0 )
        {
            steer.Normalize();
            steer *= MoveSpeed;
            steer -= velocity;
            Vector3.ClampMagnitude(steer, MaxForce);
        }
        return steer;
    }

    public Vector3 align(List <FlockBehavior> lstFlock)
    {
        Vector2 sum = new Vector3(0, 0);
        int count = 0;
        foreach(FlockBehavior _flock in lstFlock)
        {
            float distance = Vector3.Distance(transform.position, _flock.transform.position);
            if(_flock != this)
            {
                if ((distance > 0) && (distance < neighbordist))
                {
                    sum += _flock.velocity;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            sum /= count;
            sum.Normalize();
            sum *= MoveSpeed;
            Vector3 steer = sum - velocity;
            Vector3.ClampMagnitude(steer, MaxForce);
            return steer;
        }
        else
        {
            return new Vector3(0, 0);
        }
    }

    Vector3 cohesion(List <FlockBehavior> lstFlock)
    {
        Vector3 sum = new Vector3(0, 0);   // Start with empty vector to accumulate all positions
        int count = 0;
        foreach (FlockBehavior _flock in lstFlock)
        {
            float distance = Vector3.Distance(transform.position, _flock.transform.position);
            if ((distance > 0) && (distance < neighbordist))
            {
                sum += (_flock.transform.position); // Add position
                count++;
            }
        }
        if (count > 0)
        {
            sum /= (count);
            return seek(sum);  // Steer towards the position
        }
        else
        {
            return new Vector3(0, 0);
        }
    }
    Vector3 seek(Vector3 target)
    {
        Vector2 desired = target - transform.position;
                                                          // Scale to maximum speed
        desired.Normalize();
        desired *= (MoveSpeed);

        Vector3 steer = desired - velocity;
        steer = Vector2.ClampMagnitude(steer, MaxForce);
        return steer;
    }
    public void flock(List<FlockBehavior> lstFlock)
    {
        Vector3 sep = separate(lstFlock);   // Separation
        Vector3 ali = align(lstFlock);      // Alignment
        Vector3 coh = cohesion(lstFlock);   // Cohesion
                                         // Arbitrarily weight these forces
        sep *= 1.5f;
        ali *= 1.0f;
        coh *= 1.0f;
        // Add the force vectors to acceleration
        AddForce(sep);
        AddForce(ali);
        AddForce(coh);
    }
    public void run(List<FlockBehavior> lstFlock)
    {
        //Vector2 vecMouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, -m_camera.transform.position.z));
        //AddForce(seek(vecMouseWorld));
        //flock(lstFlock);

        //Vector2 vecMouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, -m_camera.transform.position.z));
        //AddForce(seek(vecMouseWorld));
        //flock(lstFlock);
        //updateVelocity();
        //borders();

        m_vcAcceleration.Set(0, 0);
        Vector2 vecMouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, -m_camera.transform.position.z));
        AddForce(seek(vecMouseWorld));
        Vector3 vecAcc = new Vector3(m_vcAcceleration.x, m_vcAcceleration.y, 0);
        transform.position += vecAcc;
    }
    void borders()
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
    void updateVelocity()
    {
        // Update velocity
        velocity += (m_vcAcceleration);
        // Limit speed
        Vector2.ClampMagnitude(velocity, MoveSpeed);
        transform.position += new Vector3(velocity.x, velocity.y,0);
        // Reset accelertion to 0 each cycle
        m_vcAcceleration.Set(0, 0);
    }
}
