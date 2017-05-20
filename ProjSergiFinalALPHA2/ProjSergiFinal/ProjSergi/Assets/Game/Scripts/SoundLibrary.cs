using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour {
    public SoundGroup[] soundGroups;

    Dictionary<string, SoundGroup> groupDictionary = new Dictionary<string, SoundGroup>();

    void Awake()
    {
        foreach (SoundGroup soundGroup in soundGroups)
        {
            groupDictionary.Add(soundGroup.groupID, soundGroup);
        }
    }
    public AudioClip GetClipFromName(string name)
    {
        if (groupDictionary.ContainsKey(name))
        {

            //if (groupDictionary[name].HasMany)
            //{
            //    AudioClip[] sounds = groupDictionary[name].group;
            //    return sounds[Random.Range(0, sounds.Length)];
            //}
            //else
            //{
               return groupDictionary[name].Clip;
            //}
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string groupID;
        public AudioClip Clip;
        //public bool HasMany;
        //public AudioClip[] group;
    }
}
