using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIT : MonoBehaviour
{
    private int ai = -1;
    private int opponent = 1;

    public int bestMove(int[] board)
    {
        int bestScore = int.MinValue;
        int best = -1;

        for(int i = 0; i < board.Length; i++)
        {
            if(board[i] == 0)
            {
                board[i] = ai;
                int moveVal = minimax(board, 0, false, int.MinValue, int.MaxValue);
                board[i] = 0;

                if(moveVal > bestScore)
                {
                    bestScore = moveVal;
                    best = i;
                }
            }
        }
        return best;
    }

     private int minimax(int[] board, int depth, bool isMaximizing, int alpha, int beta)
     {
        if (checkWin(board, ai))
            return 10 - depth;
        if (checkWin(board, opponent))
            return depth - 10;
        if (tie(board))
            return 0;

        if (isMaximizing)
        {
            int best = int.MinValue;
            for(int i = 0; i < board.Length; i++)
            {
                if(board[i] == 0)
                {
                    board[i] = ai;
                    best = Mathf.Max(best, minimax(board, depth + 1, !isMaximizing, alpha, beta));
                    board[i] = 0;
                    alpha = Mathf.Max(alpha, best);
                    if (beta <= alpha)
                        break;
                }
            }
            return best; 
        }
        else
        {
            int best = int.MaxValue;
            for (int i = 0; i < board.Length; i++)
            {
                 if (board[i] == 0)
                {
                    board[i] = opponent;
                    best = Mathf.Min(best, minimax(board, depth + 1, !isMaximizing, alpha, beta));
                    board[i] = 0;
                    beta = Mathf.Min(beta, best);
                    if (beta <= alpha)
                        break;
                }
            }
            return best;
        }
     }

    private bool checkWin(int[] board, int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i * 3] == player && board[i * 3 + 1] == player && board[i * 3 + 2] == player)
                return true;
            if (board[i] == player && board[i + 3] == player && board[i + 6] == player)
                return true;
        }
        if (board[0] == player && board[4] == player && board[8] == player)
            return true;
        if (board[2] == player && board[4] == player && board[6] == player)
            return true;

        return false;
    }

    private bool tie(int[] board)
    {
        foreach (int num in board)
        {
            if (num == 0)
                return false;
        }
        return true;
    }
}
