using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class dealing with activity of players
public class PlayerController : MonoBehaviour
{
    float startPosX;
    float startPosY;
    bool isBeingHeld = false;
    [SerializeField]
    float bulletSpeed;
    PlayerState playerState;

    //Dragging object with mousemovement.
    private void Update()
    {
        if (isBeingHeld == true)
        {
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            this.gameObject.transform.localPosition = new Vector3(pos.x - startPosX, pos.y - startPosY, 0);
        }

    }
    //Getting the position of object and enabling the movement of object
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
    // Disabling the movement of object when mousebutton up
    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
    // getting player giving the position to player after collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BoxCollider2D>() && !collision.GetComponent<PlayerController>())
        {
            SpwanPlayer(collision);
        }
        
    }
    //player changes it's state from attacking to rest , when remove from fire area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BoxCollider2D>() && !collision.GetComponent<PlayerController>())
        {
            playerState = PlayerState.OnRest;
        }

    }
    //spwan player at spwan area after drag
    private void SpwanPlayer(Collider2D collision)
    {
        this.transform.position = collision.GetComponent<Transform>().position;
        isBeingHeld = false;
        playerState = PlayerState.OnAttacking;
        SpwanBullets();
    }
    //to spwan bullets one by one after 2 sec
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
    //To move bullet 
    private void MoveBullet(GameObject bullet)
    {
        Rigidbody2D rb;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * bulletSpeed);
    }
}





