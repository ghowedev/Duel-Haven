using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    private AudioSource loopingSource;  // For continuous sounds (footsteps, fireball travel)
    private AudioSource oneShotSource;  // For quick, one-time sounds (melee attacks, ability casts)

    private void Awake()
    {
        loopingSource = gameObject.AddComponent<AudioSource>();
        loopingSource.loop = true;

        oneShotSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayLoopingSound(AudioClip clip, float volume = 1f)
    {
        if (loopingSource.clip == clip && loopingSource.isPlaying) return; // Prevent redundant replay

        loopingSource.clip = clip;
        loopingSource.volume = volume;
        loopingSource.Play();
    }

    public void StopLoopingSound()
    {
        if (loopingSource.isPlaying)
        {
            loopingSource.Stop();
        }
    }

    public void PlayOneShotSound(AudioClip clip, float volume = 1f)
    {
        oneShotSource.PlayOneShot(clip, volume);  // Doesn't interfere with looping sounds
    }
}
