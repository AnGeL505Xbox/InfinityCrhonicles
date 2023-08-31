using UnityEngine;

public class HealthModSkill : Skill
{
    [Header("Health Mod")]
    public float damage;

    public HealthModType modType;

    protected float damageMultiplier = 1f;

    [Range(0f, 1f)]
    public float criticalChance = 0f;


    protected override void OnRun()
    {
        float amount = GetModification();

        #region Critical Chance

        float dice = Random.Range(0f, 1f);
        if (dice <= criticalChance)
        {
            amount *= 2f;
        }

        #endregion

        #region Singular Skills

        Stats reciverStats = reciver.GetCurrentStats();
        Stats emmiterStats = emmiter.GetCurrentStats();
        SingularSkillTag reciverSSkill = reciverStats.singularSkill;
        SingularSkillTag emmiterSSkill = emmiterStats.singularSkill;
        ElementType tempElementMain = emmiterStats.type_main;
        ElementType tempElementSecond = emmiterStats.type_second;

        if (reciverSSkill == SingularSkillTag.ABSORPTION && (elementType == tempElementMain || elementType == tempElementSecond) && amount < 0)
        {
            amount *= -0.25f;
        }
        else if (emmiterSSkill == SingularSkillTag.REPULSION && skillType == SkillType.ULTIMATE)
        {
            damageMultiplier *= 2f;
        }
        else if ((emmiterSSkill == SingularSkillTag.BLOODY_NIGTH && elementType == ElementType.UMBRA) || (emmiterSSkill == SingularSkillTag.STELLAR_FLASH && elementType == ElementType.LUMEN) || (emmiterSSkill == SingularSkillTag.TELEKINESIS && elementType == ElementType.PSYCHIC) || (emmiterSSkill == SingularSkillTag.ELEKTROKINESIS && elementType == ElementType.ELEKTRO) || (emmiterSSkill == SingularSkillTag.IGNIS_FATUUS && elementType == ElementType.IGNIS) || (emmiterSSkill == SingularSkillTag.BRINICLE && (elementType == ElementType.AQUA || elementType == ElementType.CRYO)) || (emmiterSSkill == SingularSkillTag.OBSIDIAN_SCALES && (elementType == ElementType.TERRA || elementType == ElementType.NORMAL)) || (emmiterSSkill == SingularSkillTag.ATOMIC_FUSION && elementType == ElementType.VORTEX))
        {
            damageMultiplier = Random.Range(1f, 2f);
        }
        else if (emmiterSSkill == SingularSkillTag.PHOTOSYNTHESIS || elementType == ElementType.NATURA)
        {
            if (amount < 0) emmiter.ModifyHealth(amount * -0.15f);
            else emmiter.ModifyHealth(amount * 0.15f);
            damageMultiplier = Random.Range(1f, 2f);
        }

        amount *= damageMultiplier;

        #endregion

        if (reciverStats.evasion > emmiterStats.speed)
        {
            int x = Random.Range(0, 4);
            int y = Random.Range(0, 2);
            if (x < y)
            {
                amount = 0;
                Debug.Log("Ataque evitado");
            }

            //Probabilidad de que funcione es de 25%
        }

        reciver.ModifyHealth(amount);
    }

    public float GetModification()
    {
        Stats emmiterStats = emmiter.GetCurrentStats();
        Stats reciverStats = reciver.GetCurrentStats();

        float modifier = AnalizeElement(reciverStats, elementType);
        switch (modType)
        {
            case HealthModType.STAT_BASE:
                //float rawDamage = ((0.75f * damage) * (emmiterStats.attack / reciverStats.defense)) * modifier;
                float rawDamage = damage * (emmiterStats.attack / reciverStats.defense) * modifier;
                return (rawDamage);

            case HealthModType.FIXED:
                return (damage * modifier);
            case HealthModType.PERCENTAGE:
                return (reciverStats.maxHealth * damage);
        }
        throw new System.InvalidOperationException("HealthModSkill::GetDamage. Unreachable!");
    }

