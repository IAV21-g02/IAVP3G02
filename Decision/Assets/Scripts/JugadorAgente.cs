/*    
   Copyright (C) 2020 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
namespace UCM.IAV.Movimiento
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Clase para modelar el controlador del jugador como agente
    /// </summary>
    public class JugadorAgente : Agente
    {
        /// <summary>
        /// El componente de cuerpo rígido
        /// </summary>
        private Rigidbody _cuerpoRigido;
        /// <summary>
        /// Dirección del movimiento
        /// </summary>
        private Vector3 _dir;

        /// <summary>
        /// Al despertar, establecer el cuerpo rígido
        /// </summary>
        private void Awake()
        {
            _cuerpoRigido = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// En cada tick, mover el avatar del jugador según las órdenes de este último
        /// </summary>
        public override void Update()
        {
            velocidad.x = Input.GetAxis("Horizontal");
            velocidad.z = Input.GetAxis("Vertical");
            // Faltaba por normalizar el vector
            velocidad.Normalize();
            velocidad *= velocidadMax; 
        }

        /// <summary>
        /// En cada tick fijo, haya cuerpo rígido o no, hago simulación física y cambio la posición de las cosas (si hay cuerpo rígido aplico fuerzas y si no, no)
        /// </summary>
        public override void FixedUpdate()
        {
            if (_cuerpoRigido == null)
            {
                transform.Translate(velocidad * Time.deltaTime, Space.World);
            }
            else
            {
                // El cuerpo rígido no podrá estar marcado como cinemático
                _cuerpoRigido.AddForce(velocidad * Time.deltaTime, ForceMode.VelocityChange); // Cambiamos directamente la velocidad, sin considerar la masa (pidiendo que avance esa distancia de golpe)
            } 
        }

        /// <summary>
        /// En cada parte tardía del tick, encarar el agente
        /// </summary>
        public override void LateUpdate()
        {
            transform.LookAt(transform.position + velocidad);
        }
    }
}
