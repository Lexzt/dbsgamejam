using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    public static InputController instance;

    public GameObject PlayerObj;

    public float inAirSpeed = 4.0f;
    public float onGroundSpeed = 15.0f;
    public float JumpHeight = 50.0f;

    public bool LeftBounce = false;
    public bool RightBounce = false;

    [SerializeField]
    private bool AbleToJump = true;
    private Vector3 OriginalDirecton;

    void Awake()
    {
        instance = this;
    }

	void Start () 
    {
        //Debug.Log("Device Orientation: " + Input.deviceOrientation);
        OriginalDirecton = Input.acceleration;
	}
	
	void Update () 
    {
        Vector2 Force = new Vector2((Input.acceleration.x * onGroundSpeed), 0.0f);

        if (AbleToJump)
        {
            if (Input.touchCount == 1)
            {
                Force = new Vector2((Input.acceleration.x * inAirSpeed), JumpHeight);
                AbleToJump = false;
            }
        }

        if (RightBounce)
        {
            if (Input.touchCount == 1)
            {
                Debug.Log("New Force Added!");
                Force = new Vector2(-50f, JumpHeight);
                RightBounce = false;
            }
        }
        else if (LeftBounce)
        {
            if (Input.touchCount == 1)
            {
                Force = new Vector2(50f, JumpHeight);
                LeftBounce = false;
            }
        }

        PlayerObj.rigidbody2D.AddForce(Force);
	}

    public void ResetJump()
    {
        AbleToJump = true;
        LeftBounce = false;
        RightBounce = false;
    }
    
}
