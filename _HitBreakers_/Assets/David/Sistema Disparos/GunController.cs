using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunController : NetworkBehaviour {

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

    public GameObject animCont;
    public GameObject player;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    private void Start()
    {
        this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);

    }

    // Update is called once per frame
    void Update () {

        if (player.GetComponent<NetworkBehaviour>().isLocalPlayer)
        {

            animCont.GetComponent<Animator>().SetBool("disparando", false);

            //Si se pulsa el boton, en este caso el click izquierdo del raton, la variable isFiring pasa a true.
            if (Input.GetMouseButtonDown(0))
            {
                isFiring = true;
            }

            //Si soltamos el boton, en este caso el click izquierdo del raton, la variable isFiring pasa a false.
            if (Input.GetMouseButtonUp(0))
            {
                isFiring = false;
            }
        }

        //Si isFiring es true
        if (isFiring)
        {
            CmdFire();
        }
        else
        {
            shotCounter -= Time.deltaTime;
        }
		
	}

    [Command]
    void CmdFire()
    {
        shotCounter -= Time.deltaTime;
        //Cuando shotCounter llega a 0
        if (shotCounter <= 0)
        {
            //Reiniciamos shotCounter al valor de timeBetweenShots, esto nos permite ajustar el tiempo entre disparos.
            shotCounter = timeBetweenShots;
            animCont.GetComponent<Animator>().SetBool("disparando", true);
            //Instanciamos el objeto bala en la posicion del objeto de referencia firePoint, con su posicion y su rotacion.
            //    BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //    //Asignamos la velocidad a la bala.
            //    newBullet.speed = bulletSpeed;
            //    newBullet.tiempoVida = tiempoVidaBala;
            //    newBullet.dmgBala = dmgBala;


            Debug.Log("DISPARO");

            var bala = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.GetComponent<Transform>().position,
            bulletSpawn.GetComponent<Transform>().rotation);

            // Add velocity to the bullet
            bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 6;

            // Spawn the bullet on the Clients
            NetworkServer.Spawn(bala);

            // Destroy the bullet after 2 seconds
            Destroy(bala, tiempoVidaBala);





        }
    }


}
