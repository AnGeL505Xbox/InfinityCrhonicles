using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public string id_name;
    public StatusPanel status_panel;

    public CombatManager combat_manager;

    public CardFigther figther;

    public List<StatusMod> statusModifier;

    protected Stats stats;
    protected List<Skill> skills;
    protected Skill ultimateSkill;
    protected int cooldownTurn;

    public bool isAlive
    {
        get => stats.health > 0;
    }

    protected virtual void Start()
    {
        status_panel.SetStats(figther.idName, stats);
        skills = figther.spirit.skills;
        ultimateSkill = figther.ultimateSkill;
    }

    public void ModifyHealth(float amount)
    {
        stats.health = Mathf.Clamp(stats.health + amount, 0f, stats.maxHealth);
        stats.health = Mathf.Round(stats.health);
        status_panel.SetHealth(stats.health, stats.maxHealth);
    }

    public void AddMod(Stats modifiedStats)
    {
        this.stats = modifiedStats;
    }

    public Stats GetCurrentStats()
    {
        return stats;
    }

    public Skill GetUltimateSkill()
    {
        return ultimateSkill;
    }

    public void SetStats(Stats newStats)
    {
        stats = newStats;
    }

    public void SetTurn(int turn)
    {
        int skillTurn = ultimateSkill.cooldown;
        cooldownTurn = skillTurn - turn;
    }


    public abstract void InitTurn();
}
