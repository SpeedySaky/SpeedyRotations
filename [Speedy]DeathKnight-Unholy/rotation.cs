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
        SlowDelay = 800;
    }

    public override bool InCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Runic = Player.ManaPercent;
        if (Player.IsCasting || Player.IsChanneling) return false;

        var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;

        var TargetHealth = Target.HealthPercent;

        var Target = WoW.Target;
        if (!WoW.Pet.Target.IsEmpty() && WoW.Pet.IsInCombat)
            if (!WoW.Pet.Target.Compare(WoW.Me.Target))
                if (WoW.Trigger("AssistPet"))
                    if (WoW.Me.Target.Compare(WoW.Pet.Target))
                        return true;
        WoW.StartAttack();
        WoW.StartPetAttack();
        if (WoW.CanCast("Death Pact") && WoW.IsValid(WoW.Pet) && Player.Health <= 10)
        {
            Console.WriteLine("Casting Death Pact");
            if (WoW.Cast("Death Pact"))
                return true;

        }
        if (WoW.CanCast("Icebound Fortitude") && Player.Health <= 30)
        {
            Console.WriteLine("Casting Icebound Fortitude");
            if (WoW.Cast("Icebound Fortitude"))
                return true;

        }
        if (WoW.CanCast("Summon Gargoyle") && !Player.IsCasting )
        {
            Console.WriteLine("Casting Summon Gargoyle");
            if (WoW.Cast("Summon Gargoyle"))
                return true;
        }
        if (WoW.CanCast("Bone Shield") && Player.HasAura("Bone Shield")
        {
            Console.WriteLine("Casting Bone Shield");
            if (WoW.Cast("Bone Shield"))
                return true;
        }
        if (WoW.CanCast("Blood Tap") && Player.HasAura("Blood Tap")
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
        }
        if (WoW.CanCast("Empower Rune Weapon") && !Player.IsCasting)
        {
            Console.WriteLine("Casting Empower Rune Weapon");
            if (WoW.Cast("Empower Rune Weapon"))
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

        if (WoW.CanCast("Icy Touch") && !Target.HasAura("Frost Fever"))
        {
            Console.WriteLine("Casting Icy Touch");
            if (WoW.Cast("Icy Touch"))
                return true;
        }
        if (WoW.CanCast("Scourge Strike") && !Target.HasAura("Blood Plague"))
        {
            Console.WriteLine("Casting Scourge Strike");
            if (WoW.Cast("Scourge Strike")) 
            return true;
        }
        if (WoW.CanCast("Plague Strike") && !Target.HasAura("Blood Plague"))
        {
            Console.WriteLine("Casting Plague Strike");
            if (WoW.Cast("Plague Strike"))
            
                return true;
        }
        if (WoW.CanCast("Ghoul Frenzy"))
        {
            Console.WriteLine("Casting Ghoul Frenzy");
            if (WoW.Cast("Ghoul Frenzy"))
                return true;
        }
        if (WoW.CanCast("Blood Strike") && !Target.HasAura("Desolation"))
        {
            Console.WriteLine("Casting Blood Strike");
            if (WoW.Cast("Blood Strike"))
                return true;
        }
        if (WoW.CanCast("Blood Strike"))
        {
            Console.WriteLine("Casting Blood Strike");
            if (WoW.Cast("Blood Strike"))
                return true;
        }
        if (WoW.CanCast("Death Coil") && Runic>60)
        {
            Console.WriteLine("Casting Death Coil");
            if (WoW.Cast("Death Coil"))
                return true;
        }
        if (WoW.CanCast("Death and Decay"))
        {
            Console.WriteLine("Casting Death and Decay");
            if (WoW.Cast("Death and Decay"))
                return true;

        }
        if (WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Pestilence"))
        {
            Console.WriteLine("Casting Pestilence");
            if (WoW.Cast("Pestilence"))
                return true;

        }
        if (WoW.CanCast("Army of the Dead") && !Player.IsCasting && WoW.SpellOnCooldown("Army of the Dead") WoW.HostilesNearby(10, true, true) >= 2)
        {
            Console.WriteLine("Casting Army of the Dead");
            if (WoW.Cast("Army of the Dead"))
                return true;
        }
        if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Blood Boil"))
        {
            Console.WriteLine("Casting Blood Boil");
            if (WoW.Cast("Blood Boil"))
                return true;

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
        }
        WoW.StartPetAttack();
        if (Target.IsDead || Target.IsGhost) return false;
        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }
}