using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Descubre_Servidor : NetworkDiscovery {

    public void Arranca_Servidor() {

        broadcastData = "servidor"; ;
        Initialize();
        StartAsServer();
    }

    public void Arranca_Cliente() {
        Initialize();
        StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("OnReceiveBroadcast" + data);
        GetComponent<Administrador_Red>().str_ip_servidor= fromAddress;
        GetComponent<Administrador_Red>().Iniciar(false);

        StopBroadcast();
    }

}
