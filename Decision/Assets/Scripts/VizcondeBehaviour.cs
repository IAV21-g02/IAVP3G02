using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;


public class VizcondeBehaviour : MonoBehaviour
{
    //Public
    public float force = 2500.0f;
    public Camera ghostCamera;
    public Camera singerCamera;
    public Camera selfCamera;
    public GameObject mainCamera;
    public GameObject palancaEste;
    public GameObject palancaOeste;

    //Private
    private Rigidbody rb;
    //Vector director del movimiento
    private Vector3 dir;
    private Vector3 rot;
    private Vector3 m_EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera.SetActive(false);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(dir * force, ForceMode.Force);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    // Update is called once per frame
    void Update()
    {
        dir.z = Input.GetAxis("Vertical");
        rot.x = Input.GetAxis("Horizontal");
        m_EulerAngleVelocity.y = rot.x * 100;

        if (Input.GetKeyDown(KeyCode.Space)) {
            ChangeCamera();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Escalera")
        {
            force = 5000.0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Escalera")
        {
            force = 2500.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (obj.CompareTag("Lampara") && obj.GetComponent<Rigidbody>().useGravity) 
            {
                obj.GetComponent<Rigidbody>().useGravity = false;
                obj.transform.position = obj.GetComponentInParent<Transform>().position;
                int numLamparas = (int)Variables.Application.Get("LamparasCaidas");
                numLamparas--;
                Variables.Application.Set("LamparasCaidas", numLamparas);    
            }
            else if (obj.CompareTag("Celda"))
            {
                //Para abrir la celda
                //obj.GetComponent<ComportamientoCelda>().Abrir();
            }
            else if (obj.GetComponent<PalancaBarca>())
            {
                obj.GetComponent<PalancaBarca>().TraeBarca();
            }
            //Recoloca lampara
            //obj.GetComponent<ComportamientoLampara>().setLamparaCaida(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Cantante" && Input.GetKeyDown(KeyCode.E))
        {
            //Consuela a la cantante
            //obj.GetComponent<ComportamientoCantante>().setTranquila(true);
        }
        else if (obj.tag == "Objeto" && Input.GetKeyDown(KeyCode.E))
        {
            //Le pega a los objetos
            //obj.getComponent<ComportamientoObjeto>().setGolpeado(true);
            //GameObject.Find("Fantasma").setEnfadado()
        }
    }

    private void ChangeCamera()
    {
        //Si esta activa
        if (mainCamera.active)
        {
            mainCamera.active = false;
            ghostCamera.enabled = true;
            singerCamera.enabled = true;
            selfCamera.enabled = true;
        }
        else
        {
            mainCamera.active = true;
            ghostCamera.enabled = false;
            singerCamera.enabled = false;
            selfCamera.enabled = false;
        }
    }
}
