using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots=0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float health=100f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion=1;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float explosionVolume=0.6f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.4f;

    [SerializeField] int scoreValue=150;

    void Start () 
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () 
    {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if ( shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProccessHit(damageDealer);
    }

    private void ProccessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {


        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound,Camera.main.transform.position,explosionVolume);
        FindObjectOfType<GameSession>().addToScore(scoreValue);
    }
}
