using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : MonoBehaviour
{
    public float timeBeforeShoot;
    public GameObject bullet;
    public float bulletSpeed;
    public AudioSource audioSource;

    private float currTimeBeforeShoot;
    private Transform shootOrigin;
    private bool targetAcquired;

    private Sniper s;
    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Sniper>();
        targetAcquired = s.playerFound;
        currTimeBeforeShoot = -10;
        shootOrigin = s.rayPoint;
    }

    // Update is called once per frame
    void Update()
    {
        targetAcquired = s.playerFound;

        if(targetAcquired && currTimeBeforeShoot == -10)
        {
            currTimeBeforeShoot = 0;
        }

        if(currTimeBeforeShoot >= timeBeforeShoot && currTimeBeforeShoot != -10)
        {
            if(targetAcquired)
            {
                ShootBullet();
            }
            currTimeBeforeShoot = -10;
        }
        else if(currTimeBeforeShoot >= 0 && currTimeBeforeShoot != -10)
        {
            currTimeBeforeShoot += Time.deltaTime;
        }
    }

    public void ShootBullet()
    {
        GameObject b = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation);
        audioSource.Play();
        b.GetComponent<Rigidbody>().velocity = b.transform.forward * bulletSpeed;
    }
}
