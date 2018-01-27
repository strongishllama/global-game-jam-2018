using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiver:NetworkManager {
    NetworkClient mClient;

    private void Start() {

        autoCreatePlayer = false;
        StartHost();
        NetworkServer.RegisterHandler(MsgType.Highest + 1, HandleMessage);

    }

    void HandleMessage(NetworkMessage msg) {
        var InputMessage = msg.ReadMessage<InputMessage>();
        Debug.LogError("Msg received" + msg.msgType);
        Debug.LogError(msg.conn.connectionId);

    }

   // public override void OnServerConnect(NetworkConnection conn) {
   //     
   //     //mClient = new NetworkClient();
   //     //mClient.RegisterHandler(MsgType.Highest,HandleMessage);
   //     //mClient.Connect("127.0.0.1",7777);
   //
   //     Debug.LogError(conn.address);
   // }

    void OnConnected(NetworkMessage msg) {
        Debug.Log("HGERFNMFJKEW");
    }

    void OnError(NetworkMessage msg) {
        Debug.Log("Error");
    }

}


public class InputMessageType {
    public static short Input = MsgType.Highest + 1;
}


