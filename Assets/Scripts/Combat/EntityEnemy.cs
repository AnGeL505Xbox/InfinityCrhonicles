using System.Collections;
using UnityEngine;

public class EntityEnemy : Entity
{
    private void Awake()
    {
        SetStats(figther.GetStats());
    }

    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }

    IEnumerator IA()
    {

        yield return new WaitForSeconds(1f);

        Skill currentSkill;

        int r = Random.Range(0, 100);

        if (cooldownTurn != 0)
        {
            currentSkill = skills[Random.Range(0, skills.Count)];
        }
        else
        {
            if (r <= 75)
            {
                currentSkill = skills[Random.Range(0, skills.Count)];
            }
            else
            {
                currentSkill = ultimateSkill;
            }
        }

        currentSkill.SetEmmiterAndReciver(this, combat_manager.GetOpposingEntity());

        combat_manager.OnFigtherSkill(currentSkill);
    }
}
