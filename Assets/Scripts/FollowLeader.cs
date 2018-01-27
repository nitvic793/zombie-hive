using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowLeader : MonoBehaviour
{

    public NavMeshAgent navMesh;
    public bool isLeader = true;
    float maxVelocity = 10.0f;
    float maxForce = 10.0f;
    float maxSpeed = 10.0f;
    float mass = 10.0f;
    Vector3 velocity;
    Vector3 position;
    const float SEPARATION_RADIUS = 1.0f;
    const float MAX_SEPARATION = 10.0f;
    const float LEADER_BEHIND_DIST = 5.0f;
    
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        GoToMousePositionIfLeader();
    }

    void GoToMousePositionIfLeader()
    {
        if (Input.GetMouseButtonDown(0) && isLeader)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Plane")
                {
                    navMesh.destination = hit.point;
                }
            }
        }
    }
}
