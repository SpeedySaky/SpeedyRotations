﻿using System;
using System.Threading;
using Shadow_WoW;
using Shadow_WoW.Internal;
using Shadow_WoW.Managers;
using Shadow_WoW.Warcraft;


internal class Rotation : IRotation
{

    public override void Initialize()
    {
        QuickDelay = 350;
        SlowDelay = 750;
    }

    public override bool InCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Energy = Player.Energy;
        var Combo = Player.ComboPoints;
        if (Player.IsCasting || Player.IsChanneling) return false;
        var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;
        var TargetHealth = Target.HealthPercent;

        //healing



        //combat
        WoW.StartAttack();
		
		 if (WoW.CanCast("Blood Fury") && (WoW.HostilesNearby(5, true, true) >= 2))
        {
            Console.WriteLine("Blood Fury");
            if (WoW.Cast("Blood Fury"))
                return true;
        }		
        if (WoW.CanCast("Berserking"))
        {
            Console.WriteLine("Casting Berserking");
            if (WoW.Cast("Berserking"))
                return true;
        }
        if (WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Evasion"))
        {
            Console.WriteLine("Casting Evasion");
            if (WoW.Cast("Evasion"))
                return true;
        }
        if (WoW.HostilesNearby(5, true, true) >= 2 && WoW.CanCast("Blade Flurry") && Energy >= 25 && !Player.HasAura("Blade Flurry"))
        {
            Console.WriteLine("Casting Blade Flurry");
            if (WoW.Cast("Blade Flurry"))
                return true;
        }
        if (Target.IsCasting && WoW.CanCast("Kick"))
        {
            Console.WriteLine("Casting Kick");
            if (WoW.Cast("Kick"))
                return true;
        }
        if (Target.IsCasting && WoW.CanCast("Gouge"))
        {
            Console.WriteLine("Casting Gouge");
            if (WoW.Cast("Gouge"))
                return true;
        }
        if (WoW.CanCast("Adrenaline Rush"))
        {
            Console.WriteLine("Casting Adrenaline Rush");
            if (WoW.Cast("Adrenaline Rush"))
                return true;
        }
        if (WoW.CanCast("Slice and Dice") && !Player.HasAura("Slice and Dice") && Combo >= 2 && Energy >= 25)
        {
            Console.WriteLine("Casting Slice and Dice");
            if (WoW.Cast("Slice and Dice"))
                return true;
        } 
		
		if (WoW.CanCast("Rupture") && !Target.HasAura("Rupture") && Combo >= 2 && Energy >= 25 && TargetHealth>30)
        {
            Console.WriteLine("Casting Rupture");
            if (WoW.Cast("Rupture"))
                return true;
        }
        if (WoW.CanCast("Expose Armor") && !Target.HasAura("Expose Armor") && Combo >= 2 && Energy >= 25)
        {
            Console.WriteLine("Casting Expose Armor");
            if (WoW.Cast("Expose Armor"))
                return true;
        }
        if (WoW.CanCast("Kidney Shot") && Energy >= 25 && Combo >= 1 && TargetHealth <= 25)
        {
            Console.WriteLine("Casting Kidney Shot");
            if (WoW.Cast("Kidney Shot"))
                return true;
        }
		if (WoW.CanCast("Shiv") && Energy >= 60)
        {
            Console.WriteLine("Casting Shiv");
            if (WoW.Cast("Shiv"))
                return true;
        }
        if (WoW.CanCast("Sinister Strike") && Energy >= 40)
        {
            Console.WriteLine("Casting Sinister Strike");
            if (WoW.Cast("Sinister Strike"))
                return true;
        }
        if (WoW.CanCast("Riposte") && Energy >= 10)
        {
            Console.WriteLine("Casting Riposte");
            if (WoW.Cast("Riposte"))
                return true;
        }
        if (WoW.CanCast("Mutilate") && Energy >= 60)
        {
            Console.WriteLine("Casting Mutilate");
            if (WoW.Cast("Mutilate"))
                return true;
        }
      
        if (WoW.CanCast("Eviscerate") && Combo >= 3)
        {
            Console.WriteLine("Casting Eviscerate");
            if (WoW.Cast("Eviscerate"))
                return true;
        }




        return false;
    }

    public override bool OutOfCombat()
    {
        var Player = WoW.Me;
        var Health = Player.HealthPercent;
        var Energy = Player.Energy;
        var Combo = Player.ComboPoints;
        var Target = WoW.Target;
		if (Player.IsDead || Player.IsGhost) return false;
        if (Player.IsInCombat || Player.IsMoving) return false;
        if (Player.IsCasting || Player.IsChanneling) return false;
        if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
        var TargetDistance = Target.Position.Distance3D(Player.Position);
        

 if (WoW.HasItem("Instant Poison") && !WoW.HasEnchantment(uItemSlot.OffHand, "Instant"))
        {
            Console.WriteLine("Enchanting Offhand");
            if (WoW.Trigger("offpoison"))
                return true;
        }
   if (WoW.HasItem("Instant Poison") && !WoW.HasEnchantment(uItemSlot.MainHand, "Instant"))
        {
           Console.WriteLine("Enchanting Mainhand");
            if (WoW.Trigger("mainpoison"))
                return true;
        }

       

        if (WoW.CanCast("Sprint"))
        {
            Console.WriteLine("Casting Sprint");
            if (WoW.Cast("Sprint"))
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
