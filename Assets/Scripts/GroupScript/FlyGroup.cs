using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyGroup : MonoBehaviour
{
    public GameObject ObjCreate;
    public int CountsToCreate;

    public float MoveSpeed;
    public float desiredSeparation;
    public float MaxForce;
    public float neighborDist;

    private List<FlockBehavior> m_lstFlock;
    // Start is called before the first frame update
    void Start()
    {
        m_lstFlock = new List<FlockBehavior>();
        for (int nIdx = 0; nIdx < CountsToCreate; nIdx++ )
        {
            float fXPos = Random.Range(-3f, 3f);
            float fYPos = Random.Range(-3f, 3f);
            GameObject objCreate = Instantiate(ObjCreate, new Vector3(fXPos, fYPos, 0), Quaternion.identity, transform);
            FlockBehavior _flock = objCreate.GetComponent<FlockBehavior>();
            if(_flock != null)
            {
                _flock.MoveSpeed            = MoveSpeed;
                _flock.desiredSeparation    = desiredSeparation;
                _flock.MaxForce             = MaxForce;
                _flock.neighbordist         = neighborDist;
                m_lstFlock.Add(_flock);
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockBehavior _flock in m_lstFlock)
        {
            _flock.run(m_lstFlock);
        }
    }
}
