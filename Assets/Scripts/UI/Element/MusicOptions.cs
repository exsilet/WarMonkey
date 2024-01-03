using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Element
{
    public class MusicOptions : MonoBehaviour
    {
        [SerializeField] private Transform _panelOptions;
        [SerializeField] private AudioSource _audioMusic;
        [SerializeField] private AudioSource _audioSound;
        [SerializeField] private Image _imageMusic;
        [SerializeField] private Image _imageSound;
        [SerializeField] private Sprite _musicOn;
        [SerializeField] private Sprite _musicOff;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        
        private bool isEnabledMusic = true;
        private bool isEnabledSound = true;

        private void Start() => 
            _panelOptions.gameObject.SetActive(false);

        public void ButtonClickMusic()
        {
            if (isEnabledMusic)
            {
                _imageMusic.sprite = _musicOff;
                isEnabledMusic = false;
                _audioMusic.mute = true;
            }
            else
            {
                _imageMusic.sprite = _musicOn;
                isEnabledMusic = true;
                _audioMusic.mute = false;
            }
            
        }
        
        public void ButtonClickSound()
        {
            if (isEnabledSound)
            {
                _imageSound.sprite = _soundOff;
                isEnabledSound = false;
                _audioSound.mute = true;
            }
            else
            {
                _imageSound.sprite = _soundOn;
                isEnabledSound = true;
                _audioSound.mute = false;
            }
            
        }
    }
}