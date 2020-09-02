using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config param
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] float laserSpeed = 1f;
    [SerializeField] float laserPeriod = 2f;
    [SerializeField] GameObject laserPrefab;

    Coroutine firing;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetMoveArea();
        
    }

    void Update()
    {
        Move();
        Shoot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageControl damageDealer = other.gameObject.GetComponent<DamageControl>();
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageControl damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firing = StartCoroutine(ShootContinuos());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firing);
        }
    }
    IEnumerator ShootContinuos()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                    transform.position, Quaternion.identity)
                    as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(laserPeriod);
        }
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetMoveArea()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.08f, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.92f, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.05f, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.33f, 0)).y - padding;
    }

}
