using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //void Rewind()
    //{
    //    if (pointsInTime.Count > 0)
    //    {
    //        PointInTime pointInTime = pointsInTime[0];
    //        transform.position = pointInTime.position;
    //        transform.rotation = pointInTime.rotation;
    //        pointsInTime.RemoveAt(0);
    //    }
    //    else
    //    {
    //        StopRewind();
    //    }

    //}

    //void Record()
    //{
    //    if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
    //    {
    //        pointsInTime.RemoveAt(pointsInTime.Count - 1);
    //    }

    //    pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, gameObject, true));
    //}

    //public void StartRewind()
    //{
    //    isRewinding = true;
    //    rb.isKinematic = true;
    //}

    //public void StopRewind()
    //{
    //    isRewinding = false;
    //    rb.isKinematic = false;
    //}
}