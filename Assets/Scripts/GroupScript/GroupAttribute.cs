using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupAttribute : MonoBehaviour {

	public float MoveForce;
	public float rotatingSpeed;
	public float LimitSpeed;
	public Transform TargetFollow;

    private Rigidbody2D m_rigid;
    private Camera m_camera;
    private void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_camera = Camera.main;
    }
    private void Update()
    {
        Vector2 vecMouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0.0f, 0.0f, -m_camera.transform.position.z));
        Vector3 vecNewDir = new Vector3(vecMouseWorld.x - transform.position.x, vecMouseWorld.y - transform.position.y, 0);
        transform.position = transform.position + vecNewDir.normalized * MoveForce;
        transform.right = vecNewDir.normalized;
    }
}
