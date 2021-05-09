using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzEscenario : MonoBehaviour
{
    public GameObject luzGameObject;
    private Light luz;
    private float tiempoCambiaLuz = 0;
    private float currTiempoCambiaLuz = 0;
    public Vector3 anguloGiro;
    // Start is called before the first frame update
    void Start()
    {
        luz = luzGameObject.GetComponent<Light>();
        tiempoCambiaLuz = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        currTiempoCambiaLuz += Time.deltaTime;
        if (currTiempoCambiaLuz >= tiempoCambiaLuz)
        {
            luz.color = Random.ColorHSV();
            currTiempoCambiaLuz = 0;
            tiempoCambiaLuz = Random.Range(3, 5);
        }
        transform.Rotate(anguloGiro);
    }
}
