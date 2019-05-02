using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectSounds))]
public class ObjectSoundsGUI : Editor
{
    ObjectSounds _sounds;
    static GameObject _object;
    static AudioSource _source;

    private void OnEnable()
    {
        _sounds = (ObjectSounds)target;

        if (_object == null)
        {
            _object = new GameObject();
            _source = _object.AddComponent<AudioSource>();
        }
    }

    private void OnDisable()
    {
        _source.Stop();
        DestroyImmediate(_source);
        DestroyImmediate(_object);
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
            _source.volume = s.volume * s.volumeScale;

            GUILayout.Label("Pitch: ");
            s.pitch = EditorGUILayout.Slider(s.pitch, .1f, 2f);
            _source.pitch = s.pitch;

            GUILayout.Label("Play Type: ");
            s.type = (SoundPlayType)EditorGUILayout.EnumPopup(s.type);

            GUILayout.Label("", GUI.skin.horizontalSlider);

            if (GUILayout.Button("Play!"))
            {
                if (_sounds.objectSounds[0].type == SoundPlayType.Loop)
                {
                    _source.clip = _sounds.objectSounds[0].clip;
                    _source.volume = _sounds.objectSounds[0].volume * _sounds.objectSounds[0].volumeScale;
                    _source.pitch = _sounds.objectSounds[0].pitch;
                    _source.Play();
                }
            }

            if (GUILayout.Button("Stop!"))
            {
                _source.Stop();
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
