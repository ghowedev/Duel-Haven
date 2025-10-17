using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/AudioData")]
public class AudioData : ScriptableObject
{
    public AudioClip soundClip;
    public float volume = 1f;
    public bool spatialized;
}
