using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour {

    //config parameters
    [Header("player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding;
    [SerializeField] float health = 500f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float explosionVolume = 0.6f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0,1)]float shootSoundVolume=0.25f;
    [SerializeField] float waitTime = 3f;
  

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed  = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [SerializeField] string moveX;
    [SerializeField] string moveY;



    float xMin;
    float xMax;
    float yMin;
    float yMax;
   
    Coroutine fireCoroutine;
   

    void Start ()
    {
        SetUpMoveBoundries();
	}


    void Update ()
    {
        Move();
        Fire();
	}

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());

        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound,Camera.main.transform.position,shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
       // var deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
       // var deltaY = Input.GetAxis("Vertical")*Time.deltaTime*moveSpeed;


        var deltaX = Input.GetAxis(moveX) * Time.deltaTime * moveSpeed;
         var deltaY = Input.GetAxis(moveY) * Time.deltaTime * moveSpeed;

        var newXPos=Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        var newYpos=Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);
        transform.position = new Vector2(newXPos, newYpos);
    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
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
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, explosionVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }

  

   
}
