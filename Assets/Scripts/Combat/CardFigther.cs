using UnityEngine;

[System.Serializable]
public class CardFigther : MonoBehaviour
{

    //public Sprite image;
    public int idGlobal;
    public string idName;
    public string description;


    [Header("Stats")]
    public SingularSkillTag singularSkill;

    //Stats base
    public float baseHp;
    public float baseAtk;
    public float baseDef;
    public float baseSpd;
    public float baseEv;
    public ElementType baseTypeMain;
    public ElementType baseTypeSecond;

    //Stats modificables
    protected float maxHealth;
    protected float health;
    protected float attack;
    protected float defense;
    protected float speed;
    protected float evasion;
    protected ElementType typeMain;
    protected ElementType typeSecond;


    public Rarity rarity;
    protected Stats stats;

    public CardSpirit spirit;

    public Skill ultimateSkill;

    public Stats GetStats()
    {
        maxHealth = baseHp + spirit.addHealth;
        health = maxHealth;
        attack = (baseAtk + spirit.addAttack) * spirit.multiplerAttack;
        defense = (baseDef + spirit.addDefense) * spirit.multiplerDefense;
        speed = (baseSpd + spirit.addSpeed) * spirit.multiplerSpeed;
        evasion = (baseEv + spirit.addEvasion) * spirit.multiplerEvasion;

        typeMain = baseTypeMain;
        typeSecond = baseTypeSecond;

        SingularSkillModifier();

        stats = new Stats(idName, description, maxHealth, health, attack, defense, speed, evasion, typeMain, typeSecond, rarity, singularSkill);
        return stats;
    }

    public void SingularSkillModifier()
    {
        switch (singularSkill)
        {
            case SingularSkillTag.NULL:
                break;
            case SingularSkillTag.ADAPTATION:
                break;
            case SingularSkillTag.ABSORPTION:
                break;
            case SingularSkillTag.LIGTH_SPEED:
                speed *= 1.25f;
                evasion *= 1.25f;
                break;
            case SingularSkillTag.REPULSION:
                break;
            case SingularSkillTag.FIRMNESS:
                attack *= 1.5f;
                defense *= 1.5f;
                speed *= Random.Range(0.9f, 0.95f);
                evasion *= Random.Range(0.9f, 0.95f);
                break;
            case SingularSkillTag.BLOODY_NIGTH:
                attack *= Random.Range(1.25f, 1.5f);
                speed *= Random.Range(1.25f, 1.5f);
                break;
            case SingularSkillTag.IGNIS_FATUUS:
                attack *= Random.Range(1.1f, 1.2f);
                break;
            case SingularSkillTag.TELEKINESIS:
                evasion *= Random.Range(1.1f, 1.2f);
                break;
            case SingularSkillTag.ELEKTROKINESIS:
                speed *= Random.Range(1.15f, 1.25f);
                break;
            case SingularSkillTag.STELLAR_FLASH:
                defense *= Random.Range(1.25f, 1.5f);
                evasion *= Random.Range(1.25f, 1.5f);
                break;
            case SingularSkillTag.BRINICLE:
                attack *= Random.Range(1.35f, 1.55f);
                break;
            case SingularSkillTag.OBSIDIAN_SCALES:
                defense *= 1.25f;
                break;
            case SingularSkillTag.ATOMIC_FUSION:
                attack *= 1.25f;
                break;
            case SingularSkillTag.PHOTOSYNTHESIS:
                break;
        }
    }
}