using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : MonoBehaviour
{
    public float health, maxHealth, width, height, mana, maxMana;
    public RectTransform healthbar;
    public RectTransform manabar;
    public Text XP;
    public Image UpSpell;
    public Image DownSpell;
    public Image LeftSpell;
    public Image RightSpell;
    public GameObject FadeInPanel;

    private void Awake()
    {
        FadeInPanel.SetActive(true);
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        this.health = health;
        float newWidth = (health / maxHealth) * width;
        healthbar.sizeDelta = new Vector2(newWidth, height);
    }

    public void SetMaxMana(float maxMana)
    {
        this.maxMana = maxMana;
    }

    public void SetMana(float mana)
    {
        this.mana = mana;
        float newWidth = (mana / maxMana) * width;
        manabar.sizeDelta = new Vector2(newWidth, height);
    }

    public void SetXP(int xp)
    {
        XP.text = "XP: " + xp;
    }

    public void SetSpellIcon(string spell, Color color)
    {
        switch(spell)
        {
            case "Up":
                UpSpell.color = color;
                break;
            case "Left":
                LeftSpell.color = color;
                break;
            case "Right":
                RightSpell.color = color;
                break;
            case "Down":
                DownSpell.color = color;
                break;
        }
    }

    
}
