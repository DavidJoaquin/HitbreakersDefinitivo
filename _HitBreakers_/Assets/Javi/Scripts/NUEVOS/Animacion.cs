using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Estados))]

public class XAnimacio : NetworkBehaviour
{

    Animator A_animator;
    Estados estados;

    // Use this for initialization
    void Start()
    {
        A_animator = GetComponent<Animator>();

        if (!isServer)
            return;

        estados = GetComponent<Estados>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isServer)
            return;

        if (estados.b_Habilidad1)
        {
            Rpc_habilidad1();
        }


        bool b_movimiento = estados.b_Arriba || estados.b_Abajo
            || estados.b_Derecha || estados.b_Izquierda;
        //poner nombre animación
        A_animator.SetBool("andando", b_movimiento);
        Rpc_movimiento(b_movimiento);
    }

    [ClientRpc]
    void Rpc_movimiento(bool b_movimiento)
    {
        A_animator.SetBool("andando", b_movimiento);
    }

    [ClientRpc]
    void Rpc_habilidad1()
    {
        // poner código animación
       // A_animator.SetTrigger("A_Salto");
    }
}
