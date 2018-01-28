using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVerticalGates : MonoBehaviour {

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
        if (moveNow && !moved)
        {
            if (transform.position.z < finalPosition.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime);
            }
            else
                moved = true;
        }
    }
}
