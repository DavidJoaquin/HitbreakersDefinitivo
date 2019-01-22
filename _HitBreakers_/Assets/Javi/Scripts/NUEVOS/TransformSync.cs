﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TransformSync : NetworkBehaviour
{


    Vector3 v3_posicio_sync;
    Quaternion q_rotacio_sync;

    private void Start()
    {
        v3_posicio_sync = transform.position;
        q_rotacio_sync = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (isServer)
            Rpc_Envia_Transform(transform.position, transform.rotation);
        else
        {
            transform.position = Vector3.Lerp(transform.position, v3_posicio_sync, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, q_rotacio_sync, 0.5f);
        }
    }

    [ClientRpc]
    void Rpc_Envia_Transform(Vector3 v3_posicio, Quaternion q_rotacio)
    {
        v3_posicio_sync = v3_posicio;
        q_rotacio_sync = q_rotacio;
    }

}