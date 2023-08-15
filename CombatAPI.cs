public class ShadowAPI
{
    public bool WriteLog(string Message, LogType Type);
    public bool LogGeneral(string Message);
    public bool LogDebug(string Message);
    public bool LogWarning(string Message);
    public bool LogError(string Message);
    public long Time; // get's current time in milliseconds.

    #region Misc

        // Returns the player's name.
        public string MyName;

        // Returns the player's realm name.
        public string MyRealm;

        // Return the last displayed message.
        public string LastMessage;

        // Check if a frame is active, and visible.
        // Can debug frames and get names with /fstack in game.
        public bool HasFrame(string Name, bool IsVisible = true);

        // Attempt to click on a frame by name.
        public static void ClickFrame(string Name);

        // Check if a key is pressed by int, Forms.Keys, or by name
        // ** Name is only for advanced users with access to supported keys. **
        public bool IsPressed(int Key);
        public bool IsPressed(Keys Key);
        public bool IsPressed(string Name);

    #endregion

    #region Item

        // Check if the player has an item in their bags.
        public bool HasItem(object NameOrID);

        // Get the number of selected items in the players bags.
        public int ItemCount(object NameOrID);

        // return the selected items cooldown in milliseconds.
        public long ItemCooldown(object NameOrID);

        // Check if the selected item is on cooldown.
        public bool ItemOnCooldown(object NameOrID);

        // Check if the selected slot is enchanted by a specific id.
        // Enchantment ID's here: https://wow.tools/dbc/?dbc=spellitemenchantment&build=2.5.4.43861#page=1
        public bool HasEnchantment(uItemSlot Slot, object NameOrID);

        // return selected item's enchantment duration in milliseconds
        public long EnchantRemaining(uItemSlot Slot);

        // check if item in selected slot is enchanted at all.
        public bool IsEnchanted(uItemSlot Slot);

    #endregion

    #region Spell

        // Has a variety of open calls to pull spell data from the db2's by id.
        public SpellInfo C__Spell;

        // Try to get the id of last successful spell cast from frame cache.
        public int LastCastID;

        // Try to get the name of last successful spell cast from frame cache.
        public string LastCastName;

        // Returns true if the spell is active in the players spellbook.
        public bool HasSpell(object NameOrID);

        // Returns the spell's cooldown in milliseconds
        public long SpellCooldown(object NameOrID);

        // returns true is spell is on cooldown
        public bool SpellOnCooldown(object NameOrID);

        // Get the school of the entered spell. 
        public uSchool SpellSchool(object NameOrID);

        // Get the dispel type of the entered spell. 
        public uDispelType SpellDispel(object NameOrID);

        // Get the spell mechanic of the entered spell. 
        public uSpellMechanic SpellMechanic(object NameOrID);

        // Get the what type of power the entered spell uses. 
        public uPowerType SpellPower(object NameOrID);

        // Get the base cost of entered spell.
        public int SpellCost(object NameOrID);

        // Check if selected spell is in range of entered unit.
        public bool SpellInRange(object NameOrID, wUnit Unit = null);

        // Check if a spell is castable using a collection of the above checks.
        public bool CanCast(object NameOrID, wUnit Unit = null, bool CheckCooldown = true, bool Casting = true);

    #endregion

    #region Actions

        // Will attempt to cast selected spell on selected unit, and click on selected units location if aoe is enabled.
        public bool Cast(string Name, GUID Guid, bool IsAoe = false);

        // Will attempt to cast selected spell on target unit, and click on their location.
        public bool Cast(string Name, Vector Position);

        // attempt to cast a spell by id
        public bool Cast(int SpellID);

        // attempt to cast a spell by name
        public bool Cast(string Name);

        // attempt to use an item by id
        public bool Use(int ItemID);

        // attempt to use an item by name
        public bool Use(string Name);

        // attempt to use a macro by name
        // ** The macro MUST have a keybind set up first. **
        public bool Trigger(string Name);

        // attempt to use a game command by name
        // ** The command MUST have a keybind set up first. **
        public bool Perform(string Name);

    #endregion

    #region Globals

        // new guid accessor api, check below.
        public GuidAPI Guids = new();

        // Check if the player is currently in game.
        public bool IsInGame => WowGlobals.InGame;

        // Check if the player is reloading, or entering the world.
        public bool IsReloading => WowGlobals.Reloading;

    #endregion

    #region Object Manager

        // Returns a wUnit instance of selected unit.
        public wUnit Me;
        public wUnit Pet;
        public wUnit Focus;
        public wUnit Target;
        public wUnit LastEnemy;
        public wUnit LastTarget;
        public wUnit LastFriend;

        // return a list of alll loaded, active totems.
        public List<wUnit> Totems;

        // return a list of alll loaded, active totems owned by the player.
        public List<wUnit> MyTotems;

        // Check if a totem is active by name. Defaults to totems only owned by the player.
        public bool IsActiveTotem(string Name, bool PlayerOnly = true);

        // get a list of all loaded, active npc units.
        public List<wUnit> Npcs;

        // get a list of all loaded, active npc units within x yards of the player.
        public List<wUnit> NpcsInRange(int MaxRange);

        // get a list of all loaded, active player units.
        public List<wUnit> Players;

        // get a list of all loaded, active player units within x yards of the player.
        public List<wUnit> PlayersInRange(int MaxRange);

        // Try to target a specific unit by guid.
        public bool TryTarget(GUID Guid);

        // Return amount of unit's targeting the player.
        public int TargetingMe();

        // Return amount of npc unit's targeting the player.
        public int UnitsTargetingMe();

        // Return amount of player unit's targeting the player.
        public int PlayersTargetingMe();

        // Return amount of unit's in combat, and targeting the player.
        public int FightingMe();

        // Return amount of npc unit's in combat, and targeting the player.
        public int UnitsFightingMe();

        // Return amount of player unit's in combat, and targeting the player.
        public int PlayersFightingMe();

        // Return amount of hostile unit's near the player. Can filter by units in comat, and/or targeting the player.
        // ** Neutral units are considered 'hostile' **
        public int HostilesNearby(int Distance = 100, bool InCombat = false, bool TargetingMe = false);

        // Return amount of friendly unit's near the player. Can filter by units in comat, and/or targeting the player.
        public int FriendsNearby(int Distance = 100, bool InCombat = false, bool TargetingMe = false);

        // return the 2d distance between two units.
        public double Distance2D(wUnit Unit1, wUnit Unit2);

        // return the 3d distance between two units.
        public double Distance3D(wUnit Unit1, wUnit Unit2);

        // Check if the entered object is not null, and is a valid unit, object or collection.
        public bool IsValid(object obj);

        #endregion

    #region Command Wrapping

        // New targeting api, refer to 'TargetingAPI' class below.
        public TargetingAPI TargetUnit;

        // All of the below are just wrappers around .Perform("COMMAND");
        public bool Dismount();
        public bool ExitVehicle();
        public bool StopAttack();
        public bool StopCasting();
        public bool StartAttack();
        public bool StartPetAttack();
        public bool ToggleAttack();
        public bool AssistTarget();
        public bool FollowTarget();
        public bool InteractMouse();
        public bool InteractTarget();
        public bool TargetSelf();
        public bool TargetPet();
        public bool TargetLastTarget();
        public bool TargetNearestEnemy();
        public bool TargetNearestFriendly();
        public bool TargetLastEnemy();
        public bool TargetLastFriend();

    #endregion
}

