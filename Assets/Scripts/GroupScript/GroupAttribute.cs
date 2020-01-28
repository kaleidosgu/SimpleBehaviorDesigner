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

        Vector2 vecTransNorm = transform.position.normalized;
        //Vector3 vecNewDir = vecMouseWorld.normalized - vecTransNorm;
        Vector3 vecNewDir = new Vector3(vecMouseWorld.x - transform.position.x, vecMouseWorld.y - transform.position.y, 0);
        transform.position = transform.position + vecNewDir.normalized * MoveForce;
        //m_rigid.MovePosition(m_rigid.position + new Vector2(vecNewDir.x, vecNewDir.y) * MoveForce);
        transform.right = vecNewDir.normalized;

        //transform.position = Vector3.Lerp(transform.position, new Vector3(vecMouseWorld.x, vecMouseWorld.y, transform.position.z), 0.05f);
        //m_rigid.velocity = transform.right * MoveForce;
        //m_rigid.AddForce(MoveForce * transform.right);
        //transform.position = new Vector3( vecMouseWorld.x,vecMouseWorld.y, transform.position.z);
        //Vector2 velocity = new Vector2(1.75f, 1.1f);
        //m_rigid.MovePosition(m_rigid.position + velocity * Time.deltaTime);
    }
}
