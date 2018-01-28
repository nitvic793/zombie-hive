using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour {

    public float Health = 100.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Health==0F)
        {
            gameObject.SetActive(false);
        }
	}

    public void InflictDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0F, 100.0f);
    }
}
