using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death") && collision.GetComponent<EnemyAI>())
        {
            collision.GetComponent<EnemyAI>().KillEnemy();
        }

        if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") && !collision.otherCollider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
