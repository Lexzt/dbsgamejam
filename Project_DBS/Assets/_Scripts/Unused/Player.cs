using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private float LeftRightDist = 0.25f;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        float LeftRightDist = 0.25f;
        Vector3 Pos = transform.position + new Vector3(LeftRightDist, 0.0f, 0.0f);
        Vector3 InvPos = transform.position + new Vector3(-LeftRightDist, 0.0f, 0.0f);
        Vector3 PosUp = transform.position + new Vector3(LeftRightDist, LeftRightDist, 0.0f);
        Vector3 InvPosUp = transform.position + new Vector3(-LeftRightDist, LeftRightDist, 0.0f);

        Debug.DrawLine(Pos, Pos + Vector3.down * 0.4f);
        Debug.DrawLine(InvPos, InvPos + Vector3.down * 0.4f);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.4f);
        Debug.DrawLine(PosUp, PosUp + Vector3.right * 0.05f);
        Debug.DrawLine(InvPosUp, InvPosUp + -Vector3.right * 0.05f);

        transform.rotation = Quaternion.identity;
	}

    void FixedUpdate()
    {
        bool ContinueCheck = true;
        Vector3 Pos = transform.position + new Vector3(LeftRightDist, 0.0f, 0.0f);
        Vector3 InvPos = transform.position + new Vector3(-LeftRightDist, 0.0f, 0.0f);
        Vector3 PosUp = transform.position + new Vector3(LeftRightDist, LeftRightDist, 0.0f);
        Vector3 InvPosUp = transform.position + new Vector3(-LeftRightDist, LeftRightDist, 0.0f);

        RaycastHit2D hitLeft = Physics2D.Raycast(Pos, -Vector2.up, 0.4f);
        RaycastHit2D hitRight = Physics2D.Raycast(InvPos, -Vector2.up, 0.4f);
        RaycastHit2D hitMid = Physics2D.Raycast(transform.position, -Vector2.up, 0.4f);

        if (hitLeft.collider != null && ContinueCheck)
        {
            if (hitLeft.collider.gameObject.tag == "Ground" || hitLeft.collider.gameObject.tag == "Tiles")
            {
                InputController.instance.ResetJump();
                ContinueCheck = false;
            }
        }

        if (hitRight.collider != null && ContinueCheck)
        {
            if (hitRight.collider.gameObject.tag == "Ground" || hitRight.collider.gameObject.tag == "Tiles")
            {
                InputController.instance.ResetJump();
                ContinueCheck = false;
            }
        }

        if (hitMid.collider != null && ContinueCheck)
        {
            if (hitMid.collider.gameObject.tag == "Ground" || hitMid.collider.gameObject.tag == "Tiles")
            {
                InputController.instance.ResetJump();
                ContinueCheck = false;
            }
        }

        if (ContinueCheck == true)
        {
            hitRight = Physics2D.Raycast(PosUp, Vector2.right, 0.05f);
            hitLeft = Physics2D.Raycast(InvPosUp, -Vector2.right, 0.05f);

            if (hitRight.collider != null)
            {
                if (hitRight.collider.gameObject.tag == "Ground")
                {
                    InputController.instance.RightBounce = true;
                }
            }

            if (hitLeft.collider != null)
            {
                if (hitLeft.collider.gameObject.tag == "Ground")
                {
                    InputController.instance.LeftBounce = true;
                }
            }
        }
    }
}
