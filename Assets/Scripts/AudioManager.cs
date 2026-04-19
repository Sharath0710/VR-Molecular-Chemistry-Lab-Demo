using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource source;
    public AudioClip bondSuccessClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBondSound()
    {
        source.PlayOneShot(bondSuccessClip);
    }
}