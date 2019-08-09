using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int lifes = 3;

    //[HideInInspector]
    public int currentLifes;
    public Text textLife;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        currentLifes = lifes;
        textLife.text = ": " + currentLifes;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void QuitLife()
    {
        currentLifes--;
        textLife.text = ": " + currentLifes;
        
        sprite.color = Color.red;
        GetComponent<PlayerController>().fireRate = 0.5f;

        StartCoroutine(RedShip());
    }

    IEnumerator RedShip()
    {
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }
}
