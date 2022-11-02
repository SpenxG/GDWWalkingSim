using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 targetPosition;
    public Transform target;
    public float smoothTime = 1.0f; 
    public float speed = 3;
    public float timeCount = 0.0f;
    Vector3 velocity;  
    Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the Platform to Up or Down
        //Sets Transformation Point
        Vector3 targetPosition = target.TransformPoint(new Vector3());
        

        if (timeCount > 5.0f){

            transform.position = Vector3.SmoothDamp(transform.position, startPosition, ref velocity, smoothTime, speed);

        }

        if (timeCount < 5.0f) {

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
        }

        timeCount = timeCount + Time.deltaTime;

        if (timeCount > 9.0f){
            timeCount = 0.0f;
        }

    }
}
