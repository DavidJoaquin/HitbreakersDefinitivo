using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Estados))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : NetworkBehaviour {

    //velocidad base de movimiento
    Vector3 v3_velocidad_horizontal = Vector3.zero;
    Vector3 v3_velocidad_suelo = Vector3.zero;
    Vector3 v3_velocidad_vertical = Vector3.zero;

    public Vector3 v3_gravetat = new Vector3(0, -9.8f, 0);
    public float f_velocitat = 4f;
    public float f_salto = 2f;
       
    Estados estados;

    //el parámetro del player
    private CharacterController player;

    //dirección de movimento
    private Vector3 inputMovimiento;
    //velocidad hacia la dirección de movimiento
    private Vector3 velocidadMovimiento;
    //la cámara que usaremos para apuntar
    private Camera mainCamera;
    Vector3 apuntar;
    private Vector3 ultimaDireccion;

    public ControladorCamara camaraPrinc;

    public Vector3 posRaycast;

    public GameObject assignAuthorityObj;
    //?
    private float shotCounter;
    //?
    public float timeBetweenShots;

    public GameObject misilPreFab;
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;


    // Use this for initialization
    void Start () {
       

        //asignamos el player al Rigidbody que hemos definido (solo hay uno por ahora, habrá que ver como lo hacemos luego con los items)
        player = GetComponent<CharacterController>();
        //lo mismo con la cámara, tendremos que usar tags o algo luego para definir la que queremos
        mainCamera = FindObjectOfType<Camera>();

        if (isLocalPlayer)
        {
            mainCamera.GetComponent<ControladorCamara>().asignarPlayer(this.gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer)
            return;

        //si toco suelo
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),
            out hit, player.height / 2 + 0.1f))
        {
            if (hit.transform.GetComponent<Velocidad_Suelo>() != null)
                transform.parent = hit.transform;
            else
                transform.parent = null;

            v3_velocidad_horizontal = Vector3.zero;
            v3_velocidad_vertical = Vector3.zero;
            v3_velocidad_suelo = Vector3.zero;

            //moviento horizontal
            if (estados.b_Arriba)
            {
                v3_velocidad_horizontal += new Vector3(0, 0, f_velocitat);
            }
            if (estados.b_Abajo)
            {
                v3_velocidad_horizontal += new Vector3(0, 0, -f_velocitat);
            }
            if (estados.b_Izquierda)
            {
                v3_velocidad_horizontal += new Vector3(-f_velocitat, 0, 0);
            }
            if (estados.b_Derecha)
            {
                v3_velocidad_horizontal += new Vector3(f_velocitat, 0, 0);
            }
            //Me la guardo para posible salto
            /* if (estados.b_Habilidad4)
             {
                 v3_velocidad_horizontal += new Vector3(0, f_salto, 0);
             }
             */

            if (estados.b_Habilidad1)
            {
                
            }

            if (estados.b_Habilidad2)
            {
                
            }

            if (estados.b_Habilidad3)
            {
                
            }

            if (estados.b_Disparo)
            {
                
            }

            if (estados.b_Definitiva)
            {
                
            }
          
        }
        else //si estoy en el aire
        {
            if (transform.parent != null)
            {
                v3_velocidad_suelo = transform.parent.GetComponent<Velocidad_Suelo>().v3_velocidad;
                transform.parent = null;
            }
        }

        v3_velocidad_vertical += (v3_gravetat * Time.deltaTime);
        player.Move((transform.TransformDirection(v3_velocidad_horizontal) + v3_velocidad_vertical + v3_velocidad_suelo) * Time.deltaTime);

        //creamos un puntero que sale de la camará hacia la posición del ratón

        Ray punteroCamara = mainCamera.ScreenPointToRay(Input.mousePosition);
        //recogemos donde está la superficie, habrá que actualizarlo luego?
        Plane superficie = new Plane(Vector3.up, Vector3.zero);
        //creamos un parámetro para la longitud que tendrá el raycast desde la cámara al suelo
        float longitudPuntero;

         // Si hay puntero disponible, es decir, que apunta dentro del suelo
        if (superficie.Raycast(punteroCamara, out longitudPuntero)) {
            // creamos un vector3 que coge la posición del puntero
            apuntar = punteroCamara.GetPoint(longitudPuntero);
            // con esto hacemos que el puntero sea visible en el modo debug, para controlar que funciona bien
            Debug.DrawLine(punteroCamara.origin, apuntar, Color.blue);
            // aquí hacemos que el player mire hacía el puntero, pero al forzar el eje y a la posicion del jugador, mirará siempre
            //recto en esa dirección
            posRaycast = new Vector3(apuntar.x, transform.position.y, apuntar.z);
            

            transform.LookAt(posRaycast);
        }
 }
    /*Recordar como ejemplo habilidad 1
    [Command]
    void CmdHabilidad1()
    {
        timerHabilidad1 -= Time.deltaTime;
        //Cuando shotCounter llega a 0
        if (timerHabilidad1 <= 0)
        {
            //Reiniciamos shotCounter al valor de timeBetweenShots, esto nos permite ajustar el tiempo entre disparos.
            timerHabilidad1 = cdHabilidad1;
            animCont.GetComponent<Animator>().SetBool("disparando", true);
            //Instanciamos el objeto bala en la posicion del objeto de referencia firePoint, con su posicion y su rotacion.
            //    BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //    //Asignamos la velocidad a la bala.
            //    newBullet.speed = bulletSpeed;
            //    newBullet.tiempoVida = tiempoVidaBala;
            //    newBullet.dmgBala = dmgBala;


            Debug.Log("Habilidad1");

            var misil = (GameObject)Instantiate(
            misilPreFab,
            bulletSpawn.GetComponent<Transform>().position,
            bulletSpawn.GetComponent<Transform>().rotation);

            // Add velocity to the bullet
            //    bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 6;

            // Spawn the bullet on the Clients
            NetworkServer.Spawn(misil);

            // Destroy the bullet after 2 seconds
            //    Destroy(bala, tiempoVidaBala);

        }
    }

    */


     /* Para recordar como hacer el Dash
    private void dash() {
  
        transform.position += ultimaDireccion * distancia;
        timerDash = cdDash;  
    }
    */
}
