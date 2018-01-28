using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementGates : MonoBehaviour {

    public bool moveNow;
    private bool moved;
    private Vector3 finalPosition;

    // Use this for initialization
    void Start () {
        moved = false;
        finalPosition = new Vector3(transform.position.x + 6.0f, transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (moveNow)
        {
            if (!moved)
            {
                if (transform.position.x < finalPosition.x)
                {
                    transform.position = new Vector3(transform.position.x + (Time.deltaTime * 0.13f), transform.position.y, transform.position.z);
                }
                else
                    moved = true;
            }
        }
        else
        {
            checkTrigger();
        }
    }

    void checkTrigger()
    {
        var zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (var zombie in zombies)
        {
            float range = 6.0f;
            if ((transform.position.z - 0.5 < zombie.transform.position.z) &&
                (transform.position.z + 0.5 > zombie.transform.position.z))
            {
                if (((transform.position.x) < (zombie.transform.position.x)) &&
                    ((transform.position.x + range) > (zombie.transform.position.x)))
                {
                    moveNow = true;
                }
            }
        }
    }

    }
