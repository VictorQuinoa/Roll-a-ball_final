using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public float Sensibilidad = 100f; 
    public Transform playerBody;    
    private float xRotacion = 0f;  
    private float yRotacion = 0f;
    private float speed =10f;
  

    private void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * Sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensibilidad * Time.deltaTime;

        xRotacion -= mouseY;
        //xRotacion = Mathf.Clamp(xRotacion, -90f, 90f);
        
        yRotacion += mouseX;
       // yRotacion = Mathf.Clamp(yRotacion,-180f,180f);
        transform.localRotation= Quaternion.Euler(xRotacion,yRotacion,0f);

       // transform.localRotation = Quaternion.Euler(xRotacion, 0f, 0f);
      

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");     

      
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        move.y = 0; 

        playerBody.Translate(move * speed * Time.deltaTime, Space.World);
     
        playerBody.Rotate(Vector3.up * mouseX);

        
    }
}