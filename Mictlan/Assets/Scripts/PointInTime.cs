using UnityEngine;

public class PointInTime
{

    public Vector3 position;
    public Quaternion rotation;
    public GameObject piece;
    public bool enabled;


    public PointInTime(Vector3 _position, Quaternion _rotation, GameObject _piece, bool _enabled)
    {
        position = _position;
        rotation = _rotation;
        piece = _piece;
        enabled = _enabled;
    }

}