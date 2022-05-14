using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Class is dealing with activity occur on enemy Object
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float healthAmount;
    [SerializeField]
    float decreaseHealthAmount;
    [SerializeField]
    GameObject healthbar;
    Vector2 healthBarLocalScale;

    private void Start()
    {
        healthBarLocalScale = healthbar.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Rigidbody2D>())
        {
            TakeDamage(collision);
        }
    }

    //Enemy Affecting by bullet and reducing the healthbar
    private void TakeDamage(Collider2D collision)
    {
        healthAmount -= decreaseHealthAmount;
        healthBarLocalScale.x = healthAmount / 1000;
        healthbar.transform.localScale = healthBarLocalScale;
        collision.gameObject.SetActive(false);
    }
}
