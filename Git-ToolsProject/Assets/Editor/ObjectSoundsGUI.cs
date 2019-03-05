using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectSounds))]
public class ObjectSoundsGUI : Editor
{
    ObjectSounds _sounds;

    private void OnEnable()
    {
        _sounds = (ObjectSounds)target;
    }

    public override void OnInspectorGUI()
    {
        foreach (Sound s in _sounds.objectSounds)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name: ");
            s.name = EditorGUILayout.TextField(s.name);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Clip: ");
            s.clip = (AudioClip)EditorGUILayout.ObjectField(s.clip, typeof(AudioClip), true);
            GUILayout.EndHorizontal();

            GUILayout.Label("", GUI.skin.horizontalSlider);

            GUILayout.Label("Volume: ");
            s.volume = EditorGUILayout.Slider(s.volume, 0f, 1f);

            GUILayout.Label("Volume Scale: ");
            s.volumeScale = EditorGUILayout.Slider(s.volumeScale, 0f, 1f);

            GUILayout.Label("Pitch: ");
            s.pitch = EditorGUILayout.Slider(s.pitch, .1f, 2f);

            GUILayout.Label("Play Type: ");
            s.type = (SoundPlayType)EditorGUILayout.EnumPopup(s.type);

            GUILayout.Label("", GUI.skin.horizontalSlider);

            if (GUILayout.Button("Play!"))
            {
                Debug.Log("Play! No implementation yet!");
            }

            if (GUILayout.Button("Stop!"))
            {
                Debug.Log("Stop! No implementation yet!");
            }
        }

        if (GUILayout.Button("Add"))
        {
            Debug.Log("Add, no implementation yet!");
        }

        if (GUILayout.Button("Remove"))
        {
            Debug.Log("Remove, no implementation yet!");
        }
    }
}
