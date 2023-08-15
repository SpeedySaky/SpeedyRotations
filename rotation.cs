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
		QuickDelay = 250;
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
};


	public override bool InCombat()
	{

		var Player = WoW.Me;
		var Mana = Player.ManaPercent;
		var Health = Player.HealthPercent;

		if (Player.IsCasting || Player.IsChanneling) return false;

		var Target = WoW.Target;
		var TargetHealth = Target.HealthPercent;

		if (Player.IsCasting || Player.IsChanneling) return false;
		if (Target.IsDead || Target.IsGhost) return false;
		if (WoW.Target.IsDead || WoW.Target.IsGhost) return false;
		var TargetDistance = Target.Position.Distance2D(Player.Position);
		
		if (WoW.CanCast("Healing Wave") && Health <= 60 && Mana > 5)
		{
			Console.WriteLine("Casting Healing Wave");
			if (WoW.Cast("Healing Wave"))
				return true;
		}

		if (WoW.CanCast("Gift of the Naaru") && !Player.IsCasting && Health <= 50)
		{
			Console.WriteLine("Gift of the Naaru");
			if (WoW.Cast("Gift of the Naaru"))
				return true;
		}

		WoW.StartAttack();

		if (WoW.CanCast("Shamanistic Rage") && !Player.IsCasting && Health < 60)
		{
			Console.WriteLine("Casting Shamanistic Rage");
			if (WoW.Cast("Shamanistic Rage"))
				return true;
		}
		if (WoW.CanCast("Lightning Bolt") && !Player.IsCasting && Player.AuraStack(51528) == 5 || Player.AuraStack(51529) == 5 || Player.AuraStack(51530) == 5 || Player.AuraStack(51531) == 5 || Player.AuraStack(51532) == 5)
		{
			Console.WriteLine("Casting Lightning Bolt");
			if (WoW.Cast("Lightning Bolt"))
				return true;
		}
		if (WoW.CanCast("Earth Shock") && Target.IsCasting || Target.IsChanneling)
		{
			Console.WriteLine("Casting Earth Shock");
			if (WoW.Cast("Earth Shock"))
				return true;
		}

		if (WoW.CanCast("Feral Spirit"))
		{
			Console.WriteLine("Casting Feral Spirit");
			if (WoW.Cast("Feral Spirit"))
				return true;
		}
		if (WoW.CanCast("Wind Shear") && Target.IsCasting || Target.IsChanneling)
		{
			Console.WriteLine("Casting Wind Shear");
			if (WoW.Cast("Wind Shear"))
				return true;
		}

		if (!WoW.IsActiveTotem("Stoneskin Totem", false) && !WoW.IsActiveTotem("Stoneskin Totem II", false) && !WoW.IsActiveTotem("Stoneskin Totem III", false) && !WoW.IsActiveTotem("Stoneskin Totem IV", false) && !WoW.IsActiveTotem("Stoneskin Totem V", false) && !WoW.IsActiveTotem("Stoneskin Totem VI", false) && !WoW.IsActiveTotem("Stoneskin Totem VII", false) && !WoW.IsActiveTotem("Stoneskin Totem VIII", false) && !WoW.IsActiveTotem("Stoneskin Totem IX", false) && !WoW.IsActiveTotem("Stoneskin Totem X", false) && !WoW.IsActiveTotem("Stoneclaw Totem", false) && !WoW.IsActiveTotem("Stoneclaw Totem II", false) && !WoW.IsActiveTotem("Stoneclaw Totem III", false) && !WoW.IsActiveTotem("Stoneclaw Totem IV", false) && !WoW.IsActiveTotem("Stoneclaw Totem V", false) && !WoW.IsActiveTotem("Stoneclaw Totem VI", false) && !WoW.IsActiveTotem("Stoneclaw Totem VII", false) && !WoW.IsActiveTotem("Stoneclaw Totem VIII", false) && !WoW.IsActiveTotem("Stoneclaw Totem IX", false) && !WoW.IsActiveTotem("Stoneclaw Totem X", false) && WoW.CanCast("Stoneskin Totem"))
		{
			Console.WriteLine("Casting Stoneskin Totem");
			if (WoW.Cast("Stoneskin Totem"))
				return true;
		}
		if (!WoW.IsActiveTotem("Searing Totem", false) && !WoW.IsActiveTotem("Searing Totem II", false) && !WoW.IsActiveTotem("Searing Totem III", false) && !WoW.IsActiveTotem("Searing Totem IV", false) && !WoW.IsActiveTotem("Searing Totem V", false) && !WoW.IsActiveTotem("Searing Totem VI", false) && !WoW.IsActiveTotem("Searing Totem VII", false) && !WoW.IsActiveTotem("Searing Totem VIII", false) && !WoW.IsActiveTotem("Searing Totem IX", false) && !WoW.IsActiveTotem("Searing Totem X", false) && WoW.CanCast("Searing Totem"))
		{
			Console.WriteLine("Casting Searing Totem");
			if (WoW.Cast("Searing Totem"))
				return true;
		}
		if (!WoW.IsActiveTotem("Searing Totem", false) && !WoW.IsActiveTotem("Searing Totem II", false) && !WoW.IsActiveTotem("Searing Totem III", false) && !WoW.IsActiveTotem("Searing Totem IV", false) && !WoW.IsActiveTotem("Searing Totem V", false) && !WoW.IsActiveTotem("Searing Totem VI", false) && !WoW.IsActiveTotem("Searing Totem VII", false) && !WoW.IsActiveTotem("Searing Totem VIII", false) && !WoW.IsActiveTotem("Searing Totem IX", false) && !WoW.IsActiveTotem("Searing Totem X", false) && WoW.CanCast("Fire Nova"))
		{
			Console.WriteLine("Casting Fire Nova");
			if (WoW.Cast("Fire Nova"))
				return true;
		}

		if (WoW.HostilesNearby(10, true, true) >= 2 && !WoW.IsActiveTotem("Stoneclaw Totem", false) && !WoW.IsActiveTotem("Stoneclaw Totem II", false) && !WoW.IsActiveTotem("Stoneclaw Totem III", false) && !WoW.IsActiveTotem("Stoneclaw Totem IV", false) && !WoW.IsActiveTotem("Stoneclaw Totem V", false) && !WoW.IsActiveTotem("Stoneclaw Totem VI", false) && !WoW.IsActiveTotem("Stoneclaw Totem VII", false) && !WoW.IsActiveTotem("Stoneclaw Totem VIII", false) && !WoW.IsActiveTotem("Stoneclaw Totem IX", false) && !WoW.IsActiveTotem("Stoneclaw Totem X", false) && WoW.CanCast("Stoneclaw Totem"))
		{
			Console.WriteLine("Casting Stoneclaw Totem");
			if (WoW.Cast("Stoneclaw Totem"))
				return true;
		}

		if (WoW.CanCast("Flame Shock") && !Player.IsCasting && !Target.HasAura("Flame Shock") && TargetHealth >= 30)
		{
			Console.WriteLine("Casting Flame Shock");
			if (WoW.Cast("Flame Shock"))
				return true;
		}
		if (WoW.CanCast("Stormstrike") && !Player.IsCasting && !Target.HasAura("Stormstrike"))
		{
			Console.WriteLine("Casting Stormstrike");
			if (WoW.Cast("Stormstrike"))
				return true;
		}
		if (WoW.CanCast("Earth Shock") && !Player.IsCasting && Target.HasAura("Stormstrike") && TargetHealth >= 50)
		{
			Console.WriteLine("Casting Earth Shock");
			if (WoW.Cast("Earth Shock"))
				return true;
		}

		if (WoW.CanCast("Lightning Bolt") && !Player.IsCasting && TargetHealth >= 70 && Mana >= 80)
		{
			Console.WriteLine("Casting Lightning Bolt");
			if (WoW.Cast("Lightning Bolt"))
				return true;
		}
		if (WoW.CanCast("Lava Lash") && !Player.IsCasting)
		{
			Console.WriteLine("Casting Lava Lash");
			if (WoW.Cast("Lava Lash"))
				return true;
		}

		return false;


	}

	public override bool OutOfCombat()
	{

		var Player = WoW.Me;
		var Mana = Player.ManaPercent;
		var Health = Player.HealthPercent;
		var Target = WoW.Target;
		var TargetHealth = Target.HealthPercent;
		if (Player.IsCasting || Player.IsChanneling) return false;
		if (Player.IsDead || Player.IsGhost) return false;
		if (Player.IsInCombat) return false;
		if (Player.IsMounted || Player.IsFlying) return false;
		if (Player.HasAura("Drink") || Player.HasAura("Food")) return false;
		if (Player.IsCasting || Player.IsChanneling) return false;
		var auras = Player.Auras();
		foreach (var aura in auras)
		{
			if (Dispels.Contains(WoW.C__Spell.Name(aura.SpellID)) && WoW.CanCast("Cure Disease") || WoW.SpellDispel(aura.SpellID) == uDispelType.Disease && WoW.CanCast("Cure Toxins") || WoW.SpellDispel(aura.SpellID) == uDispelType.Poison && WoW.CanCast("Cure Toxins"))

			{
				Console.WriteLine("Casting Cure Toxins");
				if (WoW.Cast("Cure Toxins"))
					return true;
			}
		}
		if (WoW.CanCast("Healing Wave") && !Player.IsCasting && Health < 40)
		{
			Console.WriteLine("Casting Healing Wave");
			if (WoW.Cast("Healing Wave"))
				return true;
		}
		if (!Player.HasAura("Lightning Shield") && Mana > 10 && WoW.CanCast("Lightning Shield"))
		{
			Console.WriteLine("Casting Lightning Shield");
			if (WoW.Cast("Lightning Shield"))
				return true;
		}
		if (WoW.CanCast("Totemic Recall") && WoW.HasSpell("Totemic Recall") && WoW.IsActiveTotem("Stoneskin Totem", true) || WoW.IsActiveTotem("Searing Totem", true))
		{
			Console.WriteLine("Casting Totemic Recall");
		if (WoW.Cast("Totemic Recall"))
		return true;
		}

		if (!WoW.HasEnchantment(uItemSlot.MainHand, "Windfury") && WoW.Me.Level >= 30 && Mana > 10 && WoW.CanCast("Windfury Weapon"))
		{
			Console.WriteLine("Casting Windfury Weapon");
			if (WoW.Cast("Windfury Weapon"))
				return true;
		}
		if (!WoW.HasEnchantment(uItemSlot.OffHand, "Flametongue") && WoW.Me.Level >= 40 && Mana > 10 && WoW.CanCast("Flametongue Weapon"))
		{
			Console.WriteLine("Casting Flametongue Weapon");
			if (WoW.Cast("Flametongue Weapon"))
				return true;
		}
		if (!WoW.HasEnchantment(uItemSlot.MainHand, "Rockbiter") && Mana > 10 && WoW.CanCast("Rockbiter Weapon") && WoW.Me.Level < 30)
		{
			Console.WriteLine("Casting Rockbiter Weapon");
			if (WoW.Cast("Rockbiter Weapon"))
				return true;
		}
		if (Target.IsDead || Target.IsGhost) return false;
		if (WoW.CanCast("Ghost Wolf") && !Player.HasAura("Ghost Wolf") && Mana > 50)
		{
			Console.WriteLine("Casting Ghost Wolf");
			if (WoW.Cast("Ghost Wolf"))
				return true;
		}
	


		return false;

	}

	public override bool WhileMounted()
	{
		return false;
	}
}
