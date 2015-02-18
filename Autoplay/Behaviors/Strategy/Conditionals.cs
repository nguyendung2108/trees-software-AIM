using System;
using System.Collections.Generic;
using System.Linq;
using AIM.Autoplay.Modes;
using AIM.Autoplay.Util.Objects;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Autoplay.Behaviors.Strategy
{
    internal class Conditionals
    {
        private static readonly Heroes Heroes = new Heroes();

        internal Conditional AlliesAreDead =
            new Conditional(() => ObjectHandler.Get<Obj_AI_Hero>().All(h => h.IsAlly && h.IsDead));

        internal Conditional JoinTeamFight =
            new Conditional(() => ObjectHandler.Get<Obj_AI_Hero>().Any(h => !h.InFountain()));

        internal Conditional LowHealth =
            new Conditional(() => Heroes.Me.HealthPercentage() < Base.Menu.Item("LowHealth").GetValue<Slider>().Value);

        internal Conditional NoMinions =
            new Conditional(
                () =>
                    ((Utility.Map.GetMap().Type == Utility.Map.MapType.SummonersRift)
                        ? (Environment.TickCount - Load.LoadedTime < 115)
                        : (Environment.TickCount - Load.LoadedTime <= 60)) ||
                    ((Utility.Map.GetMap().Type == Utility.Map.MapType.SummonersRift)
                        ? (Heroes.Me.Level == 1)
                        : (Heroes.Me.Level <= 3)));

        internal Conditional ShouldCollectHealthRelic =
            new Conditional(
                () => Relics.ClosestRelic() != null && new Conditionals().LowHealth.Tick() == BehaviorState.Success);

        internal Conditional ShouldPushLane = new Conditional(
            () =>
            {
                var heroes = new Heroes();
                var minions = new Minions();

                return heroes.AllyHeroes.All(h => h.InFountain()) || Heroes.Me.Level >= 16 ||
                       !heroes.EnemyHeroes.Any(h => h.IsVisible) ||
                       (float) (Heroes.Me.ChampionsKilled + Heroes.Me.Assists) /
                       ((Heroes.Me.Deaths == 0) ? 1 : Heroes.Me.Deaths) > 2.5f ||
                       !minions.EnemyMinions.Any(m => m.IsVisible);
            });

        internal Conditional ShouldTryToKill = new Conditional(
            () =>
            {
                var spells = new List<SpellSlot> { SpellSlot.Q, SpellSlot.W, SpellSlot.E };
                return
                    (Heroes.EnemyHeroes.Any(
                        h =>
                            h.Health <
                            Heroes.Me.GetComboDamage(h, spells) + Heroes.Me.GetAutoAttackDamage(Heroes.Me) * 2));
            });
    }
}