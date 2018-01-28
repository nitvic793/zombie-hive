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
    const float FOLLOW_THRESHOLD = 15.0f;
    const float ATTACK_THRESHOLD = 2.0f;
    public float HitRate = 3.0f;
    public float hitTime = 0F;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        isAttacking = false;
        UpdateState();
        switch (state)
        {
            case StatesEnum.Walking:
                GoToMousePositionIfLeader();
                GoToSoldierIfNearby();
                break;
            case StatesEnum.Dead:
                navMesh.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                break;
            case StatesEnum.Attacking:
                isAttacking = true;
                break;
        }
        GetComponentInChildren<Animator>().SetFloat("Health", Health);
        GetComponentInChildren<Animator>().SetBool("Attacking", isAttacking);
    }

    void UpdateState()
    {
        if (Health > 0.0F)
        {
            state = StatesEnum.Walking;
            AttackSoldierIfNearby();
        }
        else
        {
            state = StatesEnum.Dead;
        }
    }

    public void InflictDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, 100);
    }

    void GoToSoldierIfNearby()
    {
        var soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        foreach (var soldier in soldiers)
        {
            var distance = Vector3.Distance(soldier.transform.position, transform.position);
            if (distance < FOLLOW_THRESHOLD && soldier.GetComponent<SoldierScript>().Health > 0F)
            {
                navMesh.destination = soldier.transform.position;
                break;
            }
        }
    }

    void AttackSoldierIfNearby()
    {
        hitTime += Time.deltaTime;
        var soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        foreach (var soldier in soldiers)
        {
            var soldierScript = soldier.GetComponent<SoldierScript>();
            var distance = Vector3.Distance(soldier.transform.position, transform.position);
            if (distance < ATTACK_THRESHOLD)
            {
                if (hitTime > 1F)
                {
                    soldierScript.InflictDamage(HitRate);
                    hitTime = 0F;
                }
                state = StatesEnum.Attacking;
            }
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
