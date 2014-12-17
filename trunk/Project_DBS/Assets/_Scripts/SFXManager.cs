using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SFXManager : MonoBehaviour 
{
	private static 	SFXManager _instance;

	private float 	clipEnd; 

	[System.Serializable]
	public class SoundInfo 
	{
		public string		soundTag;
		public string		soundName;
		public AudioSource 	soundSource;
		public bool			isPlayNow;
		//public bool 		isDontDestroy;
	}

	public 	List<SoundInfo> _SoundsList	= new List<SoundInfo>();

	public static SFXManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SFXManager>();

				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	public void Destroy()
	{
		Destroy( this );
	}

	void Update ()
	{
		if ( GameObject.Find("Player") != null )
		{
			this.transform.position = GameObject.Find("Player").transform.position;
		}

		for ( int i = 0; i < _SoundsList.Count; i++ )
		{
			// To Start the Sound when playing Now is being checked...
			if ( _SoundsList[i].isPlayNow == true )
			{
				_SoundsList[i].soundSource.Play();

				_SoundsList[i].isPlayNow = false;
			}

			// To save the instance just for the next scene...
//			if ( _SoundsList[i].isDontDestroy == true )
//			{
//				DontDestroyOnLoad( this.gameObject );
//
//				if( this == _instance )
//				{
//					Destroy(this.gameObject);
//
//					_SoundsList[i].isDontDestroy = false;
//				}
//			}
		}
	}

	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;

			//DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	
	public void PlaySound( string TagSFX )
	{
		for ( int i = 0; i < _SoundsList.Count; i++ )
		{
			if ( _SoundsList[i].soundTag == TagSFX )
			{
				_SoundsList[i].soundSource.Play();
			}
		}
	}
	
	public void PlayOnceSound( string TagSFX )
	{
		if ( Time.time > clipEnd )
		{ // if previous clip not playing anymore...
			for ( int i = 0; i < _SoundsList.Count; i++ )
			{
				if ( _SoundsList[i].soundTag == TagSFX )
				{
					_SoundsList[i].soundSource.PlayOneShot( _SoundsList[i].soundSource.clip );

					clipEnd = Time.time + _SoundsList[i].soundSource.clip.length;
				}
			}
		}
	}

	public bool IsPlaying( string TagSFX )
	{
		for ( int i = 0; i < _SoundsList.Count; i++ )
		{
			if ( _SoundsList[i].soundTag == TagSFX )
			{
				return _SoundsList[i].soundSource.isPlaying;
			}
		}

		return false;
	}

	public void ChangeVolume ( string TagSFX, float soundVal )
	{
		for ( int i = 0; i < _SoundsList.Count; i++ )
		{
			if ( _SoundsList[i].soundTag == TagSFX )
			{
				_SoundsList[i].soundSource.volume = soundVal;
			}
		}
	}

	public void StopSound( string TagSFX )
	{
		for ( int i = 0; i < _SoundsList.Count; i++ )
		{
			if ( _SoundsList[i].soundTag == TagSFX )
			{
				_SoundsList[i].soundSource.Stop();
			}
		}
	}

    public void SetPitch( string TagSFX, float pitch )
    {
        for ( int i = 0; i < _SoundsList.Count; i++ )
        {
            if ( _SoundsList[i].soundTag == TagSFX )
            {
                _SoundsList[i].soundSource.pitch = pitch;
            }
        }
    }

    public float GetPitch( string TagSFX )
    {
        for (int i = 0; i < _SoundsList.Count; i++)
        {
            if (_SoundsList[i].soundTag == TagSFX)
            {
                return _SoundsList[i].soundSource.pitch;
            }
        }
        return -4.0f;
    }
}