public class GuidAPI
{
    public GUID Mouseover;
    public GUID Myself;
    public GUID MyPet;

    public GUID LastTarget;
    public GUID LastFriend;
    public GUID LastEnemy;
    public GUID Target;
    public GUID Focus;
}

public class TargetingAPI
{
    public bool Myself;
    public bool MyPet;

    public bool Focus;
    public bool Nearest;
    public bool Mouseover;

    public bool Previous;
    public bool LastTarget;
    public bool LastHostile;

    public bool LastFriend;
    public bool NearestFriend ;
    public bool LastFriendPlayer ;
    public bool NearestFriendPlayer ;

    public bool LastEnemy ;
    public bool NearestEnemy ;
    public bool LastEnemyPlayer ;
    public bool NearestEnemyPlayer ;

    public bool Party1 ;
    public bool PartyPet1;

    public bool Party2 ;
    public bool PartyPet2 ;

    public bool Party3 ;
    public bool PartyPet3;

    public bool Party4;
    public bool PartyPet4;
}

public class SpellInfo
{
    public string Name(int SpellID);
    public string Subtext(int SpellID);
    public string Description(int SpellID);
    public uSchool School(int SpellID);

    public SpellCost Cost;
    public SpellRange Range;
    public SpellCategory Category;
}

public class SpellRange
{
    public string Name(int SpellID);
    public float[] EnemyRange(int SpellID);
    public float[] FriendRange(int SpellID);

    public float Radius1(int SpellID);
    public float Radius2(int SpellID);
}

public class SpellCost
{
    public int Base(int SpellID);
    public int PerLevel(int SpellID);
    public int PerSecond(int SpellID);
    public uPowerType Type(int SpellID);
}

public class SpellCategory
{
    public string Name(int SpellID);
    public int CategoryId(int SpellID);
    public uDispelType Dispel(int SpellID);
    public uSpellMechanic Mechanic(int SpellID);
}

public enum LogType
{
    None,
    General,
    Pathing,
    Whisper,
    Warning,
    Combat,
    Debug,
    Error
}