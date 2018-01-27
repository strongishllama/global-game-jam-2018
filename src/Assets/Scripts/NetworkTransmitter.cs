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
    ReceiveBroadcast broadcastReceiver;
    void Start() {
        networkPort = 7776;
        inputMessage = new InputMessage();
        autoCreatePlayer = false;
        broadcastReceiver = this.gameObject.AddComponent<ReceiveBroadcast>();
        // mClient = StartClient();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("Sending message");
            SendInput();
        }

        // if (Input.GetTouch(0).phase == TouchPhase.Began) {
        //     SendInput();
        // }

    }
    public void NetworkStartClient(GameObject button) {
        networkAddress = inputfield.text;
        if(!IsClientConnected()) {
            broadcastReceiver.connected = false;
            broadcastReceiver.Initialize();
            broadcastReceiver.StartAsClient();
        }
        // inputfield.gameObject.SetActive(false);
        // button.SetActive(false);
        //mClient = StartClient();

    }

    public void StartConnection() {
        mClient = StartClient();
    }
    public void SendInput() {
        //NetworkServer.SendToAll(InputMessageType.Input,new InputMessage());
        //Debug.Log(mClient);
        mClient.Send(MsgType.Highest + 1,new InputMessage());
    }

    public override void OnClientConnect(NetworkConnection conn) {
        Debug.Log("Connected to server");
    }

    public override void OnClientDisconnect(NetworkConnection conn) {
        Debug.LogError("Disconneceted");
    }

}

public class ReceiveBroadcast:NetworkDiscovery {
    NetworkTransmitter mManager;
    public bool connected;
    public void Awake() {
        mManager = this.GetComponent<NetworkTransmitter>();
        showGUI = true;
        //Initialize();
    }

    public override void OnReceivedBroadcast(string fromAddress,string data) {
        if(!connected) {
            mManager.networkAddress = fromAddress;
            Debug.LogError(fromAddress);
            mManager.StartConnection();
            connected = true;
        }

        //StopBroadcast();
    }

}

public class InputMessage:MessageBase {
    //i mean its just an empty class
}
