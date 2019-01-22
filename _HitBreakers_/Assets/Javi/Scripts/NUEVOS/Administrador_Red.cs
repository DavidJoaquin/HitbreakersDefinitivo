using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


class MsgB_Llista_personajes : MessageBase
{
    public bool b_goku = true;
    public bool b_minion = true;
}


class MsgB_Numero_personajes : MessageBase
{
    public int i_numero;
}




public class Administrador_Red : NetworkManager
{

    public GameObject go_Menu_Seleccion_Cliente;

    bool b_servidor = true;
    public string str_ip_servidor = "127.0.0.1";


    public GameObject go_goku;
    public GameObject go_minion;



    public void Iniciar(bool b_srvd)
    {
        b_servidor = b_srvd;
        if (b_servidor)
            Arranca_Servidor();
        else
            Arranca_Cliente();
    }



    ///////////////////////////////////////////////
    ///  SERVIDOR
    ///  

    MsgB_Llista_personajes msgb_lista_personajes;

    void Arranca_Servidor()
    {
        Debug.Log("SOY SERVIDOR");
        NetworkManager.singleton.StartServer();

        msgb_lista_personajes = new MsgB_Llista_personajes();

    }


    void Servidor_Envia_Lista()
    {
        NetworkServer.SendToAll(1000, msgb_lista_personajes);
    }


    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Tengo un cliente desde " + conn.address);

        Servidor_Envia_Lista();

    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {


        int i_selecion = extraMessageReader.ReadMessage<MsgB_Numero_personajes>().i_numero;

        if (i_selecion == 1)
        {
            playerPrefab = go_goku;
            msgb_lista_personajes.b_goku = false;
        }
        if (i_selecion == 2)
        {
            playerPrefab = go_minion;
            msgb_lista_personajes.b_minion = false;
        }

        Servidor_Envia_Lista();

        //Metido player para pruebas, cambiar a seleccion
        NetworkServer.AddPlayerForConnection(conn, (GameObject)Instantiate(playerPrefab), playerControllerId);
    }

    ///////////////////////////////////////////////
    ///  CLIENTE
    ///  

    NetworkClient nc_client;
    GameObject go_menu;

    MsgB_Llista_personajes msgb_lista_cliente_presonajes;

    void Arranca_Cliente()
    {
        go_menu = (GameObject)Instantiate(go_Menu_Seleccion_Cliente);

        Debug.Log("SOY CLIENTE");
        if (str_ip_servidor == string.Empty)
            str_ip_servidor = "127.0.0.1";

        NetworkManager.singleton.networkAddress = str_ip_servidor;

        nc_client = NetworkManager.singleton.StartClient();
        nc_client.RegisterHandler(1000, Cliente_Rec_Lista);
    }

    NetworkConnection nc_netcon;

    public override void OnClientConnect(NetworkConnection connection)
    {
        nc_netcon = connection;
    }


    void Cliente_Rec_Lista(NetworkMessage networkMessage)
    {
        if (go_menu == null)
            return;

        msgb_lista_cliente_presonajes = networkMessage.ReadMessage<MsgB_Llista_personajes>();

        if (msgb_lista_cliente_presonajes.b_goku == false)
            go_menu.GetComponent<Menu_Seleccion_Personaje>().Desactiva_Boton_Goku();
        if (msgb_lista_cliente_presonajes.b_minion == false)
            go_menu.GetComponent<Menu_Seleccion_Personaje>().Desactiva_Boton_Minion();

    }

    public void Client_Envia_Seleccion_Personaje(int i_numero)
    {

        MsgB_Numero_personajes msgb_selecion = new MsgB_Numero_personajes();
        msgb_selecion.i_numero = i_numero;
        ClientScene.AddPlayer(nc_netcon, 0, msgb_selecion);
        Destroy(go_menu);
    }

}


