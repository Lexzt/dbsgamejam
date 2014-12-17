using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private float movingSpeed = 5.0f;
    [SerializeField] private float yLimitPercentage = 75.0f;

    private bool automove;
    private float yPositionLimit;
    private float cameraHeight;
    private float cameraYBottom;

    //-----------------------
    public GameObject player;

	// Use this for initialization
	void Start() 
    {
        //------------------------------------
        player.rigidbody2D.isKinematic = true;

        cameraHeight = (camera.pixelHeight * 0.01f);
        CalculateYPositionLimit();

        InvokeRepeating("CheckForAutoMove", 0.1f, 0.02f);
	}
	
	// Update is called once per frame
	void Update() 
    {
        //-------------------------
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);
        }

        if (automove)
        {
            MoveCameraToMeetLimit();
        }
	}

    private void CheckForAutoMove()
    {
        if (player.transform.position.y >= yPositionLimit)
        {
            automove = true;
            CancelInvoke("CheckForAutoMove");
            InvokeRepeating("MoveCameraConsistent", 0.01f, 0.1f);
        }
    }

    private void MoveCameraConsistent()
    {
        transform.position += new Vector3(0.0f, movingSpeed, 0.0f);
        CalculateYPositionLimit();
    }

    private void MoveCameraToMeetLimit()
    {
        if (player.transform.position.y >= yPositionLimit)
        {
            float offset = player.transform.position.y - yPositionLimit;
            Vector3 from = transform.position;
            Vector3 to = from + new Vector3(0.0f, offset, 0.0f);
            transform.position = Vector3.Lerp(from, to, Time.deltaTime * 1.5f);
        }
    }

    private void CalculateYPositionLimit()
    {
        cameraYBottom = transform.position.y - (cameraHeight * 0.5f);
        yPositionLimit = cameraYBottom + (yLimitPercentage * 0.01f * cameraHeight);
    }
}
