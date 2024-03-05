using Data;
using Infrastructure.Service.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Element
{
    public class MusicOptions : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Transform _panelOptions;
        [SerializeField] private Image _imageMusic;
        [SerializeField] private Image _imageSound;
        [SerializeField] private Sprite _musicOn;
        [SerializeField] private Sprite _musicOff;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        [SerializeField] private AudioMixerGroup _mixer;
        
        private bool _isEnabledMusic = true;
        private bool _isEnabledSound = true;
        private float _minVolume = -80;
        private float _maxVolume = 0;
        private PlayerProgress _player;

        private const string MusicParameter = "Music";
        private const string SfxParameter = "SFX";
        private const string KeyMusic = "MusicEnable";
        private const string KeySfx = "SFXEnable";

        private void Start()
        {
            _panelOptions.gameObject.SetActive(false);
            
            // ButtonClickMusic();
            // ButtonClickSound();

            ChangeSpriteMusic();
        }

        public void ButtonClickMusic()
        {
            if (_isEnabledMusic)
            {
                _imageMusic.sprite = _musicOff;
                _isEnabledMusic = false;
                _mixer.audioMixer.SetFloat(MusicParameter, _minVolume);
            }
            else
            {
                _imageMusic.sprite = _musicOn;
                _isEnabledMusic = true;
                _mixer.audioMixer.SetFloat(MusicParameter, _maxVolume);
            }
        }

        public void ButtonClickSound()
        {
            if (_isEnabledSound)
            {
                _imageSound.sprite = _soundOff;
                _isEnabledSound = false;
                _mixer.audioMixer.SetFloat(SfxParameter, _minVolume);
            }
            else
            {
                _imageSound.sprite = _soundOn;
                _isEnabledSound = true;
                _mixer.audioMixer.SetFloat(SfxParameter, _maxVolume);
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _player = progress;
            _isEnabledMusic = progress.MusicData.IsEnabledMusic;
            _isEnabledSound = progress.MusicData.IsEnabledSound;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.MusicData.IsEnabledMusic = _isEnabledMusic;
            progress.MusicData.IsEnabledSound = _isEnabledSound;
        }

        private void ChangeSpriteMusic()
        {
            if (_isEnabledMusic)
            {
                _imageMusic.sprite = _musicOn;
                _isEnabledMusic = true;
                _mixer.audioMixer.SetFloat(MusicParameter, _maxVolume);
            }
            else
            {
                _imageMusic.sprite = _musicOff;
                _isEnabledMusic = false;
                _mixer.audioMixer.SetFloat(MusicParameter, _minVolume);
            }

            if (_isEnabledSound)
            {
                _imageSound.sprite = _soundOn;
                _mixer.audioMixer.SetFloat(SfxParameter, _maxVolume);
            }
            else
            {
                _imageSound.sprite = _soundOff;
                _mixer.audioMixer.SetFloat(SfxParameter, _minVolume);
            }
        }
    }
}