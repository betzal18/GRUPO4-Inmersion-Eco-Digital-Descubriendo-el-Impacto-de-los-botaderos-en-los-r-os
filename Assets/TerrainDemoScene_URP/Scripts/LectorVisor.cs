using UnityEngine;

public class LectorVisor : MonoBehaviour
{
    private SensorAmbiental puntoActual;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 15f))
        {
            SensorAmbiental sensor = hit.collider.GetComponent<SensorAmbiental>();

            if (sensor != null)
            {
                if (puntoActual != sensor)
                {
                    puntoActual = sensor;
                    puntoActual.analizando = true;
                    puntoActual.cronometro = 0f;
                }
            }
            else
            {
                ResetearLector();
            }
        }
        else
        {
            ResetearLector();
        }
    }

    void ResetearLector()
    {
        if (puntoActual != null)
        {
            puntoActual.analizando = false;
            puntoActual.cronometro = 0f;
            puntoActual = null;
        }
    }
}
