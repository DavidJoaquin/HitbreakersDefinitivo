using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFijarRotacion : MonoBehaviour {

    public Transform player;
    public GameObject playerContr;


	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.position = player.position + new Vector3(0,25,0);

    }
}
