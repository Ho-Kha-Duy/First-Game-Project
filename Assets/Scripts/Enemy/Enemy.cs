using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    
    [HideInInspector]
    public float speed;

    public GameObject deathEffect;
    public int moneyGain = 50;
    public float startHealth = 100;
    private float health;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow (float percentage)
    {
        speed = startSpeed * (1f - percentage);
    }

    void Die()
    {
        isDead = true;

        PlayerStats.Money += moneyGain;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        SpawnManager.EnemiesAlive--;

        Destroy(gameObject);
    }
}
