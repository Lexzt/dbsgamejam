using UnityEngine;
using System.Collections;

public class BorderController : MonoBehaviour 
{
    public GameObject hitParticle;
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider.tag == "Bullet")
        {
            //Spawn particle effect as hit wall feedback
            Instantiate(hitParticle, collision2D.gameObject.transform.position, Quaternion.identity);
            //Destroy bullet
            Destroy(collision2D.gameObject);
			
			SFXManager.instance.PlaySound("HitImpact");
        }
        else if (collision2D.collider.tag == "Tiles")
        {
            //ContactPoint2D[] Array = collision2D.contacts;
            //for (int i = 0; i < Array.Length; i++ )
            //{
            //    Debug.Log(i + " " + Array[i].point);
            //}
        }
    }
}
