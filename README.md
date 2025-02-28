# Roll a ball

## Descripción 

En este proyecto se desarrolla un juego siguiendo el tutorial de Unity de roll a ball.
La aplicación consiste en la creación de un mapa en el cual el jugador controlando la bola debe recoger una serie de puntos para ganar.

## Desarrollo
<details>
  <summary>Cámara</summary>

  Para el control de la cámara se crea un script que permite el cambio de perspectiva pulsando la tecla f.
  
  Una vez pulsada la cámara cambia de una vista cenital a una en primera persona, adaptando los controles a esta última y fijando el cursor para evitar errores en el movimiento de cámara.

</details>

<details>
  
  <summary>Enemigos</summary>

  La creación de enemigos requiere de el uso de NavMesh. Este elemento mapea el mapa creado para que los enemigos creados detecten los lugares por los que moverse.

  El movimiento se da mediante un script en el que estableceremos el NavMesh y establecemos el objetivo de los enemigos en la posición del jugador para que intenten alcanzarle.

  ```
public Transform player;
    private NavMeshAgent NavMeshAgent;
    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            NavMeshAgent.SetDestination(player.position);
        }
        
    }
```

  Por último, se activa la función trigger y se les etiqueta como enemigo. Esto, codificando el script del jugador para detectar el contacto con el enemigo , pierdas la partida.

  ```
if (other.gameObject.CompareTag("Enemigo") && !isInvulnerable)
        {
            rb.gameObject.SetActive(false);
            Destroy(rb);
            countText.text = "Has perdido";
        }
```

  A mayores, he esablecido elementos del entorno ( agua y cactus) con el mismo efecto de eliminar al jugador, pero sin capacidad de movimiento.
  
</details>

<details>
  <summary>
    Elementos interactivos
  </summary>

  Dentro de los elementos interactivos encontramos una rampa que impulsa al jugador, y los pickups que debe recoger.

  ## Pick Ups

   Los pickups son los puntos que el jugador debe recoger para ganar la partida.
   
   Para su creación, se crea un cubo y se coloca en el mapa de la manera deseada.
   
   Para darles su función se les eiqueta como pickups, y como a los enemigos se les establece como trigger. La diferencia es que en este caso, al tocar el jugador los pickups, los que desaparecen son estosm y al jugador le aumenta la puntución y, en caso de recoger 
   todos, se lanza un mensaje de victoria.

   ```
void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

 void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 14)
        {
            winTextObject.SetActive(true);
        }
    }
   ```

## Rampa

   La rampa simplemente cumple la función de impulsar al jugador cuando pase sobre ella, so mediante otro trigger y el uso de vectores y una fuerza que realiza el lanzamiento.

   ```
public class Ramp : MonoBehaviour
{
    [Tooltip("Dirección fija de lanzamiento (debe estar normalizada)")]
    public Vector3 launchDirection = new Vector3(1, 0.5f, 0);

    public float launchForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegurar que solo afecte a la pelota
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 finalLaunchDirection = launchDirection.normalized;
                rb.velocity = Vector3.zero;
                rb.AddForce(finalLaunchDirection * launchForce, ForceMode.Impulse);
                Debug.Log("Pelota lanzada en dirección: " + finalLaunchDirection);
            }
        }
    }
}
   ```

  Este script es propio de la rampa y en este caso detecta la etiqueta del jugador.

  
</details>
