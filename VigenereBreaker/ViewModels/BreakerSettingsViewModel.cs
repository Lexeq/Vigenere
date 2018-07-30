using System;
using VigenereBreaker.Model;

namespace VigenereBreaker.ViewModels
{
    class BreakerSettingsViewModel : BaseViewModel
    {
        Language lang;

        public BreakerSettingsViewModel(Language language)
        {
            this.lang = language ?? throw new ArgumentNullException(nameof(language));
            KeywordLength = 1;
        }

        private bool manual;
        public bool ManualKeywordLength
        {
            get { return manual; }
            set
            {
                manual = value;
                OnPropertyChanged();
            }
        }

        private int keyLength;
        public int KeywordLength
        {
            get { return keyLength; }
            set
            {
                keyLength = value > 0 ? value : 1;
                OnPropertyChanged();
            }
        }

        public double IOC
        {
            get
            {
                return lang.VigenereBreaker.MaxIocDeviation;
            }
            set
            {
                lang.VigenereBreaker.MaxIocDeviation = value; ;
                OnPropertyChanged();
            }
        }

        public double CaesarCoefficient
        {
            get { return lang.CaesarBreaker.MaxDeviation; }
            set
            {
                lang.CaesarBreaker.MaxDeviation = value;
                OnPropertyChanged();
            }
        }
    }
}
