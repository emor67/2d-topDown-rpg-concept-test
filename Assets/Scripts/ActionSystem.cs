using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        //canvas.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Yey!");
            //canvas.gameObject.SetActive(true);

        }
    }
   
}
