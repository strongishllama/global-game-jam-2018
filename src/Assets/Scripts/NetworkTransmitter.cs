using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class NetworkTransmitter:NetworkManager {
    // Use this for initialization
    NetworkClient mClient;
    [SerializeField] InputField inputfield;
    InputMessage inputMessage;
    void Start() {
        inputMessage = new InputMessage();
        autoCreatePlayer = false;
       // mClient = StartClient();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("Sending message");
            SendInput();
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            SendInput();
        }

    }
    public void NetworkStartClient(GameObject button) {
        networkAddress = inputfield.text;
        inputfield.gameObject.SetActive(false);
        button.SetActive(false);
        mClient = StartClient();

    }

    public void SendInput() {
        //NetworkServer.SendToAll(InputMessageType.Input,new InputMessage());
        mClient.Send(MsgType.Highest + 1, inputMessage);
    }

    public override void OnClientConnect(NetworkConnection conn) {
        Debug.Log("Connected to server");
    }

}

public class InputMessage:MessageBase {
    //i mean its just an empty class
}
