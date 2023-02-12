using System;
using System.Threading;
using Shadow_WoW.Warcraft;
using Shadow_WoW;
using Shadow_WoW.Internal;
using Shadow_WoW.Managers;
using System.Collections.Generic;



internal class Rotation : IRotation
{

    public override void Initialize()
    {
        QuickDelay = 50;
        SlowDelay = 650;
    }
    private List<string> Dispels = new List<string>()
{
    "Contagion of Rot",
    "Bonechewer Rot",
    "Ghoul Rot",
    "Maggot Slime",
    "Corrupted Strength",
    "Corrupted Agility",
    "Corrupted Intellect",
    "Corrupted Stamina",
    "Black Rot",
    "Volatile Infection",
    "Ghoul Plague",
    "Corrupting Plague",
    "Lacerating Bite",
    "Sporeskin",
    "Cadaver Worms",
    "Rabies",
    "Diseased Shot",
    "Tetanus",
    "Dredge Sickness",
    "Noxious Catalyst",
    "Spirit Decay",
    "Withered Touch",
    "Putrid Enzyme",
    "Infected Wound",
    "Infected Spine",
    "Black Sludge",
    "Silithid Pox",
    "Festering Rash",
    "Dark Sludge",
    "Fevered Fatigue",
    "Muculent Fever",
    "Infected Bite",
    "Fungal Decay",
    "Diseased Spit",
    "Choking Vines",
    "Fevered Disease",
    "Lingering Vines",
    "Festering Wound",
    "Creeping Vines",
    "Parasite",
    "Wandering Plague",
    "Irradiated",
    "Dark Plague",
    "Plague Mind",
    "Diseased Slime",
    "Putrid Stench",
    "Wither",
    "Seething Plague",
    "Death's Door",
    "Plague Strike",
};

    public override bool InCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;

        if (Player.IsCasting || Player.IsChanneling) return false;

        var Target = WoW.Target;
        var TargetDistance = Target.Position.Distance2D(Player.Position);
        if (Target.IsDead || Target.IsGhost) return false;



        var Mana = Player.ManaPercent;
        var TargetHealth = Target.HealthPercent;

        //healing


        if (WoW.CanCast("Renew") && Health < 90 && !Player.HasAura("Renew") && Mana >= 10 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Renew");
            if (WoW.Cast("Renew"))
                return true;
        }

        if (WoW.CanCast("Greater Heal") && Health <= 45 && Mana >= 20 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Greater Heal");
            if (WoW.Cast("Greater Heal"))
                return true;
        }
        if (WoW.CanCast("Lesser Heal") && Health <= 45 && WoW.Me.Level <= 10 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Lesser Heal");
            if (WoW.Cast("Lesser Heal"))
                return true;
        }
        if (WoW.CanCast("Flash Heal") && Health <= 60 && Mana >= 20 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Flash Heal");
            if (WoW.Cast("Flash Heal"))
                return true;
        }
        if (WoW.CanCast("Heal") && Health <= 65 && Mana >= 20 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Heal");
            if (WoW.Cast("Heal"))
                return true;
        }

        if (WoW.CanCast("Desperate Prayer") && Health < 40 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Desperate Prayer");
            if (WoW.Cast("Desperate Prayer"))
                return true;
        }
        if (!Player.HasAura("Power Word: Shield") && !Player.HasAura("Weakened Soul") && WoW.CanCast("Power Word: Shield") && Mana >= 10 && TargetHealth >= 30)
        {
            Console.WriteLine("Casting Power Word: Shield");
            if (WoW.Cast("Power Word: Shield"))
                return true;
        }


        //combat


        if (WoW.CanCast("Shadowform") && !Player.HasAura("Shadowform") && Mana >= 20)
        {
            Console.WriteLine("Casting Shadowform");
            if (WoW.Cast("Shadowform"))
                return true;
        }
        if (WoW.CanCast("Shadowfiend") && Mana >= 20)
        {
            Console.WriteLine("Casting Shadowfiend");
            if (WoW.Cast("Shadowfiend"))
                return true;
        }

