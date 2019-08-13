using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private Ctrl ctrl;

    public AudioClip cusor;
    public AudioClip drop;
    public AudioClip ballon;
    public AudioClip lineClear;

    private AudioSource audioSource;
    [HideInInspector]
    public bool isMute = false;

    void Awake() {
        ctrl = this.GetComponent<Ctrl>();
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayCusor() {
        PlayClip(cusor);
    }

    public void PlayDrop()
    {
        PlayClip(drop);
    }

    public void PlayBallon() {
        PlayClip(ballon);
    }

    public void PlayLineClear() {
        PlayClip(lineClear);
    }

    private void PlayClip(AudioClip audioClip) {
        if (isMute == true) {
            return;
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void SetMute() {
        isMute = !isMute;
        ctrl.view.MuteSetActive(isMute);
        if (isMute == false) {
            PlayCusor();
        }
    }
}
