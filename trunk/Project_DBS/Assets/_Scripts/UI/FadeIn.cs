using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    UISprite uiSprite;

    [SerializeField]
    private float speed = 1.0f;

	// Use this for initialization
	void Awake() 
    {
        uiSprite = GetComponent<UISprite>();
	}
	
	// Update is called once per frame
	void Update() 
    {
        Color nowColor = uiSprite.color;
        Color finalColor = new Color(nowColor.r, nowColor.g, nowColor.b, 0.0f);
        uiSprite.color = Color.Lerp(nowColor, finalColor, Time.deltaTime * speed);
	}

    void OnLevelWasLoaded()
    {
        uiSprite.color = new Color(uiSprite.color.r, uiSprite.color.g, uiSprite.color.b, 1.0f);
    }
}
