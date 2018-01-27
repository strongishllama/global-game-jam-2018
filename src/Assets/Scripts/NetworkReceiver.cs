using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiver:NetworkManager {
    NetworkClient mClient;
    int highestID = 0;
    public GameObject CarInstance;

    [SerializeField]
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [SerializeField]
    private List<CarDrift> players = new List<CarDrift>();

    private void Start() {

        autoCreatePlayer = false;
        networkAddress = LocalIpAddress();
        StartHost();
        Debug.Log(LocalIpAddress());
        NetworkServer.RegisterHandler(MsgType.Highest + 1,HandleMessage);
        NetworkServer.RegisterHandler(MsgType.Connect,OnConnected);
        this.gameObject.AddComponent<BroadcastMessage>();
    }

    public string LocalIpAddress() {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(IPAddress ip in host.AddressList) {
            if(ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
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
        if(msg.conn.connectionId > highestID) {

        } else
            players[msg.conn.connectionId - 1].SetButton(); // turn player
    }

    //When a client connects
    void OnConnected(NetworkMessage msg) {

        //If new highest ID means a new player has joined. Give them a car.
        if(msg.conn.connectionId > highestID) {
            GameObject NewCar = Instantiate(CarInstance,GetSpawnPoint(),CarInstance.transform.rotation);
            NewCar.GetComponent<CarDrift>().SetNetworked();
            players.Add(NewCar.GetComponent<CarDrift>());
            highestID = msg.conn.connectionId; // Set highest id so 1 car per player
        }


        ColourMessage message = new ColourMessage();
        switch(msg.conn.connectionId) {
        case 1:
        message.color = Color.red;
        break;
        case 2:
        message.color = Color.blue;
        break;
        case 3:
        message.color = Color.green;
        break;
        case 4:
        message.color = Color.magenta;
        break;
        default:
        this.GetComponent<BroadcastMessage>().StopBroadcast();
        break;
        }
        Debug.Log(msg.conn.address);
        NetworkServer.SendToClient(msg.conn.connectionId,InputMessageType.PlayerColour,message);
    }

    void OnError(NetworkMessage msg) {
        Debug.Log("Error");
    }

    private Vector3 GetSpawnPoint() {
        for(int i = 0;i < spawnPoints.Count;i++) {
            if(spawnPoints[i].IsEmpty) {
                return spawnPoints[i].transform.position;
            }
        }

        Debug.Log("Error: No free spawn points left.");
        return Vector3.zero;
    }

}

