using System;
using System.Threading;
using Shadow_WoW;
using Shadow_WoW.Internal;
using Shadow_WoW.Managers;
using Shadow_WoW.Warcraft;
using System.Collections.Generic;

internal class Rotation : IRotation
{

    public override void Initialize()

    {
        QuickDelay = 150;
        SlowDelay = 550;
        Console.WriteLine("Spedysaky");
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
        var Mana = Player.ManaPercent;
        if (Player.IsCasting || Player.IsChanneling) return false;
        var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;
        var TargetHealth = Target.HealthPercent;
        

        if (WoW.CanCast("Retribution Aura") && !Player.HasAura("Retribution Aura") )
        {
            Console.WriteLine("Casting Retribution Aura");
            if (WoW.Cast("Retribution Aura")) ;
            else
                Console.WriteLine("Casting Devotion Aura");
            if (WoW.Cast("Devotion Aura"))

                return true;
        }

        //healing


        if (WoW.CanCast("Lay on Hands") && Health < 15  && !Player.HasAura("Forbearance"))
        {
            Console.WriteLine("Casting Lay on Hands");
            if (WoW.Cast("Lay on Hands"))
                return true;
        }


        if (WoW.CanCast("Blessing of Might") && !Player.HasAura("Blessing of Might") && !Player.HasAura("Hand of Protection") && !Player.HasAura("Divine Protection"))
        {
            Console.WriteLine("Casting Blessing of Might");
            if (WoW.Cast("Blessing of Might")) ;
            else
                Console.WriteLine("Casting Blessing of Wisdom");
            if (WoW.Cast("Blessing of Wisdom")) ;

            return true;
        }
        if (WoW.CanCast("Flash of Light") && Player.HasAura(59578) && Health < 75 || WoW.CanCast("Flash of Light")  && Player.HasAura(53489) && Health < 75)
        {
            Console.WriteLine("Casting Flash of Light");
            if (WoW.Cast("Flash of Light"))
                return true;
        }

        if (WoW.CanCast("Divine Protection") && Health < 45 && !Player.IsCasting && !Player.HasAura("Forbearance"))
        {
            Console.WriteLine("Casting Divine Protection");
            if (WoW.Cast("Divine Protection"))
                return true;
        }
        if (Player.HasAura("Divine Protection") && Health <= 50 && WoW.CanCast("Holy Light"))
        {
            Console.WriteLine("Casting Holy Light");
            if (WoW.Cast("Holy Light"))
                return true;
        }

        if (WoW.CanCast("Hand of Protection") && Health < 60 && !Player.IsCasting && !Player.HasAura("Forbearance"))
        {
            Console.WriteLine("Casting Hand of Protection");
            if (WoW.Cast("Hand of Protection"))
                return true;
        }
        if (Player.HasAura("Hand of Protection") && Health <= 65 && WoW.CanCast("Holy Light"))
        {
            Console.WriteLine("Casting Holy Light");
            if (WoW.Cast("Holy Light"))
                return true;
        }
        if (Health <= 50 && WoW.CanCast("Holy Light"))
        {
            Console.WriteLine("Casting Holy Light");
            if (WoW.Cast("Holy Light"))
                return true;
        }
        //DPS
        if (Player.HasAura(59578) && WoW.CanCast("Exorcism") && Health > 75 || Player.HasAura(53489) && WoW.CanCast("Exorcism") && Health > 75)
        {
            Console.WriteLine("Casting Exorcism");
            if (WoW.Cast("Exorcism"))
                return true;
        }
        if (WoW.CanCast("Seal of Command") && !Player.HasAura("Seal of Command"))
        {
            Console.WriteLine("Casting Seal of Command");
            if (WoW.Cast("Seal of Command")) ;

            else
                Console.WriteLine("Casting Seal of Righteousness");
            if (WoW.Cast("Seal of Righteousness"))

                return true;
        }
        
        if (WoW.CanCast("Judgement of Wisdom") && !Target.HasAura("Judgement of Wisdom"))
        {
            Console.WriteLine("Casting Judgement of Wisdom");
            if (WoW.Cast("Judgement of Wisdom")) ;
            else
                Console.WriteLine("Casting Judgement of Light");
            if (WoW.Cast("Judgement of Light"))
           return true;
        }
       
        if (WoW.CanCast("Hammer of Justice") && Target.IsCasting || Target.IsChanneling)
        {
            Console.WriteLine("Casting Hammer of Justice");
            if (WoW.Cast("Hammer of Justice"))
                return true;
        }
        if (WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Consecration"))
        {
            Console.WriteLine("Casting Consecration");
            if (WoW.Cast("Consecration"))
                return true;
        }
        if (WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Divine Storm"))
        {
            Console.WriteLine("Casting Divine Storm");
            if (WoW.Cast("Divine Storm"))
                return true;
        }
        if (WoW.Target.Type == uType.Undead && WoW.HostilesNearby(10, true, true) >= 2 || WoW.Target.Type == uType.Demon && WoW.HostilesNearby(10, true, true) >= 2)
        {
            Console.WriteLine("Casting Holy Wrath");
            if (WoW.Cast("Holy Wrath"))
                return true;
        }
        if (WoW.CanCast("Hammer of Wrath") && TargetHealth <= 20)
        {
            Console.WriteLine("Casting Hammer of Wrath");
            if (WoW.Cast("Hammer of Wrath"))
                return true;
        }
        if (WoW.CanCast("Crusader Strike") && Mana > 50)
        {
            Console.WriteLine("Casting Crusader Strike");
            if (WoW.Cast("Crusader Strike"))
                return true;
        }
        




        return false;
    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Mana = Player.ManaPercent;
        if (Player.IsDead || Player.IsGhost) return false;
        if (Player.IsInCombat ) return false;
        if (Player.IsCasting || Player.IsChanneling) return false;
        if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
		if (Player.IsMounted || Player.IsFlying) return false;

		var Target = WoW.Target;
        var auras = Player.Auras();
        foreach (var aura in auras)
        {
            if (Dispels.Contains(WoW.C__Spell.Name(aura.SpellID)) || WoW.SpellDispel(aura.SpellID) == uDispelType.Disease || WoW.SpellDispel(aura.SpellID) == uDispelType.Poison && WoW.CanCast("Purify"))

            {
                Console.WriteLine("Casting Purify");
                if (WoW.Cast("Purify"))
                    return true;
            }
        }
        if (WoW.CanCast("Blessing of Might") && !Player.HasAura("Blessing of Might"))
        {
            Console.WriteLine("Casting Blessing of Might");
            if (WoW.Cast("Blessing of Might"))
                return true;
        }
        if (WoW.CanCast("Gift of the Naaru") && Health <= 40)
        {
            Console.WriteLine("Casting Gift of the Naaru");
            if (WoW.Cast("Gift of the Naaru"))
                return true;
        }
        if (WoW.CanCast("Seal of Command") && !Player.HasAura("Seal of Command"))
        {
            Console.WriteLine("Casting Seal of Command");
            if (WoW.Cast("Seal of Command")) ;
            else
                Console.WriteLine("Casting Seal of Righteousness");
            if (WoW.Cast("Seal of Righteousness"))
                return true;
        }
       


        if (WoW.CanCast("Retribution Aura") && WoW.CanCast("Devotion Aura") && !Player.HasAura("Retribution Aura") && !Player.HasAura("Devotion Aura"))
        {
            Console.WriteLine("Casting Retribution Aura");
            if (WoW.Cast("Retribution Aura")) ;
            else
                Console.WriteLine("Casting Devotion Aura");
            if (WoW.Cast("Devotion Aura"))

                return true;
        }

        if (WoW.CanCast("Holy Light") && Health <= 40)
        {
            Console.WriteLine("Casting Holy Light");
            if (WoW.Cast("Holy Light"))
                return true;
        }
        if (Target.IsDead || Target.IsGhost) return false;

        return false;
    }

    public override bool WhileMounted()
    {
        var Player = WoW.Me;
        if (WoW.CanCast("Crusader Aura") && !Player.HasAura("Crusader Aura"))
        {
            if (WoW.Cast("Crusader Aura"))
                return true;
        }
        return false;
    }
}