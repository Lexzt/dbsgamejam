using UnityEngine;
using System.Collections;

public class TetrisWallTrigger : MonoBehaviour {

    public float SpawnDuration = 5.0f;
    private float SpawnTime;

    public GameObject endlessParticle;

	void Start () 
    {
        SpawnTime = Time.time;
	}
	
	void Update () 
    {
        if (Time.time - SpawnTime > SpawnDuration)
        {
            Destroy(transform.parent.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tiles")
        {
            // Spawn particle effect as hit block feedback
            if (other.rigidbody2D == null)
            {
                Instantiate(endlessParticle, other.transform.parent.position, Quaternion.identity);
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                Instantiate(endlessParticle, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }
}
