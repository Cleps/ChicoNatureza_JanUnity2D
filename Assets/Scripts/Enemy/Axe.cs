using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isInvisbleDamage = false;
    void Start()
    {
        if(isInvisbleDamage)
        {
            Destroy(gameObject, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<Player>().takeDamege();
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}