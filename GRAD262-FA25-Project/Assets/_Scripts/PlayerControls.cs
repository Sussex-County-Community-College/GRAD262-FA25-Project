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
    private GameOverMenu gameOverMenu;
    private Animator animator;
    private float rotationSpeed = 3000;
    private Vector3 moveDir;
    private bool invincible = false;
    private bool attacking = false;
    private Color flashColor = Color.white;
    private Color originalColor;

    void Start()
    {
        originalColor = playerMaterial.color; //Remebers the orignial color of the player.

        //Retrieves components from the player.
        spells = GetComponent<Spells>();
        KnockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        hud.SetMaxHealth(maxHealth);
        hud.SetMana(maxMana);
        gameOverMenu = GameObject.Find("Canvas").GetComponent<GameOverMenu>();

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
        animator.SetFloat("forward", moveDir.magnitude); //Animates the movement.

        //Turns the player in the direction they are looking in. 
        if(moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //UpSpell casting controls--------------------------------------------------------------------------------------------------------------------------
        GameObject lightningCharge = null;
        bool lightningCharged = false;
        if (Input.GetKeyDown(KeyCode.UpArrow) && mana >= spells.lightningManaCost && !attacking)
        {
            attacking = true;
            lightningCharged = true;
            lightningCharge = spells.LightingChargeStart();
            hud.SetSpellIcon("Up", Color.gray);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && lightningCharged)
        {
            lightningCharged = false;
            spells.LightningStrike();
            hud.SetSpellIcon("Up", Color.white);
            mana -= spells.lightningManaCost;
            hud.SetMana(mana);
        }

        //DownSpell casting controls--------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.DownArrow) && mana >= spells.earthquakeManaCost && !attacking)
        {
            attacking = true;
            spells.CastEarthquake();
            hud.SetSpellIcon("Down", Color.gray);
            mana -= spells.earthquakeManaCost;
            hud.SetMana(mana);
            Invoke("ResetAttack", spells.earthquakeAnimDuration);
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            hud.SetSpellIcon("Down", Color.white);
        }

        //LeftSpell casting controls--------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftArrow) && mana >= spells.fireManaCost && !attacking)
        {
            attacking = true;
            spells.CastFireball();
            hud.SetSpellIcon("Left", Color.gray);
            mana -= spells.fireManaCost;
            hud.SetMana(mana);
            Invoke("ResetAttack", spells.fireAnimDuration);
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            hud.SetSpellIcon("Left", Color.white);
        }

        //RightSpell casting controls--------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.RightArrow) && mana >= spells.waterManaCost && !attacking)
        {
            attacking = true;
            spells.CastWaterSplash();
            hud.SetSpellIcon("Right", Color.gray);
            mana -= spells.waterManaCost;
            hud.SetMana(mana);
            Invoke("ResetAttack", spells.waterAnimDuration);
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
            animator.SetTrigger("takeDamage");
            invincible = true;
            KnockBack.KnockBackEffect(attacker, knockBackForce, forceDelay);
            StartCoroutine(DamageFlash());
            Invoke("ResetInvincible", invincibleDuration);
        }

        //Triggers Game Over.
        if (health <= 0) 
        {
            playerMaterial.color = originalColor;
            gameOverMenu.GameOver(); 
        } 
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

    private void ResetAttack()
    {
        attacking = false;
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
