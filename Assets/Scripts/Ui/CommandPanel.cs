using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject[] btNormalSkills;
    public GameObject btUltimateSkill;

    [Header("Texts")]
    public Text[] txNormalSkills;
    public Text txUltimateSkill;

    void Awake()
    {
        this.Hide();

        foreach (var btn in this.btNormalSkills)
        {
            btn.SetActive(false);
        }
    }

    public void ConfigureButtons(int index, string skillName)
    {
        btNormalSkills[index].SetActive(true);
        txNormalSkills[index].text = skillName;
    }

    public void ConfigureUltimateButton(string skillName)
    {
        btUltimateSkill.SetActive(true);
        txUltimateSkill.text = skillName;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
