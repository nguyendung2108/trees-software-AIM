using BehaviorSharp.Components.Composites;

namespace AIM.Autoplay.Behaviors.Strategy
{
    internal class Sequences
    {
        internal Sequence CollectHealthPack = new Sequence(
            new Actions().CollectHealthRelic, new Conditionals().ShouldCollectHealthRelic);

        internal Sequence LanePush = new Sequence(
            new Actions().PushLane, new Conditionals().ShouldPushLane, new Inverters().LowHealth);

        internal Sequence StayWithinExpRange = new Sequence(new Actions().StayWithinExpRange);
        internal Sequence TeamFight = new Sequence(new Actions().Teamfight);
        internal Sequence TryToKill = new Sequence(new Actions().KillEnemy);
        internal Sequence WalkToLane = new Sequence(new Actions().ProtectFarthestTurret, new Conditionals().NoMinions);
    }
}