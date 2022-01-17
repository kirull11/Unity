using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public float damage = 21f;
    public float fireRate = 1f;
    public float force = 155f;
    public float range = 15f;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public GameObject hitEffect;

    public Camera _cam;
    private float nextFire = 0f;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        EnemyHp enemyHp;

        _audioSource.PlayOneShot(shotSFX);

        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position,_cam.transform.forward,out hit,range))
        {
            if (hit.transform.tag == "Enemy")
            {
                enemyHp = hit.transform.GetComponent<EnemyHp>();
                enemyHp.health--;
                Debug.Log(enemyHp.health);
            }
            Debug.Log("Вы попали в обьект!" + hit.collider);

            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);

            if(hit.rigidbody !=null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }
}
