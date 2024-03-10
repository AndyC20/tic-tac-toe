using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TTT : MonoBehaviour
{
    [SerializeField] 
    Button[] buttons = new Button[9];

    public int[] board = new int[9];

    public bool PlayerTurn = false;

    public bool gameStatus = true;

    public TextMeshProUGUI resultText;

    private AIT TTTAI;

    void Start()
    {
        TTTAI = GetComponent<AIT>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<ButtonClick>().gameManager = this;
            buttons[i].GetComponent<ButtonClick>().buttonIndex = i;
        }
    }

    void Update()
    {
        if (!PlayerTurn && gameStatus)
        {
            AIMove();
        }
        if(loseCheck())
        {
            resultText.text = "ai win";
            gameStatus = false;
        }
    }
     

    public void PlayerMove(int num, int player)
    {
        Debug.Log("PlayerTurn before move: " + PlayerTurn);
        if(player == 1 && board[num] == 0 && PlayerTurn)
        {
            board[num] = player;
            buttons[num].GetComponentInChildren<TextMeshProUGUI>().text = "X";
            endGame();
            Debug.Log("PlayerTurn after move: " + PlayerTurn);
        }
    }

    void endGame()
    {
        if(tie())
        {
            resultText.text = "Tie";
            gameStatus = false;
        }

        if(winCheck())
        {
            resultText.text = "Player win";
            gameStatus = false;
        }
    }

    public bool tie()
    {
        foreach (int num in board)
        {
            if (num == 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool winCheck()
    {
        for(int i = 0; i < board.Length; i+=3)
        {
            if(board[i] == 1 && board[i+1] == 1 && board[i+2] == 1)
            {
                return true;
            }
        }
        for(int i = 0; i < 3; i++)
        {
            if(board[i] == 1 && board[i+3] == 1 && board[i+6] == 1)
            {
                return true;
            }
        }
        if(board[0] == 1 && board[4] == 1 && board[8] == 1)
            return true;

        if(board[2] == 1 && board[4] == 1 && board[6] == 1)
            return true;
            
        return false;
    }

    public bool loseCheck()
    {
        for(int i = 0; i < board.Length; i+= 3){
            if(board[i] == -1 && board[i+1] == -1 && board[i+2] == -1)
            {
                return true;
            }
        }
        for(int i = 0; i < 3; i++)
        {
            if(board[i] == -1 && board[i+3] == -1 && board[i+6] == -1)
            {
                return true;
            }
        }
        if(board[0] == -1 && board[4] == -1 && board[8] == -1)
            return true;

        if(board[2] == -1 && board[4] == -1 && board[6] == -1)
            return true;
            
        return false;
    }

    public void restart()
    {
        for(int i = 0; i < board.Length; i++)
        {
            board[i] = 0;
        }
        foreach (Button button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        resultText.text = "";
        gameStatus = true;
    }

    
    void AIMove()
    {
        if (gameStatus && !PlayerTurn)
        {
            int best = TTTAI.bestMove(board);
            if (best != -1)
            {
                board[best] = -1; 
                buttons[best].GetComponentInChildren<TextMeshProUGUI>().text = "O";
                PlayerTurn = true;
                endGame();
            }
        }
    }

    
}
