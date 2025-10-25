using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    public float health;
    public float invincibleDuration;
    public Material playerMaterial;
    public float flashDuration = 0.1f;

    private bool invincible = false;
    private Color flashColor = Color.white;
    private Color originalColor;
    private Rigidbody rb;
    private Spells spells;
    private KnockBack KnockBack;



    void Start()
    {
        originalColor = playerMaterial.color;
        rb = GetComponent<Rigidbody>();
        spells = GetComponent<Spells>();
        KnockBack = GetComponent<KnockBack>();
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

        GameObject lightningCharge = null;
        //Spell casting controls
        if (Input.GetKeyDown(KeyCode.UpArrow))
            lightningCharge = spells.LightingChargeStart();
        if (Input.GetKeyUp(KeyCode.UpArrow))
            spells.LightningStrike();

                if (Input.GetKeyDown(KeyCode.DownArrow))
            spells.Earthquake();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            spells.Fireball();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            spells.WaterSplash();

    }

    public void TakeDamage(int damage, GameObject attacker, float knockBackForce, float forceDelay)
    {
        if (!invincible)
        {
            health -= damage;
            invincible = true;
            KnockBack.KnockBackEffect(attacker, knockBackForce, forceDelay);
            StartCoroutine(DamageFlash());
            Invoke("ResetInvincible", invincibleDuration);
        }

        if (health <= 0) { Debug.Log("Player is Dead :("); }
    }

    private void ResetInvincible()
    {
        invincible = false;
    }

    private IEnumerator DamageFlash()
    {
        playerMaterial.color = flashColor;
        yield return new WaitForSeconds(flashDuration/3);
        playerMaterial.color = originalColor;
        yield return new WaitForSeconds(flashDuration/3);
        playerMaterial.color = flashColor;
        yield return new WaitForSeconds(flashDuration/3);
        playerMaterial.color = originalColor;
    }

    private void OnApplicationQuit()
    {
        playerMaterial.color = originalColor;
    }
}
