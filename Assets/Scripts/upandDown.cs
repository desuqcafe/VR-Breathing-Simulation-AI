using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upandDown : MonoBehaviour
{
//adjust this to change speed
public float speed = 6f;                          // 1.55 deep breath
//adjust this to change how high it goes
public float height = 0.05f;

Vector3 currentPos, endPos;

private void Start()
{
    currentPos = transform.position;
}
// void Update()
// {
//     //calculate what the new Y position will be
//     float newY = Mathf.Sin(Time.time * speed) * height + currentPos.y;
    
//     currentPos = transform.position;

//     endPos = new Vector3(transform.position.x, newY, transform.position.z);
//     //set the object's Y to the new calculated Y
//     transform.position = Vector3.Lerp(currentPos, endPos, speed * Time.deltaTime);
//     }
// }

void Update()
{
    //calculate what the new Y position will be
    float newY = Mathf.Sin(Time.time * speed) * height + currentPos.y;

    if (height > 0)
    {
        //if height is greater than 0, move only up
        newY = Mathf.Clamp(newY, currentPos.y, currentPos.y + height);
    }
    else if (height < 0)
    {
        //if height is less than 0, move only down
        newY = Mathf.Clamp(newY, currentPos.y + height, currentPos.y);
    }

    currentPos = transform.position;

    endPos = new Vector3(transform.position.x, newY, transform.position.z);
    //set the object's Y to the new calculated Y
    transform.position = Vector3.Lerp(currentPos, endPos, speed * Time.deltaTime);
}
}