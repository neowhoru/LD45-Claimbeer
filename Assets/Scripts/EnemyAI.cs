using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float chaseRadius;
    public float attakRadius;
    public Transform homePosition;
    public float moveSpeed;
    private bool isCollide = false;

    public ParticleSystem deathParticle;
    void Start()
    {
        homePosition = transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame

    public void KillEnemy()
    {
        deathParticle.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.tag = "Untagged";
        Destroy(gameObject, 1);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isCollide = true;
        }
    }
}
