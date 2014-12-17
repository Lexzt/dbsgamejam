using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour 
{
    UISprite uiSprite;

    public string sceneToLoad;

    [SerializeField]
    private float speed = 1.0f;

	// Use this for initialization
	void Start() 
    {
        uiSprite = GetComponent<UISprite>();
        uiSprite.depth = 100;
	}
	
	// Update is called once per frame
	void Update() 
    {
        Color nowColor = uiSprite.color;
        Color finalColor = new Color(nowColor.r, nowColor.g, nowColor.b, 1.0f);
        uiSprite.color = Color.Lerp(nowColor, finalColor, Time.deltaTime * speed);

        if (uiSprite.color.a >= 0.9f)
        {
            Application.LoadLevel(sceneToLoad);
        }
	}
}
