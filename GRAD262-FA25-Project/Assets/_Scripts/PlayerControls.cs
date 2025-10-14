using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public Spells spells;

    
    
    void Start()
    {
        
    }

    void Update()
    {
        //Player movement controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //Spell casting controls
        if (Input.GetKeyDown(KeyCode.UpArrow))
            spells.LightningStrike();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            spells.Earthquake();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            spells.Firebolt();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            spells.WaterSplash();

    }
}
