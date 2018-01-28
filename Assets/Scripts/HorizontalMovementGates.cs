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
        if (moveNow && !moved)
        {
            if (transform.position.x < finalPosition.x)
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                moved = true;
        }
    }

}
