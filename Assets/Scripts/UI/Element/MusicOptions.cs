using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.Element
{
    public class MusicOptions : MonoBehaviour
    {
        [SerializeField] private Transform _panelOptions;
        [SerializeField] private Image _imageMusic;
        [SerializeField] private Image _imageSound;
        [SerializeField] private Sprite _musicOn;
        [SerializeField] private Sprite _musicOff;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        [SerializeField] private AudioMixerGroup _mixer;
        
        private bool isEnabledMusic;
        private bool isEnabledSound;
        private float _minVolume = -80;
        private float _maxVolume = 0;
        
        private const string MusicParameter = "Music";
        private const string SfxParameter = "SFX";
        private const string KeyMusic = "MusicEnable";
        private const string KeySfx = "SFXEnable";

        private void Start()
        {
            _panelOptions.gameObject.SetActive(false);

            isEnabledMusic = PlayerPrefs.GetInt(KeyMusic) == 1;
            isEnabledSound = PlayerPrefs.GetInt(KeySfx) == 1;
            ChangeSpriteMusic();
        }

        public void ButtonClickMusic()
        {
            if (isEnabledMusic)
            {
                _imageMusic.sprite = _musicOff;
                isEnabledMusic = false;
                _mixer.audioMixer.SetFloat(MusicParameter, _minVolume);
            }
            else
            {
                _imageMusic.sprite = _musicOn;
                isEnabledMusic = true;
                _mixer.audioMixer.SetFloat(MusicParameter, _maxVolume);
            }
            
            
            PlayerPrefs.SetInt(KeyMusic, isEnabledMusic ? 1 : 0);
        }

        private void ChangeSpriteMusic()
        {
            if (isEnabledMusic)
            {
                _imageMusic.sprite = _musicOn;
                _mixer.audioMixer.SetFloat(MusicParameter, _maxVolume);
            }
            else
            {
                _imageMusic.sprite = _musicOff;
                _mixer.audioMixer.SetFloat(MusicParameter, _minVolume);
            }

            if (isEnabledSound)
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
        
        public void ButtonClickSound()
        {
            if (isEnabledSound)
            {
                _imageSound.sprite = _soundOff;
                isEnabledSound = false;
                _mixer.audioMixer.SetFloat(SfxParameter, _minVolume);
            }
            else
            {
                _imageSound.sprite = _soundOn;
                isEnabledSound = true;
                _mixer.audioMixer.SetFloat(SfxParameter, _maxVolume);
            }
            
            PlayerPrefs.SetInt(KeySfx, isEnabledMusic ? 1 : 0);
        }
    }
}