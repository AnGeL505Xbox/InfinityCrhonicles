public class Stats
{
    public string idName;
    public string description;

    public float maxHealth;
    public float health;
    public float attack;
    public float defense;
    public float speed;
    public float evasion;

    public ElementType type_main;
    public ElementType type_second;
    public ElementMix typeMixed;
    public Rarity rarity;

    public SingularSkillTag singularSkill;

    public Stats(string idName, string description, float maxHealth, float health, float attack, float defense, float speed, float evasion, ElementType type_main, ElementType type_second, Rarity rarity, SingularSkillTag hiddenSkill)
    {
        this.idName = idName;
        this.description = description;
        this.maxHealth = maxHealth;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.evasion = evasion;
        this.type_main = type_main;
        this.type_second = type_second;
        this.rarity = rarity;
        this.singularSkill = hiddenSkill;
        

        this.typeMixed = mixedElement(type_main, type_second);
    }

    public ElementMix mixedElement(ElementType t1, ElementType t2)
    {
        ElementMix elemix = ElementMix.NULL;
        if (t2 == ElementType.NORMAL) return elemix;
        else
        {
            if ((t1 == ElementType.LUMEN && t2 == ElementType.UMBRA) || (t1 == ElementType.UMBRA && t2 == ElementType.LUMEN)) elemix = ElementMix.LUMBRA;
            else if ((t1 == ElementType.IGNIS && t2 == ElementType.TERRA) || (t1 == ElementType.TERRA && t2 == ElementType.IGNIS)) elemix = ElementMix.IGNTERRA;
            else if ((t1 == ElementType.AQUA && t2 == ElementType.CRYO) || (t1 == ElementType.CRYO && t2 == ElementType.AQUA)) elemix = ElementMix.AQYO;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.TERRA) || (t1 == ElementType.TERRA && t2 == ElementType.VORTEX)) elemix = ElementMix.VORTERRA;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.CRYO) || (t1 == ElementType.CRYO && t2 == ElementType.VORTEX)) elemix = ElementMix.CRYEX;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.UMBRA) || (t1 == ElementType.UMBRA && t2 == ElementType.VORTEX)) elemix = ElementMix.UMBREX;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.LUMEN) || (t1 == ElementType.LUMEN && t2 == ElementType.VORTEX)) elemix = ElementMix.LUMREX;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.AQUA) || (t1 == ElementType.AQUA && t2 == ElementType.VORTEX)) elemix = ElementMix.VORQUA;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.NATURA) || (t1 == ElementType.NATURA && t2 == ElementType.VORTEX)) elemix = ElementMix.NATUREX;
            else if ((t1 == ElementType.VORTEX && t2 == ElementType.PSYCHIC) || (t1 == ElementType.PSYCHIC && t2 == ElementType.VORTEX)) elemix = ElementMix.VORESIS;
            else elemix = ElementMix.UNKNOW;
            return elemix;
        }
    }
}
