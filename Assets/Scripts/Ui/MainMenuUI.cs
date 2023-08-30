using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [Header("Game Manager")]
    public CardInventoryUi cardInventoryUi;

    [Header("UI")]
    public StoryPanel[] stories;
    public GameObject panelCardChest;
    public GameObject panelRoulette;
    public GameObject panelStory;
    public GameObject panelOption;
    public Button btCardPanel;
    public Button btRoulette;
    public Button btStoryPanel;
    public Text txGoldCoin;
    public Text txStellarCoin;
    public Text txOptionPath;
    public RectTransform storyP;

    [Header("CardUI")]
    public List<Button> btFigthers; //27
    public List<Button> btSpirits; //40
    //public List<Text> txFigthers;
    //public List<Text> txSpirits;

    public GameObject figtherPanel;
    public RectTransform figtherPanelRect;
    public GameObject spiritPanel;
    public RectTransform spiritPanelRect;

    public Text txDescription;

    private void Awake()
    {
        storyP.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        stories[0].Show();
        for (int x = 1; x < stories.Length; x++)
        {
            stories[x].Hide();
        }

        panelCardChest.SetActive(false);
        panelRoulette.SetActive(false);
        panelStory.SetActive(false);
        panelOption.SetActive(false);

        #region CardUI
        figtherPanel.SetActive(false);
        spiritPanel.SetActive(false);
        #endregion
    }
    private void Start()
    {
        if (GameController.currentGame.tutorial == TutorialStatus.START || GameController.currentGame.tutorial == TutorialStatus.STORY || GameController.currentGame.tutorial == TutorialStatus.BATTLE)
        {
            btCardPanel.enabled = false;
            btRoulette.enabled = false;
        }
        else if (GameController.currentGame.tutorial == TutorialStatus.ROULETTE || GameController.currentGame.tutorial == TutorialStatus.END || GameController.currentGame.tutorial == TutorialStatus.NULL)
        {
            btCardPanel.enabled = true;
            btRoulette.enabled = true;
        }
        else
        {
            btCardPanel.enabled = true;
            btRoulette.enabled = false;
        }

        SaveLoad.PathData("Data", "Save");
    }
    private void Update()
    {
        txGoldCoin.text = $"Moneda de oro: {GameController.currentGame.goldCoin}";
        txStellarCoin.text = $"Moneda estelar: {GameController.currentGame.stellarCoin}";
    }


    //Evento OnClick del boton Stories
    public void StoryController()
    {
        int x = 0;

        panelCardChest.SetActive(false);
        panelRoulette.SetActive(false);
        panelOption.SetActive(false);
        panelStory.SetActive(true);

        for (int i = 0; i < stories.Length; i++)
        {
            for (int j = 0; j < GameController.currentGame.chapters.Count; j++)
            {
                if (stories[i].idChapter == GameController.currentGame.chapters[j])
                { 
                    stories[i].Show();
                    break;
                }
            }
        }

        for (int i = 0; i < stories.Length; i++)
        {
            if (stories[i].isActiveAndEnabled == true) x += 1;
        }

        storyP.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 212.5f * x);
    }
    //Evento OnClick del boton Inventory Card
    public void CardPresetController()
    {
        #region CardUI
        figtherPanel.SetActive(false);
        spiritPanel.SetActive(false);
        #endregion

        panelRoulette.SetActive(false);
        panelStory.SetActive(false);
        panelOption.SetActive(false);
        panelCardChest.SetActive(true);
    }
    //Evento OnClick del boton Roulette Card
    public void RouletteController()
    {
        panelCardChest.SetActive(false);
        panelStory.SetActive(false);
        panelOption.SetActive(false);
        panelRoulette.SetActive(true);
    }
    //Evento OnClick del boton Options
    public void OptionController()
    {
        panelCardChest.SetActive(false);
        panelStory.SetActive(false);
        panelRoulette.SetActive(false);
        panelOption.SetActive(true);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void DeleteData()
    {
        SaveLoad.DeleteData("Data", "Save");
        SceneManager.LoadScene(0);
    }


    #region CardUI
    public void CUISetPanel(bool isFigther)
    {
        float w = 0; float tt3 = 0; float tt4 = 0;
        #region Set Panels & Buttons
        if (isFigther)
        {
            for (int x = 0; x < btFigthers.Count; x++)
            {
                Text txFigther = btFigthers[x].GetComponentInChildren<Text>();
                txFigther.text = GameController.currentGame.totalFigthers[x].idName;

                //txFigthers[x].text = totalFigther[x].idName;

                for (int y = 0; y < GameController.currentGame.playerFigtherCards.Count; y++)
                {
                    if (txFigther.text == GameController.currentGame.playerFigtherCards[y].idName)
                    {
                        tt3 += 1;
                        btFigthers[x].enabled = true;
                        btFigthers[x].gameObject.SetActive(true);
                        break;
                    }
                    else
                    {
                        btFigthers[x].enabled = false;
                        btFigthers[x].gameObject.SetActive(false);
                    }
                }
            }
            tt3 /= 2;
            if (tt3 <= 8) w = 0;
            else if (tt3 > 8 && tt3 <= 14) w -= (tt3 - 8);
            else w = 6;

            w += -0.45f;
            figtherPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 760 + -(95 * Mathf.Round(w)));

            figtherPanel.SetActive(true);
            spiritPanel.SetActive(false);
        }
        else
        {
            for (int x = 0; x < btSpirits.Count; x++)
            {
                Text txSpirit = btSpirits[x].GetComponentInChildren<Text>();
                txSpirit.text = GameController.currentGame.totalSpirits[x].idName;
                
                //txSpirits[x].text = totalSpirit[x].idName;

                for (int y = 0; y < GameController.currentGame.playerSpiritCards.Count; y++)
                {
                    if (txSpirit.text == GameController.currentGame.playerSpiritCards[y].idName)
                    {
                        tt4 += 1;
                        btSpirits[x].enabled = true;
                        btSpirits[x].gameObject.SetActive(true);
                        break;
                    }
                    else
                    {
                        btSpirits[x].enabled = false;
                        btSpirits[x].gameObject.SetActive(false);
                    }
                }
            }
            tt4 /= 2;
            if (tt4 <= 8) w = 0;
            else if (tt4 > 8 && tt4 <= 20) w -= (tt4 - 8);
            else w = 12;

            w += -0.45f;
            spiritPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 760 + -(95 * (Mathf.Round(w))));

            figtherPanel.SetActive(false);
            spiritPanel.SetActive(true);
        }
        #endregion
        
    }

    public void CUISetFigtherDescription(int index)
    {
        CardFigther selectedCard = GameController.currentGame.tFigtherCard[index];
        GameController.currentGame.currentPlayerFigther = selectedCard;
        txDescription.text = GameController.currentGame.tFigtherCard[index].description;
    }
    public void CUISetSpiritDescription(int index)
    {
        CardSpirit selectedSpirit = GameController.currentGame.tSpiritCard[index];
        GameController.currentGame.currentPlayerFigther.spirit = selectedSpirit;
        txDescription.text = GameController.currentGame.tSpiritCard[index].description;
    }
    #endregion
}