using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
      
    }
    public bool isMute=false;
    

    public AudioSource bgmAudioSource;

    void Awake()
    {
        _instance = this;

        DoMute();
    }

    public void SwitchMuteState(bool isOn)
    {
        isMute = !isOn;
        DoMute();
    }

    void DoMute()
    {
        if (isMute)
        {
            bgmAudioSource.Pause();
        }
        else
        {
            bgmAudioSource.Play();
        }
    }

   
}
