using System;

namespace Data
{
    [Serializable]
    public class MusicData
    {
        public bool IsEnabledMusic;
        public bool IsEnabledSound;

        public MusicData()
        {
            IsEnabledMusic = true;
            IsEnabledSound = true;
        }
    }
}