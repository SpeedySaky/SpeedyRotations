using System;
using System.Threading;
using Shadow_WoW;
using Shadow_WoW.Warcraft;
using Shadow_WoW;
using Shadow_WoW.Internal;
using Shadow_WoW.Managers;
using Shadow_WoW.Warcraft;

internal class Rotation : IRotation
{

    public override void Initialize()
    {
        QuickDelay = 50;
        SlowDelay = 600;
    }

    public override bool InCombat()
    {
        var Player = WoW.Me;        
        var Health = Player.HealthPercent;
        var Points = Player.ComboPoints;
        if (Player.IsCasting || Player.IsChanneling) return false;

        var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;

                        
            var Pet = WoW.Pet;
            var Energy = Player.Mana;            
            var PetHealth = Pet.HealthPercent;
            var TargetHealth = Target.HealthPercent;


            if (!Player.IsCasting && PetHealth <= 50 && !Pet.HasAura("Mend Pet"))
            {
                WoW.Cast("Mend Pet");
                return true;
            }

            if (!Player.IsCasting && WoW.CanCast("Intimdation"))
            {
                WoW.Cast("Intimidation");
                return true;
            }

            if (WoW.CanCast("Bestial Wrath") && !Player.IsCasting)
            {
                WoW.Cast("Bestial Wrath");
                return true;
            }

            if (WoW.CanCast("Concussive Shot") && !Player.IsCasting)
            {
                WoW.Cast("Concussive Shot");
                return true;
            }

            if (WoW.CanCast("Arcane Shot") && !Player.IsCasting && TargetHealth <= 50)
            {
                WoW.Cast("Arcane Shot");
                return true;
            }

            if (WoW.CanCast("Scorpid Sting") && !Player.IsCasting && !Target.HasAura("Scorpid Sting"))
            {
                WoW.Cast("Scorpid Sting");
                return true;
            }

       
        return false;
    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Energy = Player.Mana;
        var Health = Player.HealthPercent;
        var Points = Player.ComboPoints;
        var Target = WoW.Target;
        var Pet = WoW.Pet;
        if (Player.IsDead || Player.IsGhost) return false;
        if (Player.IsInCombat || Player.IsMoving) return false;
        if (Player.IsCasting || Player.IsChanneling) return false;

        if (Energy > 20 && !Player.HasAura("Aspect of the Hawk"))
        {
            WoW.Cast("Aspect of the Hawk");
            return true;
        }

        if (!Player.Target.IsEmpty() && WoW.IsValid(Target))
        {            
			WoW.Trigger("pull");
            return true;
        }

        if (!Pet.Target.IsEmpty() && !Player.IsInCombat)
        {
            WoW.TargetPet();
            Thread.Sleep(250);
            WoW.AssistTarget();
            return true;
        }

        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }
}