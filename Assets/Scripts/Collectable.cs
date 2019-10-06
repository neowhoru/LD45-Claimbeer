using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum COLLECT_TYPE { CANDLE, JUMPBOOTS, ARROW, SWIM, DASH  }
    // Start is called before the first frame update
    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
