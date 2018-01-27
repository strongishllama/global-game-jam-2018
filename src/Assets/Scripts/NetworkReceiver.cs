using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiver:NetworkManager {
    NetworkClient mClient;

    private void Start() {

        autoCreatePlayer = false;
        var config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        NetworkServer.Configure(config, 12);
        StartHost();

        mClient = new NetworkClient();
        mClient.RegisterHandler(MsgType.Highest, HandleMessage);
        mClient.Connect("127.0.0.1",7777);

    }

    void HandleMessage(NetworkMessage msg) {
        var InputMessage = msg.ReadMessage<InputMessage>();
        Debug.Log(msg.msgType);
    }

    public override void OnServerConnect(NetworkConnection conn) {
        mClient = new NetworkClient();
        mClient.RegisterHandler(MsgType.Highest,HandleMessage);
        mClient.Connect("127.0.0.1",7777);

        Debug.LogError(conn.address);
    }


}


public class InputMessageType {
    public static short Input = MsgType.Highest + 1;
}


