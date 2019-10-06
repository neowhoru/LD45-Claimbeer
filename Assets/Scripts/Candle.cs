using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Candle : Collectable
{
    public COLLECT_TYPE collectableType; 
    public Door[] doorsToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(Door door in doorsToActivate)
            {
                door.UnlockDoor();
            }
            EnlightTheWorld();
            gameManager.ActivateCollectable(collectableType);
            Destroy(gameObject);

        }
    }

    private void EnlightTheWorld()
    {
        Debug.Log("Enlight World");
        GameObject[] lights = GameObject.FindGameObjectsWithTag("RoomLights");
        foreach(GameObject light in lights)
        {
            light.GetComponent<Light2D>().enabled = true;
        }
    }
}
