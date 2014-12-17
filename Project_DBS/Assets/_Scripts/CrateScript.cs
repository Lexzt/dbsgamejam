using UnityEngine;
using System.Collections;

public class CrateScript : MonoBehaviour {

    [HideInInspector]
    public CRATE_TYPE Crate_Data = CRATE_TYPE.NONE;

    private Vector3 PlayerPosition;

    public GameObject TetrisWall;
    public GameObject SlowTime;

    private GameObject Cannon;

    public float LengthOfMultiple = 3f;
    public float LengthOfDestructive = 3f;

	void Start () 
    {
        PlayerPosition = GameObject.Find("Hostage").transform.position;
        Cannon = GameObject.Find("Cannon");
	}
	
	void Update () 
    {
	
	}
    
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.tag == "Bullet")
        {
            ProcessCrateData();

            Destroy(collision2D.gameObject);
            Destroy(this.gameObject);
        }
    }

    void ProcessCrateData()
    {
        switch(Crate_Data)
        {
            case CRATE_TYPE.TETRISWALL:
                Instantiate(TetrisWall, Vector3.zero, Quaternion.identity);
                break;
            case CRATE_TYPE.SLOWTIME:
                Instantiate(SlowTime, Vector3.zero, Quaternion.identity);
                break;
            case CRATE_TYPE.MULTIPLEBULLETS:
                Cannon.GetComponent<CannonController>().SetState(CANNON_SHOOT_TYPE.eMULTIPLE);
                Cannon.GetComponent<CannonController>().SetAndStartTimer(LengthOfMultiple);
                break;
            case CRATE_TYPE.DESTRUCTIVEBULLETS:
                Cannon.GetComponent<CannonController>().SetState(CANNON_SHOOT_TYPE.eDESTRUCTIVE);
                Cannon.GetComponent<CannonController>().SetAndStartTimer(LengthOfDestructive);
                break;
            default:
                break;
        }
    }
}
