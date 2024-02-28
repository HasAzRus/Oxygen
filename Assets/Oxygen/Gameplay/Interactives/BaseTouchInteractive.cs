namespace Oxygen
{
    public abstract class BaseTouchInteractive : BaseInteractive
    {
        protected virtual void OnStopInteraction(Player player)
        {
            
        }
        
        public void StopInteraction(Player player)
        {
            OnStopInteraction(player);
        }
    }
}