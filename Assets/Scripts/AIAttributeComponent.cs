﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttributeComponent : MonoBehaviour {

    public float LowForceValue;
    public float ForceValue;
    public float LimitDistance;
    public float PowerForceValue;
    public float LimitSpeed;
    public float rotatingSpeed;

    public Transform TransTarget;

    public float DisOfCheckObstacle;
    public LayerMask CheckLayerMask;

    public float DiffAngle;

    public float currentTime;
    public float KeepMovingTime;

    //设定临时目标后，表示AI达到位置的距离差
    public float DistanceWithTempTarget;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
