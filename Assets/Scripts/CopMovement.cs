using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopMovement : MonoBehaviour
{

    public float speed = 1;
    public float radius = 5;

    void Update()
    {
        checkRadius(transform.position);
    }

    void checkRadius(Vector3 center)
    {
        var zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (var zombie in zombies)
        {
            float maxRange = 6;
            RaycastHit hit;

            if (Vector3.Distance(transform.position, zombie.transform.position) < maxRange)
            {
                var normal = (zombie.transform.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, normal, out hit, maxRange))
                {
                    GetComponent<Rigidbody>().velocity = -normal * speed;
                }
            }
        }
    }
}
