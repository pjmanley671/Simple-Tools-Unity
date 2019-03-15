using UnityEngine;
using System.Collections.Generic;

/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */

[CreateAssetMenu(menuName = "Sounds")]
public class ObjectSounds : ScriptableObject
{
    public List<Sound> objectSounds;
    static readonly KeyNotFoundException _soundException = new KeyNotFoundException("<color=blue>Sound not found: ");

    public void InitSounds(GameObject gameObject)
    {
        for (int i = 0; i < objectSounds.Capacity; i++)
        {
            if (objectSounds[i].source == null)
            {
                objectSounds[i].source = gameObject.AddComponent<AudioSource>();
                objectSounds[i].source.clip = objectSounds[i].clip;
                objectSounds[i].source.volume = objectSounds[i].defaultVolume;
                objectSounds[i].source.pitch = objectSounds[i].pitch;
                if (objectSounds[i].type == SoundPlayType.Loop)
                    objectSounds[i].source.loop = true;
                else
                    objectSounds[i].source.loop = false;
            }
        }
    }

    public void InitSounds(GameObject gameObject, AudioSource _aSource)
    {
        for (int i = 0; i < objectSounds.Capacity; i++)
        {
            if (objectSounds[i].source == null)
            {
                objectSounds[i].source = _aSource;
                objectSounds[i].source.clip = objectSounds[i].clip;
                objectSounds[i].source.volume = objectSounds[i].defaultVolume;
                objectSounds[i].source.pitch = objectSounds[i].pitch;
                if (objectSounds[i].type == SoundPlayType.Loop)
                    objectSounds[i].source.loop = true;
                else
                    objectSounds[i].source.loop = false;
            }
        }
    }

    public Sound GetSound(string p_name)
    {
        Sound _s = null;
        try
        {
            for (int i = 0; i < objectSounds.Capacity; i++)
            {
                if (objectSounds[i].name == p_name)
                    _s = objectSounds[i];
            }

            if(_s == null) throw _soundException;
        }
        catch (KeyNotFoundException k)
        {
            Debug.Log(k + p_name + "</color>");
            Debug.LogAssertion(k + p_name + "</color>");
        }

        return _s;
    }

    public void Play(Sound s)
    {
        if (s == null)
            return;

        if (!s.source.isPlaying)
        {
            if (s.type == SoundPlayType.OneShot)
                s.source.PlayOneShot(s.clip);
            else
                s.source.Play();
        }
    }

    public void Play(string name)
    {
        Play(GetSound(name));
    }

    public void Play(string name, float p_volume)
    {
        Sound l_sound = GetSound(name);
        if (l_sound != null)
        {
            l_sound.source.volume = p_volume;
            l_sound.volume = p_volume;
            Play(l_sound);
        }
    }

    public void Play(string name, float p_volume, float pitch)
    {
        Sound l_sound = GetSound(name);
        l_sound.volume = p_volume;
        l_sound.pitch = pitch;
        l_sound.source.volume = p_volume;
        l_sound.source.pitch = pitch;
    }

    public void SetVolume(string p_name, float p_volume)
    {
        Sound s = GetSound(p_name);

        if (s != null)
        {
            s.volume = p_volume;
            s.source.volume = s.volume;
        }
    }

    public float GetVolume(string name)
    {
        return (GetSound(name) != null) ? GetSound(name).volume : 0f;
    }

    public void SetPitch(string p_name, float p_pitch)
    {
        Sound s = GetSound(p_name);
        if (s != null)
        {
            s.pitch = p_pitch;
            s.source.pitch = s.pitch;
        }
    }

    public float GetPitch(string name)
    {
        return (GetSound(name) != null) ? GetSound(name).pitch : 0f;
    }
}
