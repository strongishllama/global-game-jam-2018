using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

    private static PlayMusic m_Music = null;

    public AudioClip m_Clip;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(gameObject);
		if(m_Music == null) {
            m_Music = this;

            AudioSource audioS = gameObject.AddComponent<AudioSource>();
            audioS.clip = m_Clip;
            audioS.loop = true;
            audioS.Play();
        } else {
            Destroy(gameObject);
        }

    }

}
