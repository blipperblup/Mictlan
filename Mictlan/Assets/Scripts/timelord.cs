using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timelord : MonoBehaviour
{

    List<Move> history = new List<Move>();
    public Move LastMove()
    {
        if (history.Count > 0)
        {
            Move move = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);
            return move;
        }
        return null;
    }
    public void Record(Move move)
    {
        history.Add(move);
    }

    public void Rewind()
    {
        if(history.Count > 0)
        {
            Move move = history[history.Count - 1];
            move.Undo();           
            history.RemoveAt(history.Count - 1);

        }
                
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Rewind();
        }
    }
}
