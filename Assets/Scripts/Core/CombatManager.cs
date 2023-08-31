using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    protected bool isCombatActive;
    protected CombatStatus combatStatus;
    protected Skill currentFighterSkill;
    protected List<Entity> entityGroup = new();
    protected int turn;
    protected int cooldownCount;
    protected bool isStartBattle;
    protected bool isFirstTurn;

    protected Stats pStats;
    protected Stats eStats;

    public Entity player;
    public Entity enemy;
    public Image imageColor;
    public Color color;

    public DificultyTag dificulty;
    public BattleTag battleTag;
    public List<float> nextStage;
    public List<int> newChapters = new();

    protected TutorialStatus ts;

    protected ElementType tempEl1;
    protected ElementType tempEl2;


    private void Start()
    {
        //Hidden Skill Activation
        if (player.figther.singularSkill == SingularSkillTag.ADAPTATION)
        {
            tempEl1 = player.figther.ultimateSkill.elementType;
            player.figther.ultimateSkill.AdaptationElement(enemy.figther.baseTypeMain);
        }
        else if (enemy.figther.singularSkill == SingularSkillTag.ADAPTATION)
        {
            tempEl2 = enemy.figther.ultimateSkill.elementType;
            enemy.figther.ultimateSkill.AdaptationElement(player.figther.baseTypeMain);
        }

        imageColor.color = color;
        ts = GameController.currentGame.tutorial;

        LogPanel.write("Battle Initiated!");

        player.combat_manager = this;
        enemy.combat_manager = this;

        combatStatus = CombatStatus.TURN;
        isCombatActive = true;
        isStartBattle = true;
        StartCoroutine(this.CombatLoop());
    }

    IEnumerator CombatLoop()
    {
        while (isCombatActive)
        {
            switch (combatStatus)
            {
                case CombatStatus.TURN:
                    //Inicializar Turnos
                    pStats = player.GetCurrentStats();
                    eStats = enemy.GetCurrentStats();
                    if (isStartBattle)
                    {
                        turn = 0;
                        cooldownCount = 0;

                        if (pStats.speed > eStats.speed)
                        {
                            entityGroup.Add(player);
                            entityGroup.Add(enemy);
                        }
                        else
                        {
                            entityGroup.Add(enemy);
                            entityGroup.Add(player);
                        }
                        isFirstTurn = true;
                    }
                    else
                    {
                        if (turn >= 1)
                        {
                            turn = 0;
                            isFirstTurn = true;
                        }
                        else
                        {
                            turn = 1;
                            isFirstTurn = false;
                        }
                    }

                    if (isFirstTurn && !isStartBattle)
                    {
                        if (pStats.speed > eStats.speed)
                        {
                            entityGroup[0] = player;
                            entityGroup[1] = enemy;
                        }
                        else
                        {
                            entityGroup[0] = enemy;
                            entityGroup[1] = player;
                        }
                    }

                    yield return new WaitForSeconds(1f);

                    var currentTurn = entityGroup[turn];

                    if (isStartBattle) isStartBattle = false;

                    if (currentTurn.id_name == player.id_name && isStartBattle == false)
                    {
                        Skill temp = player.GetUltimateSkill();
                        if (cooldownCount == temp.cooldown) cooldownCount = temp.cooldown;
                        else cooldownCount += 1;
                    }

                    currentTurn.SetTurn(cooldownCount);

                    LogPanel.write($"{currentTurn.figther.idName} has the turn.");
                    currentTurn.InitTurn();

                    isStartBattle = false;
                    combatStatus = CombatStatus.WAITING_FOR_TURN;
                    break;
                case CombatStatus.WAITING_FOR_TURN:
                    yield return null;
                    break;
                case CombatStatus.ENTITY_ACTION:
                    LogPanel.write($"{entityGroup[turn].figther.idName} uses {currentFighterSkill.skillName}");

                    yield return null;

                    currentFighterSkill.Run();

                    if (currentFighterSkill.skillType == SkillType.ULTIMATE && entityGroup[turn].id_name == player.id_name) cooldownCount = 0;

                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);
                    combatStatus = CombatStatus.CHECK_VICTORY;

                    currentFighterSkill = null;
                    break;
                case CombatStatus.CHECK_VICTORY:
                    foreach (var fgtr in entityGroup)
                    {
                        if (fgtr.isAlive == false)
                        {
                            if (fgtr == player)
                            {
                                LogPanel.write("Derrota");
                                SavingData(false);
                            }
                            else
                            {
                                LogPanel.write("VICTORIA");
                                GameController.currentGame.goldCoin += Random.Range(1, 5);
                                SavingData(true);
                            }
                            isCombatActive = false;
                            yield return new WaitForSeconds(2.5f);
                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            combatStatus = CombatStatus.TURN;
                        }
                    }
                    break;
            }
        }
    }

    public Entity GetOpposingEntity()
    {
        if (turn == 0) return entityGroup[1];
        else return entityGroup[0];
    }

    public void OnFigtherSkill(Skill skill)
    {
        this.currentFighterSkill = skill;
        this.combatStatus = CombatStatus.ENTITY_ACTION;
    }

    public void SavingData(bool isPLayerAlive)
    {
        if (isPLayerAlive == true)
        {
            //Tutorial Code
            switch (battleTag)
            {
                case BattleTag.Tutorial1:
                    GameController.currentGame.tutorial = TutorialStatus.CARDINV;
                    break;
                case BattleTag.Tutorial2:
                    GameController.currentGame.tutorial = TutorialStatus.ROULETTE;
                    break;
                case BattleTag.TutorialFinal:
                    GameController.currentGame.tutorial = TutorialStatus.END;
                    for (int i = 0; i < newChapters.Count; i++) GameController.currentGame.chapters.Add(newChapters[i]);
                    break;
                case BattleTag.BattleNormal:
                    GameController.currentGame.tutorial = TutorialStatus.NULL;
                    break;
                case BattleTag.BattleBoss:
                    GameController.currentGame.tutorial = TutorialStatus.NULL;
                    for (int i = 0; i < newChapters.Count; i++) GameController.currentGame.chapters.Add(newChapters[i]);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < nextStage.Count; i++) GameController.currentGame.stages.Add(nextStage[i]);
        }

        //Deshacer los efectos de las habilidades ocultas
        if (player.figther.singularSkill == SingularSkillTag.ADAPTATION)
        {
            player.figther.ultimateSkill.elementType = tempEl1;
        }
        else if (enemy.figther.singularSkill == SingularSkillTag.ADAPTATION)
        {
            enemy.figther.ultimateSkill.elementType = tempEl2;
        }

        SaveLoad.SaveData<Globals>(GameController.currentGame, "Data", "Save");
    }
}