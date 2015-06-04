namespace VendingMachine.UI
{
    /// <summary>
    /// Не будем тянуть в простой проект Prism и InteractionRequest
    /// </summary>
    public interface IInteractionService
    {
        void Notify(string message);
    }
}