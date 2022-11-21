using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour {
    [SerializeField]
    public TimeController _TimeController;
    public float walkSpeed = 4f;
    public int isNight = 0;

    Vector3 forward, right;
    private float moveSpeed;
    private DateTime currentTime;
    private float saveTimeHour;
    private float saveTimeMin;
    private float startHour;
    private float timeMultiplier;
    //public Transform movePoint;
    public AudioSource music;
    public AudioSource footsteps;
    public AudioClip doorSound;
    //public LayerMask whatStopMovement;
    [SerializeField]
    private float delayBeforeLoading = 2f;
    private float timeElapsed;
    public Vector3 targetLocation;
    public Transform player;
    private float tLocX;
    private float tLocY;
    private float tLocZ;
    
    void Awake() 
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    //Saves Whether night is enabled or disabled, as well as saving the current time
    void OnDisable(){
        PlayerPrefs.SetInt("isNight", isNight);
        PlayerPrefs.SetFloat("locX", tLocX);
        PlayerPrefs.SetFloat("locY", tLocY);
        PlayerPrefs.SetFloat("locZ", tLocZ);
        /* 
        saveTimeHour = _TimeController.currentTime.Hour;
        saveTimeMin = _TimeController.currentTime.Minute;
        PlayerPrefs.SetFloat("currentHour", saveTimeHour);
        PlayerPrefs.SetFloat("currentMin", saveTimeMin);
        */
    }


    //Updates night and current time when reloading
    void OnEnable(){
        isNight = PlayerPrefs.GetInt("isNight");
        /*    
        saveTimeHour = PlayerPrefs.GetFloat("currentHour", saveTimeHour);
        saveTimeMin = PlayerPrefs.GetFloat("currentMin", saveTimeMin);
        _TimeController.currentTime.AddHours(saveTimeHour);
        _TimeController.currentTime.AddMinutes(saveTimeMin);
        */
        //player.transform.position = new Vector3(tLocX, tLocY, tLocZ);

    }

	// Use this for initialization
	void Start () {
        //currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        // -45 degrees from the world x axis
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;

        // Initial speed
        moveSpeed = walkSpeed;
    
        //movePoint.parent = null;
        footsteps = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
        timeElapsed += Time.deltaTime;
        // Movement
        if (Input.anyKey) {
            Move();
            footsteps.enabled = true;
        }
        else{
            footsteps.enabled = false;
        }

        if (_TimeController.currentTime.Hour >= 20 || _TimeController.currentTime.Hour < 8) {
            music.enabled = false;
            //Debug.Log(_TimeController.currentTime.Hour);
        }
        else{
            music.enabled = true;
        }

        targetLocation = player.transform.position;
        tLocX = targetLocation.x;
        tLocY = targetLocation.y;
        tLocZ = targetLocation.z;

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
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Bed")){
            isNight += 1;
            Debug.Log("Hit Bed -> Trigger Night Cycle");
        }
        if (other.gameObject.CompareTag("PlayerHouse")){
            music.PlayOneShot(doorSound, 1.0f);
            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene("Bedroom", LoadSceneMode.Single);
            }
            //Debug.Log("Player House -> Trigger Scene Change");
        }
        if (other.gameObject.CompareTag("PlayerDoor")){
            music.PlayOneShot(doorSound, 1.0f);
            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene("MainCity", LoadSceneMode.Single);
            }
            //Debug.Log("Player House -> Trigger Scene Change");
        }
        if (other.gameObject.CompareTag("TeaShop")){
            music.PlayOneShot(doorSound, 1.0f);
            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene("TeaShop", LoadSceneMode.Single);
            }
            //Debug.Log("Player House -> Trigger Scene Change");
        }
        if (other.gameObject.CompareTag("TeaShopDoor")){
            music.PlayOneShot(doorSound, 1.0f);
            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene("MainCity", LoadSceneMode.Single);
            }
            //Debug.Log("Player House -> Trigger Scene Change");
        }
    }



}