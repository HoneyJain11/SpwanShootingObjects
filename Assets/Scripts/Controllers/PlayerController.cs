using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float startPosX;
    float startPosY;
    bool isBeingHeld = false;
    [SerializeField]
     float bulletSpeed;
    PlayerState playerState;


    private void Update()
    {
        if (isBeingHeld == true)
        {
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            this.gameObject.transform.localPosition = new Vector3(pos.x - startPosX, pos.y - startPosY, 0);
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            isBeingHeld = true;

        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BoxCollider2D>() && !collision.GetComponent<PlayerController>())
        {
            Debug.Log("player collided with area " + collision.gameObject.name);
            SpwanPlayer(collision);
        }
        
    }

    private void SpwanPlayer(Collider2D collision)
    {
        this.transform.position = collision.GetComponent<Transform>().position;
        isBeingHeld = false;
        playerState = PlayerState.OnAttacking;
        SpwanBullets();
    }

    private async void SpwanBullets()
    {
        if(playerState == PlayerState.OnAttacking)
        {
            GameObject bullet = ObjectPooling.Instance.GetPooledObject();
            bullet.SetActive(true);
            bullet.transform.position = this.transform.position;
            MoveBullet(bullet);
            await new WaitForSeconds(2.0f);
            SpwanBullets();
        }
       
    }

    private void MoveBullet(GameObject bullet)
    {
        Rigidbody2D rb;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * bulletSpeed);
    }
}





