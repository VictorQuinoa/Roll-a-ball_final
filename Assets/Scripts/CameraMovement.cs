using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    private bool isFirstPerson = false;
    private bool isCenital = false;

    public float firstPersonHeight = 0.5f;
    public float cenitalHeight = 10f;
    public float cenitalDistance = 5f; // Nueva distancia para alejar la cámara cenital
    public float rotationSpeed = 20f;  // Velocidad de rotación en la vista cenital

    public float mouseSensitivity = 100f;
    public float movementSpeed = 5f;

    private float xRotation = 0f; // Rotación vertical
    private float yRotation = 0f; // Rotación horizontal

    private Rigidbody playerRigidbody;

    void Start()
    {
        offset = transform.position - player.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Cambiar entre primera persona y tercera persona
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFirstPerson = !isFirstPerson;
            isCenital = false;  // Asegurarse de que no esté en modo cenital
            Cursor.lockState = isFirstPerson ? CursorLockMode.Locked : CursorLockMode.None;
        }

        // Cambiar al modo cenital cuando se presione la tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCenital = !isCenital;
            isFirstPerson = false;  // Asegurarse de que no esté en modo primera persona
            Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor para vista cenital
        }

        if (isFirstPerson)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    void LateUpdate()
    {
        if (isFirstPerson)
        {
            transform.position = player.transform.position + new Vector3(0, firstPersonHeight, 0);
        }
        else if (isCenital)
        {
            // Colocar la cámara en la posición cenital, pero desplazada para verla desde un ángulo
            Vector3 cenitalPosition = player.transform.position + new Vector3(0, cenitalHeight, -cenitalDistance);
            transform.position = cenitalPosition;

            // Rotar alrededor del jugador en el eje Y
            transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

            // Asegurar que la cámara siempre mire al jugador
            transform.LookAt(player.transform.position);
        }
        else
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position);
        }
    }

    private void HandleFirstPersonMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection = (forward * moveVertical + right * moveHorizontal).normalized;
        playerRigidbody.velocity = movementDirection * movementSpeed + new Vector3(0, playerRigidbody.velocity.y, 0);
    }
}
