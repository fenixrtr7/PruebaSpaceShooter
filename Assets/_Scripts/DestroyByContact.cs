using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    // public GameObject playerExplosion;

    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        // Obtener el componente GameObject
        GameObject gameControllerObject = GameObject.FindWithTag("GameManager");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("No se encuentra el GameController script");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Evitar la destruccio del objeto por estr en contacto con boundary o enemy
        if (other.CompareTag ("Boundary"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        if(other.tag =="Player")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);

            // Llamar al player para quitar vida

            if(other.GetComponent<Health>().currentLifes <= 0)
            {
                gameController.GameOver();
                Destroy(other.gameObject);
            }else
            {
                other.GetComponent<Health>().QuitLife();
            }
            
        }
        gameController.AddScore(scoreValue);

        if(other.tag =="Laser")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
