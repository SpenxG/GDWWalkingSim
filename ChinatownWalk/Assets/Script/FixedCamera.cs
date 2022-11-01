using UnityEngine;
using System.Collections;

public class FixedCamera : MonoBehaviour {

    public Transform target;
    public float smoothTime = 0.3F;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update () {

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x + 3, target.position.y, target.position.z + 20 ), ref velocity, smoothTime);

    }
}