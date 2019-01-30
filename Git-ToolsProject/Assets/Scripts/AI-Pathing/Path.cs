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
    public List<PathPoint> pathPoints; // The PathPoints that this scriptableObject contains.
    private int positionInArray = 0; // The position in the List that is currently active for the runtime representation of the current target point.

    #region Setters & Getters
    public void SetNextPathLocation()
    { // Sets the next path location. Goes to Start by default if it reaches past the number of set points.
        positionInArray = (positionInArray <= pathPoints.Capacity) ? positionInArray + 1 : 0;
    }
    public Vector3 GetPathToLocation()
    { // Returns the PathPoint location.
        return pathPoints[positionInArray].location;
    }
    #endregion
}
