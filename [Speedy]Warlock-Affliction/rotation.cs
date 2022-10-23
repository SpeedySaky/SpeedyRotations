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
        QuickDelay = 250;
        SlowDelay = 750;
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

        if (!WoW.Pet.Target.IsEmpty() && WoW.Pet.IsInCombat)
            if (!WoW.Pet.Target.Compare(WoW.Me.Target))
                if (WoW.Trigger("AssistPet"))
                    if (WoW.Me.Target.Compare(WoW.Pet.Target))
                        return true;

        WoW.StartPetAttack();

        // healing
        if (!WoW.IsValid(WoW.Pet) || WoW.Pet.IsDead && WoW.HasItem("Soul Shard") && WoW.Me.Level >= 10)
        {
            Console.WriteLine("Casting Summon Felguard");
            if (WoW.Cast("Summon Felguard")) ;
            else
                Console.WriteLine("Casting Summon Voidwalker");
            if (WoW.Cast("Summon Voidwalker"))
                return true;
        }
        if (WoW.HasItem("Fel Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Fel Healthstone"))
        {
            Console.WriteLine("Using Fel Healthstone");
            if (WoW.Use("Fel Healthstone"))
                return true;
        }
        if (WoW.HasItem("Demonic Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Demonic Healthstone"))
        {
            Console.WriteLine("Using Demonic Healthstone");
            if (WoW.Use("Demonic Healthstone"))
                return true;
        }
        if (WoW.HasItem("Master Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Master Healthstone"))
        {
            Console.WriteLine("Using Master Healthstone");
            if (WoW.Use("Master Healthstone"))
                return true;
        }
        if (WoW.HasItem("Major Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Major Healthstone"))
        {
            Console.WriteLine("Using Major Healthstone");
            if (WoW.Use("Major Healthstone"))
                return true;
        }
        if (WoW.HasItem("Greater Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Greater Healthstone"))
        {
            Console.WriteLine("Using Greater Healthstone");
            if (WoW.Use("Greater Healthstone"))
                return true;
        }
        if (WoW.HasItem("Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Healthstone"))
        {
            Console.WriteLine("Using Healthstone");
            if (WoW.Use("Healthstone"))
                return true;
        }
        if (WoW.HasItem("Minor Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Minor Healthstone"))
        {
            Console.WriteLine("Using Minor Healthstone");
            if (WoW.Use("Minor Healthstone"))
                return true;
        }
        if (WoW.HasItem("Lesser Healthstone") && Health <= 50 && !WoW.ItemOnCooldown("Lesser Healthstone"))
        {
            Console.WriteLine("Using Lesser Healthstone");
            if (WoW.Use("Lesser Healthstone"))
                return true;
        }
        //rotation
		if (WoW.CanCast("Life Tap") && Health > 80 && Mana < 80)
        {
            Console.WriteLine("Casting Life Tap");
            if (WoW.Cast("Life Tap"))
                return true;
        }
		if (WoW.CanCast("Haunt") && !Target.HasAura("Haunt"))
        {
            Console.WriteLine("Casting Haunt");
            if (WoW.Cast("Haunt"))
                return true;
        }
        if (WoW.CanCast("Curse of Agony") && !Player.IsCasting && !Target.HasAura("Curse of Agony") && TargetHealth >= 30 && Mana >= 10)
        {
            Console.WriteLine("Curse of Agony");
            if (WoW.Cast("Curse of Agony"))
                return true;
        }
        if (WoW.CanCast("Corruption") && !Player.IsCasting && !Target.HasAura("Corruption") && TargetHealth >= 30 && Mana >= 10)
        {
            Console.WriteLine("Corruption");
            if (WoW.Cast("Corruption"))
                return true;
        }

        if (WoW.CanCast("Unstable Affliction") && !Player.IsCasting && !Target.HasAura("Unstable Affliction") && WoW.CanCast("Immolate") && !Target.HasAura("Immolate") && TargetHealth >= 30 && Mana >= 10)
        {
            Console.WriteLine("Casting Unstable Affliction");
            if (WoW.Cast("Unstable Affliction"))
                return true;
        }
        if (!Target.HasAura("Unstable Affliction") && WoW.CanCast("Immolate") && !Target.HasAura("Immolate") && TargetHealth >= 30 && Mana >= 10)
        {
            Console.WriteLine("Casting Immolate");
            if (WoW.Cast("Immolate"))
                return true;
        }
        if (!Player.IsCasting && WoW.CanCast("Conflagrate") && Target.HasAura("Immolate") && TargetHealth >= 40 && Mana >= 10)
        {
            Console.WriteLine("Casting Conflagrate");
            if (WoW.Cast("Conflagrate"))
                return true;
        }


        if (WoW.CanCast("Drain Soul") && WoW.ItemCount("Soul Shard") <= 2 && TargetHealth <= 30)
        {
            Console.WriteLine("Casting Drain Soul");
            if (WoW.Cast("Drain Soul"))
                return true;

        }
        if (WoW.CanCast("Health Funnel") && !Player.IsCasting && PetHealth <= 50 && !Pet.HasAura("Health Funnel"))
        {
            Console.WriteLine("Casting Health Funnel");
            if (WoW.Cast("Health Funnel"))
                return true;
        }

        if (WoW.CanCast("Shadow Bolt") && !Player.IsCasting && Player.HasAura(17941) && Mana >= 30)
        {
            Console.WriteLine("Casting Shadow Bolt");
            if (WoW.Cast("Shadow Bolt"))
                return true;
        }

        if (WoW.CanCast("Drain Life") && !Player.IsCasting && Health <= 70 && !Player.IsCasting && Mana >= 10)
        {
            Console.WriteLine("Casting Drain Life");
            if (WoW.Cast("Drain Life"))
                return true;

        }
        if (WoW.CanCast("Soul Fire") && !Player.IsCasting && TargetHealth > 20 && WoW.HasItem("Soul Shard") && Mana >= 10 && WoW.ItemCount("Soul Shard") >= 2)
        {
            Console.WriteLine("Casting Soul Fire");
            if (WoW.Cast("Soul Fire"))
                return true;
        }
        if (WoW.CanCast("Shadow Bolt") && !Player.IsCasting && TargetHealth >= 70 && Mana >= 50 && WoW.Me.Level < 12)
        {
            Console.WriteLine("Casting Shadow Bolt");
            if (WoW.Cast("Shadow Bolt"))
                return true;
        }
        if (WoW.CanCast("Shoot") && !Player.IsCasting)
        {
            Console.WriteLine("Casting Casting Shoot");
            if (WoW.Cast("Shoot"))
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
        var Pet = WoW.Pet;
        var PetHealth = Pet.HealthPercent;
        var Target = WoW.Target;
        if (!WoW.Pet.Target.IsEmpty() && WoW.Pet.IsInCombat)
            if (!WoW.Pet.Target.Compare(WoW.Me.Target))
                if (WoW.Trigger("AssistPet"))
                    if (WoW.Me.Target.Compare(WoW.Pet.Target))
                        return true;



        if (WoW.CanCast("Life Tap") && Health > 50 && Mana < 30)
        {
            Console.WriteLine("Casting Life Tap");
            if (WoW.Cast("Life Tap"))
                return true;
        }
        if (WoW.CanCast("Fel Armor") && !Player.HasAura("Fel Armor") && !Player.HasAura("Demon Armor") && !Player.HasAura("Demon Skin"))
        {
            Console.WriteLine("Casting Fel Armor");
            if (WoW.Cast("Fel Armor"))
                return true;

        }
        if (WoW.CanCast("Demon Armor") && !Player.HasAura("Fel Armor") && !Player.HasAura("Demon Armor") && !Player.HasAura("Demon Skin"))
        {
            Console.WriteLine("Casting Demon Armor");
            if (WoW.Cast("Demon Armor"))

                return true;
        }
        if (WoW.CanCast("Demon Skin") && !Player.HasAura("Fel Armor") && !Player.HasAura("Demon Armor") && !Player.HasAura("Demon Skin"))
        {
            Console.WriteLine("Casting Demon Skin");
            if (WoW.Cast("Demon Skin"))

                return true;
        }



        if (WoW.CanCast("Health Funnel") && !Player.IsCasting && PetHealth <= 50 && !Pet.HasAura("Health Funnel"))
        {
            Console.WriteLine("Casting Health Funnel");
            if (WoW.Cast("Health Funnel"))
                return true;
        }


        if (!WoW.IsValid(WoW.Pet) || WoW.Pet.IsDead && WoW.HasItem("Soul Shard") && WoW.Me.Level >= 10)
        {
            Console.WriteLine("Casting Summon Felguard");
            if (WoW.Cast("Summon Felguard")) ;
            else
                Console.WriteLine("Casting Summon Voidwalker");
            if (WoW.Cast("Summon Voidwalker"))
                return true;
        }
        if (!WoW.IsValid(WoW.Pet) || WoW.Pet.IsDead && WoW.CanCast("Summon Imp") && WoW.Me.Level <= 10)
        {
            Console.WriteLine("Casting Summon Imp");
            if (WoW.Cast("Summon Imp"))
                return true;
        }

        if (WoW.CanCast("Create Healthstone") && !Player.IsCasting && WoW.HasItem("Soul Shard") && !WoW.HasItem("Minor Healthstone") && !WoW.HasItem("Lesser Healthstone") && !WoW.HasItem("Healthstone") && !WoW.HasItem("Greater Healthstone") && !WoW.HasItem("Major Healthstone") && !WoW.HasItem("Master Healthstone") && !WoW.HasItem("Demonic Healthstone") && !WoW.HasItem("Fel Healthstone"))
        {
            Console.WriteLine("Casting Create Healthstone");
            if (WoW.Cast("Create Healthstone"))
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

