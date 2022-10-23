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
        QuickDelay = 100;
        SlowDelay = 400;
    }

    public override bool InCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Runic = Player.RunePowerPercent;
        if (Player.IsCasting || Player.IsChanneling) return false;
		string DaD = "Death and Decay";
        var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;
		var Blood = WoW.BloodRunes();
		var Unholy = WoW.UnholyRunes();		
		var Frost = WoW.FrostRunes();
		var Death = WoW.DeathRunes();
        var TargetHealth = Target.HealthPercent;

       if (!WoW.IsValid(WoW.Pet) || WoW.Pet.IsDead && WoW.CanCast("Raise Dead"))
        {
            Console.WriteLine("Casting Raise Dead");
            if (WoW.Cast("Raise Dead"))
                return true;
        }
        if (!WoW.Pet.Target.IsEmpty() && WoW.Pet.IsInCombat)
            if (!WoW.Pet.Target.Compare(WoW.Me.Target))
                if (WoW.Trigger("AssistPet"))
                    if (WoW.Me.Target.Compare(WoW.Pet.Target))
                        return true;
        
		
        if (WoW.CanCast("Death Pact") && WoW.IsValid(WoW.Pet) && Player.Health <= 10)
        {
            Console.WriteLine("Casting Death Pact");
            if (WoW.Cast("Death Pact"))
                return true;

        }
		if (WoW.CanCast("Icebound Fortitude") && Health <= 30 && Runic>=20)
        {
            Console.WriteLine("Casting Icebound Fortitude");
            if (WoW.Cast("Icebound Fortitude"))
                return true;

        }
		if ( WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Pestilence") )
        {
            if (WoW.Cast("Pestilence"))
                return true;
        
        }
		if (WoW.CanCast("Army of the Dead") && !Player.IsCasting && WoW.SpellOnCooldown("Army of the Dead") && WoW.HostilesNearby(10, true, true) >= 2)
        {
            if (WoW.Cast("Army of the Dead"))
                return true;
        }
        if ( WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Blood Boil"))
        {
            if (WoW.Cast("Blood Boil"))
                return true;
        
        }
        if (WoW.CanCast("Lifeblood") && Health <=50)
        {
            Console.WriteLine("Casting Lifeblood");
            if (WoW.Cast("Lifeblood"))
                return true;
        }
        if (WoW.CanCast("Summon Gargoyle") && !Player.IsCasting && Runic>=60 )
        {
            Console.WriteLine("Casting Summon Gargoyle");
            if (WoW.Cast("Summon Gargoyle"))
                return true;
        }
        if (WoW.CanCast("Bone Shield") && !Player.HasAura("Bone Shield"))
        {
            Console.WriteLine("Casting Bone Shield");
            if (WoW.Cast("Bone Shield"))
                return true;
        }
        if (WoW.CanCast("Blood Tap") && Player.HasAura("Blood Tap"))
        {
            Console.WriteLine("Casting Blood Tap");
            if (WoW.Cast("Blood Tap"))
                return true;
        }
        if (WoW.CanCast("Unbreakable Armor") && !Player.IsCasting && Player.HasAura("Unbreakable Armor"))
        {
            Console.WriteLine("Casting Unbreakable Armor");
            if (WoW.Cast("Unbreakable Armor"))
                return true;
        }if (WoW.CanCast("Ghoul Frenzy") && !WoW.Pet.HasAura("Ghoul Frenzy"))
        {
            Console.WriteLine("Casting Ghoul Frenzy");
            if (WoW.Cast("Ghoul Frenzy"))
                return true;
        }
        if (WoW.CanCast("Empower Rune Weapon") && !Player.IsCasting)
        {
            Console.WriteLine("Casting Empower Rune Weapon");
            if (WoW.Cast("Empower Rune Weapon"))
                return true;
        }
		
		if (WoW.CanCast("Death Strike") && Health <=60 && Unholy>=1 && Frost>=1 && Target.HasAura("Frost Fever")&& Target.HasAura("Blood Plague"))
        {
            Console.WriteLine("Casting Death Strike");
            if (WoW.Cast("Death Strike")) 
			
            return true;
        }
        if (WoW.CanCast("Strangulate") && Target.IsCasting || Target.IsChanneling)
        {
            Console.WriteLine("Casting Strangulate");
            if (WoW.Cast("Strangulate"))
                return true;
        }
        if (WoW.CanCast("Mind Freeze") && (Target.IsCasting || Target.IsChanneling))
        {
            Console.WriteLine("Casting Mind Freeze");
            if (WoW.Cast("Mind Freeze"))
                return true;
        }
		if (WoW.CanCast("Obliterate") && Target.HasAura("Frost Fever")&& Target.HasAura("Blood Plague")&& Unholy>=1 && Frost>=1  || Death>=1 && Frost>=1|| Death>=1 && Unholy>=1)
        {
            Console.WriteLine("Casting Obliterate");
            if (WoW.Cast("Obliterate"))
                return true;
        }
		if (WoW.CanCast("Death Coil") && Runic>60)
        {
            Console.WriteLine("Casting Death Coil");
            if (WoW.Cast("Death Coil"))
                return true;
        }
        if (WoW.CanCast("Icy Touch") && !Target.HasAura("Frost Fever") && Frost>=1 ||Death>=1  )
        {
            Console.WriteLine("Casting Icy Touch");
            if (WoW.Cast("Icy Touch"))
                return true;
        }
		
		if (WoW.CanCast("Plague Strike")&& !Target.HasAura("Blood Plague") && Unholy>=1 ||Death>=1)
        {
            Console.WriteLine("Casting Plague Strike");
            if (WoW.Cast("Plague Strike"))
            
                return true;
        }
        if (WoW.CanCast("Scourge Strike") && Health >=65 && !Target.HasAura("Blood Plague")&& Unholy>=1 && Frost>=1 ||Death>=1 && Unholy>=1||Death>=1 && Frost>=1)
        {
            Console.WriteLine("Casting Scourge Strike");
            if (WoW.Cast("Scourge Strike")) 
            return true;
        }
        
		if (WoW.CanCast("Blood Strike")  && Death>=1 )
        {
            Console.WriteLine("Casting Blood Strike");
            if (WoW.Cast("Blood Strike")) 
            return true;
        }              
        
         if (Player.IsCasting && !Player.IsChanneling)
        {
            if (WoW.CanCast(DaD))
            {
                if (WoW.SpellInRange(DaD))
                {
                    //For casting on top of player
                    if (WoW.Cast(DaD, Player.Position))
                    {
                        return true;
                    }
                   
                }
            }
        }
        





        return false;
    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Runic = Player.ManaPercent;
        var Target = WoW.Target;
        if (Player.IsDead || Player.IsGhost) return false;
        if (Player.IsInCombat || Player.IsMoving) return false;
        if (Player.IsCasting || Player.IsChanneling) return false;
        if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;

        if (!WoW.IsValid(WoW.Pet) || WoW.Pet.IsDead && WoW.CanCast("Raise Dead"))
        {
            Console.WriteLine("Casting Raise Dead");
            if (WoW.Cast("Raise Dead"))
                return true;
        }
		
        if (WoW.CanCast("Rune Tap") && Health <= 80)
        {
            Console.WriteLine("Casting Rune Tap");
            if (WoW.Cast("Rune Tap"))
                return true;
        }
        if (WoW.CanCast("Horn of Winter") && !Player.HasAura("Horn of Winter"))
        {
            Console.WriteLine("Casting Horn of Winter");
            if (WoW.Cast("Horn of Winter"))
                return true;
        }
        if (WoW.CanCast("Unholy Presence") && !Player.HasAura("Unholy Presence"))
        {
            Console.WriteLine("Casting Unholy Presence");
            if (WoW.Cast("Unholy Presence"))
                return true;
        }if (!Target.IsDead || !Target.IsGhost) return true;
        WoW.StartPetAttack();
		if (WoW.CanCast("Death Grip"))
        {
            Console.WriteLine("Casting Death Grip");
            if (WoW.Cast("Death Grip"))
                return true;
        }
        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }
}