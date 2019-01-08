using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


class Estados_a_Enviar
{
    public bool b_Arriba = false;
    public bool b_Abajo = false;
    public bool b_Izquierda = false;
    public bool b_Derecha = false;
    public bool b_Habilidad1 = false;
    public bool b_Habilidad2 = false;
    public bool b_Habilidad3 = false;
    public bool b_Habilidad4 = false;
    public bool b_Recarga = false;
    public bool b_Definitiva = false;
    public bool b_Disparo = false;
}



public class Estados : NetworkBehaviour
{
    public bool b_Arriba = false;
    public bool b_Abajo = false;
    public bool b_Izquierda = false;
    public bool b_Derecha = false;
    public bool b_Habilidad1 = false;
    public bool b_Habilidad2 = false;
    public bool b_Habilidad3 = false;
    public bool b_Habilidad4 = false;
    public bool b_Recarga = false;
    public bool b_Definitiva = false;
    public bool b_Disparo = false;


    Estados_a_Enviar enviar = new Estados_a_Enviar();

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;


        if (Input.GetKeyDown(KeyCode.W))
            enviar.b_Arriba = true;
        if (Input.GetKeyUp(KeyCode.W))
            enviar.b_Arriba = false;

        if (Input.GetKeyDown(KeyCode.S))
            enviar.b_Abajo = true;
        if (Input.GetKeyUp(KeyCode.S))
            enviar.b_Abajo = false;

        if (Input.GetKeyDown(KeyCode.A))
            enviar.b_Izquierda = true;
        if (Input.GetKeyUp(KeyCode.A))
            enviar.b_Izquierda = false;

        if (Input.GetKeyDown(KeyCode.D))
            enviar.b_Derecha = true;
        if (Input.GetKeyUp(KeyCode.D))
            enviar.b_Izquierda = false;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            enviar.b_Disparo = true;
        if (Input.GetKeyUp(KeyCode.Mouse0))
            enviar.b_Disparo = false;

        if (Input.GetKeyDown(KeyCode.Mouse1))
            enviar.b_Habilidad1 = true;
        if (Input.GetKeyUp(KeyCode.Mouse1))
            enviar.b_Habilidad1 = false;

        if (Input.GetKeyDown(KeyCode.E))
            enviar.b_Habilidad2 = true;
        if (Input.GetKeyUp(KeyCode.E))
            enviar.b_Habilidad2 = false;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            enviar.b_Habilidad3 = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            enviar.b_Habilidad3 = false;

        if (Input.GetKeyDown(KeyCode.Space))
            enviar.b_Habilidad4 = true;
        if (Input.GetKeyUp(KeyCode.Space))
            enviar.b_Habilidad4 = false;

        if (Input.GetKeyDown(KeyCode.Q))
            enviar.b_Definitiva = true;
        if (Input.GetKeyUp(KeyCode.Q))
            enviar.b_Definitiva = false;

        if (Input.GetKeyDown(KeyCode.R))
            enviar.b_Recarga = true;
        if (Input.GetKeyUp(KeyCode.R))
            enviar.b_Recarga = false;

        Cmd_Envia_Eventos(enviar);
    }


    [Command]
    void Cmd_Envia_Eventos(Estados_a_Enviar eventos)
    {
        b_Arriba = eventos.b_Arriba;
        b_Abajo = eventos.b_Abajo;
        b_Izquierda = eventos.b_Izquierda;
        b_Derecha = eventos.b_Derecha;
        b_Habilidad1 = eventos.b_Habilidad1;
        b_Habilidad2 = eventos.b_Habilidad2;
        b_Habilidad3 = eventos.b_Habilidad3;
        b_Habilidad4 = eventos.b_Habilidad4;
        b_Recarga = eventos.b_Recarga;
        b_Definitiva = eventos.b_Definitiva;
        b_Disparo = eventos.b_Disparo;
}
}
