using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockBehavior : MonoBehaviour
{
    public float MoveSpeed;
    public float desiredSeparation;
    public float MaxForce;
    public float neighbordist;
    public Vector2 velocity;
    private Camera m_camera;
    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2();
        m_camera = Camera.main;
    }

    public void AddForce(Vector3 vecForce)
    {
        transform.position = transform.position + vecForce;
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
        Vector2 vecMouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, -m_camera.transform.position.z));
        AddForce(seek(vecMouseWorld));
        flock(lstFlock);
    }
}
