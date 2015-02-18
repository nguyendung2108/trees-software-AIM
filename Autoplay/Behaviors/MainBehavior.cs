using System;
using System.Linq;
using AIM.Autoplay.Behaviors.Strategy;
using AIM.Autoplay.Modes;
using AIM.Autoplay.Util.Objects;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Behaviors
{
    internal class MainBehavior
    {
        internal static Behavior Root = new Behavior(
            new IndexSelector(
                () =>
                {
                    var heroes = new Heroes();
                    var minions = new Minions();

                    if (!ObjectHandler.Get<Obj_AI_Minion>().Any())
                    {
                        Console.WriteLine("5");
                        return 5;
                    }
                    if (ObjectHandler.Get<Obj_AI_Hero>().Any(h => h.IsAlly && !h.IsMe && !h.InFountain()))
                    {
                        return 1;
                    }
                    if (Heroes.Me.IsDead)
                    {
                        return 0;
                    }
                    if (heroes.AllyHeroes.All(h => h.InFountain()) || Heroes.Me.Level >= 16 ||
                        !heroes.EnemyHeroes.Any(h => h.IsVisible) ||
                        (float) (Heroes.Me.ChampionsKilled + Heroes.Me.Assists) /
                        ((Heroes.Me.Deaths == 0) ? 1 : Heroes.Me.Deaths) > 2.5f ||
                        !minions.EnemyMinions.Any(m => m.IsVisible))
                    {
                        return 2;
                    }
                    if (Heroes.Me.HealthPercentage() < Base.Menu.Item("LowHealth").GetValue<Slider>().Value &&
                        Relics.ClosestRelic() != null)
                    {
                        return 3;
                    }
                    return 4;
                }, new Sequence(), new Sequences().TeamFight, new Sequences().LanePush,
                new Sequences().CollectHealthPack, new Sequences().StayWithinExpRange, new Sequences().WalkToLane));
    }
}