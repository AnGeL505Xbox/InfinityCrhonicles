using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryUi : MonoBehaviour
{
    [Header("Color desactivado")]
    //public ColorBlock disabled;
    [Header("Color activado")]
    //public ColorBlock active;
    [Header("Botones")]
    public Button[] btFigtherCards; //18
    public Text[] txFigtherCards;
    public Button[] btSpiritCards; //15
    public Text[] txSpiritCards;

    protected List<Button> btFigther = new();
    [Header("Paneles")]
    public GameObject figtherPanel;
    public GameObject spiritPanel;
    [Header("Descripcion")]
    public Text txDescription;

    private void Awake()
    {
        figtherPanel.SetActive(false);
        spiritPanel.SetActive(false);
    }

    public void GetCards()
    {
        int t = GameController.currentGame.tEventFig.Count + GameController.currentGame.tFigtherCard.Count;
        for (int i = 0; i < btFigtherCards.Length; i++)
        {
            for (int j = 0; j < GameController.currentGame.playerFigtherCards.Count; j++)
            {
                if (txFigtherCards[i].text != GameController.currentGame.playerFigtherCards[j].idName)
                {
                    //btFigtherCards[i].colors = disabled;
                    //btFigtherCards[i].gameObject.SetActive(false);
                    btFigtherCards[i].enabled = false;
                }
                else
                {
                    //btFigtherCards[i].colors = active;
                    //btFigtherCards[i].gameObject.SetActive(true);
                    btFigtherCards[i].enabled = true;
                    break;
                }
            }
        }
        for (int i = 0; i < btSpiritCards.Length; i++)
        {
            for (int j = 0; j < GameController.currentGame.playerSpiritCards.Count; j++)
            {
                if (txSpiritCards[i].text != GameController.currentGame.playerSpiritCards[j].idName)
                {
                    //btSpiritCards[i].gameObject.SetActive(false);
                    btSpiritCards[i].enabled = false;
                }
                else
                {
                    //btSpiritCards[i].gameObject.SetActive(true);
                    btSpiritCards[i].enabled = true;
                    break;
                }
            }
        }
    }

    public void SetPanel(bool isFigther)
    {
        List<CardFigther> t = GameController.currentGame.tFigtherCard;
        for (int i = 0; i < GameController.currentGame.tEventFig.Count; i++)
        {
            t.Add(GameController.currentGame.tEventFig[i]);
        }
        List<CardSpirit> tt = GameController.currentGame.tSpiritCard;
        for (int i = 0; i < GameController.currentGame.tEventSpirit.Count; i++)
        {
            tt.Add(GameController.currentGame.tEventSpirit[i]);
        }

        if (isFigther)
        {
            spiritPanel.SetActive(false);
            figtherPanel.SetActive(true);
        }
        else
        {
            figtherPanel.SetActive(false);
            spiritPanel.SetActive(true);
        }

        for (int i = 0; i < btFigtherCards.Length; i++)
        {
            txFigtherCards[i].text = t[i].idName;
        }
        for (int i = 0; i < btSpiritCards.Length; i++)
        {
            txSpiritCards[i].text = tt[i].idName;
        }
        GetCards();
    }

    public void SetFigtherDescription(int index)
    {
        CardFigther selectedCard = GameController.currentGame.tFigtherCard[index];
        GameController.currentGame.currentPlayerFigther = selectedCard;
        txDescription.text = GameController.currentGame.tFigtherCard[index].description;
    }

    public void SetSpiritDescription(int index)
    {
        CardSpirit selectedSpirit = GameController.currentGame.tSpiritCard[index];
        GameController.currentGame.currentPlayerFigther.spirit = selectedSpirit;
        txDescription.text = GameController.currentGame.tSpiritCard[index].description;
    }
}
