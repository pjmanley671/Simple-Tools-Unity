using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */

[CreateAssetMenu(menuName = "AI - Path")]
public class Path : ScriptableObject
{
    public List<PathPoint> pathPoints;
    private int positionInArray = 0;

    #region Setters & Getters
    public void SetNextPathLocation()
    { positionInArray = (positionInArray <= pathPoints.Capacity) ? positionInArray + 1 : 0; }
    public Vector3 GetPathToLocation()
    { return pathPoints[positionInArray].location; }
    #endregion
}
