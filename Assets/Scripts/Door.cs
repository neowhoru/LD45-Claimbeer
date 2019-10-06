using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool isDoorOpened = false;
    public Sprite doorOpen;
    public Sprite doorClosed;
    private BoxCollider2D collider;
    private Animator anim;
    private AudioSource audioSource;
    public enum DoorDirection { LEFT, RIGHT, DOWN, UP }
    public DoorDirection direction;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (doorClosed != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
        }
        collider = GetComponent<BoxCollider2D>();
        cam = Camera.main.GetComponent<CameraMovement>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();

        if (isDoorOpened)
        {
            UnlockDoor();
        }
        else
        {
            LockDoor();
        }
        
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
        isDoorOpened = true;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeCamera(collision);
            //collision.transform.position += playerChange;
        }
    }

    public void ChangeCamera(Collider2D collision)
    {
        switch (direction)
        {
            case DoorDirection.LEFT:
                Vector2 directionVectorLeft = gameManager.leftCameraChange;
                Vector3 directionPlayerVectorLeft = gameManager.leftPlayerChange;
                cam.minPosition += directionVectorLeft;
                cam.maxPosition += directionVectorLeft;
                collision.transform.position += directionPlayerVectorLeft;
                break;

            case DoorDirection.RIGHT:
                Vector2 directionVectorRight = gameManager.rightCameraChange;
                Vector3 directionPlayerVectorRight = gameManager.rightPlayerChange;
                cam.minPosition += directionVectorRight;
                cam.maxPosition += directionVectorRight;
                collision.transform.position += directionPlayerVectorRight;
                break;

            case DoorDirection.DOWN:
                Vector2 directionVectorDown = gameManager.downwellChange;
                Vector3 directionPlayerVectorDown = gameManager.downPlayerChange;
                cam.minPosition += directionVectorDown;
                cam.maxPosition += directionVectorDown;
                collision.transform.position += directionPlayerVectorDown;
                break;

            case DoorDirection.UP:
                Vector2 directionVectorUp = gameManager.upwellChange;
                Vector3 directionPlayerVectorUp = gameManager.upPlayerChange;
                cam.minPosition += directionVectorUp;
                cam.maxPosition += directionVectorUp;
                collision.transform.position += directionPlayerVectorUp;
                break;
        }
    }

    public void UnlockDoor()
    {
        collider.isTrigger = true;
        isDoorOpened = true;
        anim.SetBool("IsOpen", true);
    }

    public void LockDoor()
    {
        collider.isTrigger = false;
        isDoorOpened = false;
        anim.SetBool("IsOpen", false);
    }


}
