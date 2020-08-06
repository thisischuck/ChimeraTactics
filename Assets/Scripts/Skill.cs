using UnityEngine;

public class Skill
{
    private int cooldown;
    private bool isUsed;
    private string name;
    private int range;
    private int damage;

    public Skill(string skillName, int skillCooldown, int skillRange, int skillDmg)
    {
        name = skillName;
        cooldown = skillCooldown;
        range = skillRange;
        damage = skillDmg;
        isUsed = false;
    }

    #region Public Acess
    public int Cooldown
    {
        get { return cooldown; }
    }

    public int Range
    {
        get { return range; }
    }

    public string Name
    {
        get { return name; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public bool SkillUsed
    {
        get { return isUsed; }
        set { isUsed = value; }
    }
    #endregion
}