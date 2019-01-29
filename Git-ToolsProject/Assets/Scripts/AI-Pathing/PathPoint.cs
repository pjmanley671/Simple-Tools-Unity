using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */

[System.Serializable]
public class PathPoint
{
    #region ManipulatableObjectData
    public enum PointBehavior : byte
    {
        Start = 0,
        Idle = 1,
        PassThrough = 2,
        Interact = 3,
        End = 4
    }
    public PointBehavior beviourAtPoint;
    public Vector3 location;
    #endregion

    #region Constructors
    public PathPoint()
    {
        beviourAtPoint = PointBehavior.Start;
        location = Vector3.zero;
    }
    public PathPoint(Vector3 _location)
    {
        beviourAtPoint = PointBehavior.Start;
        location = _location;
    }
    #endregion
}
