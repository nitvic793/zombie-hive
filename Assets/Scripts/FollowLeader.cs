using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowLeader : MonoBehaviour
{
    public NavMeshAgent navMesh;
    public bool isLeader = true;
    public float Health = 100.0f;
    bool isAttacking = false;
    public StatesEnum state = StatesEnum.Walking;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        isAttacking = false;
        UpdateState();
        switch(state)
        {
            case StatesEnum.Walking:
                GoToMousePositionIfLeader();
                break;
            case StatesEnum.Dead:
                navMesh.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                break;
            case StatesEnum.Attacking:
                isAttacking = true;
                break;
        }
        if (isAttacking) Debug.Log(name);
        GetComponentInChildren<Animator>().SetFloat("Health", Health);
        GetComponentInChildren<Animator>().SetBool("Attacking", isAttacking);
    }

    void UpdateState()
    {
        if(Health>0.0F)
        {
            //walk or attack
        }
        else
        {
            state = StatesEnum.Dead;
        }
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
