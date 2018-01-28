using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainMenuAfterTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5);
	}


    void OnDestroy() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
