using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour 
{
    public GameObject fadeOut;

    private bool used;

	// Use this for initialization
	void Start() 
    {
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void LoadGameScene(string LevelName)
    {
        if (!used)
        {
            if (LevelName == "Base")
            {
                GameManager.Instance.eGameType = GAME_TYPE.eBASE;
            }
            else
            {
                GameManager.Instance.eGameType = GAME_TYPE.eENDLESS;
            }

            used = true;
            GameObject fadeOutObj = Instantiate(fadeOut) as GameObject;
            fadeOutObj.GetComponent<FadeOut>().sceneToLoad = LevelName;
        }
    }

    public void LoadOptionScene()
    {
    }

    public void LoadCreditScene()
    {
    }
}
