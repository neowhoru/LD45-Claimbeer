using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Collectable
{
    public COLLECT_TYPE collectableType;
    // Start is called before the first frame update
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.ActivateCollectable(collectableType);
            collision.GetComponent<PlayerMovement>().canShoot = true;
            Destroy(gameObject);

        }
    }
}
