namespace AIM.Autoplay.Behaviors.Strategy
{
    internal class Inverters
    {
        internal Inverter AlliesAreDead = new Inverter(new Conditionals().AlliesAreDead);
        internal Inverter LowHealth = new Inverter(new Conditionals().LowHealth);
    }
}