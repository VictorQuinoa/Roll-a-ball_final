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
