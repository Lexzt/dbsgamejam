using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour 
{
    public Vector3 direction;
    public float speed;
    public bool Destructive = false;
	
	// Update is called once per frame
	void Update() 
    {
        transform.position += direction * speed * Time.deltaTime;
	}
}
