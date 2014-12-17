using UnityEngine;
using System.Collections;

public enum GAME_TYPE
{
    eNONE = -1,
    eBASE,
    eENDLESS,
};

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    public GAME_TYPE eGameType = GAME_TYPE.eBASE;

    void Awake()
    {
        if (Instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        //if (eGameType == GAME_TYPE.eBASE && LevelLoaded)
        //{
        //    // You need to do the Win Lose Condition here for if time is > 60 Seconds or whatever.
        //}
        //else if (eGameType == GAME_TYPE.eENDLESS && LevelLoaded)
        //{
        //    if (SetTime)
        //    {
        //        CreatedTime = Time.time;
        //        SetTime = false;
        //    }

        //    if (Time.time - CreatedTime > TimeBeforeUpdateInterval)
        //    {
        //        SpawnController.Instance.spawnInterval -= 0.4f;
        //        SpawnController.Instance.StopSpawn();
        //        SpawnController.Instance.StartSpawn(0.0f);
        //    }
        //}
	}
}
