using UnityEngine;

public class MovimientoIngeniero : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 8.0f;
    private CharacterController controller;
    private Transform camara;
    private float gravedad = -9.81f;
    private Vector3 velocidadVertical;

    [Header("Control Temporal (Sin Giroscopio)")]
    public float velocidadGiroTactil = 0.2f;
    private float rotacionX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Camera camaraComponente = GetComponentInChildren<Camera>();
        if (camaraComponente != null) camara = camaraComponente.transform;
        else camara = Camera.main?.transform;
    }

    void Update()
    {
        // 1. GIRAR A LOS LADOS TOCANDO LA PANTALLA (Ya que el celular no tiene giroscopio)
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            if (toque.phase == TouchPhase.Moved)
            {
                // Al arrastrar el dedo horizontalmente, giras el cuerpo del personaje
                transform.Rotate(0, toque.deltaPosition.x * velocidadGiroTactil, 0);
            }
        }

        // 2. LEER LA INCLINACIÓN (ARRIBA/ABAJO) USANDO EL ACELERÓMETRO FÍSICO
        // Input.acceleration lee la gravedad del teléfono. El eje Z nos dice si miras al piso.
        Vector3 aceleracion = Input.acceleration;

        // Convertimos la inclinación del teléfono en rotación para mirar arriba y abajo
        rotacionX = -aceleracion.z * 90f;
        rotacionX = Mathf.Clamp(rotacionX, -60f, 60f); // Evitamos giros extraños

        if (camara != null)
        {
            camara.localEulerAngles = new Vector3(rotacionX, 0, 0);
        }

        if (controller == null) return;

        Vector3 direccionMovimiento = Vector3.zero;

        // 3. AVANZAR AL MIRAR HACIA ABAJO (Detectado por el Acelerómetro)
        if (camara != null)
        {
            // Si inclinas el celular hacia adelante (como mirando al piso con las gafas)
            if (rotacionX > 15f && rotacionX < 60f)
            {
                Vector3 dirMirada = camara.forward;
                dirMirada.y = 0; // Mantiene el movimiento en el suelo
                direccionMovimiento = dirMirada.normalized;
            }
        }

        // Aplicamos el avance
        if (direccionMovimiento != Vector3.zero)
        {
            controller.Move(direccionMovimiento * velocidad * Time.deltaTime);
        }

        // Gravedad estándar
        if (controller.isGrounded && velocidadVertical.y < 0) velocidadVertical.y = -2f;
        else velocidadVertical.y += gravedad * Time.deltaTime;

        controller.Move(velocidadVertical * Time.deltaTime);
    }
}

