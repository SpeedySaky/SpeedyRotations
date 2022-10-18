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
        QuickDelay = 50;
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




        //healing

        if (WoW.CanCast("Rejuvenation") && Health <= 40 && !Player.HasAura("Rejuvenation"))
        {
            Console.WriteLine("Casting Rejuvenation");
            if (WoW.Cast("Rejuvenation"))
                return true;
        }
        if (WoW.CanCast("Regrowth") && Health <= 40 && !Player.HasAura("Regrowth"))
        {
            Console.WriteLine("Casting Regrowth");
            if (WoW.Cast("Regrowth"))
                return true;
        }
        if (WoW.CanCast("Healing Touch") && Health <= 30)
        {
            Console.WriteLine("Casting Healing Touch");
            if (WoW.Cast("Healing Touch"))
                return true;
        }
        if (!Player.HasAura("Innervate") && WoW.CanCast("Innervate") && Mana <= 30)
        {
            Console.WriteLine("Casting Innervate");
            if (WoW.Cast("Innervate"))
                return true;
        }

        //cat form
        WoW.StartAttack();
        if (!Player.HasAura("Cat Form") && WoW.CanCast("Cat Form") )
        {
            Console.WriteLine("Casting Cat Form");
            if (WoW.Cast("Cat Form"))
                return true;
        }
        if (WoW.CanCast("Maim")&& Player.HasAura("Cat Form") && (Target.IsCasting || Target.IsChanneling))
        {
            Console.WriteLine("Casting Maim");
            if (WoW.Cast("Maim"))
                return true;
        }
        if (Energy >= 60 && WoW.CanCast("Berserk") && !Player.HasAura("Tiger's Fury") && !Player.HasAura("Berserk") && TargetHealth >= 50 && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Berserk");
            if (WoW.Cast("Berserk"))
                return true;
        }

        if (!Target.HasAura("Faerie Fire (Feral)") && WoW.CanCast("Faerie Fire (Feral)") && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Faerie Fire (Feral)");
            if (WoW.Cast("Faerie Fire (Feral)"))
                return true;
        }

        if (WoW.CanCast("Savage Roar") && !Player.HasAura("Savage Roar") && Points >= 2 && Target.HealthPercent >= 40 && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Savage Roar");
            if (WoW.Cast("Savage Roar"))
                return true;
        }
        if (Energy >= 30 && WoW.CanCast("Tiger's Fury") && !Player.HasAura("Tiger's Fury") && !Player.HasAura("Berserk") && TargetHealth >= 50 && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Tiger's Fury");
            if (WoW.Cast("Tiger's Fury"))
                return true;
        }
        if (Energy > 35 && WoW.CanCast("Rake") && !Target.HasAura("Rake") && Target.HealthPercent >= 30 && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Rake");
            if (WoW.Cast("Rake"))
                return true;
        }

        if (Energy > 30 && Points >= 2 && WoW.CanCast("Rip") && !Target.HasAura("Rip") && Target.HealthPercent >= 30 && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Rip");
            if (WoW.Cast("Rip"))
                return true;
        }
        if (Energy > 35 && Points >= 5 && WoW.CanCast("Ferocious Bite") && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Ferocious Bite");
            if (WoW.Cast("Ferocious Bite"))
                return true;
        }
        if (Energy >= 45 && WoW.CanCast("Mangle (Cat)") && Points < 5 && Player.HasAura("Cat Form") && !Target.HasAura("Mangle (Cat)"))
        {
            Console.WriteLine("Casting Mangle (Cat)");
            if (WoW.Cast("Mangle (Cat)"))
                return true;
        }
        if (Energy >= 45 && WoW.CanCast("Claw") && Points < 5 && Player.HasAura("Cat Form") )
        {
            Console.WriteLine("Casting Claw");
            if (WoW.Cast("Claw"))
                return true;
        }
        if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Swipe") && Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Swipe");
            if (WoW.Cast("Swipe"))
                return true;
        }

        if (Health < 60 && Mana > 60 && WoW.CanCast("Barkskin"))
        {
            Console.WriteLine("Casting Barkskin");
            if (WoW.Cast("Barkskin"))
                return true;
        }

        //bear form

        if (!Player.HasAura("Bear Form") && WoW.CanCast("Bear Form") && Mana >= 35 && !Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Bear Form");
            if (WoW.Cast("Bear Form"))
            return true;
        }
		 if (!Player.HasAura("Bear Form") && !Player.HasAura("Dire Bear Form") && WoW.CanCast("Dire Bear Form") && Mana >= 35 && !Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Dire Bear Form");
            if (WoW.Cast("Dire Bear Form"))
                return true;
        }

        if (!Target.HasAura("Faerie Fire (Feral)") && WoW.CanCast("Faerie Fire (Feral)") && Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Faerie Fire (Feral)");
            if (WoW.Cast("Faerie Fire (Feral)"))
                return true;
        }
		if (WoW.CanCast("Enrage") && Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Enrage");
            if (WoW.Cast("Enrage"))
                return true;
        }
        if (Energy > 35 && WoW.CanCast("Lacerate") && !Target.HasAura("Lacerate") && Target.HealthPercent >= 30 && Player.HasAura("Bear Form")&& !Player.HasAura("Cat Form") || Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Lacerate");
            if (WoW.Cast("Lacerate"))
                return true;
        }

        if (Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form") && WoW.CanCast("Demoralizing Roar") && !Target.HasAura("Demoralizing Roar")&& !Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Demoralizing Roar");
            if (WoW.Cast("Demoralizing Roar"))
                return true;
        }

        if (Health < 60 && Mana > 60 && WoW.CanCast("Survival Instincts") && Player.HasAura("Bear Form") && !Player.HasAura("Cat Form")|| Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Survival Instincts");
            if (WoW.Cast("Survival Instincts"))
                return true;
        }

        if (WoW.CanCast("Frenzied Regeneration") && Player.HasAura("Bear Form")&& !Player.HasAura("Cat Form") && Health<=50 || Player.HasAura("Dire Bear Form") && Player.HasAura("Survival Instincts"))
        {
            Console.WriteLine("Casting Frenzied Regeneration");
            if (WoW.Cast("Frenzied Regeneration"))
                return true;
        }

        if (Health < 60 && Mana > 60 && WoW.CanCast("Barkskin")&& !Player.HasAura("Cat Form")&& Player.HasAura("Bear Form")|| Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Barkskin");
            if (WoW.Cast("Barkskin"))
                return true;
        }
        if (Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form") && Target.IsCasting && WoW.CanCast("Bash") && !Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Bash");
            if (WoW.Cast("Bash"))
                return true;
        }

        if (Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form") && WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Swipe") && !Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Swipe");
            if (WoW.Cast("Swipe"))
                return true;
        }

        if (Energy > 40 && WoW.CanCast("Mangle (Bear)") && !Target.HasAura("Mangle (Bear)") && Target.HealthPercent >= 40 && Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Mangle (Bear)");
            if (WoW.Cast("Mangle (Bear)"))
                return true;
        }

        if (Player.HasAura("Bear Form") || Player.HasAura("Dire Bear Form") && WoW.CanCast("Maul") && !Player.HasAura("Cat Form"))
        {
            Console.WriteLine("Casting Maul");
            if (WoW.Cast("Maul"))
                return true;
        }



        //no form



        if (WoW.CanCast("Faerie Fire") && !Target.HasAura("Faerie Fire") && !Player.HasAura("Bear Form") && !Player.HasAura("Cat Form") && !Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Faerie Fire");
            if (WoW.Cast("Faerie Fire"))
                return true;
        }
        if (WoW.CanCast("Wrath") && TargetHealth >= 40 && Mana >= 30 && !Player.HasAura("Bear Form") && !Player.HasAura("Cat Form") && !Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Wrath");
            if (WoW.Cast("Wrath"))
                return true;
        }
        if (WoW.CanCast("Wrath") && TargetHealth <= 40 && TargetDistance > 7 && Mana >= 30 && !Player.HasAura("Bear Form") && !Player.HasAura("Cat Form") && !Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Wrath");
            if (WoW.Cast("Wrath"))
                return true;
        }

        if (WoW.CanCast("Moonfire") && TargetHealth >= 40 && !Target.HasAura("Moonfire") && !Player.HasAura("Bear Form") && !Player.HasAura("Cat Form") && !Player.HasAura("Dire Bear Form"))
        {
            Console.WriteLine("Casting Moonfire");
            if (WoW.Cast("Moonfire"))
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
        if (Player.HasAura("Swift Flight Form")) return false;
        if (Player.HasAura("Flight Form")) return false;
        if (Player.HasAura("Travel Form")) return false;


        if (!Player.HasAura("Mark of the Wild") && WoW.CanCast("Mark of the Wild") && !Player.HasAura("Gift of the Wild"))
        {
            Console.WriteLine("Buffing MotW");
            if (WoW.Cast("Mark of the Wild"))
                return true;
        }

        if (!Player.HasAura("Innervate") && WoW.CanCast("Innervate") && Mana <= 30)
        {
            Console.WriteLine("Casting Innervate");
            if (WoW.Cast("Innervate"))
                return true;
        }
        if (!Player.HasAura("Thorns") && WoW.CanCast("Thorns"))
        {
            Console.WriteLine("Buffing Thorns");
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
            Console.WriteLine("Casting Regrowth");
            if (WoW.Cast("Regrowth"))
                return true;
        }
        if (WoW.CanCast("Healing Touch") && Health <= 30)
        {
            Console.WriteLine("Casting Healing Touch");
            if (WoW.Cast("Healing Touch"))
                return true;
        }
        if (!Player.HasAura("Cat Form") && WoW.CanCast("Cat Form"))
        {
            Console.WriteLine("Casting Cat Form");
            if (WoW.Cast("Cat Form"))
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