        if (WoW.CanCast("Shoot") && Mana <= 10)
        {
            Console.WriteLine("Casting Shoot");
            if (WoW.Cast("Shoot"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Shadow Word: Pain") && !Target.HasAura("Shadow Word: Pain") && Mana >= 15 && TargetHealth >= 30)
        {
            Console.WriteLine("Casting Shadow Word: Pain");
            if (WoW.Cast("Shadow Word: Pain"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Shadow Word: Pain") && !Target.HasAura("Shadow Word: Pain") && Mana >= 15 && TargetHealth >= 20 && WoW.Me.Level <= 5)
        {
            Console.WriteLine("Casting Shadow Word: Pain");
            if (WoW.Cast("Shadow Word: Pain"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Vampiric Touch") && !Target.HasAura("Vampiric Touch") && Mana >= 20)
        {
            Console.WriteLine("Casting Vampiric Touch");
            if (WoW.Cast("Vampiric Touch"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Devouring Plague") && !Target.HasAura("Devouring Plague") && Mana >= 20 && Target.HealthPercent >= 20)
        {
            Console.WriteLine("Casting Devouring Plague");
            if (WoW.Cast("Devouring Plague"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Mind Blast") && Target.HealthPercent >= 50 && Mana >= 30 && !WoW.SpellOnCooldown("Mind Blast"))
        {
            Console.WriteLine("Casting Mind Blast");
            if (WoW.Cast("Mind Blast"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Mind Flay") && TargetDistance <= 25 && Mana >= 50 && TargetHealth >= 80)
        {
            Console.WriteLine("Casting Mind Flay");
            if (WoW.Cast("Mind Flay"))
                return true;
        }
        if (WoW.CanCast("Starshards") && !Target.HasAura("Starshards") && !Player.HasAura("Shadowform") && Mana >= 20)
        {
            Console.WriteLine("Casting Starshards");
            if (WoW.Cast("Starshards"))
                return true;
        }
        if (WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Psychic Scream") && Mana >= 10)
        {
            Console.WriteLine("Casting Psychic Scream");
            if (WoW.Cast("Psychic Scream"))
                return true;
        }

        if (!Player.IsCasting && WoW.CanCast("Holy Fire") && TargetHealth >= 50 && !Target.HasAura("Holy Fire") && Mana >= 50 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Holy Fire");
            if (WoW.Cast("Holy Fire"))
                return true;
        }

        if (!Player.IsCasting && WoW.CanCast("Smite") && TargetHealth >= 70 && Mana >= 50 && !Player.HasAura("Shadowform"))
        {
            Console.WriteLine("Casting Smite");
            if (WoW.Cast("Smite"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Smite") && Mana >= 10 && WoW.Me.Level <= 5)
        {
            Console.WriteLine("Casting Smite low lvl");
            if (WoW.Cast("Smite"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Shadow Word: Death") && TargetHealth <= 5 && Mana >= 5)
        {
            Console.WriteLine("Casting Shadow Word: Death");
            if (WoW.Cast("Shadow Word: Death"))
                return true;
        }

        if (WoW.CanCast("Shoot") && WoW.Me.Level >= 6)
        {
            Console.WriteLine("Casting Shoot");
            if (WoW.Cast("Shoot"))
                return true;
        }


        return false;
    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Mana = Player.ManaPercent;
        var Health = Player.HealthPercent;
        if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
        if (Player.IsDead || Player.IsGhost || Player.IsCasting) return false;
        var Target = WoW.Target;
        var auras = Player.Auras();
        foreach (var aura in auras)
        {
            if (Dispels.Contains(WoW.C__Spell.Name(aura.SpellID)) && WoW.CanCast("Cure Disease") || WoW.SpellDispel(aura.SpellID) == uDispelType.Disease && WoW.CanCast("Cure Disease") || WoW.SpellDispel(aura.SpellID) == uDispelType.Poison && WoW.CanCast("Cure Disease"))

            {
                Console.WriteLine("Casting Cure Disease");
                if (WoW.Cast("Cure Disease"))
                    return true;
            }
        }
		 if (WoW.CanCast("Shadowform") && !Player.HasAura("Shadowform") && Mana >= 20)
        {
            Console.WriteLine("Casting Shadowform");
            if (WoW.Cast("Shadowform"))
                return true;
        }
        if (!Player.HasAura("Renew") && Health <= 80)
        {
            Console.WriteLine("Casting Renew");
            if (WoW.Cast("Renew")) ;
            return true;
        }


        if (!Player.HasAura("Power Word: Fortitude") && WoW.CanCast("Power Word: Fortitude"))
        {
            Console.WriteLine("Casting Power Word: Fortitude");
            if (WoW.Cast("Power Word: Fortitude"))
                return true;
        }
        if (!Player.HasAura("Vampiric Embrace") && WoW.CanCast("Vampiric Embrace"))
        {
            Console.WriteLine("Casting Vampiric Embrace");
            if (WoW.Cast("Vampiric Embrace"))
                return true;
        }
        if (!Player.HasAura("Divine Spirit") && WoW.CanCast("Divine Spirit"))
        {
            Console.WriteLine("Casting Divine Spirit");
            if (WoW.Cast("Divine Spirit"))
                return true;
        }
        if (!Player.HasAura("Shadow Protection") && WoW.CanCast("Shadow Protection"))
        {
            Console.WriteLine("Casting Shadow Protection");
            if (WoW.Cast("Shadow Protection"))
                return true;
        }
        if (!Player.HasAura("Inner Fire") && WoW.CanCast("Inner Fire"))
        {
            Console.WriteLine("Casting Inner Fire");
            if (WoW.Cast("Inner Fire"))
                return true;
        }

        if (Target.IsDead || Target.IsGhost) return false;


        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }
}