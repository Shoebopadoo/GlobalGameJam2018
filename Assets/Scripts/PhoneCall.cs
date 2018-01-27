using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCall {
    
    public AudioClip DialogClip;
    public float Length; // Seconds
   
    public static PhoneCall RandomCall()
    {
        PhoneCall call = new PhoneCall();
        call.DialogClip = ClipManager.RandomClip();
        call.Length = call.DialogClip.length;
        
        return call;
    }
}

public static class ClipManager
{
    public static Dictionary<string,AudioClip> AllClips;

    private static string _clipPath = "Audio/Calls";

    /// <summary>
    /// Load all the audioclips from their source
    /// </summary>
    public static void LoadClips()
    {
        AllClips = new Dictionary<string, AudioClip>();
        AudioClip[] tClips = Resources.LoadAll<AudioClip>(_clipPath);
        
        foreach(AudioClip clip in tClips)
        {
            Debug.Log(clip.name + " Loaded successfully");
            AllClips.Add(clip.name, clip);
        }
    }

    /// <summary>
    /// Get a random audioclip
    /// </summary>
    /// <returns>Random clip</returns>
    public static AudioClip RandomClip()
    {
        List<AudioClip> tClips = new List<AudioClip>(AllClips.Values);
        int count = tClips.Count;

        int idx = Random.Range(0, count);
        return tClips[idx];
    }
}


