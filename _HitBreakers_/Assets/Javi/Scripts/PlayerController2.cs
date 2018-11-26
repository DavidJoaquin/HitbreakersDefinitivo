using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController2 : NetworkBehaviour {

    //velocidad base de movimiento
    public float velocidad;
    //el parámetro del player
    private Rigidbody player;
    //dirección de movimento
    private Vector3 inputMovimiento;
    //velocidad hacia la dirección de movimiento
    private Vector3 velocidadMovimiento;
    //la cámara que usaremos para apuntar
    private Camera mainCamera;
    Vector3 apuntar;
    private Vector3 ultimaDireccion;

    public GameObject bulletPrefab;
    
    public ControladorCamara camaraPrinc;

    public Vector3 posRaycast;

    public GameObject animCont;

    public GameObject assignAuthorityObj;

    public bool isFiring;

    private float shotCounter;

    public float timeBetweenShots;

    public float tiempoVidaBala;

    public float distancia;
    public float cdDash;
    public float timerDash;

    public float cdHabilidad1;
    public float cdHabilidad2;
    public float cdHabilidad3;

    public float timerHabilidad1;
    public float timerHabilidad2;
    public float timerHabilidad3;

    public GameObject misilPreFab;
    public GameObject shotGunBulletPrefab;
    [SerializeField]
    public GameObject shotGunSpawn1;
    public GameObject shotGunSpawn2;
    public GameObject shotGunSpawn3;
    public GameObject shotGunSpawn4;
    public GameObject shotGunSpawn5;
    public GameObject shotGunSpawn6;
    public GameObject shotGunSpawn7;

    public GameObject misilSpawner;


    // Use this for initialization
    void Start () {
        cdDash = 4f;
        cdHabilidad1 = 4f;
        cdHabilidad2 = 4f;
        cdHabilidad3 = 4f;
        timerHabilidad1 = 0f;
        timerHabilidad2 = 0f;
        timerHabilidad3 = 0f;

        timerDash = 0f;
        distancia = 0.8f;

        //asignamos el player al Rigidbody que hemos definido (solo hay uno por ahora, habrá que ver como lo hacemos luego con los items)
        player = GetComponent<Rigidbody>();
        //lo mismo con la cámara, tendremos que usar tags o algo luego para definir la que queremos
        mainCamera = FindObjectOfType<Camera>();

        if (isLocalPlayer)
        {
            mainCamera.GetComponent<ControladorCamara>().asignarPlayer(this.gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {
        //dash();
        //recogemos si se pulsa el eje de horizontal y el de vertical que definimos en la configuración del juego
        inputMovimiento = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //definimos que se mueva en esa dirección con la velocidad base
        velocidadMovimiento = inputMovimiento * velocidad;
        ultimaDireccion = velocidadMovimiento;
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            animCont.GetComponent<Animator>().SetBool("andando", true);
        }
        else
        {
            animCont.GetComponent<Animator>().SetBool("andando", false);
        }

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
      
        shotCounter -= Time.deltaTime;
        timerDash -= Time.deltaTime;
        timerHabilidad1 -= Time.deltaTime;
        timerHabilidad2 -= Time.deltaTime;
        timerHabilidad3 -= Time.deltaTime;

        if (isLocalPlayer)
        {

            animCont.GetComponent<Animator>().SetBool("disparando", false);

            //Si se pulsa el boton, en este caso el click izquierdo del raton, la variable isFiring pasa a true.
            if (Input.GetButton("Fire1"))
            {
                isFiring = true;
            }

            //Si soltamos el boton, en este caso el click izquierdo del raton, la variable isFiring pasa a false.
            if (Input.GetButtonUp("Fire1"))
            {
                isFiring = false;
            }
        }

        //Si isFiring es true
        if (isLocalPlayer)
        {
            if (isFiring)
            {
                CmdFire();
            }
            else
            {
                shotCounter -= Time.deltaTime;
            }

        }
    
        if (Input.GetButtonDown("Jump")) {
            if(timerDash <= 0)
            {
                dash();
            }
            else
            {
                timerDash -= Time.deltaTime;
            }
            
        }
        if (Input.GetButton("Fire2")) {
            if (timerHabilidad1 <= 0)
            {
                CmdHabilidad1();
            }
            else
            {
                timerHabilidad1 -= Time.deltaTime;
            }
        }
        if (Input.GetButton("Fire3")){
            if (timerHabilidad2 <= 0)
            {
                    CmdHabilidad2();
            
            }
            else
            {
                timerHabilidad2 -= Time.deltaTime;
            }
           
        }
        if (Input.GetButton(""))
        {
            if (timerHabilidad3 <= 0)
            {
                //    dash();
            }
            else
            {
                timerHabilidad3 -= Time.deltaTime;
            }
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
            shotGunSpawn1.GetComponent<Transform>().position,
            shotGunSpawn1.GetComponent<Transform>().rotation);




            // Add velocity to the bullet
            //    bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 6;

            // Spawn the bullet on the Clients
            NetworkServer.Spawn(bala);

            // Destroy the bullet after 2 seconds
            //    Destroy(bala, tiempoVidaBala);

        }
    }

    [Command]
    void CmdHabilidad2()
    {
        
        timerHabilidad2 -= Time.deltaTime;
        //Cuando shotCounter llega a 0
        if (shotCounter <= 0)
        {
            //Reiniciamos shotCounter al valor de timeBetweenShots, esto nos permite ajustar el tiempo entre disparos.
            timerHabilidad2 = cdHabilidad2;
            animCont.GetComponent<Animator>().SetBool("disparando", true);
            //Instanciamos el objeto bala en la posicion del objeto de referencia firePoint, con su posicion y su rotacion.
            //    BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //    //Asignamos la velocidad a la bala.
            //    newBullet.speed = bulletSpeed;
            //    newBullet.tiempoVida = tiempoVidaBala;
            //    newBullet.dmgBala = dmgBala;


            Debug.Log("DISPARO");

            var balaEscopeta1 = (GameObject)Instantiate(
            shotGunBulletPrefab,
            shotGunSpawn1.GetComponent<Transform>().position,
            shotGunSpawn1.GetComponent<Transform>().rotation);
            NetworkServer.Spawn(balaEscopeta1);

            var balaEscopeta2 = (GameObject)Instantiate(
            shotGunBulletPrefab,
            shotGunSpawn2.GetComponent<Transform>().position,
            shotGunSpawn2.GetComponent<Transform>().rotation);
            NetworkServer.Spawn(balaEscopeta2);

            var balaEscopeta3 = (GameObject)Instantiate(
            shotGunBulletPrefab,
            shotGunSpawn3.GetComponent<Transform>().position,
            shotGunSpawn3.GetComponent<Transform>().rotation);

            var balaEscopeta4 = (GameObject)Instantiate(
            shotGunBulletPrefab,
            shotGunSpawn4.GetComponent<Transform>().position,
            shotGunSpawn4.GetComponent<Transform>().rotation);

            var balaEscopeta5 = (GameObject)Instantiate(
            shotGunBulletPrefab,
            shotGunSpawn5.GetComponent<Transform>().position,
            shotGunSpawn5.GetComponent<Transform>().rotation);

            var balaEscopeta6 = (GameObject)Instantiate(
           shotGunBulletPrefab,
           shotGunSpawn6.GetComponent<Transform>().position,
           shotGunSpawn6.GetComponent<Transform>().rotation);

            var balaEscopeta7 = (GameObject)Instantiate(
           shotGunBulletPrefab,
           shotGunSpawn7.GetComponent<Transform>().position,
           shotGunSpawn7.GetComponent<Transform>().rotation);

            // Add velocity to the bullet
            //    bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 6;

            // Spawn the bullet on the Clients
            
            
            NetworkServer.Spawn(balaEscopeta3);
            NetworkServer.Spawn(balaEscopeta4);
            NetworkServer.Spawn(balaEscopeta5);
            NetworkServer.Spawn(balaEscopeta6);
            NetworkServer.Spawn(balaEscopeta7);


            // Destroy the bullet after 2 seconds
            //    Destroy(bala, tiempoVidaBala);

        }
    }
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
            misilSpawner.GetComponent<Transform>().position,
            misilSpawner.GetComponent<Transform>().rotation);

            // Add velocity to the bullet
            //    bala.GetComponent<Rigidbody>().velocity = bala.transform.forward * 6;

            // Spawn the bullet on the Clients
            NetworkServer.Spawn(misil);

            // Destroy the bullet after 2 seconds
            //    Destroy(bala, tiempoVidaBala);

        }
    }


    private void FixedUpdate()
    {
        //le decimos que la velocidad del player actualize su posición
        player.velocity = velocidadMovimiento;
    }





    private void dash() {
        transform.position += ultimaDireccion * distancia;
        timerDash = cdDash;
        
        
    }
}
