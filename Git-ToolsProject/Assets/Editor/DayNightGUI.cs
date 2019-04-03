using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Hours))]
public class DayNightGUI : Editor
{
    private Hours _hours;
    private static GameObject _object;
    private static Light _light;
    private static bool simulating;
    private static List<bool> visibleHours;

    private void OnEnable()
    {
        _hours = (Hours)target;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        if (_object == null)
        {
            _object = new GameObject();
        } // End of if(_object is null)

        if (_object.GetComponent<Light>() == null)
        {
            _light = _object.AddComponent<Light>();
            _light.type = LightType.Directional;
            _light.SetLightDirty();
        } // End of if(_object light is null)
    } // End of OnEnable()

    private void OnDisable()
    {
        /* Cleans up all in scene references and behaivors */
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        DestroyImmediate(_light);
        DestroyImmediate(_object);
        /***************************************************/
    } // End of OnDisable()

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Length of time in minutes: ");
        _hours.timeBetweenHours = EditorGUILayout.FloatField(_hours.timeBetweenHours);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!simulating)
        {

        }
        else
        {

        }
    } // End of OnSceneGUI(1 SceneView)
}
