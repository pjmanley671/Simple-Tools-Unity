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
        EditorUtility.SetDirty(_hours);

        
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

        if(_hours.timeOfDays.Capacity <= 0)
        {
            _hours.timeOfDays.Add(new TimeOfDay());
            _hours.timeOfDays.TrimExcess();
        }

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Light Color: ");
        _hours.timeOfDays[0]._colorAtTime = EditorGUILayout.ColorField(_hours.timeOfDays[0]._colorAtTime);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Light Intensity: ");
        _hours.timeOfDays[0]._colorIntensity = EditorGUILayout.FloatField(_hours.timeOfDays[0]._colorIntensity);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        _hours.timeOfDays[0]._timeAngle = EditorGUILayout.Vector3Field("angleOfSun", _hours.timeOfDays[0]._timeAngle);
        GUILayout.EndHorizontal();


        GUILayout.EndVertical();




    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (_light != null)
        {
            _light.color = _hours.timeOfDays[0]._colorAtTime;
            _object.transform.rotation = Quaternion.Euler(_hours.timeOfDays[0]._timeAngle);
        }
        if (!simulating)
        {

        }
        else
        {

        }
    } // End of OnSceneGUI(1 SceneView)
}
