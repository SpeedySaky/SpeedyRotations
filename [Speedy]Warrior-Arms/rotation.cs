using System;
using System.Threading;
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
        var Rage = Player.RagePercent;
        if (Player.IsCasting || Player.IsChanneling) return false;
        var Target = WoW.Target;

        var TargetHealth = Target.HealthPercent;
        WoW.StartAttack();
        //combat 

        if (WoW.CanCast("Overpower") && Rage >= 15 && Player.HasAura(60503))
        {
            Console.WriteLine("Casting Overpower");
            if (WoW.Cast("Overpower") && Rage >= 5)
                return true;
        }
        if (WoW.CanCast("Victory Rush"))
        {
            Console.WriteLine("Casting Victory Rush");
            if (WoW.Cast("Victory Rush")) ;
            else
            if (WoW.Cast("Heroic Strike"))
                return true;
        }
        if (WoW.CanCast("Berseker Stance") && !Player.HasAura("Berserker Stance"))
        {
            Console.WriteLine("Casting Berseker Stance");
            if (WoW.Cast("Berseker Stance"))
                return true;
        }
        if (Target.IsCasting && WoW.CanCast("Pummel") && Player.HasAura("Berserker Stance"))
        {
            Console.WriteLine("Casting Pummel");
            if (WoW.Cast("Pummel"))
                return true;
        }
        if (WoW.CanCast("Bloodrage") && Health > 70)
        {
            Console.WriteLine("Casting Bloodrage");
            if (WoW.Cast("Bloodrage"))
                return true;
        }
        if (WoW.CanCast("Execute") && TargetHealth <= 20 && Rage >= 15)
        {
            Console.WriteLine("Casting Execute");
            if (WoW.Cast("Execute"))
                return true;
        }

        if (WoW.CanCast("Battle Shout") && !Player.HasAura("Battle Shout") && Rage >= 10)
        {
            Console.WriteLine("Casting Battle Shout");
            if (WoW.Cast("Battle Shout"))
                return true;
        }
        if (WoW.CanCast("Demoralizing Shout") && !Target.HasAura("Demoralizing Shout") && Rage >= 10)
        {
            Console.WriteLine("Casting Demoralizing Shout");
            if (WoW.Cast("Demoralizing Shout"))
                return true;
        }
        if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Thunder Clap") && !Target.HasAura("Thunder Clap") && Rage >= 20)
        {
            Console.WriteLine("Casting Thunder Clap");
            if (WoW.Cast("Thunder Clap"))
                return true;
        }
        if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Cleave") && Target.HasAura("Thunder Clap") && Rage >= 20)
        {
            Console.WriteLine("Casting Cleave");
            if (WoW.Cast("Cleave"))
                return true;
        }
        if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Sweeping Strikes"))
        {
            Console.WriteLine("Casting Sweeping Strikes");
            if (WoW.Cast("Sweeping Strikes"))
                return true;
        }
        if (WoW.UnitsFightingMe() >= 3 && WoW.CanCast("Retaliation"))
        {
            Console.WriteLine("Casting Retaliation");
            if (WoW.Cast("Retaliation"))
                return true;
        }
        if (WoW.CanCast("Rend") && !Target.HasAura("Rend") && Rage >= 10)
        {
            Console.WriteLine("Casting Rend");
            if (WoW.Cast("Rend"))
                return true;
        }
        if (WoW.CanCast("Hamstring") && !Target.HasAura("Hamstring") && Rage >= 10)
        {
            Console.WriteLine("Casting Hamstring");
            if (WoW.Cast("Hamstring"))
                return true;
        }


        if (WoW.CanCast("Mortal Strike") && Rage >= 30)
        {
            Console.WriteLine("Casting Mortal Strike");
            if (WoW.Cast("Mortal Strike"))
                return true;
        }
        if (WoW.CanCast("Heroic Strike") && Rage >= 15)
        {
            Console.WriteLine("Casting Heroic Strike");
            if (WoW.Cast("Heroic Strike"))
                return true;
        }
       

        if (WoW.CanCast("Overpower") && Rage >= 15)
        {
            Console.WriteLine("Casting Overpower");
            if (WoW.Cast("Overpower") && Rage >= 5) ;
            else
            if (WoW.Cast("Heroic Strike"))
                return true;
        }



        return false;
    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Rage = Player.RagePercent;
        var Target = WoW.Target;
        var TargetDistance = Target.Position.Distance3D(Player.Position);
        if (Player.IsDead || Player.IsGhost) return false;
        if (Player.IsInCombat || Player.IsMoving) return false;
        if (Player.IsCasting || Player.IsChanneling) return false;
		if (Target.IsDead || Target.IsGhost) return false;	


        if (WoW.CanCast("Charge") && !WoW.SpellOnCooldown("Charge"))
        {
            if (WoW.Cast("Charge"))
                return true;
        }
		
        return false;
    }

           


    public override bool WhileMounted()
    {
        return false;
    }
}
