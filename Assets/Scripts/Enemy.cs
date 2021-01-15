using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int scoreValue = 100;
    [SerializeField] int health=100;  
    [SerializeField] float minTimeBetweenShots=0.5f;
    [SerializeField] float maxTimeBetweenShots = 2f;
    [SerializeField] float  projectileSpeed = -2f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion;
    [SerializeField] GameObject lootHealth;
    [SerializeField] GameObject lootPowerUp;
    [Header( "Audio")]
    [SerializeField] AudioClip shoot;
    [SerializeField] float shootSoundVolume;
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathSoundVolume;

    float shotCounter;
    float lootSpeed = 6;
    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    void Update()
    {
        CountDowmAndShoot();
    }

    private void CountDowmAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(shoot , Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
        damageDealer.Hit();
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound,Camera.main.transform.position , deathSoundVolume);
        Loot();
    }

    private void Loot()
    {
        int rand = Random.Range(1, 100);        
        if (rand > 90)
        {
            GameObject lootOneHeatlth = Instantiate(lootHealth, transform.position, Quaternion.identity) as GameObject;
            lootOneHeatlth.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -lootSpeed);
        }
        if (rand < 15)
        {
            GameObject lootPower = Instantiate(lootPowerUp, transform.position, Quaternion.identity) as GameObject;
            lootPower.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -lootSpeed);
        }
    }
}
