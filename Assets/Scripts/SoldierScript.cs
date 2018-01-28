using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour {

    public float Health = 100.0f;
    float totalTime = 0F;
    public GameObject muzzleFlash;
    List<GameObject> muzzleFlashes = new List<GameObject>();
    public float hitRate = 20F;
	// Use this for initialization
	void Start () {
		
	}

    void AttackZombiesIfNearby()
    {
        var zombies = GameObject.FindGameObjectsWithTag("Zombie");
        float minDistance = 10000F;
        GameObject minZombie = null;
        foreach(var zombie in zombies)
        {
            var direction = Vector3.Normalize(zombie.transform.position - transform.position);
            var distance = Vector3.Distance(zombie.transform.position, transform.position);
            RaycastHit hit;
            if(distance<35.0f && Physics.Raycast(transform.position,direction, out hit))
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
            
            var direction = Vector3.Normalize(minZombie.transform.position - transform.position);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 1F);
            var angle = Vector3.Angle(transform.position, minZombie.transform.position);
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            if (totalTime > 2F)
            {
                GetComponent<AudioSource>().Play();
                minZombie.GetComponent<FollowLeader>().InflictDamage(hitRate);
                totalTime = 0F;
                muzzleFlashes.Add(Instantiate(muzzleFlash, transform));
            }
        }        
    }
	
	// Update is called once per frame
	void Update () {
        if (totalTime > 2F)
        {
            if (muzzleFlashes.Count > 0)
            {
                foreach(var flash in muzzleFlashes)
                {
                    Destroy(flash);
                }
                muzzleFlashes.Clear();
            }
        }
        totalTime += Time.deltaTime;
		if(Health==0F)
        {
            gameObject.SetActive(false);
        }
        else
        {
            AttackZombiesIfNearby();
        }
	}

    public void InflictDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0F, 100.0f);
    }
}
