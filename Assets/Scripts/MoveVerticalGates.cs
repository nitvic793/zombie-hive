using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVerticalGates : MonoBehaviour
{

    public bool moveNow;
    private bool moved;
    private Vector3 finalPosition;

    // Use this for initialization
    void Start()
    {
        moved = false;
        finalPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 6.0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (moveNow)
        {
            if (!moved)
            {
                if (transform.position.z < finalPosition.z)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Time.deltaTime * 0.18f));
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
            if ((transform.position.x - 0.5 < zombie.transform.position.x) &&
                (transform.position.x + 0.5 > zombie.transform.position.x))
            {
                if (((transform.position.z) < (zombie.transform.position.z)) &&
                    ((transform.position.z + range) > (zombie.transform.position.z)))
                {
                    moveNow = true;
                }
            }
        }

    }
}
