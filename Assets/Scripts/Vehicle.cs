using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool Iscar;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Iscar)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }

    


}
