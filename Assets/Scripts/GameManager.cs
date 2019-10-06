using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image candleItem;
    public Image bootsItem;
    public Image arrowItem;
    public Image swimItem;
    public Image dashItem;

    public Sprite candleSpriteActive;
    public Sprite bootsSpriteActive;
    public Sprite arrowSpriteActive;
    public Sprite swimSpriteActive;
    public Sprite dashSpriteActive;

    private AudioSource audioSource;
    public AudioClip upgradeSound;

    public RectTransform messagePanel;
    public Text messageText;

    public Vector2 leftCameraChange;
    public Vector2 rightCameraChange;
    public Vector2 downwellChange;
    public Vector2 upwellChange;

    public Vector2 leftPlayerChange;
    public Vector2 rightPlayerChange;
    public Vector2 downPlayerChange;
    public Vector2 upPlayerChange;

    void Start()
    {
        GetComponent<Light2D>().enabled = false;
        GameObject[] lights = GameObject.FindGameObjectsWithTag("RoomLights");
        foreach (GameObject light in lights)
        {
            light.GetComponent<Light2D>().enabled = false;
        }
        HideMessageBox();
        audioSource = GetComponent<AudioSource>();

    }

    public void HideMessageBox()
    {
        messagePanel.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
    }

    public void ShowMessageBox(String text, float intervalToDismiss)
    {
        messagePanel.gameObject.SetActive(true);
        messageText.text = text;
        messageText.gameObject.SetActive(true);
        Invoke("HideMessageBox", intervalToDismiss);
    }

    public void GameOver()
    {
        // ToDo:
        ShowMessageBox("YOU DIED!", 3f);
        Invoke("RestartGame", 3);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    internal void ActivateCollectable(Collectable.COLLECT_TYPE collectableType)
    {
        audioSource.PlayOneShot(upgradeSound);
        switch (collectableType)
        {
            case Collectable.COLLECT_TYPE.CANDLE:
                ShowMessageBox("YOU GOT THE <color=red>CANDLE</color>.\r\nALL LIGHTS ARE ON.", 4f);
                candleItem.sprite = candleSpriteActive;
                break;

            case Collectable.COLLECT_TYPE.JUMPBOOTS:
                ShowMessageBox("YOU GOT THE <color=brown>Jump Boots</color>.\r\nYOU CAN NOW JUMP.", 4f);
                bootsItem.sprite = bootsSpriteActive;
                break;

            case Collectable.COLLECT_TYPE.ARROW:
                ShowMessageBox("YOU GOT THE <color=yellow>BOW</color>.\r\nYOU CAN NOW SHOOT ARROWS.", 4f);
                arrowItem.sprite = arrowSpriteActive;
                break;

            case Collectable.COLLECT_TYPE.SWIM:
                ShowMessageBox("YOU GOT THE <color=blue>FLIPPERS</color>.\r\nYOU CAN NOW SWIM IN WATER.", 4f);
                swimItem.sprite = swimSpriteActive;
                break;

            case Collectable.COLLECT_TYPE.DASH:
                ShowMessageBox("YOU GOT THE <color=pink>DASH</color>.\r\nYOU CAN NOW DASH TO DESTROY BLOCKS.", 4f);
                dashItem.sprite = dashSpriteActive;
                break;
        }
        
    }
}
