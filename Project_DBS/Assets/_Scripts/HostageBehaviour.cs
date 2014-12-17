using UnityEngine;
using System.Collections;

public class HostageBehaviour : MonoBehaviour 
{
    public void DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
