using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy data")]
    [SerializeField] int health = 100;
    [SerializeField] int pointMultiplier = 1;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBShots = 0.2f;
    [SerializeField] float maxTimeBShots = 3f;

    [Header("Projectile and particles")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float VFXduration = 1f;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathVolume = 0.7f;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootVolume = 0.7f;

    Vector3 ProjView = new Vector3(0, 0, 180f);

    

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBShots, maxTimeBShots);
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBShots, maxTimeBShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile,
                    transform.position, Quaternion.Euler(ProjView))
                    as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageControl damageDealer = other.gameObject.GetComponent<DamageControl>();
        if (!damageDealer) { return; }
        HitProcess(damageDealer);
    }

    private void HitProcess(DamageControl damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, VFXduration);
        FindObjectOfType<GameState>().AddScore(pointMultiplier);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
    }
}
