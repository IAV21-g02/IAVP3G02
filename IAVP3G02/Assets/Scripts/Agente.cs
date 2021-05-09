/*    
   Copyright (C) 2020 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
namespace UCM.IAV.Movimiento {

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine; 

/// <summary>
/// La clase Agente es responsable de modelar los agentes y gestionar todos los comportamientos asociados para combinarlos (si es posible) 
/// </summary>
    public class Agente : MonoBehaviour {
        /// <summary>
        /// Mezclar por peso
        /// </summary>
        [Tooltip("Mezclar por peso.")]
        public bool mezclarPorPeso = false;
        /// <summary>
        /// Mezclar por prioridad
        /// </summary>
        [Tooltip("Mezclar por prioridad.")]
        public bool mezclarPorPrioridad = false;
        /// <summary>
        /// Umbral de prioridad para tener el valor en cuenta
        /// </summary>
        [Tooltip("Umbral de prioridad.")]
        public float umbralPrioridad = 0.2f;
        /// <summary>
        /// Velocidad máxima
        /// </summary>
        [Tooltip("Velocidad máxima.")]
        public float velocidadMax;
        /// <summary>
        /// Aceleración máxima
        /// </summary>
        [Tooltip("Aceleración máxima.")]
        public float aceleracionMax;
        /// <summary>
        /// Rotación máxima
        /// </summary>
        [Tooltip("Rotación máxima.")]
        public float rotacionMax;
        /// <summary>
        /// Aceleración angular máxima
        /// </summary>
        [Tooltip("Aceleración angular máxima.")]
        public float aceleracionAngularMax;
        /// <summary>
        /// Orientacion (es como la velocidad angular)
        /// </summary>
        [Tooltip("Orientación.")]
        public float orientacion;
        /// <summary>
        /// Rotatción (valor que puede variar, como la velocidad, para cambiar la orientación)
        /// </summary>
        [Tooltip("Rotación.")]
        public float rotacion;
        /// <summary>
        /// Velocidad
        /// </summary>
        [Tooltip("Velocidad.")]
        public Vector3 velocidad;
        /// <summary>
        /// Valor de dirección / direccionamiento
        /// </summary>
        [Tooltip("Dirección.")]
        protected Direccion direccion;
        /// <summary>
        /// Grupos de direcciones, agrupados por valor de prioridad
        /// </summary>
        [Tooltip("Grupos de direcciones.")]
        private Dictionary<int, List<Direccion>> grupos;
        /// <summary>
        /// Componente de cuerpo rígido
        /// </summary>
        [Tooltip("Cuerpo rígido.")]
        private Rigidbody cuerpoRigido;

        /// <summary>
        /// Al comienzo, se inicialian algunas variables
        /// </summary>
        void Start()
        {
            velocidad = Vector3.zero;
            direccion = new Direccion();
            grupos = new Dictionary<int, List<Direccion>>();
            cuerpoRigido = GetComponent<Rigidbody>(); // Cojo el cuerpo rígido
        }

        /// <summary>
        /// En cada tick fijo, si hay cuerpo rígido, uso el simulador físico aplicando fuerzas o no
        /// </summary>
        public virtual void FixedUpdate()
        {
            if (cuerpoRigido == null)
                return;

            Vector3 displacement = velocidad * Time.deltaTime;
            orientacion += rotacion * Time.deltaTime;
            // Necesitamos "constreñir" inteligentemente la orientación al rango (0, 360)
            if (orientacion < 0.0f)
                orientacion += 360.0f;
            else if (orientacion > 360.0f)
                orientacion -= 360.0f;
            // El ForceMode dependerá de lo que quieras conseguir
            // Estamos usando VelocityChange sólo con propósitos ilustrativos
            cuerpoRigido.AddForce(displacement, ForceMode.VelocityChange);
            Vector3 orientationVector = OriToVec(orientacion);
            cuerpoRigido.rotation = Quaternion.LookRotation(orientationVector, Vector3.up);
        }

        /// <summary>
        /// En cada tick, hace lo básico del movimiento del agente
        /// </summary>
        public virtual void Update()
        {
            if (cuerpoRigido != null)
                return;
            // ... código previo
            Vector3 desplazamiento = velocidad * Time.deltaTime;
            orientacion += rotacion * Time.deltaTime;
            // Necesitamos "constreñir" inteligentemente la orientación al rango (0, 360)
            if (orientacion < 0.0f)
                orientacion += 360.0f;
            else if (orientacion > 360.0f)
                orientacion -= 360.0f;
            transform.Translate(desplazamiento, Space.World);
            // Restaura la rotación al punto inicial antes de rotar el objeto nuestro valor
            transform.rotation = new Quaternion();
            transform.Rotate(Vector3.up, orientacion);
        }

        /// <summary>
        /// En cada parte tardía del tick, hace tareas de corrección numérica (ajustar a los máximos, la combinación etc.
        /// </summary>
        public virtual void LateUpdate()
        {
            if (mezclarPorPrioridad)
            {
                direccion = GetPrioridadDireccion();
                grupos.Clear();
            }
            velocidad += direccion.lineal * Time.deltaTime;
            rotacion += direccion.angular * Time.deltaTime;

            if (velocidad.magnitude > velocidadMax)
            {
                velocidad.Normalize();
                velocidad = velocidad * velocidadMax;
            }

            if (rotacion > rotacionMax)
            {
                rotacion = rotacionMax;
            }

            if (direccion.angular == 0.0f)
            {
                rotacion = 0.0f;
            }

            if (direccion.lineal.sqrMagnitude == 0.0f)
            {
                velocidad = Vector3.zero;
            }

            // En realidad si se quiere cambiar la orientación lo suyo es hacerlo con un comportamiento, no así:
            transform.LookAt(transform.position + velocidad);

            // Se limpia el steering de cara al próximo tick
            direccion = new Direccion();
        }

        /// <summary>
        /// Establece la dirección tal cual
        /// </summary>
        public void SetDireccion(Direccion direccion)
        {
            this.direccion = direccion;
        }

        /// <summary>
        /// Establece la dirección por peso
        /// </summary>
        /// <param name="direccion"></param>
        /// <param name="peso"></param>
        public void SetDireccion(Direccion direccion, float peso)
        {
            this.direccion.lineal += (peso * direccion.lineal);
            this.direccion.angular += (peso * direccion.angular);
        }

        /// <summary>
        /// Establece la dirección por prioridad
        /// </summary>
        /// <param name="direccion"></param>
        /// <param name="prioridad"></param>
        public void SetDireccion(Direccion direccion, int prioridad)
        {
            if (!grupos.ContainsKey(prioridad))
            {
                grupos.Add(prioridad, new List<Direccion>());
            }
            grupos[prioridad].Add(direccion);
        }

        /// <summary>
        /// Devuelve el valor de dirección calculado por prioridad
        /// </summary>
        /// <returns></returns>
        private Direccion GetPrioridadDireccion()
        {
            Direccion direccion = new Direccion();
            List<int> gIdList = new List<int>(grupos.Keys);
            gIdList.Sort();
            foreach (int gid in gIdList)
            {
                direccion = new Direccion();
                foreach (Direccion direccionIndividual in grupos[gid])
                {
                    direccion.lineal += direccionIndividual.lineal;
                    direccion.angular += direccionIndividual.angular;
                }
                if (direccion.lineal.magnitude > umbralPrioridad
                     || Mathf.Abs(direccion.angular) > umbralPrioridad)
                {
                    return direccion;
                }
            }
            return direccion;
        }
        /// <summary>
        /// Calculates el Vector3 dado un cierto valor de orientación
        /// </summary>
        /// <param name="orientacion"></param>
        /// <returns></returns>
        public Vector3 OriToVec(float prientacion)
        {
            Vector3 vector = Vector3.zero;
            vector.x = Mathf.Sin(prientacion * Mathf.Deg2Rad) * 1.0f;
            vector.z = Mathf.Cos(prientacion * Mathf.Deg2Rad) * 1.0f;
            return vector.normalized;
        }
    }
}
