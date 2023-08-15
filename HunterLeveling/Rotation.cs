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
		var Mana = Player.ManaPercent;
        if (Player.IsCasting || Player.IsChanneling) return false;

        var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;


        var Pet = WoW.Pet;
        var PetHealth = Pet.HealthPercent;
        var TargetHealth = Target.HealthPercent;
		var TargetDistance = Target.Position.Distance2D(Player.Position);

		if(!WoW.Pet.Target.IsEmpty() && WoW.Pet.IsInCombat)
		if (!WoW.Pet.Target.Compare(WoW.Me.Target))
					 if(WoW.Trigger("AssistPet"))
						if(WoW.Me.Target.Compare(WoW.Pet.Target))
							return true;
		
		WoW.StartPetAttack();

		// healing
		
		
			
		
		
		//ranged combat
		if (WoW.CanCast("Aspect of the Hawk") && TargetDistance>=5 && Player.HasAura("Aspect of the Hawk"))
			{
            if(WoW.Cast("Aspect of the Hawk"))
            return true;
			}
		if (WoW.CanCast("Rapid Fire") && TargetDistance>=5 )
			{
            if(WoW.Cast("Rapid Fire"))
            return true;
			}
		if (WoW.CanCast("Bestial Wrath") && !Player.IsCasting)
            {
                if(WoW.Cast("Bestial Wrath"))
                return true;
            }
		if (WoW.CanCast("Kill Command") && !Player.IsCasting)
            {
               if( WoW.Cast("Kill Command"))
                return true;
            }
		if (WoW.CanCast("Multi-Shot") && !Player.IsCasting && WoW.UnitsFightingMe() >= 2 && Mana>=30)
            {
                if(WoW.Cast("Multi-Shot"))
                return true;
            }
        if (WoW.CanCast("Serpent Sting ") && TargetDistance>=5 && !Target.HasAura("Serpent Sting") && TargetHealth>=25 && Mana>=30)
            {
            if(WoW.Cast("Serpent Sting"))
            return true;
            }
		if (WoW.CanCast("Hunter's Mark ") && TargetDistance>=5 && !Target.HasAura("Hunter's Mark"))
            {
            if(WoW.Cast("Hunter's Mark"))
            return true;
            }
		if (WoW.CanCast("Concussive Shot") && TargetDistance>=5 && Mana>=30 && !Target.HasAura("Concussive Shot"))
            {
                if(WoW.Cast("Concussive Shot"))
                return true;
            }
		if (WoW.CanCast("Steady Shot") && TargetDistance>=5 && TargetHealth >= 25 )
            {
                if(WoW.Cast("Steady Shot"))
                return true;
            }
		if (WoW.CanCast("Arcane Shot") && TargetDistance>=5 && TargetHealth >= 50 && Mana>=30)
            {
                if(WoW.Cast("Arcane Shot"))
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
        if (Player.IsInCombat || Player.IsMoving) return false;
        if (Player.IsCasting || Player.IsChanneling) return false;
        if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
		var Target = WoW.Target;
        if (Target.IsDead || Target.IsGhost) return false;

        var Pet = WoW.Pet;
        var PetHealth = Pet.HealthPercent;		
        var TargetHealth = Target.HealthPercent;
		var TargetDistance = Target.Position.Distance2D(Player.Position);

		if(!WoW.Pet.Target.IsEmpty() && WoW.Pet.IsInCombat)
		if (!WoW.Pet.Target.Compare(WoW.Me.Target))
					 if(WoW.Trigger("AssistPet"))
						if(WoW.Me.Target.Compare(WoW.Pet.Target))
							return true;
		
		//pet logic
			
        		 if (!Player.IsCasting && PetHealth <= 50 && !Pet.HasAura("Mend Pet"))
            {
                if(WoW.Cast("Mend Pet"))
                return true;
            }
		if (WoW.IsValid(WoW.Pet) || !WoW.Pet.IsDead && !Player.IsCasting && WoW.CanCast("Call Pet") && WoW.CanCast("Revive Pet"))
        {
            if (WoW.Cast("Call Pet"));
			else
			if (WoW.Cast("Revive Pet"))
                    return true;
        }
		


        return false;
    }

    public override bool WhileMounted()
    {
        return false;
    }
}