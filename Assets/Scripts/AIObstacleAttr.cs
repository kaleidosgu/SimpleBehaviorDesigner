using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObstacleAttr : MonoBehaviour {
    public bool DrawGizmos;
    public Color GizmosColor;
    public Color GizmosHitColor;

    private AIAttributeComponent m_attrCom;
    private Rigidbody2D m_rigBody;
    // Use this for initialization
    void Start () {
        m_rigBody = GetComponent<Rigidbody2D>();
        m_attrCom = GetComponent<AIAttributeComponent>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDrawGizmos()
    {
        if (DrawGizmos == true && m_attrCom != null)
        {
            Vector3 vecPosFrom = transform.position;
            Vector3 vecOriPos = vecPosFrom;
            Vector3 vecPosTo = transform.right;
            vecPosTo = vecPosTo.normalized * m_attrCom.DisOfCheckObstacle + vecOriPos;
            RaycastHit2D _hit2d = Physics2D.Raycast(transform.position, transform.right, m_attrCom.DisOfCheckObstacle, m_attrCom.CheckLayerMask);
            if (_hit2d == true)
            {
                Gizmos.color = GizmosHitColor;
            }
            else
            {
                Gizmos.color = GizmosColor;
            }
            Gizmos.DrawLine(vecPosFrom, vecPosTo);
        }
    }
}
