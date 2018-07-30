namespace VigenereBreaker.ViewModels
{
    interface IMessageService
    {
        void Show(string text);

        void Show(string text, string caption);
    }
}
