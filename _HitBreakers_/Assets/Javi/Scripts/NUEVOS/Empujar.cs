using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Empujar : NetworkBehaviour
{
    
    public float pushPower = 2.0f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isServer)
            return;
        //Buscamos el rigidbody del objeto con el que hemos colisionado,
        Rigidbody body = hit.collider.attachedRigidbody;
        //Si no tiene rigidbody, o tiene activada la kinematica o ???????
        if (body == null || body.isKinematic || hit.moveDirection.y < -0.3)
            return;
        
        body.velocity = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z) *
            pushPower;
    }
}
