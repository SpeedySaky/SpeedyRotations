using System;
using System.Threading;
using Shadow_WoW.Warcraft;
using Shadow_WoW;
using Shadow_WoW.Internal;
using Shadow_WoW.Managers;



internal class Rotation : IRotation
{

    public override void Initialize()
    {
        QuickDelay = 250;
        SlowDelay = 650;
    }

    public override bool InCombat()
    {
        var Player = WoW.Me;
        var IsCasting = Player.IsCasting;
        var IsChanneling = Player.IsChanneling;
        var Energy = Player.Energy;
        var Mana = Player.ManaPercent;
        var Health = Player.HealthPercent;
        var Points = Player.ComboPoints;


        if (IsCasting || IsChanneling) return false;
        var Target = WoW.Target;
        var TargetHealth = Target.HealthPercent;
        var TargetDistance = Target.Position.Distance3D(Player.Position);
		
        if (Target.IsDead || Target.IsGhost) return false;

		if (WoW.CanCast("Rejuvenation") && Health <= 60 && !Player.HasAura("Rejuvenation"))
        {
            Console.WriteLine("Casting Rejuvenation");
            if (WoW.Cast("Rejuvenation"))
                return true;
        }
        if (WoW.CanCast("Regrowth") && Health <= 40 && !Player.HasAura("Regrowth"))
        {
            Console.WriteLine("Casting Regrowth" );
            if (WoW.Cast("Regrowth"))
                return true;
        }
        if (WoW.CanCast("Healing Touch") && Health <= 30)
        {
            Console.WriteLine("Casting Healing Touch" );
            if (WoW.Cast("Healing Touch"))
                return true;
        }
		
        if (!Player.HasAura("Innervate") && WoW.CanCast("Innervate") && Mana <= 30)
        {
            Console.WriteLine("Casting Innervate");

            if (WoW.Cast("Innervate"))
                return true;
        }

        if (!Player.HasAura("Moonkin Form") && WoW.CanCast("Moonkin Form"))
        {
            Console.WriteLine("Casting Moonkin Form");

            if (WoW.Cast("Moonkin Form"))
                return true;
        }

        if (WoW.CanCast("Faerie Fire") && !Target.HasAura("Faerie Fire") && Player.HasAura("Moonkin Form"))
        {
            Console.WriteLine("Casting Faerie Fire");

            if (WoW.Cast("Faerie Fire"))
                return true;
        }
		
	
            if (WoW.CanCast("Force of Nature"))
            {
                if (WoW.SpellInRange("Force of Nature"))
                {                  
                    
                    
                    if (WoW.Cast("Force of Nature",Player.Position))
                    {
                        return true;
                    }
                }
            }
        
        
       
        if (WoW.CanCast("Starfall"))
        {
            Console.WriteLine("Casting Starfall" );

            if (WoW.Cast("Starfall"))
                return true;
        }


        if (WoW.CanCast("Insect Swarm") && Player.HasAura(48517) && !Target.HasAura("Insect Swarm"))
        {
            Console.WriteLine("Casting Insect Swarm" );

            if (WoW.Cast("Insect Swarm"))
                return true;
        }
        if (WoW.CanCast("Moonfire") && Player.HasAura(48518) && !Target.HasAura("Moonfire"))
        {
            Console.WriteLine("Casting Moonfire" );

            if (WoW.Cast("Moonfire"))
                return true;
        }
        if (WoW.CanCast("Wrath") && Player.HasAura(48517))
        {
            Console.WriteLine("Casting Wrath" );

            if (WoW.Cast("Wrath"))
                return true;
        }
        if (WoW.CanCast("Starfire") && Player.HasAura(48518))
        {
            Console.WriteLine("Casting Starfire");

            if (WoW.Cast("Starfire"))
                return true;
        }
        if (WoW.CanCast("Wrath") && WoW.CanCast("Starfire"))
        {
            Console.WriteLine("Casting Wrath");

            if (WoW.Cast("Wrath")); 
            else
                Console.WriteLine("Casting Starfire" );

            if (WoW.Cast("Starfire"))
                return true;
        }

        return false;

    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Mana = Player.ManaPercent;
        var Target = WoW.Target;
        var Health = Player.HealthPercent;
		
        var TargetDistance = Target.Position.Distance3D(Player.Position);
        var TargetHealth = Target.HealthPercent;
        if (Player.IsDead || Player.IsGhost || Player.IsCasting || Player.IsMounted) return false;
        if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;

      



        if (!Player.HasAura("Moonkin Form") && WoW.CanCast("Moonkin Form"))
        {
            Console.WriteLine("Casting Faerie Fire" );

            if (WoW.Cast("Moonkin Form"))
                return true;
        }
        if (!Player.HasAura("Innervate") && WoW.CanCast("Innervate") && Mana <= 30)
        {
            Console.WriteLine("Casting Faerie Fire" );

            if (WoW.Cast("Innervate"))
                return true;
        }
        if (!Player.HasAura("Mark of the Wild") && WoW.CanCast("Mark of the Wild") && !Player.HasAura("Gift of the Wild"))
        {
            Console.WriteLine("Buffing MotW" );
            if (WoW.Cast("Mark of the Wild"))
                return true;
        }
        if (!Player.HasAura("Thorns") && WoW.CanCast("Thorns"))
        {
            Console.WriteLine("Buffing Thorns" );
            if (WoW.Cast("Thorns"))
                return true;
        }

        if (WoW.CanCast("Rejuvenation") && Health <= 60 && !Player.HasAura("Rejuvenation"))
        {
            Console.WriteLine("Casting Rejuvenation");
            if (WoW.Cast("Rejuvenation"))
                return true;
        }
        if (WoW.CanCast("Regrowth") && Health <= 40 && !Player.HasAura("Regrowth"))
        {
            Console.WriteLine("Casting Regrowth" );
            if (WoW.Cast("Regrowth"))
                return true;
        }
        if (WoW.CanCast("Healing Touch") && Health <= 30)
        {
            Console.WriteLine("Casting Healing Touch" );
            if (WoW.Cast("Healing Touch"))
                return true;
        }



        if (WoW.Target.IsDead || WoW.Target.IsGhost) return false;

        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }

}

