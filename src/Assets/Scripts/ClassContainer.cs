using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class InputMessage:MessageBase {
    //i mean its just an empty class
}

public class ColourMessage:MessageBase {
    public Color color;
}
//for receiver
public class BroadcastMessage:NetworkDiscovery {

    public void Awake() {
        showGUI = false;
        Initialize();
        StartAsServer();
    }

    public void StartBroadcasting() {
        Initialize();
        StartAsServer();
    }
}

public class InputMessageType {
    public static short Input = MsgType.Highest + 1;
    public static short PlayerColour = MsgType.Highest + 2;
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