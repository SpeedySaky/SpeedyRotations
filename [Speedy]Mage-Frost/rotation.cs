using System;
using System.Threading;
using Shadow_WoW;
using Shadow_WoW.Internal;
using Shadow_WoW.Managers;
using Shadow_WoW.Warcraft;

internal class Rotation : IRotation
{


	//LogGeneral(Message: "Thank you for using this rotation. For any ideas or problems with it you can contact me on discord speedysaky#8556. Any donation is much appreciated by https://gogetfunding.com/?p=7628330");


	public override void Initialize()

	{
		QuickDelay = 150;
		SlowDelay = 550;
	}

	public override bool InCombat()
	{
		var Player = WoW.Me;
		var Mana = Player.ManaPercent;
		var Health = Player.HealthPercent;
		var Pet = WoW.Pet;
		var Target = WoW.Target;
		var TargetHealth = Target.HealthPercent;
		var TargetDistance = Target.Position.Distance2D(Player.Position);
		if (Player.IsCasting || Player.IsChanneling) return false;
		if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
		if (Target.IsDead || Target.IsGhost) return false;

		if (WoW.HasItem("Mana Emerald") && Mana <= 50 && !WoW.ItemOnCooldown("Mana Emerald"))
		{
			Console.WriteLine("Using Mana Emerald");
			if (WoW.Use("Mana Emerald"))
				return true;
		}
		if (WoW.HasItem("Mana Ruby") && Mana <= 50 && !WoW.ItemOnCooldown("Mana Ruby"))
		{
			Console.WriteLine("Using Mana Ruby");
			if (WoW.Use("Mana Ruby"))
				return true;
		}
		if (WoW.HasItem("Mana Citrine") && Mana <= 50 && !WoW.ItemOnCooldown("Mana Citrine"))
		{
			Console.WriteLine("Using Mana Citrine");
			if (WoW.Use("Mana Citrine"))
				return true;
		}
		if (WoW.HasItem("Mana Jade") && Mana <= 50 && !WoW.ItemOnCooldown("Mana Jade"))
		{
			Console.WriteLine("Using Mana Jade");
			if (WoW.Use("Mana Jade"))
				return true;
		}
		if (WoW.HasItem("Mana Agate") && Mana <= 50 && !WoW.ItemOnCooldown("Mana Agate"))
		{
			Console.WriteLine("Using Mana Agate");
			if (WoW.Use("Mana Agate"))
				return true;
		}

		if ((Target.IsCasting || Target.IsChanneling) && WoW.CanCast("Counterspell"))
		{
			Console.WriteLine("Casting Counterspell");
			if (WoW.Cast("Counterspell"))
				return true;
		}
		if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Frost Nova") && TargetDistance <= 10)
		{
			Console.WriteLine("Casting Frost Nova");
			if (WoW.Cast("Frost Nova"))
				return true;
		}
		if (Target.HasAura("Frost Nova") && WoW.CanCast("Ice Lance"))
		{
			Console.WriteLine("Casting Ice Lance");
			if (WoW.Cast("Ice Lance"))
				return true;
		}

		if (TargetDistance <= 8 && WoW.CanCast("Blink"))
		{
			Console.WriteLine("Casting Blink");
			if (WoW.Cast("Blink"))
				return true;
		}


		if (!WoW.IsValid(WoW.Pet) && WoW.CanCast("Summon Water Elemental"))
		{
			Console.WriteLine("Summoning Water Elemental");
			if (WoW.Cast("Summon Water Elemental"))
				return true;
		}

		if (!Player.HasAura("Ice Barrier") && WoW.CanCast("Ice Barrier"))
		{
			Console.WriteLine("Casting Ice Barrier");
			if (WoW.Cast("Ice Barrier"))
				return true;
		}

		if (!Player.HasAura("Ice Barrier") && !Player.HasAura("Mana Barrier") && WoW.CanCast("Mana Barrier") && Mana >= 10)
		{
			Console.WriteLine("Casting Mana Barrier");
			if (WoW.Cast("Mana Barrier"))
				return true;
		}

		if (WoW.CanCast("Cone of Cold") && (WoW.HostilesNearby(10, true, true) > 1) && TargetDistance <= 10 && Mana >= 10 && WoW.CanCast("Cone of Cold"))
		{
			Console.WriteLine("Casting Cone of Cold");
			if (WoW.Cast("Cone of Cold"))
				return true;
		}

		if (WoW.CanCast("Fire Blast") && TargetHealth <= 30)
		{
			Console.WriteLine("Casting Fire Blast");
			if (WoW.Cast("Fire Blast"))
				return true;
		}

		if (WoW.CanCast("Frostbolt") && Mana >= 10 && TargetHealth >= 20 && !Target.HasAura("Frostbolt"))
		{
			Console.WriteLine("Casting Frostbolt");
			if (WoW.Cast("Frostbolt"))
				return true;
		}
		if (WoW.CanCast("Fireball") && Mana >= 10 && TargetHealth >= 30)
		{
			Console.WriteLine("Casting Fireball");
			if (WoW.Cast("Fireball"))
				return true;
		}
		if (WoW.CanCast("Cold Snap"))
		{
			Console.WriteLine("Casting Cold Snap");
			if (WoW.Cast("Cold Snap"))
				return true;
		}
		if (WoW.CanCast("Shoot"))
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
		if (Player.IsDead || Player.IsGhost) return false;
		if (Player.IsInCombat || Player.IsMoving) return false;
		if (Player.IsCasting || Player.IsChanneling) return false;
		if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
		var Target = WoW.Target;



		if (WoW.CanCast("Conjure Food") && !WoW.HasItem("Conjured Croissant") && !WoW.HasItem("Conjured Cinnamon Roll") && !WoW.HasItem("Conjured Sweet Roll") && !WoW.HasItem("Conjured Sourdough") && !WoW.HasItem("Conjured Pumpernickel") && !WoW.HasItem("Conjured Rye") && !WoW.HasItem("Conjured Bread") &&
		!WoW.HasItem("Conjured Muffin"))
		{
			Console.WriteLine("Conjuring Food");
			if (WoW.Cast("Conjure Food"))
				return true;
		}

		if (!WoW.HasItem("Conjured Fresh Water") && !WoW.HasItem("Conjured Glacier Water") && !WoW.HasItem("Conjured Mountain Spring Water") && !WoW.HasItem("Conjured Crystal Water") && !WoW.HasItem("Conjured Sparkling Water") && !WoW.HasItem("Conjured Mineral Water") && !WoW.HasItem("Conjured Spring Water") && !WoW.HasItem("Conjured Purified Water") && !WoW.HasItem("Conjured Water"))
		{
			Console.WriteLine("Conjuring Water");
			if (WoW.Cast("Conjure Water"))
				return true;
		}


		if (WoW.CanCast("Frost Armor") && !Player.HasAura("Frost Armor"))
		{
			Console.WriteLine("Buffing Frost Armor");
			if (WoW.Cast("Frost Armor"))
				return true;
		}
		if (WoW.CanCast("Arcane Intellect") && !Player.HasAura("Arcane Intellect"))
		{
			Console.WriteLine("Buffind Arcane Intellect");
			if (WoW.Cast("Arcane Intellect"))
				return true;
		}
		if (WoW.CanCast("Evocation") && Mana < 15)
		{
			Console.WriteLine("Casting Evocation");
			if (WoW.Cast("Evocation"))
				return true;
		}
		if (WoW.CanCast("Conjure Mana Emerald") && !WoW.HasItem("Mana Emerald"))
		{
			Console.WriteLine("Conjuring Mana Emerald");
			if (WoW.Cast("Conjure Mana Emerald"))
				return true;
		}
		if (WoW.CanCast("Conjure Mana Ruby") && !WoW.HasItem("Mana Ruby") && !WoW.HasItem("Mana Emerald"))
		{
			Console.WriteLine("Conjuring Mana Ruby");
			if (WoW.Cast("Conjure Mana Ruby"))
				return true;
		}
		if (WoW.CanCast("Conjure Mana Citrine") && !WoW.HasItem("Mana Citrine") && !WoW.HasItem("Mana Ruby") && !WoW.HasItem("Mana Emerald"))
		{
			Console.WriteLine("Conjuring Mana Citrine");
			if (WoW.Cast("Conjure Mana Citrine"))
				return true;
		}
		if (WoW.CanCast("Conjure Mana Jade") && !WoW.HasItem("Mana Jade") && !WoW.HasItem("Mana Citrine") && !WoW.HasItem("Mana Ruby") && !WoW.HasItem("Mana Emerald"))
		{
			Console.WriteLine("Conjuring Mana Jade");
			if (WoW.Cast("Conjure Mana Jade"))
				return true;
		}

		if (WoW.CanCast("Conjure Mana Agate") && !WoW.HasItem("Mana Jade") && !WoW.HasItem("Mana Agate") && !WoW.HasItem("Mana Citrine") && !WoW.HasItem("Mana Ruby") && !WoW.HasItem("Mana Emerald"))
		{
			Console.WriteLine("Conjuring Mana Agate");
			if (WoW.Cast("Conjure Mana Agate"))
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