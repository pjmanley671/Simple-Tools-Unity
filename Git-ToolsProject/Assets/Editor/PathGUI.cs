/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))] // Tells the Editor to only run this script if the selected object is a type of it.
public class PathGUI : Editor
{
    Path _path;
    List<bool> toggleGroup;
    private static Color[] _colors = new Color[(int)PathPoint.PointBehavior.End + 1]; // The color handle representation of the points. Made stat so all points are consistent and easy to identify across all instances.

    private void OnEnable()
    { // Makes sure no necessary data types are empty and if they are then make a new one of it.
        _path = (Path)target;

        if (_path.pathPoints == null)
        {
            //_path.pathPoints = new List<PathPoint>();
            Debug.Log(_path.pathPoints);
        }

        SceneView.onSceneGUIDelegate += OnSceneGUI; // Adds the OnSceneGUI to the delegate to allow rendering in editor.

        toggleGroup = new List<bool>(); // creates the new toggleGroup

        if (_path.pathPoints.Capacity > 0)
        {
            for (int i = 0; i < _path.pathPoints.Capacity; i++)
            { // intializes the new toggleGroup
                toggleGroup.Add(false); 
                toggleGroup.TrimExcess(); // clears any junk data automatically added on <List>.Add()
            }
        }
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI; // Clears the OnSceneGUI to the delegate to disabling the render in editor.
        toggleGroup.Clear(); // clears the toggleGroup of all data
        toggleGroup.TrimExcess(); // ensures no junk data is maintained in the list.
    }

    public override void OnInspectorGUI()
    {
        PathPoint.PointBehavior _pb = 0;
        for (int i = 0; i < _colors.Length; i++)
        {// Layouts the color fields that are associated with the behaviourPoints.
            GUILayout.BeginHorizontal();
            _pb = (PathPoint.PointBehavior)i;
            GUILayout.Label(_pb.ToString());
            _colors[i] = EditorGUILayout.ColorField(_colors[i], GUILayout.Width(Screen.width / 2));
            GUILayout.EndHorizontal();
        }

        GUILayout.Label("Number of points: " + _path.pathPoints.Capacity); // spot to see the total number of points in the list.

        #region Buttons!
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Point"))
        {
            _path.pathPoints.Add(new PathPoint());
            toggleGroup.Add(false);
        }

        if (GUILayout.Button("Remove Point"))
        {
            toggleGroup.Remove(toggleGroup[toggleGroup.Capacity - 1]);
            _path.pathPoints.Remove(_path.pathPoints[_path.pathPoints.Capacity - 1]);
        }

        if (GUILayout.Button("Clear List"))
        {
            _path.pathPoints.Clear();
            toggleGroup.Clear();
        }

        GUILayout.EndHorizontal();

        _path.pathPoints.TrimExcess();
        toggleGroup.TrimExcess();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        #endregion

        if (_path.pathPoints.Capacity > 0)
        {
            for (int i = 0; i < _path.pathPoints.Capacity; i++)
            { // Displayes the point information via the toggle group.
                toggleGroup[i] = EditorGUILayout.Foldout(toggleGroup[i], "Point: " + i + "  -  " + _path.pathPoints[i].beviourAtPoint.ToString());
                if (toggleGroup[i])
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Point Behavior: ");
                    _path.pathPoints[i].beviourAtPoint = (PathPoint.PointBehavior)EditorGUILayout.EnumPopup(_path.pathPoints[i].beviourAtPoint);
                    GUILayout.EndHorizontal();
                    _path.pathPoints[i].location = EditorGUILayout.Vector3Field("", _path.pathPoints[i].location);
                }
            }
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (_path.pathPoints.Capacity > 0)
        {
            for (int i = 0; i < _path.pathPoints.Capacity; i++)
            { // Makes each point show in the scene view and you can edit them using the position handles.
                Handles.color = _colors[(int)_path.pathPoints[i].beviourAtPoint];

                if(toggleGroup[i]) // makes it so that only if the point detail is visible in the inspector then it is adjustable.
                    _path.pathPoints[i].location = Handles.PositionHandle(_path.pathPoints[i].location, Quaternion.identity);

                Handles.SphereHandleCap(1, _path.pathPoints[i].location, Quaternion.identity, .5f, EventType.Repaint); // shows where the points are.
            }
            Handles.color = Color.white; // sets the color to something default.
            for (int i = 0; i + 1 < _path.pathPoints.Capacity; i++)
            { // Shoes the connection (linearly) between points.
                Handles.DrawLine(_path.pathPoints[i].location, _path.pathPoints[i + 1].location);
            }
            Handles.color = Color.gray;
        }
    }
}
