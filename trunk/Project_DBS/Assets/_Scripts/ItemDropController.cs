using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CRATE_TYPE
{
    NONE = -1,
    TETRISWALL,
    SLOWTIME, 
    MULTIPLEBULLETS,
    DESTRUCTIVEBULLETS,
};

public class ItemDropController : MonoBehaviour {

    public GameObject CrateObject;

    // Drop Rate is Over 100. 100 being Sure drop
    public float DropRate = 20f;
    public bool HitOnce = true;

    public List<float> SpawnRateChance;
    public List<Sprite> ListOfCrateImages;

    private static List<GameObject> spawnedCrates = new List<GameObject>();

    private Vector3 OriginalScale;

	void Start () 
    {
        OriginalScale = CrateObject.transform.localScale;
	}
	
	void Update () 
    {
	    
	}

    public void DropItem()
    {
        if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
        {
            if (HitOnce)
            {
                HitOnce = false;
                int Chance = Random.Range(0, 100);
                if (Chance < DropRate)
                {
                    GameObject Obj = Instantiate(CrateObject, transform.position, Quaternion.identity) as GameObject;

                    Chance = Random.Range(0, 100);
                    for (int i = 0; i < SpawnRateChance.Count; i++)
                    {
                        if (Chance < SpawnRateChance[i])
                        {
                            CRATE_TYPE type = (CRATE_TYPE)i;
                            Obj.GetComponent<CrateScript>().Crate_Data = type;
                            Obj.GetComponent<SpriteRenderer>().sprite = ListOfCrateImages[i];
                            Obj.transform.localScale = OriginalScale;
                            spawnedCrates.Add(Obj);
                            break;
                        }
                    }
                }
            }
        }
    }

    public static void DestroyAllCrates()
    {
        while (spawnedCrates.Count > 0)
        {
            GameObject block = spawnedCrates[0];
            spawnedCrates.RemoveAt(0);
            Destroy(block);
        }
    }
}
