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

    public ParticleSystem deathParticle;
    void Start()
    {
        homePosition = transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attakRadius)
        {
            Vector3 followPosition = new Vector3(target.position.x, homePosition.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, followPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void KillEnemy()
    {
        deathParticle.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.tag = "Untagged";
        Destroy(gameObject, 1);
    }
}
