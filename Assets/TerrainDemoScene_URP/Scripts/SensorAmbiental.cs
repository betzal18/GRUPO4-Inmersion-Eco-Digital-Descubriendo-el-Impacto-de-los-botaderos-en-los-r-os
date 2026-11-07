using UnityEngine;
using TMPro;

public class SensorAmbiental : MonoBehaviour
{
    [Header("Monitoreo de Calidad de Agua - Rio Socabaya")]
    public string codigoEstacion;
    public string ubicacionDetallada;
    public float ph;
    public float oxigenoDisuelto;
    public float conductividad;
    public string evaluacionEca;

    [HideInInspector] public float cronometro = 0f;
    [HideInInspector] public bool analizando = false;
    private TextMeshProUGUI pantallaTexto;

    void Start()
    {
        GameObject textObj = GameObject.Find("Texto_Pantalla");
        if (textObj != null)
        {
            pantallaTexto = textObj.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (analizando && pantallaTexto != null)
        {
            cronometro += Time.deltaTime;
            pantallaTexto.text = $"<b>[RECOLECTANDO MUESTRA: {codigoEstacion}]</b>\nEstabilizando electrodos: {(cronometro / 2f * 100f):F0}%";

            if (cronometro >= 2f)
            {
                MostrarResultados();
                analizando = false;
            }
        }
    }

    void MostrarResultados()
    {
        if (pantallaTexto != null)
        {
            pantallaTexto.text = $"<b>MUESTRA: {codigoEstacion}</b> ({ubicacionDetallada})\n" +
                                 $"------------------------------------\n" +
                                 $"pH: {ph} (Limite ECA: 6.5 - 8.5)\n" +
                                 $"O.D.: {oxigenoDisuelto} mg/L (Minimo: 5.0)\n" +
                                 $"C.E.: {conductividad} uS/cm (Aporte de sales)\n" +
                                 $"------------------------------------\n" +
                                 $"<b>EVALUACION: {evaluacionEca}</b>";
        }
    }
}
