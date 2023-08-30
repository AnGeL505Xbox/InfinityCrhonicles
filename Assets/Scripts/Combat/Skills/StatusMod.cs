using UnityEngine;

public class StatusMod : MonoBehaviour
{
    public StatusModType type;
    public float amount;

    public Stats Apply(Stats stats)
    {
        Stats modedStats = stats;

        switch (type)
        {
            case StatusModType.ATTACK_MOD:
                modedStats.attack += amount;
                break;
            case StatusModType.DEFENSE_MOD:
                modedStats.defense += amount;
                break;
            case StatusModType.SPEED_MOD:
                modedStats.speed += amount;
                break;
            case StatusModType.EVASION_MOD:
                modedStats.evasion += amount;
                break;
        }
        return modedStats;
    }
}