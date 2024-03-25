using Data;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Element
{
    public class MusicOptions : MonoBehaviour
    {
        private const string MusicParameter = "Music";
        private const string SfxParameter = "SFX";
        private const string KeyMusic = "MusicEnable";
        private const string KeySfx = "SFXEnable";
        
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
        private int _volumeMusic = 1;
        private int _volumeSfx = 1;

        private void Start()
        {
            _panelOptions.gameObject.SetActive(false);
            
            LoadMusicAndSfx();
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
            
            SaveMusic(KeyMusic, _volumeMusic, _isEnabledMusic);
            Debug.Log(" save ");
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
            
            SaveMusic(KeySfx, _volumeSfx, _isEnabledSound);
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

        private void SaveMusic(string key, int volume, bool music)
        {
            volume = music ? 1 : 0;

            PlayerPrefs.SetInt(key, volume);
        }

        private void LoadMusicAndSfx()
        {
            if (PlayerPrefs.HasKey(KeySfx))
            {
                _volumeSfx = PlayerPrefs.GetInt(KeySfx);
            }

            if (PlayerPrefs.HasKey(KeyMusic))
            {
                _volumeMusic = PlayerPrefs.GetInt(KeyMusic);
            }

            _isEnabledMusic = _volumeMusic == 1;
            _isEnabledSound = _volumeSfx == 1;

            SaveMusic(KeySfx, _volumeSfx, _isEnabledSound);
            SaveMusic(KeyMusic, _volumeMusic, _isEnabledMusic);
        }
    }
}