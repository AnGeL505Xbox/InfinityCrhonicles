using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteUI : MonoBehaviour
{
    [Header("MainButtons")]
    public Button mainButton;
    public Button spiritButton;
    public Button specialButton;
    public Button eventButton;

    [Header("Texts")]
    public Text title;
    public Text description;
    public Text dropLog;

    [Header("Others")]
    public GameObject mainPanel;
    public Button dropperButton;

    protected int index;

    private void Awake()
    {
        mainPanel.SetActive(false);
    }

    public void ConfigurePanel(int index)
    {
        switch (index)
        {
            case 0:
                title.text = "Ruleta de peleador";
                description.text = "En esta ruleta podras obtener cartas de peleador";
                mainPanel.SetActive(true);
                break;
            case 1:
                title.text = "Ruleta de espiritu";
                description.text = "En esta ruleta podras obtener cartas de espiritu";
                mainPanel.SetActive(true);
                break;
            case 2:
                title.text = "Ruleta especial";
                description.text = "En esta ruleta podras obtener cartas de peleador y de espiritu muy raras";
                mainPanel.SetActive(true);
                break;
            case 3:
                title.text = "Ruleta de evento";
                description.text = "En esta ruleta podras obtener cartas exclusivas de evento";
                mainPanel.SetActive(true);
                break;
        }
        this.index = index;
    }

    public void SetGacha()
    {
        if (index == 2)
        {
            if (GameController.currentGame.stellarCoin >= 4)
            {
                FigtherAndSpiritGacha(index);
                GameController.currentGame.stellarCoin -= 5;
            }
            else dropLog.text = "Te hace falta mas monedas estelares";
        }
        else if (index == 3)
        {
            if (GameController.currentGame.stellarCoin >= 9)
            {
                FigtherAndSpiritGacha(index);
                GameController.currentGame.stellarCoin -= 10;
            }
            else dropLog.text = "Te hace falta mas monedas estelares";
        }
        else
        {
            if (GameController.currentGame.goldCoin > 1)
            {
                FigtherAndSpiritGacha(index);
                GameController.currentGame.goldCoin -= 2;
            }
            else
            {
                dropLog.text = "Te hace falta mas monedas de oro";
            }
        }
        SaveLoad.SaveData<Globals>(GameController.currentGame, "Data", "Save");
    }

    protected void FigtherAndSpiritGacha(int index)
    {
        float p = Random.Range(0f, 1f);
        float fs = Random.Range(0f, 1f);
        bool isNew = false;

        CardFigther tFig;
        CardSpirit tSpirit;

        switch (index)
        {
            case 0: //Peleador Comun
                List<CardFigther> tempFigtherCommon = new();
                List<CardFigther> tempFigtherUncommon = new();
                List<CardFigther> tempFigtherRare = new();

                for (int i = 0; i < GameController.currentGame.tFigtherCard.Count; i++)
                {
                    if (GameController.currentGame.tFigtherCard[i].rarity == Rarity.COMMON) tempFigtherCommon.Add(GameController.currentGame.tFigtherCard[i]);
                    else if (GameController.currentGame.tFigtherCard[i].rarity == Rarity.UNCOMMON) tempFigtherUncommon.Add(GameController.currentGame.tFigtherCard[i]);
                    else if (GameController.currentGame.tFigtherCard[i].rarity == Rarity.RARE) tempFigtherRare.Add(GameController.currentGame.tFigtherCard[i]);
                }

                if (p <= 0.55f) tFig = tempFigtherCommon[Random.Range(0, tempFigtherCommon.Count)];
                else if (p > 0.55f && p <= 0.9f) tFig = tempFigtherUncommon[Random.Range(0, tempFigtherUncommon.Count)];
                else tFig = tempFigtherRare[Random.Range(0, tempFigtherRare.Count)];

                for (int i = 0; i < GameController.currentGame.playerFigtherCards.Count; i++)
                {
                    if (tFig.idGlobal == GameController.currentGame.playerFigtherCards[i].idGlobal)
                    {
                        isNew = false;
                        break;
                    }
                    else isNew = true;
                }

                if (isNew)
                {
                    GameController.currentGame.playerFigtherCards.Add(tFig);
                    dropLog.text = $"Se añadio {tFig.idName}";
                }
                else
                {
                    GameController.currentGame.stellarFragment += 1;
                    dropLog.text = $"Se añadio un fragmento estelar";
                }
                break;
            case 1: //Espiritu Comun
                List<CardSpirit> tempSpiritCommon = new();
                List<CardSpirit> tempSpiritUncommon = new();
                List<CardSpirit> tempSpiritRare = new();

                for (int i = 0; i < GameController.currentGame.tSpiritCard.Count; i++)
                {
                    if (GameController.currentGame.tSpiritCard[i].rarity == Rarity.COMMON) tempSpiritCommon.Add(GameController.currentGame.tSpiritCard[i]);
                    else if (GameController.currentGame.tSpiritCard[i].rarity == Rarity.UNCOMMON) tempSpiritUncommon.Add(GameController.currentGame.tSpiritCard[i]);
                    else if (GameController.currentGame.tSpiritCard[i].rarity == Rarity.RARE) tempSpiritRare.Add(GameController.currentGame.tSpiritCard[i]);
                }

                if (p <= 0.55f) tSpirit = tempSpiritCommon[Random.Range(0, tempSpiritCommon.Count)];
                else if (p > 0.55f && p <= 0.9f) tSpirit = tempSpiritUncommon[Random.Range(0, tempSpiritUncommon.Count)];
                else tSpirit = tempSpiritRare[Random.Range(0, tempSpiritRare.Count)];

                for (int i = 0; i < GameController.currentGame.playerSpiritCards.Count; i++)
                {
                    if (tSpirit.idGlobal == GameController.currentGame.playerSpiritCards[i].idGlobal)
                    {
                        isNew = false;
                        break;
                    }
                    else isNew = true;
                }

                if (isNew)
                {
                    GameController.currentGame.playerSpiritCards.Add(tSpirit);
                    dropLog.text = $"Se añadio {tSpirit.idName}";
                }
                else
                {
                    GameController.currentGame.stellarFragment += 1;
                    dropLog.text = $"Se añadio un fragmento estelar";
                }
                break;
            case 2: //Especial
                List<CardFigther> tempFigtherCUR = new();
                List<CardFigther> tempFigtherUltraRare = new();
                List<CardFigther> tempFigtherLegendary = new();

                List<CardSpirit> tempSpiritCUR = new();
                List<CardSpirit> tempSpiritUltraRare = new();
                List<CardSpirit> tempSpiritLegendary = new();

                for (int i = 0; i < GameController.currentGame.tFigtherCard.Count; i++)
                {
                    if (GameController.currentGame.tFigtherCard[i].rarity == Rarity.COMMON || GameController.currentGame.tFigtherCard[i].rarity == Rarity.UNCOMMON || GameController.currentGame.tFigtherCard[i].rarity == Rarity.RARE) tempFigtherCUR.Add(GameController.currentGame.tFigtherCard[i]);
                    else if (GameController.currentGame.tFigtherCard[i].rarity == Rarity.ULTRARARE) tempFigtherUltraRare.Add(GameController.currentGame.tFigtherCard[i]);
                    else if (GameController.currentGame.tFigtherCard[i].rarity == Rarity.LEGENDARY) tempFigtherLegendary.Add(GameController.currentGame.tFigtherCard[i]);
                }
                for (int i = 0; i < GameController.currentGame.tSpiritCard.Count; i++)
                {
                    if (GameController.currentGame.tSpiritCard[i].rarity == Rarity.COMMON || GameController.currentGame.tSpiritCard[i].rarity == Rarity.UNCOMMON || GameController.currentGame.tSpiritCard[i].rarity == Rarity.RARE) tempSpiritCUR.Add(GameController.currentGame.tSpiritCard[i]);
                    else if (GameController.currentGame.tSpiritCard[i].rarity == Rarity.ULTRARARE) tempSpiritUltraRare.Add(GameController.currentGame.tSpiritCard[i]);
                    else if (GameController.currentGame.tSpiritCard[i].rarity == Rarity.LEGENDARY) tempSpiritLegendary.Add(GameController.currentGame.tSpiritCard[i]);
                }

                if (fs <= 0.55f)
                {
                    if (p <= 0.55f) tFig = tempFigtherCUR[Random.Range(0, tempFigtherCUR.Count)];
                    else if (p > 0.55f && p <= 0.9f) tFig = tempFigtherUltraRare[Random.Range(0, tempFigtherUltraRare.Count)];
                    else tFig = tempFigtherLegendary[Random.Range(0, tempFigtherLegendary.Count)];

                    for (int i = 0; i < GameController.currentGame.playerFigtherCards.Count; i++)
                    {
                        if (tFig.idGlobal == GameController.currentGame.playerFigtherCards[i].idGlobal)
                        {
                            isNew = false;
                            break;
                        }
                        else isNew = true;
                    }

                    if (isNew)
                    {
                        GameController.currentGame.playerFigtherCards.Add(tFig);
                        dropLog.text = $"Se añadio {tFig.idName}";
                    }
                    else
                    {
                        GameController.currentGame.stellarFragment += 1;
                        dropLog.text = $"Se añadio un fragmento estelar";
                    }
                }
                else
                {
                    if (p <= 0.55f) tSpirit = tempSpiritCUR[Random.Range(0, tempSpiritCUR.Count)];
                    else if (p > 0.55f && p <= 0.9f) tSpirit = tempSpiritUltraRare[Random.Range(0, tempSpiritUltraRare.Count)];
                    else tSpirit = tempSpiritLegendary[Random.Range(0, tempSpiritLegendary.Count)];

                    for (int i = 0; i < GameController.currentGame.playerSpiritCards.Count; i++)
                    {
                        if (tSpirit.idGlobal == GameController.currentGame.playerSpiritCards[i].idGlobal)
                        {
                            isNew = false;
                            break;
                        }
                        else isNew = true;
                    }

                    if (isNew)
                    {
                        GameController.currentGame.playerSpiritCards.Add(tSpirit);
                        dropLog.text = $"Se añadio {tSpirit.idName}";
                    }
                    else
                    {
                        GameController.currentGame.stellarFragment += 1;
                        dropLog.text = $"Se añadio un fragmento estelar";
                    }
                }
                break;
            case 3: //Evento
                

                if (fs <= 0.45f)
                {
                    tFig = GameController.currentGame.tFigtherCard[Random.Range(0, GameController.currentGame.tFigtherCard.Count)];

                    for (int i = 0; i < GameController.currentGame.playerFigtherCards.Count; i++)
                    {
                        if (tFig.idGlobal == GameController.currentGame.playerFigtherCards[i].idGlobal)
                        {
                            isNew = false;
                            break;
                        }
                        else isNew = true;
                    }

                    if (isNew)
                    {
                        GameController.currentGame.playerFigtherCards.Add(tFig);
                        dropLog.text = $"Se añadio {tFig.idName}";
                    }
                    else
                    {
                        GameController.currentGame.stellarFragment += 1;
                        dropLog.text = $"Se añadio un fragmento estelar";
                    }
                }
                else
                {
                    tSpirit = GameController.currentGame.tSpiritCard[Random.Range(0, GameController.currentGame.tSpiritCard.Count)];

                    for (int i = 0; i < GameController.currentGame.playerSpiritCards.Count; i++)
                    {
                        if (tSpirit.idGlobal == GameController.currentGame.playerSpiritCards[i].idGlobal)
                        {
                            isNew = false;
                            break;
                        }
                        else isNew = true;
                    }

                    if (isNew)
                    {
                        GameController.currentGame.playerSpiritCards.Add(tSpirit);
                        dropLog.text = $"Se añadio {tSpirit.idName}";
                    }
                    else
                    {
                        GameController.currentGame.stellarFragment += 1;
                        dropLog.text = $"Se añadio un fragmento estelar";
                    }
                }
                break;
        }
    }
}