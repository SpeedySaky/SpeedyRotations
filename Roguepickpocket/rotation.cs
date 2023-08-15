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
        QuickDelay = 550;
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
        if (WoW.HostilesNearby(10, true, true) >= 2 && WoW.CanCast("Evasion"))
        {
            if (WoW.Cast("Evasion"))
            return true;
        }
        if (WoW.HostilesNearby(5, true, true) >= 2 && WoW.CanCast("Blade Flurry") && Energy >=25)
        {
           if (WoW.Cast("Blade Flurry"))
            return true;
        }


        if (Target.IsCasting && WoW.CanCast("Kick"))
        {
           if( WoW.Cast("Kick"))
            return true;
        }
        if (Target.IsCasting && WoW.CanCast("Gouge"))
        {
            if (WoW.Cast("Gouge"))
            return true;
        }
        if (WoW.CanCast("Adrenaline Rush") )
        {
            if (WoW.Cast("Adrenaline Rush"))
                return true;
        }
        if (WoW.CanCast("Slice and Dice") && !Player.HasAura("Slice and Dice") && Combo >= 2 && Energy >= 25)
        {
            if (WoW.Cast("Slice and Dice"))
            return true;
        }
		if (WoW.CanCast("Rupture") && !Target.HasAura("Rupture") && Combo >= 2 && Energy >= 25)
        {
            if (WoW.Cast("Rupture"))
            return true;
        }
		if (WoW.CanCast("Expose Armor") && !Target.HasAura("Expose Armor") && Combo >= 2 && Energy >= 25)
        {
            if (WoW.Cast("Expose Armor"))
            return true;
        }
        if(WoW.CanCast("Kidney Shot") && Energy >= 25 && Combo >= 1 && TargetHealth <= 25)
        {
            if(WoW.Cast("Kidney Shot"))
                return true;
        }
		if (WoW.CanCast("Sinister Strike") && Energy >= 40)
        {
            if (WoW.Cast("Sinister Strike"))
            return true;
        }
        if (WoW.CanCast("Riposte") && Energy >= 10)
        {
           if ( WoW.Cast("Riposte"))
                return true;
        }
        if (WoW.CanCast("Mutilate") && Energy >= 60)
        {
            if (WoW.Cast("Mutilate"))
                return true;
        }
        if (WoW.CanCast("Shiv") &&  Energy >= 60)
        {
            if(WoW.Cast("Shiv"))
                return true;
        }
        
        
        if (WoW.CanCast("Eviscerate") && Combo >= 3)
        {
            if(WoW.Cast("Eviscerate"))
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
        var TargetDistance = Target.Position.Distance2D(Player.Position);
		if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
        if (Player.IsDead || Player.IsGhost || Player.IsCasting) return false;
		if (Target.IsLootable ) return true;
       // Console.WriteLine(WoW.CanCast("Pick Pocket"));
        //Console.WriteLine(WoW.Cast("Pick Pocket"));
		if (WoW.CanCast("Stealth") && !Player.HasAura("Stealth") )
        {
            if (WoW.Cast("Stealth"))
               
            return true;
        }
        if  (TargetDistance <=10 && WoW.CanCast("Pick Pocket") && Player.HasAura("Stealth") ) 
        {
            if (WoW.Cast("Pick Pocket"))
				Console.WriteLine("Casting Pick Pocket");
				
			
            return true;            
        }
        if (!WoW.IsEnchanted(uItemSlot.MainHand) && WoW.HasItem("Instant Poison") ||!WoW.IsEnchanted(uItemSlot.MainHand) &&WoW.HasItem("Instant Poison II") ||!WoW.IsEnchanted(uItemSlot.MainHand) &&WoW.HasItem("Instant Poison III")||!WoW.IsEnchanted(uItemSlot.MainHand) &&WoW.HasItem("Instant Poison IV")||!WoW.IsEnchanted(uItemSlot.MainHand) &&WoW.HasItem("Instant Poison V") ||!WoW.IsEnchanted(uItemSlot.MainHand) &&WoW.HasItem("Instant Poison VI") ||!WoW.IsEnchanted(uItemSlot.MainHand) &&WoW.HasItem("Instant Poison VII")) 
        {
            if (WoW.Trigger("mainhand"))
                return true;
        }
        if (!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison") ||!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison II")||!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison III") ||!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison IV")||!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison V") ||!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison VI") ||!WoW.IsEnchanted(uItemSlot.OffHand) && WoW.HasItem("Instant Poison VII"))
        {
            if (WoW.Trigger("offhand"))
                return true;
        }
		
		     
        if (WoW.CanCast("Sprint") && TargetDistance >=50)
        {
            if (WoW.Cast("Sprint"))
                return true;
        }
      
		
		 if (WoW.CanCast("Sprint") && TargetDistance >=50)
        {
            if (WoW.Cast("Sprint"))
                return true;
        }
      
		
        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }
}