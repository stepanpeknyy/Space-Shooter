using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] int health=5;
    int healthValue = 1;
    [SerializeField] Sprite poweredPlayer;
    [SerializeField] Sprite normalPlayer;

    [Header("Audio")]
    [SerializeField] AudioClip damage;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip death;
    [SerializeField] float damageSoundVolume;
    [SerializeField] float shootSoundVolume;
    [SerializeField] float deathSoundVolume;
    [SerializeField] AudioClip lootHealth;
    [SerializeField] AudioClip lootPower;

    [Header ("Player Movement")]
    [SerializeField ] float  moveSpeed =10f;
    [SerializeField] float projectileSpeed = 20f;
    
    [Header("Projectile")]
    [SerializeField ] GameObject projectile;
    [SerializeField] float projTimeRecharge=0.2f;

    Coroutine firing;

    float maxX, maxY, minX, minY;
    float waitTime = 0f;
    bool poweredUp = false;
    // Start is called before the first frame update

    void Start()
    {
        SetBorders();      
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shoot , Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projTimeRecharge);
        }
    }
    private void SetBorders()
    {
        Camera gameCamera = Camera.main;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-0.75f;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y -0.75f;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+ 0.75f;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y+ 0.75f;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        TimeOfPowerUpCounting();    
    }
private void TimeOfPowerUpCounting()
    {
        if (poweredUp == true)
        {           
            waitTime += Time.deltaTime;
        }
        if (waitTime >= 5)
        {
            poweredUp = false;
            projTimeRecharge = 0.2f;
            GetComponent<SpriteRenderer>().sprite = normalPlayer;

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PickUpHealth(other);
        PickUpPower(other);
        DamagePlayer(other);
    }

    private void DamagePlayer(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        HitPlayer(damageDealer);
        damageDealer.Hit();
    }

    private void PickUpHealth(Collider2D other)
    {
        if (other.CompareTag("LootHealth"))
        {
            FindObjectOfType<GameSession>().AddToHealth(healthValue);
            other.gameObject.GetComponent<Loot>().PickUpLoot();
            health += healthValue;
            AudioSource.PlayClipAtPoint(lootHealth, Camera.main.transform.position, 0.8f);
        }
    }

    private void PickUpPower(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (projTimeRecharge >= 0.2f)
            {
                projTimeRecharge = projTimeRecharge - 0.1f;
            }
            poweredUp = true;
            GetComponent<SpriteRenderer>().sprite = poweredPlayer;
            waitTime = 0f;
            other.gameObject.GetComponent<Loot>().PickUpLoot();
            AudioSource.PlayClipAtPoint(lootPower, Camera.main.transform.position, 0.8f);

        }
    }

    private void HitPlayer(DamageDealer damageDealer)
    {
        FindObjectOfType<GameSession>().SubtractFromHealth(healthValue);
        health -= damageDealer.GetDamagePlayer();
        AudioSource.PlayClipAtPoint(damage, Camera.main.transform.position, damageSoundVolume);
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, deathSoundVolume);
            FindObjectOfType <Level>().LoadGameOver();
        }
    }



    private void Fire()
    {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               firing= StartCoroutine (FireContinuously());
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopCoroutine(firing);
            }    
    }

    private void Move()
    {
        var deltaMoveX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed ;
        var deltaMoveY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newPositionX = Mathf.Clamp (transform.position.x +deltaMoveX ,minX, maxX   );
        var newPositionY = Mathf.Clamp(transform.position.y + deltaMoveY, minY,maxY);
        transform.position = new Vector2(newPositionX, newPositionY);
    }




}


