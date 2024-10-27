using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSec : MonoBehaviour
{   
    public float timer = 0.5f;
        // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
