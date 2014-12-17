using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour 
{
    public GameObject hitParticle;
    public GameObject endlessParticle;

    [SerializeField]
    private float SetTime = 0.0f;
    [SerializeField]
    private float ResetTimeVal = 3f;
    [SerializeField]
    private bool TimeSet = false;

    private float BlockHealth = 3f;
	
	private int scaleStage;
    private static Vector3 scaleToValue = new Vector3(1.15f, 1.15f, 1.0f);
    private static float scaleSpeedDec = 5.0f;
    private static float scaleSpeedInc = 20.0f;
    private static float colorSpeedDec = 30.0f;
    private static float colorSpeedInc = 60.0f;
	
	private SpriteRenderer spriteRenderer;
	
	void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.tag == "Bullet")
        {
            //Get direction that bullet is travelling to know direction to add force
            Vector3 direction = collision2D.gameObject.GetComponent<BulletBehaviour>().direction;
            float force = 150.0f;

            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = new Vector2();
                rigidbody2D.AddForce(direction * force);
                rigidbody2D.GetComponent<ItemDropController>().DropItem();
            }
            else
            {
                transform.parent.rigidbody2D.velocity = new Vector2();
                transform.parent.rigidbody2D.AddForce(direction * force);
                transform.parent.rigidbody2D.GetComponent<ItemDropController>().DropItem();
            }

            if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
            {
                if (collision2D.gameObject.GetComponent<BulletBehaviour>().Destructive)
                {
                    BlockHealth--;
                    if (BlockHealth <= 0)
                    {
                        // Spawn particle effect as hit block feedback
                        Instantiate(endlessParticle, transform.position, Quaternion.identity);

                        // Destroy Object itself
                        Destroy(gameObject);
                    }
                }
            }

            //Spawn particle effect as hit block feedback
            Instantiate(hitParticle, collision2D.gameObject.transform.position, Quaternion.identity);
            //Destroy bullet
            Destroy(collision2D.gameObject);
			
			SFXManager.instance.PlaySound("HitImpact");
			
			scaleStage = 1;
        }
        else if (collision2D.collider.tag == "Hostage")
        {
            //Game ended as hostage is hit
            GameObject.Find("GameController").GetComponent<GameController>().GameOver();
            //Stop spawning blocks - Game ended
            GameObject.Find("SpawnController").GetComponent<SpawnController>().StopSpawn();
            //Feedback that hostage died
            collision2D.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        //else if (collision2D.collider.tag == "Ground")
        //{
        //    if (collision2D.rigidbody == null)
        //    {
        //        collision2D.transform.parent.rigidbody.isKinematic = true;
        //        collision2D.transform.parent.rigidbody.velocity = new Vector2(0.0f, 0.0f);
        //    }
        //    else
        //    {
        //        collision2D.rigidbody.isKinematic = true;
        //        collision2D.rigidbody.velocity = new Vector2(0.0f, 0.0f);
        //    }

        //    if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS && TimeSet == false)
        //    {
        //        TimeSet = true;
        //        SetTime = Time.time;
        //    //}
        //}
        //else if (collision2D.collider.tag == "Borders")
        //{
        //    if (collision2D.rigidbody == null)
        //    {
        //        collision2D.transform.parent.rigidbody.velocity = new Vector2(0.0f, collision2D.transform.parent.rigidbody.velocity.y);
        //    }
        //    else
        //    {
        //        collision2D.rigidbody.velocity = new Vector2(0.0f, collision2D.rigidbody.velocity.y);
        //    }
        //}
        //else if(collision2D.collider.tag == "BordersTop")
        //{
        //    if (collision2D.rigidbody == null)
        //    {
        //        collision2D.transform.parent.rigidbody.velocity = new Vector2(collision2D.transform.parent.rigidbody.velocity.x, 0.0f);
        //    }
        //    else
        //    {
        //        collision2D.rigidbody.velocity = new Vector2(collision2D.rigidbody.velocity.x,0.0f);
        //    }
        //}
        //else if (collision2D.collider.tag == "Tiles")
        //{
        //    if (collision2D.rigidbody == null)
        //    {
        //        collision2D.transform.parent.rigidbody.velocity /= 3;
        //    }
        //    else
        //    {
        //        collision2D.rigidbody.velocity /= 3;
        //    }
        //}
    }

    void Update()
    {
        if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
        {
            if (TimeSet)
            {
                if (Time.time - SetTime > ResetTimeVal)
                {
                    // Spawn particle effect as hit block feedback
                    Instantiate(endlessParticle, transform.position, Quaternion.identity);

                    // Destroy Object itself
                    Destroy(gameObject);
                }
            }
        }
		
		if (scaleStage == 1)
        {
            iTween.ScaleTo(gameObject, iTween.Hash( "scale", scaleToValue,
                                                    "speed", scaleSpeedInc, 
                                                    "easetype", iTween.EaseType.easeInCubic,
                                                    "oncomplete", "OnCompleteStage1Scale",
                                                    "oncompletetarget", gameObject));
            Color current = spriteRenderer.color;
            Color goal = Color.gray;
            spriteRenderer.color = Color.Lerp(current, goal, colorSpeedInc * Time.deltaTime);
        }
        else if (scaleStage == 2)
        {
            iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.one,
                                                    "speed", scaleSpeedDec,
                                                    "easetype", iTween.EaseType.easeInCubic,
                                                    "oncomplete", "OnCompleteStage2Scale",
                                                    "oncompletetarget", gameObject));
            Color current = spriteRenderer.color;
            Color goal = Color.white;
            spriteRenderer.color = Color.Lerp(current, goal, colorSpeedDec * Time.deltaTime);
        }
    }
	
	void OnCompleteStage1Scale()
    {
        scaleStage = 2;
    }

    void OnCompleteStage2Scale()
    {
        scaleStage = 0;
    }
}
