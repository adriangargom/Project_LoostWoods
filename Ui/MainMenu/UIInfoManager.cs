using UnityEngine;

public class UIInfoManager : MonoBehaviour
{
    private readonly string _linkedinUrl = "https://www.linkedin.com/in/adriangargom/";
    private readonly string _githubUrl = "https://github.com/adriangargom";
    
    public void OpenLinkedinUrl()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        Application.OpenURL(_linkedinUrl);
    }

    public void OpenGithubUrl()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        Application.OpenURL(_githubUrl);
    }
}
