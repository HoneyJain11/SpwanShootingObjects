using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private void Start()
    {
        SpwanBullets();
    }

    private async void SpwanBullets()
    {
        GameObject bullet = ObjectPooling.Instance.GetPooledObject();
        bullet.SetActive(true);
        bullet.transform.position = new Vector3(1.22f, -3.0f, 0);
        MoveBullet(bullet);
        await new WaitForSeconds(2.0f);
        SpwanBullets();
    }

    private void MoveBullet(GameObject bullet)
    {
        Rigidbody2D rb;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * bulletSpeed);
    }
}
