using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timelord : MonoBehaviour
{
    List<Move> history = new List<Move>();

    public void Record(Move move)
    {
        history.Add(move);
    }

    public Move Rewind()
    {
        Move move = null;
        if(history.Count > 0)
        {
            move = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);
        }

        return move;
    }
}
