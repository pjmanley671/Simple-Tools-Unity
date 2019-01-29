/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathGUI : Editor
{
    Path _path;
    List<bool> toggleGroup;
    private static Color[] _colors = new Color[(int)PathPoint.PointBehavior.End + 1];

    private void OnEnable()
    {
        _path = (Path)target;
        if (_path == null) _path = new Path();

        if (_path.pathPoints == null)
            _path.pathPoints = new List<PathPoint>();

        SceneView.onSceneGUIDelegate += OnSceneGUI;

        toggleGroup = new List<bool>();

        if (_path.pathPoints.Capacity > 0)
        {
            for (int i = 0; i < _path.pathPoints.Capacity; i++)
            {
                toggleGroup.Add(false);
                toggleGroup.TrimExcess();
            }
        }
    }

    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        toggleGroup.Clear();
        toggleGroup.TrimExcess();
    }

    public override void OnInspectorGUI()
    {
        if (_path == null) return;
        PathPoint.PointBehavior _pb = 0;
        for (int i = 0; i < _colors.Length; i++)
        {
            GUILayout.BeginHorizontal();
            _pb = (PathPoint.PointBehavior)i;
            GUILayout.Label(_pb.ToString());
            _colors[i] = EditorGUILayout.ColorField(_colors[i], GUILayout.Width(Screen.width / 2));
            GUILayout.EndHorizontal();
        }

        GUILayout.Label("Number of points: " + _path.pathPoints.Capacity);

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
            {
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
            foreach (PathPoint lPoint in _path.pathPoints)
            {
                Handles.color = _colors[(int)lPoint.beviourAtPoint];
                lPoint.location = Handles.PositionHandle(lPoint.location, Quaternion.identity);
                Handles.SphereHandleCap(1, lPoint.location, Quaternion.identity, .5f, EventType.Repaint);
            }

            Handles.color = Color.white;
            for (int i = 0; i + 1 < _path.pathPoints.Capacity; i++)
            {
                Handles.DrawLine(_path.pathPoints[i].location, _path.pathPoints[i + 1].location);
            }
            Handles.color = Color.gray;
        }
    }
}
