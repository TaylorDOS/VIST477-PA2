using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogisticsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myLifeTotalText;

    [SerializeField] GameObject iLostText;

    [SerializeField] GameObject otherLostText;

    [SerializeField] private int currentLife = 5;

    private string hitText = "Life Total = ";

    public bool collisionfound = false;

    public bool iLost = false;

    private bool endGame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!endGame)
        {
            if (collisionfound)
            {
                currentLife --;
                myLifeTotalText.text = hitText + currentLife.ToString();
                collisionfound = false;

                if (currentLife < 1)
                {
                    iLostText.SetActive(true);
                    iLost = true;
                }
            }
        }
    }

    public void resetGameplay()
    {
        iLost = false;
    }

    public void otherLost()
    {
        otherLostText.SetActive(true);
    }
}
