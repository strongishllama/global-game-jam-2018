using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkTransmitter:NetworkManager {
    // Use this for initialization
    NetworkClient mClient;
    void Start() {
        autoCreatePlayer = false;
        mClient = StartClient();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("Sending message");
            SendInput();
        }
    }

    public void SendInput() {
        NetworkServer.SendToAll(InputMessageType.Input,new InputMessage());
        mClient.Send(MsgType.Highest, new InputMessage());
    }

    public override void OnClientConnect(NetworkConnection conn) {
        Debug.Log("Connected to server");
    }

}

public class InputMessage:MessageBase {
    //i mean its just an empty class
}
