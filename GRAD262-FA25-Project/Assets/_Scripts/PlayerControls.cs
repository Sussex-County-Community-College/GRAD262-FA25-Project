using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public Material playerMaterial;
    public UI_Hud hud;
    public float moveSpeed; 
    public float health, maxHealth;
    public float mana, maxMana, manaRegnRate;
    public int XP = 0;
    public float invincibleDuration;
    public float flashDuration;

    private Spells spells;
    private KnockBack KnockBack;
    private Rigidbody rb;
    private float rotationSpeed = 3000;
    private Vector3 moveDir;
    private bool invincible = false;
    private Color flashColor = Color.white;
    private Color originalColor;

    void Start()
    {
        originalColor = playerMaterial.color; //Remebers the orignial color of the player.

        //Retrieves components from the player.
        spells = GetComponent<Spells>();
        KnockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody>();
        hud.SetMaxHealth(maxHealth);
        hud.SetMana(maxMana);

        //Calls a infinite looping method that will recover mana slowly over time.
        StartCoroutine(RecoverMana());
    }

    void Update()
    {
        //Player movement controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDir = new Vector3(horizontalInput, 0f, verticalInput);
        if (moveDir.magnitude > 1f)
        {
            moveDir.Normalize();
        }
        rb.AddForce(moveDir * moveSpeed *  Time.deltaTime, ForceMode.VelocityChange); //This is what applies force to the player.

        //Turns the player in the direction they are looking in. 
        if(moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //UpSpell casting controls
        GameObject lightningCharge = null;
        if (Input.GetKeyDown(KeyCode.UpArrow) && mana >= spells.lightningManaCost)
        {
            lightningCharge = spells.LightingChargeStart();
            hud.SetSpellIcon("Up", Color.gray);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && mana >= spells.lightningManaCost)
        {
            spells.LightningStrike();
            hud.SetSpellIcon("Up", Color.white);
            mana -= spells.lightningManaCost;
            hud.SetMana(mana);
        }

        //DownSpell casting controls
        if (Input.GetKeyDown(KeyCode.DownArrow) && mana >= spells.earthquakeManaCost)
        {
            spells.Earthquake();
            hud.SetSpellIcon("Down", Color.gray);
            mana -= spells.earthquakeManaCost;
            hud.SetMana(mana);
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            hud.SetSpellIcon("Down", Color.white);
        }

        //LeftSpell casting controls
        if (Input.GetKeyDown(KeyCode.LeftArrow) && mana >= spells.fireManaCost)
        {
            spells.Fireball();
            hud.SetSpellIcon("Left", Color.gray);
            mana -= spells.fireManaCost;
            hud.SetMana(mana);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            hud.SetSpellIcon("Left", Color.white);
        }

        //RightSpell casting controls
        if (Input.GetKeyDown(KeyCode.RightArrow) && mana >= spells.waterManaCost)
        {
            spells.WaterSplash();
            hud.SetSpellIcon("Right", Color.gray);
            mana -= spells.waterManaCost;
            hud.SetMana(mana);
        }
        if( Input.GetKeyUp(KeyCode.RightArrow))
        {
            hud.SetSpellIcon("Right", Color.white);
        }
            
        //Displays current XP value
        hud.SetXP(XP);
    }

    //Takes the amount of damage from an attack, who delivered the attack, the amount of knockback force,
    //and how long that force should be applied.
    //If the character is not currently invincible, they take damage, become invincible, and get knocked back.
    public void TakeDamage(int damage, GameObject attacker, float knockBackForce, float forceDelay)
    {
        if (!invincible)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            hud.SetHealth(health);
            invincible = true;
            KnockBack.KnockBackEffect(attacker, knockBackForce, forceDelay);
            StartCoroutine(DamageFlash());
            Invoke("ResetInvincible", invincibleDuration);
        }

        if (health <= 0) { Debug.Log("Player is Dead :("); } //This will eventually link to a game over system.
    }

    //Recovers mana by a publicly defined rate.
    //Loops infinitly
    private IEnumerator RecoverMana()
    {
        bool repeat = true;

        while (repeat)
        {
            mana++;
            mana = Mathf.Clamp(mana, 0, maxMana);
            hud.SetMana(mana);

            yield return new WaitForSeconds(manaRegnRate);


        }
        
    }

    //After the public amount of invincible duration, the bool will be reset to allow the player to once again take damage.
    private void ResetInvincible()
    {
        invincible = false;
    }

    //Controls the flash of invincibility.
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

    //When the game is closed, the orignal color of the player is once again applied to the material.
    //Prevents closing the game while taking damage and the color remains white.
    private void OnApplicationQuit()
    {
        playerMaterial.color = originalColor;
    }
}
