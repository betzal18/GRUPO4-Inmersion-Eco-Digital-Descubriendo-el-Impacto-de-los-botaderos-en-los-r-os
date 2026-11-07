using UnityEngine;
using TMPro;

public class WaterAnalyzer : MonoBehaviour
{
    public GameObject contaminacionInvisible;
    public TMP_Text textoResultado;

    private bool analisisActivo = false;
    private string zonaActual = "Ninguna"; // Guarda en qué parte del río estamos parados

    public void ActivarAnalisis()
    {
        analisisActivo = !analisisActivo;

        // Si el análisis está activo, enciende las partículas de contaminación invisible
        if (contaminacionInvisible != null)
        {
            contaminacionInvisible.SetActive(analisisActivo);
        }

        ActualizarPantalla();
    }

    // Detecta automáticamente cuando el Water Analyzer entra en un Punto de Muestreo del río
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Punto_Muestreo_1")
        {
            zonaActual = "Punto1";
            if (analisisActivo) ActualizarPantalla();
        }
        else if (other.gameObject.name == "Punto_Muestreo_2")
        {
            zonaActual = "Punto2";
            if (analisisActivo) ActualizarPantalla();
        }
        else if (other.gameObject.name == "Punto_Muestreo_3")
        {
            zonaActual = "Punto3";
            if (analisisActivo) ActualizarPantalla();
        }
    }

    // Cuando el usuario saca el analizador del agua, la zona se limpia
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Punto_Muestreo"))
        {
            zonaActual = "Ninguna";
            if (analisisActivo) ActualizarPantalla();
        }
    }

    // Esta función centraliza todos los resultados científicos de tu investigación
    private void ActualizarPantalla()
    {
        if (!analisisActivo)
        {
            textoResultado.text = "Modo de observación normal.\nPulse el botón para encender el escáner.";
            textoResultado.color = Color.white;
            return;
        }

        // Resultados dependiendo de la ubicación en la escena de Unity
        switch (zonaActual)
        {
            case "Punto1": // Lejos del botadero
                textoResultado.text = "🔬 ANÁLISIS: ZONA ALTA\n\nEstado: ÓPTIMO\npH: 7.2\nContaminación: 0%\nAgua libre de residuos.";
                textoResultado.color = Color.green;
                break;

            case "Punto2": // Al medio
                textoResultado.text = "🔬 ANÁLISIS: ZONA MEDIA\n\nEstado: ALERTA LEVE\npH: 6.1\nPresencia menor de metales.\nOrigen difuso.";
                textoResultado.color = Color.yellow;
                break;

            case "Punto3": // Frente al botadero El Cebollar
                textoResultado.text = "🔬 ANÁLISIS: CRÍTICO\n\n¡PELIGRO MÁXIMO!\npH: 4.5 (Muy Ácido)\nMetales pesados: Críticos\nOrigen: Filtración de El Cebollar.";
                textoResultado.color = Color.red;
                break;

            default: // Si activa el escáner fuera del agua o en el aire
                textoResultado.text = "🔬 ESCÁNER ACTIVADO\n\nAcerque el equipo al agua del río para tomar muestras.";
                textoResultado.color = Color.cyan;
                break;
        }
    }
}
