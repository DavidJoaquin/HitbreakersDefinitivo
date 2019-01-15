using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explosion : NetworkBehaviour {

    public float f_radio_explosion = 5f;
    public float f_poder_explosivo = 800f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer)
            return;

        Collider[] c_objetos = Physics.OverlapSphere
            (transform.position, f_radio_explosion);

        foreach (Collider c_afectado in c_objetos)
        {
            Rigidbody rb = c_afectado.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(
                    f_poder_explosivo,
                    transform.position,
                    f_radio_explosion);
            }
        }
        Destroy(gameObject);
    }

}
