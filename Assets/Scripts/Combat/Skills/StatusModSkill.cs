using UnityEngine;

public class StatusModSkill : Skill
{
    [Header("Atack Mod")]
    [Range(0f, 1.5f)]
    public float atkAm = 1f;

    [Header("Defense Mod")]
    [Range(0f, 1.5f)]
    public float defAm = 1f;

    [Header("Speed Mod")]
    [Range(0f, 1.5f)]
    public float spdAm = 1f;

    [Header("Evasion Mod")]
    [Range(0f, 1.5f)]
    public float evAm = 1f;

    protected override void OnRun()
    {
        reciver.AddMod(ModificateStats(reciver.GetCurrentStats()));
    }

    public Stats ModificateStats(Stats stats)
    {
        Stats modedStats = stats;

        modedStats.attack *= atkAm;
        modedStats.defense *= defAm;
        modedStats.speed *= spdAm;
        modedStats.evasion *= evAm;

        return modedStats;
    }
}
