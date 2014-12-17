using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour 
{
    public static SpawnController Instance;

    public GameObject[] spawnableBlocks;
    public float columnLength;
    public int numberOfColumns;
    public float spawnInterval;
    public float y;

    [SerializeField]
    private bool isSpawning;
    private float[] rotationAngles;
    private float halfFieldWidth;
    private List<GameObject> spawnedBlocks;

    void Awake ()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start() 
    {
        spawnedBlocks = new List<GameObject>();

        rotationAngles = new float[4];
        rotationAngles[0] = 0.0f;
        rotationAngles[1] = 90.0f;
        rotationAngles[2] = 180.0f;
        rotationAngles[3] = 270.0f;

        if (isSpawning)
        {
            InvokeRepeating("InitiateSpawn", 0.01f, spawnInterval);
        }

        halfFieldWidth = columnLength * numberOfColumns * 0.5f;
	}


    public void StartSpawn(float timeBeforeStart)
    {
        InvokeRepeating("InitiateSpawn", timeBeforeStart, spawnInterval);
        isSpawning = true;
    }

    public void StopSpawn()
    {
        CancelInvoke("InitiateSpawn");
        isSpawning = false;
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }

    private void InitiateSpawn()
    {
        int spawnBlockIndex = GetRandomSpawnBlockIndex();
        int rotationAngleIndex = GetRandomRotationAngleIndex();

        float x = GetRandomSpawnXPosition();
        float rotationZ = rotationAngles[rotationAngleIndex];

        Spawn(spawnBlockIndex, x, rotationZ);
    }

    private void Spawn(int spawnBlockIndex, float x, float rotationZ)
    {
        GameObject block = Instantiate(spawnableBlocks[spawnBlockIndex]) as GameObject;
        block.transform.position = new Vector3(x, y, 0.0f);
        block.transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationZ);
        spawnedBlocks.Add(block);
    }

    private int GetRandomSpawnBlockIndex()
    {
        return Random.Range(0, spawnableBlocks.Length - 1);
    }

    private float GetRandomSpawnXPosition()
    {
        return Random.Range(-halfFieldWidth, halfFieldWidth);
    }

    private int GetRandomRotationAngleIndex()
    {
        return Random.Range(0, 3);
    }

    public void DestroyAllBlocks()
    {
        while (spawnedBlocks.Count > 0)
        {
            GameObject block = spawnedBlocks[0];
            spawnedBlocks.RemoveAt(0);
            Destroy(block);
        }
    }
}
