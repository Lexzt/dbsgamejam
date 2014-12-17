using UnityEngine;
using System.Collections;

public enum CANNON_SHOOT_TYPE
{
    eBASIC,
    eMULTIPLE,
    eDESTRUCTIVE,
}

public class CannonController : MonoBehaviour 
{
    private CANNON_SHOOT_TYPE CannonType = CANNON_SHOOT_TYPE.eBASIC;

    public GameObject bullet;

    [SerializeField]
    private bool canRotateCannon;
    [SerializeField]
    private bool canFireBullet;

    public LineRenderer LineRender;

    public float ShootAngle = 20.0f;

    //public float PowerUp_StartTime;
    public float PowerUp_EndTime;
    public bool PowerUp_TimerStarted = false;

    void Start()
    {
        LineRender.SetVertexCount(2);
    }
	
	// Update is called once per frame
	void Update() 
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (Time.timeScale == 1)
        //    {
        //        Time.timeScale = 0.1f;
        //    }
        //    else
        //    {
        //        Time.timeScale = 1f;
        //    }
        //}

        if (PowerUp_TimerStarted)
        {
            if (Time.time > PowerUp_EndTime)
            {
                CannonType = CANNON_SHOOT_TYPE.eBASIC;
                PowerUp_TimerStarted = false;
            }
        }

        if (canRotateCannon)
        {
            RotateCannon();
        }
        if (canFireBullet)
        {
            FireBullet();
        }
	}

    private void RotateCannon()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = mouseWorldPosition - transform.position;
        offset.Normalize();
        offset *= 360.0f;
        transform.eulerAngles = new Vector3(0.0f, offset.y, 0.0f);
        transform.up = offset;
        transform.eulerAngles = new Vector3(0.0f, 0.0f, transform.eulerAngles.z);

        float Mag = (new Vector2(mouseWorldPosition.x, mouseWorldPosition.y) - new Vector2(transform.position.x, transform.position.y)).magnitude + 0.5f;

        Vector3 Pos1 = new Vector3(transform.position.x, transform.position.y, -1.0f);
        Vector3 Pos2 = new Vector3(transform.position.x + transform.up.x * Mag, transform.position.y + transform.up.y * Mag, -1.0f);

        LineRender.SetPosition(0, Pos1);
        LineRender.SetPosition(1, Pos2);
    }

    private void FireBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(GameManager.Instance.eGameType == GAME_TYPE.eBASE)
            {
                GameObject bulletSpawned = Instantiate(bullet, transform.position + (transform.up * 0.75f), Quaternion.identity) as GameObject;
                bulletSpawned.GetComponent<BulletBehaviour>().direction = transform.up;
                SFXManager.instance.PlaySound("CannonShot");
            }
            else if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
            {
                if(CannonType == CANNON_SHOOT_TYPE.eBASIC)
                {
                    GameObject bulletSpawned = Instantiate(bullet, transform.position + (transform.up * 0.75f), Quaternion.identity) as GameObject;
                    bulletSpawned.GetComponent<BulletBehaviour>().direction = transform.up;
                    SFXManager.instance.PlaySound("CannonShot");
                }
                else if(CannonType == CANNON_SHOOT_TYPE.eMULTIPLE)
                {
                    float Angle = Mathf.Deg2Rad * ShootAngle;

                    GameObject bulletSpawned1 = Instantiate(bullet, transform.position + (transform.up * 0.75f), Quaternion.identity) as GameObject;
                    GameObject bulletSpawned2 = Instantiate(bullet, transform.position + (transform.up * 0.75f), Quaternion.identity) as GameObject;
                    GameObject bulletSpawned3 = Instantiate(bullet, transform.position + (transform.up * 0.75f), Quaternion.identity) as GameObject;

                    Vector2 Direction = transform.up;
                    bulletSpawned1.GetComponent<BulletBehaviour>().direction = Direction;

                    Direction = new Vector2(Direction.x * Mathf.Cos(Angle) - Direction.y * Mathf.Sin(Angle), 
                                            Direction.x * Mathf.Sin(Angle) + Direction.y * Mathf.Cos(Angle));
                    bulletSpawned2.GetComponent<BulletBehaviour>().direction = Direction;

                    Direction = transform.up;
                    Direction = new Vector2(Direction.x * Mathf.Cos(-Angle) - Direction.y * Mathf.Sin(-Angle), 
                                            Direction.x * Mathf.Sin(-Angle) + Direction.y * Mathf.Cos(-Angle));
                    bulletSpawned3.GetComponent<BulletBehaviour>().direction = Direction;
                    SFXManager.instance.PlaySound("CannonShot");
                }
                else if(CannonType == CANNON_SHOOT_TYPE.eDESTRUCTIVE)
                {
                    GameObject bulletSpawned1 = Instantiate(bullet, transform.position + (transform.up * 0.75f), Quaternion.identity) as GameObject;
                    bulletSpawned1.GetComponent<BulletBehaviour>().direction = transform.up;
                    bulletSpawned1.GetComponent<BulletBehaviour>().Destructive = true;
                }
            }
        }
    }

    public void SetRotateCannon(bool value)
    {
        canRotateCannon = value;
    }

    public void SetFireBullet(bool value)
    {
        canFireBullet = value;
    }

    public void SetState(CANNON_SHOOT_TYPE ShootType)
    {
        CannonType = ShootType;
    }

    public void SetAndStartTimer(float NewTime)
    {
        PowerUp_EndTime = Time.time + NewTime;
        PowerUp_TimerStarted = true;
    }
}
