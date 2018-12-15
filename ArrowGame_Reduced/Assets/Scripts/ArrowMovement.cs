using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour {
	/*
	GameObject go = GameObject.Find("Main Camera");
	ArrowTarget arrowTarget = go.GetComponent<ArrowTarget>();
	*/
	public GameObject player;
	private Vector3 target;
	private Vector3 targetLocation;

	public float smoothSpeed = 10f;

	void Start() {
		//targetLocation = player.GetComponent<ArrowTarget>().targetPoint;
		//Debug.Log(targetLocation);
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate () {
		targetLocation = player.GetComponent<ArrowTarget>().targetPoint;
		//Debug.Log(targetLocation);
		target = targetLocation;
		
		Vector3 smoothPosition = Vector3.Lerp(transform.position, target, smoothSpeed);
		transform.position = smoothPosition;

		transform.LookAt(target);
	}
}
