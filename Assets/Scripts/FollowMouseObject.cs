using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseObject : MonoBehaviour {

    private Camera m_camera;
	// Use this for initialization
	void Start () {
        m_camera = Camera.main;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 vecPosTarget = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = vecPosTarget;
    }
}
