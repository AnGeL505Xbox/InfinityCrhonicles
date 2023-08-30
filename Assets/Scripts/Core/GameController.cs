using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Globals currentGame = new();

    public string path = "Data";
    public string filename = "Save";

    [Header("Player")]
    public List<CardFigther> personalFigtherCards;
    public List<CardSpirit> personalSpiritCards;

    [Header("Monedas")]
    public int goldCoins;
    public int stellarCoins;

    [Header("Cartas")]
    public List<CardFigther> totalFig; //21
    public List<CardSpirit> totalSpirit; //24

    public List<CardFigther> totalEventFig; //6
    public List<CardSpirit> totalEventSpirit; //16

    //total peleador 27
    //total espiritu 40


    private void Awake()
    {
        var dataFound = SaveLoad.LoadData<Globals>("Data", "Save");
        if (dataFound != null)
        {
            currentGame = dataFound;
        }
        else
        {
            currentGame = new Globals();

            #region Inicializacion de Cartas
            currentGame.tFigtherCard = totalFig;
            currentGame.tSpiritCard = totalSpirit;

            currentGame.tEventFig = totalEventFig;
            currentGame.tEventSpirit = totalEventSpirit;

            currentGame.totalFigthers = totalFig;
            currentGame.totalFigthers.AddRange(totalEventFig);

            currentGame.totalSpirits = totalSpirit;
            currentGame.totalSpirits.AddRange(totalEventSpirit);

            #endregion

            if (currentGame.tutorial == TutorialStatus.START)
            {
                currentGame.playerFigtherCards = personalFigtherCards;
                currentGame.currentPlayerFigther = personalFigtherCards[0];
                currentGame.playerSpiritCards = personalSpiritCards;
                currentGame.goldCoin = goldCoins;
                currentGame.stellarCoin = stellarCoins;
                currentGame.stages.Add(0.1f);
                currentGame.chapters.Add(0);
            }

            SaveLoad.SaveData<Globals>(currentGame, "Data", "Save");
        }
    }
}
