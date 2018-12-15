﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

	public GameObject piecePrefab;
    // The target marker.
    public Transform target;
    public float timeBtwSteps;
    private float timer = 0;

    // Speed in units per sec.
    public float speed;

    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    // Use this for initialization
    void Start()
    {
    	EnemyManager enemyManager = GameObject.Find("Spawner").GetComponent<EnemyManager>();
    	target = GameObject.FindWithTag("Player").transform;
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }

    void Update()
    {
        // The step size is equal to speed times frame time.
        float step = speed;

        timer += Time.fixedDeltaTime;

        if (timer > timeBtwSteps)
        {
            timer = timer - timeBtwSteps;
            // Move our position a step closer to the target.
            Vector3 smoothStep = Vector3.Lerp(transform.position, target.position, 0.125f);
            transform.position = smoothStep;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Arrow")
        {
            explode();
        }
    }
    
    public void explode()
    {
        //make object disappear
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

        Die();
    }

    void Die() {
    	EnemyManager enemyManager = GameObject.Find("Spawner").GetComponent<EnemyManager>();
    	enemyManager.numEnemies--;
    }

    void createPiece(int x, int y, int z)
    {

        //create piece
        GameObject piece;
        piece = Instantiate(piecePrefab);
        piece.GetComponent<Renderer>().material = GetComponent<Renderer>().material;

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
    }

}