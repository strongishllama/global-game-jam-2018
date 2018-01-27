using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class NetworkTransmitter:NetworkManager {
    // Use this for initialization
    NetworkClient mClient;
    [SerializeField] InputField inputfield;
    [SerializeField] Button connectButton;
    [SerializeField] Image phoneColour;
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

        connectButton.gameObject.SetActive(!IsClientConnected());

        if(Input.GetKeyDown(KeyCode.J)) {
            Debug.Log("Sending message");
            SendInput();
        }
#if UNITY_ANDROID || UNITY_IOS
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            SendInput();
        }
#endif

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
        mClient.RegisterHandler(InputMessageType.PlayerColour, ColourHandler);
        broadcastReceiver.StopBroadcast();
    }

    public void SendInput() {
        //NetworkServer.SendToAll(InputMessageType.Input,new InputMessage());
        //Debug.Log(mClient);
        mClient.Send(MsgType.Highest + 1,new InputMessage());
    }

    public void ColourHandler(NetworkMessage message) {
        var PlayerColour = message.ReadMessage<ColourMessage>();
        phoneColour.color = PlayerColour.color;
    }

    public override void OnClientConnect(NetworkConnection conn) {
        Debug.Log("Connected to server");
    }

    public override void OnClientDisconnect(NetworkConnection conn) {
        Debug.LogError("Disconnected");
    }

}




