using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObjectGroup : MonoBehaviour
{
    public GameObject ObjCreate;
    public int CountsToCreate;
    public float RandomArea;
    // Start is called before the first frame update
    void Start()
    {
        for (int nIdx = 0; nIdx < CountsToCreate; nIdx++)
        {
            float fXPos = Random.Range(-RandomArea, RandomArea);
            float fYPos = Random.Range(-RandomArea, RandomArea);
            GameObject objCreate = Instantiate(ObjCreate, new Vector3(fXPos, fYPos, 0), Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
