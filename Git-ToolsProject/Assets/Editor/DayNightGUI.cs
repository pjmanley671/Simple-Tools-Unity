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
    private static float timeLeftBetweenStep = 0f;
    private static int simulatedIndex;

    private void OnEnable()
    {
        _hours = (Hours)target;
        EditorUtility.SetDirty(_hours);

        if (_hours.timeOfDays == null)
        {
            _hours.timeOfDays = new List<TimeOfDay>
            {
                new TimeOfDay()
            };

            _hours.timeOfDays.TrimExcess();
        }
        simulating = false;

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

        if(visibleHours == null)
        {
            visibleHours = new List<bool>();
            for(int i = 0; i < _hours.timeOfDays.Capacity; i++)
            {
                visibleHours.Add(false);
            }
        }

    } // End of OnEnable()

    private void OnDisable()
    {
        /* Cleans up all in scene references and behaivors */
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        DestroyImmediate(_light);
        DestroyImmediate(_object);
        if (visibleHours != null)
        {
            visibleHours.Clear();
            visibleHours.TrimExcess();
        }
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
            visibleHours.Add(false);
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Time"))
        {
            _hours.timeOfDays.Add(new TimeOfDay());
            visibleHours.Add(false);
            _hours.timeOfDays.TrimExcess();
            visibleHours.TrimExcess();
        }

        if (GUILayout.Button("Remove Time"))
        {
            visibleHours.Remove(visibleHours[visibleHours.Capacity-1]);
            _hours.timeOfDays.Remove(_hours.timeOfDays[_hours.timeOfDays.Capacity - 1]);
            _hours.timeOfDays.TrimExcess();
            visibleHours.TrimExcess();
        }

        if (GUILayout.Button("Clear List"))
        {
            _hours.timeOfDays.Clear();
            _hours.timeOfDays.TrimExcess();
            visibleHours.Clear();
            visibleHours.TrimExcess();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Simulate!"))
        {
            timeLeftBetweenStep = _hours.timeBetweenHours;
            simulatedIndex = 0;
            simulating = !simulating;
        }

        for (int i = 0; i < _hours.timeOfDays.Capacity; i++)
        {
            visibleHours[i] = EditorGUILayout.Foldout(visibleHours[i], "hour: " + i.ToString(), true);

            if (visibleHours[i])
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Light Color: ");
                _hours.timeOfDays[i]._colorAtTime = EditorGUILayout.ColorField(_hours.timeOfDays[i]._colorAtTime);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Light Intensity: ");
                _hours.timeOfDays[i]._colorIntensity = EditorGUILayout.FloatField(_hours.timeOfDays[i]._colorIntensity);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                _hours.timeOfDays[i]._timeAngle = EditorGUILayout.Vector3Field("angleOfSun", _hours.timeOfDays[i]._timeAngle);
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!simulating)
        {
            if (_light != null)
            {
                int pausedIndex = 0;
                for(int i = 0; i < _hours.timeOfDays.Capacity; i++)
                {
                    if (visibleHours[i]) pausedIndex = i;
                }

                _light.color = _hours.timeOfDays[pausedIndex]._colorAtTime;
                _object.transform.rotation = Quaternion.Euler(_hours.timeOfDays[pausedIndex]._timeAngle);
            }
        }
        else
        {
            timeLeftBetweenStep -= .1f;
            if (timeLeftBetweenStep <= 0f)
            {
                simulatedIndex++;
                timeLeftBetweenStep = _hours.timeBetweenHours;
                if (simulatedIndex >= _hours.timeOfDays.Capacity)
                    simulatedIndex = 0;
            }

            if (_light != null)
            { 
                _light.color = _hours.timeOfDays[simulatedIndex]._colorAtTime;
                _object.transform.rotation = Quaternion.Euler(_hours.timeOfDays[simulatedIndex]._timeAngle);
            }
        }

        sceneView.Repaint();
    } // End of OnSceneGUI(1 SceneView)
}
