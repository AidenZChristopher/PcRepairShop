using System.Collections.Generic;

/*============================================================
 * PartType.cs
 * Defines all part types in the game.
 * Add a new entry for each new part you create.
 *============================================================*/
public enum PartType
{
    None,
    CPU,
    PowerSupply,
    RAM,
    GPU,
    Case,
    CPUSlot,
    RAMSlot,
    GPUSlot,
    PowerSupplySlot,
}

public static class PartTypeHelper
{
    public static readonly PartType[] HiddenTypes = new PartType[]
    {
        PartType.CPUSlot,
        PartType.RAMSlot,
        PartType.GPUSlot,
        PartType.PowerSupplySlot,
    };

    public static readonly Dictionary<PartType, string> DisplayNames = new Dictionary<PartType, string>()
    {
        { PartType.CPU,          "CPU" },
        { PartType.PowerSupply,  "Power Supply" },
        { PartType.RAM,          "RAM" },
        { PartType.GPU,          "Graphics Card" },
        { PartType.Case,         "Case" },
        };

    public static bool IsHidden(PartType partType)
    {
        foreach (PartType hidden in HiddenTypes)
            if (hidden == partType) return true;
        return false;
    }

    public static string GetDisplayName(PartType partType)
    {
        if (DisplayNames.TryGetValue(partType, out string name)) return name;
        return partType.ToString();
    }
}