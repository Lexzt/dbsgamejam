using UnityEngine;
using System.Collections;

public class SlowTime : MonoBehaviour {

    public float StartTime;
    public float TimeSlowDuration = 5.0f;

	void Start () 
    {
        StartTime = Time.time;
        Time.timeScale = 0.5f;
	}
	
	void Update () 
    {
        if (Time.time - StartTime > TimeSlowDuration)
        {
            Time.timeScale = 1f;
        }
	}
}
