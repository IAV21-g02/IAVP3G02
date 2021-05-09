using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;
using Ludiq;


public class VizcondeBehaviour : MonoBehaviour
{
    //Public
    public float force = 2500.0f;
    //Camara fantasma
    public Camera ghostCamera;
    //Camara cantante
    public Camera singerCamera;
    //Camara del vizconde
    public Camera selfCamera;
    //Camara general
    public GameObject mainCamera;
    //Panel canvas de los personajes
    public GameObject chPanel;
    //Panel canvas de las camaras generales
    public GameObject camerasPanel;

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
            if (obj.CompareTag("Celda"))
            {
                //Para abrir la celda
                //obj.GetComponent<ComportamientoCelda>().Abrir();
            }
            else if (obj.GetComponent<PalancaBarca>())
            {
                obj.GetComponent<PalancaBarca>().TraeBarca();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Recoloca lampara
            if (obj.CompareTag("Lampara") && obj.GetComponent<Rigidbody>().useGravity)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                int numLamparas = (int)Variables.Application.Get("LamparasCaidas");
                numLamparas--;
                Variables.Application.Set("LamparasCaidas", numLamparas);
                Debug.Log("Lamparas Caidas: " + numLamparas);
            }


            if (obj.tag == "Cantante")
            {
                //Consuela a la cantante
                //obj.GetComponent<ComportamientoCantante>().setTranquila(true);
            }
        }
    }

    /// <summary>
    /// Alterna entre las diversas cámaras del juego.
    /// Cámaras generales y cámaras de los personajes
    /// </summary>
    private void ChangeCamera()
    {
        if (mainCamera.active)
        {
            mainCamera.active = false;
            camerasPanel.active = false;

            chPanel.active = true;
            ghostCamera.enabled = true;
            singerCamera.enabled = true;
            selfCamera.enabled = true;
        }
        else
        {
            mainCamera.active = true;
            camerasPanel.active = true;

            chPanel.active = false;
            ghostCamera.enabled = false;
            singerCamera.enabled = false;
            selfCamera.enabled = false;
        }
    }
}
