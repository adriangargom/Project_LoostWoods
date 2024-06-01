using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingsPannelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pannels;
    [SerializeField] private int actualPannelId = 0;


    public void NextPannel()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        pannels[actualPannelId].SetActive(false);

        actualPannelId = actualPannelId+1 < pannels.Length ? actualPannelId+1 : 0;

        pannels[actualPannelId].SetActive(true);
    }
}
