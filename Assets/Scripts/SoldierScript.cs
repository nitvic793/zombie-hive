using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierScript : MonoBehaviour
{

    public float Health = 100.0f;
    float totalTime = 0F;
    public GameObject muzzleFlash;
    List<GameObject> muzzleFlashes = new List<GameObject>();
    public float hitRate = 20F;
    public int Ammo = 16;
    public NavMeshAgent navMeshAgent;
    public GameObject zombiePrefab;
    const float MIN_LOS_DISTANCE = 25.0f;
    public bool isEscaped = false;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void AttackZombiesIfNearby()
    {
        var zombies = GameObject.FindGameObjectsWithTag("Zombie");
        float minDistance = 10000F;
        GameObject minZombie = null;
        foreach (var zombie in zombies)
        {
            var direction = Vector3.Normalize(zombie.transform.position - transform.position);
            var distance = Vector3.Distance(zombie.transform.position, transform.position);
            RaycastHit hit;
            if (distance < MIN_LOS_DISTANCE && Physics.Raycast(transform.position, direction, out hit))
            {
                if (distance < minDistance && hit.transform.tag == "Zombie")
                {
                    minDistance = distance;
                    minZombie = zombie;
                }
            }
        }

        if (minZombie != null)
        {
            if (totalTime > 2F && Ammo > 0)
            {
                var direction = Vector3.Normalize(minZombie.transform.position - transform.position);
                var angle = Vector3.Angle(transform.position, minZombie.transform.position);
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                Ammo--;
                GetComponent<Animator>().SetBool("Attacking", true);
                GetComponent<AudioSource>().Play();
                minZombie.GetComponent<FollowLeader>().InflictDamage(hitRate);
                totalTime = 0F;
                muzzleFlashes.Add(Instantiate(muzzleFlash, transform));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetBool("Attacking", false);


        if (totalTime > 2F)
        {
            if (muzzleFlashes.Count > 0)
            {
                foreach (var flash in muzzleFlashes)
                {
                    Destroy(flash);
                }
                muzzleFlashes.Clear();
            }
        }
        totalTime += Time.deltaTime;
        if (Health == 0F)
        {
            var parentTransform = GameObject.Find("Zombies") == null ? null : GameObject.Find("Zombies").transform;
            Instantiate(zombiePrefab, transform.position, transform.rotation, parentTransform);
            gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().SoldiersKilled++;
        }
        else
        {
            AttackZombiesIfNearby();
            if (Ammo <= 0)
            {
                RunToTheExit();
            }
            else
            {
                GetComponent<Animator>().SetBool("Running", false);
            }
        }
    }

    private void RunToTheExit()
    {
        GetComponent<Animator>().SetBool("Running", true);
        navMeshAgent.destination = GameObject.Find("Exit").transform.position;
        if(Vector3.Distance(GameObject.Find("Exit").transform.position, transform.position)<1.5F)
        {
            gameObject.SetActive(false);
            isEscaped = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().SoldiersEscaped++;
        }
    }

    public void InflictDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0F, 100.0f);
    }
}
