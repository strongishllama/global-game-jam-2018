using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiver:NetworkManager {
    NetworkClient mClient;
    int highestID = 0;
    public GameObject CarInstance;
    private void Start() {

        autoCreatePlayer = false;
        NetworkServer.Listen(7777);
        networkAddress = LocalIpAddress();
        
        Debug.Log(LocalIpAddress());
        NetworkServer.RegisterHandler(MsgType.Highest + 1,HandleMessage);
    }

    public string LocalIpAddress() {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(IPAddress ip in host.AddressList) {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    void HandleMessage(NetworkMessage msg) {
        var InputMessage = msg.ReadMessage<InputMessage>();
        Debug.LogError("Msg received" + msg.msgType);
        Debug.LogError(msg.conn.connectionId);
       
        //If new highest ID means a new player has joined. Give them a car.
        if(msg.conn.connectionId > highestID)
        {
            Instantiate(CarInstance,transform, false);
            highestID = msg.conn.connectionId; // Set highest id so 1 car per player
        }
        else
        transform.GetChild(msg.conn.connectionId - 1).GetComponent<CarDrift>().SetButton(); // turn player
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


