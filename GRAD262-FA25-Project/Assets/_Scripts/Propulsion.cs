using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Propulsion : MonoBehaviour
{
    public float speed = 5;
    public bool destroy;
    public float destroyTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
