using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdKey : MonoBehaviour
{
    public Transform target;
    public float speed;
    
    public Vector3 direction, facing;

   
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        facing = transform.eulerAngles;

        direction = transform.position - target.position;

        

        if (direction.z > 0)
        {
            facing.y = 180;
        } else if (direction.z < 0)
        {
            facing.y = 0;
        }

        transform.eulerAngles = facing;
    }
}
