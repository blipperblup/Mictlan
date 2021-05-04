using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Move
{
    IEnumerator Undo(PlayerMover mover);
}
