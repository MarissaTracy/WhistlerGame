using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTarget : MonoBehaviour {
    public GameObject parent;
    public GameObject TargetSphere;
    public int maxSize = 400;

    public int scaleRate = 10;
    private float growth;
    private float decline;
    public Vector3 targetPoint;

    public LineRenderer laserLineRenderer;

    void Start()
    {
        TargetSphere = GameObject.FindGameObjectWithTag("ArrowTarget");
    }

    // Should I change this to FixedUpdate?
    void FixedUpdate () {
    	// Sphere is centered around right controller
    	TargetSphere.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        TargetSphere.transform.position = transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform.position;
        
        // Raycast that determines where the target will be on the 
        // invisible sphere surrounding the player
        //if (Input.GetMouseButtonDown(0) || OVRInput.Get(OVRInput.Button.One))
        //{
        	var childCamera = transform.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor");
            Ray ray = new Ray(childCamera.transform.position, childCamera.transform.forward);
            RaycastHit hit;
            ray.origin = ray.GetPoint(1000);
            ray.direction = -ray.direction;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "ArrowTarget")
                {
                	//laserLineRenderer.enabled = true;
                	//laserLineRenderer.SetPosition(0,hit.point);
                    targetPoint = hit.point;
                    //Debug.Log(targetPoint);
                }
            }
        //}

        // Wants arrow further away
        if (Input.GetKey(KeyCode.W) || OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.0)
        {
            growth = scaleRate * Time.deltaTime;

            // Check that the sphere isn't bigger that the raycast
            if (TargetSphere.transform.localScale.x < maxSize)
            {
                TargetSphere.transform.localScale += new Vector3(growth, growth, growth);
            }
            //Debug.Log(growth);
        }

        // Wants arrow closer
        if (Input.GetKey(KeyCode.S) || OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0.0)
        {
            // Make scaleRate negative in order to decrease size
            decline = -scaleRate * Time.deltaTime;

            // Check that the sphere isn't at a scale of 0
            if (TargetSphere.transform.localScale.x > 0)
            {
                TargetSphere.transform.localScale += new Vector3(decline, decline, decline);
            } else {
            	// Script that puts arrow in hand
            }
            //Debug.Log(decline);
        }
    }
}
