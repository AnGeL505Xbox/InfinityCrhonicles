using UnityEngine;

public class EntityPlayer : Entity
{
    [Header("UI")]
    public CommandPanel skillPanel;

    private void Awake()
    {
        figther = GameController.currentGame.currentPlayerFigther;
        SetStats(figther.GetStats());
    }

    public override void InitTurn()
    {
        skillPanel.Show();

        for (int i = 0; i < skills.Count; i++)
        {
            skillPanel.ConfigureButtons(i, skills[i].skillName);
        }
        skillPanel.ConfigureUltimateButton(ultimateSkill.skillName);

        if (cooldownTurn == 0) skillPanel.btUltimateSkill.SetActive(true);
        else skillPanel.btUltimateSkill.SetActive(false);
    }

    public void ExecuteSkill(int index)
    {
        Skill currentSkill;
        skillPanel.Hide();

        if (index == 3)
        {
            currentSkill = ultimateSkill;
        }
        else
        {
            currentSkill = skills[index];
        }

        currentSkill.SetEmmiterAndReciver(this, combat_manager.GetOpposingEntity());

        combat_manager.OnFigtherSkill(currentSkill);
    }
}
