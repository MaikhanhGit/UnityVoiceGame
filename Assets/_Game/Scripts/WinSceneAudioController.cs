using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _music = null;
    [SerializeField] private AudioClip _sfxEnter = null;
    [SerializeField] private AudioClip _sfxBack = null;
    [SerializeField] private float _sfxVolume = 1f;

    private void Start()
    {
        AudioHelper.PlayClip2D(_music, _sfxVolume);
    }

    public void PlayEnterSFX()
    {
        AudioHelper.PlayClip2D(_sfxEnter, _sfxVolume);
    }

    public void PlayBackSFX()
    {
        AudioHelper.PlayClip2D(_sfxBack, _sfxVolume);
    }


}
