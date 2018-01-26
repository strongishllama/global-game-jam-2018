using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiver:NetworkBehaviour {

    NetworkClient mClient;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(Input.anyKey) {
            
        }
    }
}


public class InputMessageType {
    public static short Input = MsgType.Highest + 1;
}


