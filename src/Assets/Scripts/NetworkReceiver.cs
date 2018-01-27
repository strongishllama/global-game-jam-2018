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

    private void Start() {

        autoCreatePlayer = false;
        networkAddress = LocalIpAddress();
        StartHost();
        Debug.Log(LocalIpAddress());
        NetworkServer.RegisterHandler(MsgType.Highest + 1,HandleMessage);
        this.gameObject.AddComponent<BroadcastMessage>();
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
           GameObject NewCar = Instantiate(CarInstance, GetSpawnPoint(), CarInstance.transform.rotation);
            NewCar.GetComponent<CarDrift>().SetNetworked();
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

    void OnError(NetworkMessage msg)
    {
        Debug.Log("Error");
    }

    private Vector3 GetSpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].IsEmpty)
            {
                return spawnPoints[i].transform.position;
            }
        }

        Debug.Log("Error: No free spawn points left.");
        return Vector3.zero;
    }
}

public class BroadcastMessage : NetworkDiscovery {

    public void Awake() {
        showGUI = false;
        Initialize();
        StartAsServer();
    }
}
public class InputMessageType {
    public static short Input = MsgType.Highest + 1;
}