using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GameObject fadeOut;
    public GameObject winLoseWindow;
    public UILabel winLoseWindowTitle;

    private float startGameTime;

    [SerializeField]
    private UILabel gameTime;

    public GameObject PauseUI;

    // Endless 
    // Increase Diffculity Rate
    public float TimeBeforeUpdateInterval = 10.0f;
	private float initSpawnInterval;

    public float leastSpawnInterval = 1.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseUI.active)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void StartGame()
    {
        startGameTime = Time.time;
        if (GameManager.Instance.eGameType == GAME_TYPE.eBASE)
        {
            InvokeRepeating("UpdateGameTime", 0.01f, 0.02f);
        }
        else if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
        {
            InvokeRepeating("UpdateGameTimeEndless", 0.01f, TimeBeforeUpdateInterval);
        }

        CannonController cannonController = GameObject.Find("Cannon").GetComponent<CannonController>();
        cannonController.SetRotateCannon(true);
        cannonController.SetFireBullet(true);
        SpawnController spawnController = GameObject.Find("SpawnController").GetComponent<SpawnController>();
        spawnController.StartSpawn(0.01f);
        spawnController.DestroyAllBlocks();
        initSpawnInterval = spawnController.spawnInterval;
        ItemDropController.DestroyAllCrates();
        GameObject.Find("Hostage").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("Hostage").GetComponent<BoxCollider2D>().enabled = true;
        Screen.showCursor = false;
    }

    public void StopGame()
    {
        if (GameManager.Instance.eGameType == GAME_TYPE.eBASE)
        {
            CancelInvoke("UpdateGameTime");
        }
        else if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
        {
            CancelInvoke("UpdateGameTimeEndless");
        }

        CannonController cannonController = GameObject.Find("Cannon").GetComponent<CannonController>();
        cannonController.SetRotateCannon(false);
        cannonController.SetFireBullet(false);
        SpawnController spawnController = GameObject.Find("SpawnController").GetComponent<SpawnController>();
        spawnController.StopSpawn();
        GameObject.Find("Hostage").GetComponent<BoxCollider2D>().enabled = false;
        spawnController.spawnInterval = initSpawnInterval;
    }

	public void GameOver()
    {
        winLoseWindow.SetActive(true);

        if (GameManager.Instance.eGameType == GAME_TYPE.eBASE)
        {
            winLoseWindowTitle.text = "YOU LOST!";
        }
        else if (GameManager.Instance.eGameType == GAME_TYPE.eENDLESS)
        {
            float timeScore = Time.time - startGameTime;
            if (timeScore >= 3600.0f)
            {
                int hours = (int)(timeScore / 3600.0f);
                int mins = (int)((timeScore - (hours * 3600.0f)) / 60.0f);
                float secs = timeScore - (hours * 3600.0f) - (mins * 60.0f);
                winLoseWindowTitle.text = hours.ToString() + " h " + mins.ToString() + " m " + secs.ToString("F2") + " s";
            }
            else if (timeScore >= 60.0f)
            {
                int mins = (int)(timeScore / 60.0f);
                float secs = timeScore - (mins * 60.0f);
                winLoseWindowTitle.text = mins.ToString() + " m " + secs.ToString("F2") + " s";
            }
            else
            {
                winLoseWindowTitle.text = timeScore.ToString("F2") + " s";
            }
        }
        
        StopGame();
        Screen.showCursor = true;
    }

    public void GameWin()
    {
        winLoseWindow.SetActive(true);
        winLoseWindowTitle.text = "YOU WON!";
        StopGame();
        Screen.showCursor = true;
    }
	
    private void UpdateGameTime()
    {
        float timeLeft = startGameTime + 60.0f - Time.time;

        if (timeLeft <= 0.0f)
        {
            gameTime.text = "0.0";
            GameWin();
        }
        else
        {
            gameTime.text = timeLeft.ToString("F2");
        }
    }

    private void UpdateGameTimeEndless()
    {
        SpawnController.Instance.spawnInterval -= 0.4f;
        if (SpawnController.Instance.spawnInterval < leastSpawnInterval)
        {
            SpawnController.Instance.spawnInterval = leastSpawnInterval;
            CancelInvoke("UpdateGameTimeEndless");
        }
        SpawnController.Instance.StopSpawn();
        SpawnController.Instance.StartSpawn(0.0f);
    }

    public void Pause()
    {
        GameObject.Find("Cannon").GetComponent<CannonController>().SetRotateCannon(false);
        GameObject.Find("Cannon").GetComponent<CannonController>().SetFireBullet(false);
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        Screen.showCursor = true;
    }

    public void UnPause()
    {
        GameObject.Find("Cannon").GetComponent<CannonController>().SetRotateCannon(true);
        GameObject.Find("Cannon").GetComponent<CannonController>().SetFireBullet(true);
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        Screen.showCursor = false;
    }

    public void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        GameObject fadeOutObj = Instantiate(fadeOut) as GameObject;
        fadeOutObj.GetComponent<FadeOut>().sceneToLoad = "MainMenu";
    }
}
