using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkStarter:NetworkManager {

    private void Start() {
        StartHost();
    }

}
