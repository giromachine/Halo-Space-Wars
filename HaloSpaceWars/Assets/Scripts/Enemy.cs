using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBShots = 0.2f;
    [SerializeField] float maxTimeBShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 1f;

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
            Destroy(gameObject);
        }
    }
}
