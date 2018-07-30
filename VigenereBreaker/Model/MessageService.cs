using System.Windows;
using VigenereBreaker.ViewModels;

namespace VigenereBreaker.Model
{
    class MessageService : IMessageService
    {
        public void Show(string text)
        {
            MessageBox.Show(text);
        }

        public void Show(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }
    }
}
