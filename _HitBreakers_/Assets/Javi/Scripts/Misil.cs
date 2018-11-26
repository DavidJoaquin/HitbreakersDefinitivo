using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu (menuName = "Habilidad/Misil")]
public class Misil : Habilidad {

    //BOOL - Defina cuando se está disparando y cuando no. 
    public bool isFiring;
    //BulletController - El objeto que vamos a disparar, dicho objeto tiene que tener el script BulletController asociado
    public BulletController bullet;
    //FLOAT - La velocidad de la bala
    public float bulletSpeed;
    //FLOAT - El tiempo entre disparos
    public float timeBetweenShots;
    //FLOAT - Un contador que nos permite limitar el tiempo entre dispararos junto a la variable timeBetweenShots
    private float shotCounter;
    //Transform - El objeto de referencia para la posición donde se generan las balas
    public Transform firePoint;
    public float tiempoVidaBala;
    public int dmgBala;


    public override void Inicializa(GameObject obj) {
        {
            firePoint = obj.transform;
        }

    }
    [Command]
    public override void Ejecuta() {

            //Instanciamos el objeto bala en la posicion del objeto de referencia firePoint, con su posicion y su rotacion.
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        NetworkServer.Spawn(newBullet.gameObject);
            //Asignamos la velocidad a la bala.
            newBullet.speed = bulletSpeed;
            newBullet.tiempoVida = tiempoVidaBala;
            newBullet.dmgBala = dmgBala;
        Debug.Log(dmgBala);
    
    }
}
