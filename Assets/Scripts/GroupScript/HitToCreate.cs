using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitToCreate : MonoBehaviour
{
    public GameObject ObjCreate;
    public float DistanceArea;
    private Vector3 m_vecInit;
    // Start is called before the first frame update
    void Start()
    {
        m_vecInit = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonUp(0) == true )
        {
            Vector3 vecMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, -Camera.main.transform.position.z));
            GameObject objCreate = Instantiate(ObjCreate, vecMouse, Quaternion.identity, transform);
            objCreate.GetComponent<MovingObject>().DistanceArea = DistanceArea;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(m_vecInit, DistanceArea);
    }
}
