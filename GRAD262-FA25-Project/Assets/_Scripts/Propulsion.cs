using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Propulsion : MonoBehaviour
{
    public float speed = 5;
    public bool destroy; //Allows you to public determine whether the game object will be destroyed after a length of time.
    public float destroyTime; //Defines the amount of time before the game object destroys itself.
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Once the game object appears it begins to move forward by the public speed.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (destroy)
            Invoke("DestroyObject", destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void DestroyObject() { Destroy(gameObject); }
}
