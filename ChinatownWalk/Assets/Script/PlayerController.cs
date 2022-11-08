using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 4f;

    Vector3 forward, right;
    private float moveSpeed;
    public Transform movePoint;
    public AudioSource music;
    public AudioSource footsteps;
    //public LayerMask whatStopMovement;
    

	// Use this for initialization
	void Start () {

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        // -45 degrees from the world x axis
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;

        // Initial speed
        moveSpeed = walkSpeed;
    
        movePoint.parent = null;
        footsteps = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        // Movement
        if (Input.anyKey) {
            Move();
            footsteps.enabled = true;
        }
        else{
            footsteps.enabled = false;
        }
/*
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, walkSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f){
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f){
                if (!Physics2D.OverlapBox(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopMovement))
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f){
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }       
        }
*/
	}

    void Move() {

        // Movement speed
        Vector3 rightMovement = right * moveSpeed * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Input.GetAxis("Vertical");

        // Calculate what is forward
        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        // Set new position
        Vector3 newPosition = transform.position;
        newPosition += rightMovement;
        newPosition += upMovement;

        // Smoothly move the new position
        //transform.forward = heading;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);




    }
}