using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class NetworkShare : MonoBehaviour
{

    private int reliableChannelId;
    private int socketPort = 5999;
    private int socketId;
    private List<int> connectionId;
    public string[] remoteIps;
    public int[] remotePorts;



    // Use this for initialization
    void Start()
    {
        if (remotePorts.Length == 0 || remoteIps.Length == 0)
        {
            Debug.Log("No client specified");
            return;
        }

        connectionId = new List<int>();

        NetworkTransport.Init();
        ConnectionConfig config = new ConnectionConfig();
        reliableChannelId = config.AddChannel(QosType.Reliable);
        int maxConnections = 10;
        HostTopology topology = new HostTopology(config, maxConnections);
        socketId = NetworkTransport.AddHost(topology, socketPort);
        Debug.Log("Socket Open. SocketId is: " + socketId);
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        int recHostId;
        int recConnectionId;
        int recChannelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recNetworkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event received");
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                string message = formatter.Deserialize(stream) as string;
                Debug.Log("incoming message event received: " + message);
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }

        if (Input.GetKey(KeyCode.S))
        {
            SendSocketMessage();
        }

    }

    public void Connect()
    {
        byte error;

        for (int i = 0; i < remoteIps.Length; i++)
        {
            connectionId.Add(NetworkTransport.Connect(socketId, remoteIps[i], remotePorts[i], 0, out error));
            Debug.Log("Connected to server. ConnectionId: " + connectionId);
        }
    }

    public void SendSocketMessage()
    {
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, "HelloServer");

        int bufferSize = 1024;
        for (int i = 0; i < remoteIps.Length; i++)
        {
            NetworkTransport.Send(socketId, connectionId[i], reliableChannelId, buffer, bufferSize, out error);
        }
    }


}