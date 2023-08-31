using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoryPanel : MonoBehaviour
{
    public int totalStages;
    protected int completedStages = 1;
    public int idChapter;

    [Header("Title")]
    public Text title;
    public string sTitle;

    [Header("Scenes")]
    public Button[] buttons;
    public Text[] texts;
    public string[] titles;

    private void Awake()
    {
        #region Titulos

        title.text = sTitle;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
            texts[i].text = titles[i];
        }

        #endregion
        #region Contador de Stages

        float y = 0.0f;
        List<int> currentChapterStages = new();

        if (totalStages < 6) y = 0.1f;
        else for (int i = 1; i < (totalStages - 4); i++) y = 0.1f * i;

        for (int i = 0; i < GameController.currentGame.stages.Count; i++)
        {
            float temp = Mathf.Round((GameController.currentGame.stages[i]) - y);
            if (temp == idChapter) currentChapterStages.Add((int)((GameController.currentGame.stages[i] - temp) * 10));
        }

        currentChapterStages.Sort();
        List<int> stagesCount = currentChapterStages.Distinct().ToList();

        completedStages = stagesCount.Count;


        for (int i = 0; i < completedStages; i++) buttons[i].gameObject.SetActive(true);

        #endregion
    }

    public void Show() { gameObject.SetActive(true); }

    public void Hide() { gameObject.SetActive(false); }
}