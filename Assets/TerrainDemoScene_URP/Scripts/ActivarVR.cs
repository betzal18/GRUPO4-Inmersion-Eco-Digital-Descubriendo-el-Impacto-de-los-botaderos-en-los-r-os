using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class ActivarVR : MonoBehaviour
{
    IEnumerator Start()
    {
        // Espera 3 segundos completos para que el celular cargue el río de forma segura
        yield return new WaitForSeconds(3.0f);

        // Verifica si el sistema XR está listo antes de encender los lentes
        if (XRGeneralSettings.Instance != null && XRGeneralSettings.Instance.Manager != null)
        {
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
            if (XRGeneralSettings.Instance.Manager.activeLoader != null)
            {
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
        }
    }
}
