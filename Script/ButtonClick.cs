using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClick : MonoBehaviour
{
    public int buttonIndex;
    public TTT gameManager;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (gameManager.gameStatus)
        {
            if (gameManager.board[buttonIndex] == 0)
            {
                gameManager.PlayerMove(buttonIndex, gameManager.PlayerTurn ? 1 : -1);
                gameManager.PlayerTurn = !gameManager.PlayerTurn; 
            }
        }
    }
}
