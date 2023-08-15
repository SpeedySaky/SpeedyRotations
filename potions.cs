


Minor Healing Potion
        Lesser Healing Potion
        Healing Potion
        Greater Healing Potion
        Superior Healing Potion
        Major Healing Potion
        Super Healing Potion



        Minor Mana Potion
        Lesser Mana Potion 
        Mana Potion
        Greater Mana Potion
        Superior Mana Potion
        Major Mana Potion
        Super Mana Potion



        Weak Troll's Blood Potion   Regeneration
        Elixir of Minor Fortitude   Health
        Elixir of Minor Agility     Lesser Agility
        Elixir of Wisdom            Lesser Intellect
        Strong Troll's Blood Potion Regeneration
        Elixir of Defense           Armor

     if (WoW.HasItem("Minor Healing Potion") && Health <= 75 && !WoW.ItemOnCooldown("Minor Healing Potion"))
        {
    if (WoW.Use("Minor Healing Potion")) ;
    return true;
}
if (WoW.HasItem("Lesser Healing Potion") && Health <= 65 && !WoW.ItemOnCooldown("Lesser Healing Potion"))
{
    if (WoW.Use("Lesser Healing Potion")) ;
    return true;
}
if (WoW.HasItem("Healing Potion") && Health <= 55 && !WoW.ItemOnCooldown("Healing Potion"))
{
    if (WoW.Use("Healing Potion")) ;
    return true;
}




if (WoW.HasItem("Small Barnacled Clam"))
    {
        if (WoW.Use("Small Barnacled Clam")) ;
    return true;
    }
    if (WoW.HasItem("Thick-shelled Clam"))
    {
        if (WoW.Use("Thick-shelled Clam")) ;
    return true;
}
    if (WoW.HasItem("Jaggal Clam"))
        {
        if (WoW.Use("Jaggal Clam")) ;
    return true;

}
    if (WoW.HasItem("Big-mouth Clam"))
    {
        if (WoW.Use("Big-mouth Clam")) ;
    return true;

}
    if (WoW.HasItem("Soft-shelled Clam"))
        {
        if (WoW.Use("Soft-shelled Clam")) ;
    return true;

}







if (WoW.UnitsFightingMe() >= 2 && WoW.CanCast("Thunder Clap"))
{
    if (Rage >= ClapTrigger && WoW.Cast("Thunder Clap"))
    {
        return true;
    }
}

if (WoW.HasItem("Elixir of Lion's Strength") && !Player.HasAura("Lion's Strength"))
{
    if (WoW.Use("Elixir of Lion's Strength")) ;
    return true;
}
if (WoW.HasItem("Weak Troll's Blood Potion") && !Player.HasAura("Regeneration") && !Player.HasAura("Lesser Armor"))
{
    if (WoW.Use("Weak Troll's Blood Potion")) ;
    return true;
}
if (WoW.HasItem("Elixir of Minor Defense") && !Player.HasAura("Regeneration") && !Player.HasAura("Lesser Armor"))
{
    if (WoW.Use("Elixir of Minor Defense")) ;
    return true;
}