    //Tabla Elemental v2.0
    public float AnalizeElement(Stats reciver, ElementType attackElement)
    {
        float multipier = 0f;

        if (reciver.type_second == ElementType.NORMAL)
        {
            if (attackElement == ElementType.VORTEX && reciver.type_main == ElementType.VORTEX) multipier = 0;
            else if (attackElement == reciver.type_main) multipier = 1;
            else
            {
                switch (reciver.type_main)
                {
                    case ElementType.NORMAL:
                        if (attackElement == ElementType.PSYCHIC) multipier = 2f;
                        else if (attackElement == ElementType.TERRA) multipier = 1.6f;
                        else if (attackElement == ElementType.VORTEX) multipier = 1.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.LUMEN:
                        if (attackElement == ElementType.NATURA) multipier = 2f;
                        else if (attackElement == ElementType.VORTEX) multipier = 1.6f;
                        else if (attackElement == ElementType.UMBRA) multipier = 1.3f;
                        else if (attackElement == ElementType.ELEKTRO) multipier = 0.6f;
                        else if (attackElement == ElementType.NORMAL) multipier = 0.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.UMBRA:
                        if (attackElement == ElementType.VORTEX || attackElement == ElementType.IGNIS) multipier = 1.6f;
                        else if (attackElement == ElementType.LUMEN || attackElement == ElementType.ELEKTRO) multipier = 1.3f;
                        else if (attackElement == ElementType.NORMAL) multipier = 0.3f;
                        else if (attackElement == ElementType.NATURA) multipier = 0f;
                        else multipier = 1f;
                        break;
                    case ElementType.IGNIS:
                        if (attackElement == ElementType.AQUA || attackElement == ElementType.VORTEX) multipier = 1.6f;
                        else if (attackElement == ElementType.UMBRA || attackElement == ElementType.CRYO || attackElement == ElementType.NATURA) multipier = 0.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.AQUA:
                        if (attackElement == ElementType.CRYO) multipier = 1.6f;
                        else if (attackElement == ElementType.ELEKTRO || attackElement == ElementType.NATURA) multipier = 1.3f;
                        else if (attackElement == ElementType.TERRA) multipier = 0.6f;
                        else if (attackElement == ElementType.IGNIS) multipier = 0.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.TERRA:
                        if (attackElement == ElementType.PSYCHIC) multipier = 2f;
                        else if (attackElement == ElementType.VORTEX) multipier = 1.6f;
                        else if (attackElement == ElementType.AQUA) multipier = 1.3f;
                        else if (attackElement == ElementType.NATURA) multipier = 0.6f;
                        else if (attackElement == ElementType.NORMAL) multipier = 0.3f;
                        else if (attackElement == ElementType.ELEKTRO) multipier = 0f;
                        else multipier = 1f;
                        break;
                    case ElementType.ELEKTRO:
                        if (attackElement == ElementType.LUMEN) multipier = 1.3f;
                        else if (attackElement == ElementType.UMBRA || attackElement == ElementType.AQUA || attackElement == ElementType.VORTEX) multipier = 0.6f;
                        else if (attackElement == ElementType.CRYO) multipier = 0.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.CRYO:
                        if (attackElement == ElementType.NATURA) multipier = 2f;
                        else if (attackElement == ElementType.VORTEX) multipier = 1.6f;
                        else if (attackElement == ElementType.UMBRA) multipier = 1.3f;
                        else if (attackElement == ElementType.ELEKTRO) multipier = 0.6f;
                        else if (attackElement == ElementType.NORMAL) multipier = 0.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.NATURA:
                        if (attackElement == ElementType.UMBRA || attackElement == ElementType.CRYO) multipier = 2f;
                        else if (attackElement == ElementType.IGNIS) multipier = 1.6f;
                        else if (attackElement == ElementType.TERRA) multipier = 1.3f;
                        else if (attackElement == ElementType.AQUA) multipier = 0.6f;
                        else if (attackElement == ElementType.LUMEN) multipier = 0f;
                        else multipier = 1f;
                        break;
                    case ElementType.VORTEX:
                        if (attackElement == ElementType.ELEKTRO || attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                        else if (attackElement == ElementType.NORMAL) multipier = 0.6f;
                        else if (attackElement == ElementType.LUMEN || attackElement == ElementType.UMBRA || attackElement == ElementType.IGNIS || attackElement == ElementType.TERRA) multipier = 0.3f;
                        else multipier = 1f;
                        break;
                    case ElementType.PSYCHIC:
                        if (attackElement == ElementType.VORTEX) multipier = 0.6f;
                        else if (attackElement == ElementType.NORMAL || attackElement == ElementType.TERRA) multipier = 0f;
                        else multipier = 1f;
                        break;
                }
            }
        }
        else
        {
            switch (reciver.typeMixed)
            {
                case ElementMix.LUMBRA:
                    if (attackElement == ElementType.VORTEX) multipier = 1.6f;
                    else if (attackElement == ElementType.IGNIS) multipier = 1.3f;
                    else if (attackElement == ElementType.NORMAL) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.IGNTERRA:
                    if (attackElement == ElementType.AQUA) multipier = 2f;
                    else if (attackElement == ElementType.VORTEX) multipier = 1.6f;
                    else if (attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                    else if (attackElement == ElementType.NORMAL || attackElement == ElementType.UMBRA) multipier = 0.6f;
                    else if (attackElement == ElementType.ELEKTRO || attackElement == ElementType.CRYO || attackElement == ElementType.NATURA) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.AQYO:
                    if (attackElement == ElementType.ELEKTRO) multipier = 1.6f;
                    else if (attackElement == ElementType.CRYO) multipier = 1.3f;
                    else if (attackElement == ElementType.AQUA || attackElement == ElementType.TERRA) multipier = 0.6f;
                    else if (attackElement == ElementType.IGNIS || attackElement == ElementType.NATURA) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.VORTERRA:
                    if (attackElement == ElementType.PSYCHIC) multipier = 1.6f;
                    else if (attackElement == ElementType.AQUA) multipier = 1.3f;
                    else if (attackElement == ElementType.NORMAL || attackElement == ElementType.LUMEN || attackElement == ElementType.UMBRA || attackElement == ElementType.IGNIS || attackElement == ElementType.ELEKTRO || attackElement == ElementType.NATURA) multipier = 0.6f;
                    else if (attackElement == ElementType.TERRA || attackElement == ElementType.VORTEX) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.CRYEX:
                    if (attackElement == ElementType.ELEKTRO) multipier = 1.6f;
                    else if (attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                    else if (attackElement == ElementType.LUMEN || attackElement == ElementType.UMBRA || attackElement == ElementType.IGNIS || attackElement == ElementType.AQUA) multipier = 0.6f;
                    else if (attackElement == ElementType.TERRA || attackElement == ElementType.NATURA || attackElement == ElementType.VORTEX) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.UMBREX:
                    if (attackElement == ElementType.ELEKTRO || attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                    else if (attackElement == ElementType.UMBRA || attackElement == ElementType.VORTEX) multipier = 0.6f;
                    else if (attackElement == ElementType.NORMAL || attackElement == ElementType.TERRA || attackElement == ElementType.NATURA) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.LUMREX:
                    if (attackElement == ElementType.NATURA) multipier = 2f;
                    if (attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                    else if (attackElement == ElementType.LUMEN || attackElement == ElementType.VORTEX) multipier = 0.6f;
                    else if (attackElement == ElementType.NORMAL || attackElement == ElementType.TERRA || attackElement == ElementType.IGNIS) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.VORQUA:
                    if (attackElement == ElementType.ELEKTRO || attackElement == ElementType.PSYCHIC || attackElement == ElementType.CRYO || attackElement == ElementType.NATURA) multipier = 1.3f;
                    else if (attackElement == ElementType.UMBRA || attackElement == ElementType.LUMEN || attackElement == ElementType.NORMAL) multipier = 0.6f;
                    else if (attackElement == ElementType.IGNIS || attackElement == ElementType.VORTEX) multipier = 0.3f;
                    else if (attackElement == ElementType.TERRA) multipier = 0f;
                    else multipier = 1;
                    break;
                case ElementMix.NATUREX:
                    if (attackElement == ElementType.UMBRA || attackElement == ElementType.CRYO) multipier = 1.6f;
                    else if (attackElement == ElementType.IGNIS || attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                    else if (attackElement == ElementType.AQUA || attackElement == ElementType.TERRA || attackElement == ElementType.NORMAL) multipier = 0.6f;
                    else if (attackElement == ElementType.LUMEN || attackElement == ElementType.VORTEX) multipier = 0.3f;
                    else multipier = 1;
                    break;
                case ElementMix.VORESIS:
                    if (attackElement == ElementType.ELEKTRO || attackElement == ElementType.PSYCHIC) multipier = 1.3f;
                    else if (attackElement == ElementType.UMBRA || attackElement == ElementType.LUMEN || attackElement == ElementType.IGNIS || attackElement == ElementType.VORTEX) multipier = 0.6f;
                    else if (attackElement == ElementType.NORMAL || attackElement == ElementType.TERRA) multipier = 0f;
                    else multipier = 1;
                    break;
                case ElementMix.UNKNOW:
                    multipier = 1;
                    break;
            }
        }

        if (multipier == 2.0) Debug.Log("Es super-efectivo");
        else if (multipier > 1.0 && multipier < 2.0) Debug.Log("Es efectivo");
        else if (multipier == 1.0) Debug.Log("Ataque Normal");
        else if (multipier > 0.0 && multipier < 1.0) Debug.Log("No es efectivo");
        else if (multipier == 0.0) Debug.Log("Es inmune");
        return multipier;
    }
}