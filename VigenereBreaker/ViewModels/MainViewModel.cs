using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VigenereBreaker.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private IMessageService messages;

        #region Constructors
        public MainViewModel(IList<Model.Language> langs, IMessageService messageService)
        {
            messages = messageService ?? throw new ArgumentNullException(nameof(messageService));

            Languages = langs ?? throw new ArgumentNullException(nameof(langs));
            Language = langs[0];

            FindKeyWord = true;
            OutputText = "";
            InputText = "";
        }
        #endregion

        #region Properties
        private IEnumerable<Model.Language> languages;
        public IEnumerable<Model.Language> Languages
        {
            get { return languages; }
            set
            {
                languages = value;
                OnPropertyChanged();
            }
        }

        private Model.Language language;
        public Model.Language Language
        {
            get { return language; }
            set
            {
                language = value;
                BVM = new BreakerSettingsViewModel(value);
                OnPropertyChanged();
            }
        }

        private BreakerSettingsViewModel bvm;
        public BreakerSettingsViewModel BVM
        {
            get
            {
                return bvm;
            }
            set
            {
                bvm = value;
                OnPropertyChanged();
            }
        }

        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value ?? "";
                OnPropertyChanged();
            }
        }

        private string outputText;
        public string OutputText
        {
            get { return outputText; }
            set
            {
                outputText = value ?? "";
                OnPropertyChanged();
            }
        }

        private string keyWord;
        public string Keyword
        {
            get { return keyWord; }
            set
            {
                keyWord = value;
                OnPropertyChanged();
            }
        }

        private bool findKeyWord;
        public bool FindKeyWord
        {
            get { return findKeyWord; }
            set
            {
                findKeyWord = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private ICommand encCommand;
        public ICommand EncryptCommand
        {
            get
            {
                return encCommand ?? (encCommand = new Command(Encrypt));
            }
        }

        private ICommand decCommand;
        public ICommand DecryptCommand
        {
            get
            {
                return decCommand ?? (decCommand = new Command(Decrypt));
            }
        }

        private ICommand swapCommand;
        public ICommand SwapCommand
        {
            get { return swapCommand ?? (swapCommand = new Command(MoveOutputToInput)); }
        }

        private ICommand clearInCommand;
        public ICommand ClearInputCommand
        {
            get { return clearInCommand ?? (clearInCommand = new Command(ClearInput)); }
        }

        private ICommand clearOutCommand;
        public ICommand ClearOutputCommand
        {
            get { return clearOutCommand ?? (clearOutCommand = new Command(ClearOutput)); }
        }

        #endregion

        #region Methods
        private void Encrypt()
        {
            if (string.IsNullOrEmpty(Keyword))
            {
               messages.Show("Keyword to short.");
                return;
            }
            try
            {
                OutputText = Language.VigenereCipher.Encrypt(PrepareText(InputText), Keyword);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void Decrypt()
        {
            if (!FindKeyWord && string.IsNullOrEmpty(Keyword))
            {
                MessageBox.Show("Key is to short.");
                return;
            }

            try
            {
                if (FindKeyWord)
                {
                    if (bvm.ManualKeywordLength)
                    {
                        Keyword = Language.VigenereBreaker.FindKey(InputText, bvm.KeywordLength);
                    }
                    else
                    {
                        Keyword = Language.VigenereBreaker.FindKey(InputText);
                    }
                }

                OutputText = Language.VigenereCipher.Decrypt(InputText, Keyword);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private string PrepareText(string text)
        {
            return new string(text.ToLower().Where(char.IsLetter).ToArray());
        }

        private void MoveOutputToInput()
        {
            InputText = OutputText;
            ClearOutput();
        }

        private void ClearInput()
        {
            InputText = string.Empty;
        }

        private void ClearOutput()
        {
            OutputText = string.Empty;
        }

        public void HandleException(Exception exception)
        {
            messages.Show(exception.Message, exception.GetType().Name);
        }
        #endregion
    }
}