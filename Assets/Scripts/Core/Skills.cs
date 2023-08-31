using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;
    public float animationDuration;
    public int cooldown;
    public int turnsDuration;
    public bool selfInflected;

    public GameObject effectPrfb;

    protected Entity emmiter;
    protected Entity reciver;

    [Header("Tags")]
    public ElementType elementType;
    public SkillType skillType;

    private void Animate()
    {
        Vector3 scale = new Vector3(5, 5, 5);
        this.effectPrfb.transform.localScale.Scale(scale);
        var go = Instantiate(this.effectPrfb, this.reciver.transform.position, Quaternion.identity, reciver.transform);
        Destroy(go, this.animationDuration);
    }

    public void Run()
    {
        if (this.selfInflected)
        {
            this.reciver = this.emmiter;
        }
        this.Animate();

        for (int i = 0; i < turnsDuration; i++)
        {
            this.OnRun();
        }
    }

    public void SetEmmiterAndReciver(Entity _emmiter, Entity _reciver)
    {
        this.emmiter = _emmiter;
        this.reciver = _reciver;
    }

    protected abstract void OnRun();

    public void AdaptationElement(ElementType enemy_element)
    {
        ElementType temp = ElementType.NORMAL;
        switch (enemy_element)
        {
            case ElementType.NORMAL:
                temp = ElementType.VORTEX;
                break;
            case ElementType.LUMEN:
                temp = ElementType.UMBRA;
                break;
            case ElementType.UMBRA:
                temp = ElementType.LUMEN;
                break;
            case ElementType.IGNIS:
                temp = ElementType.VORTEX;
                break;
            case ElementType.AQUA:
                temp = ElementType.ELEKTRO;
                break;
            case ElementType.TERRA:
                temp = ElementType.AQUA;
                break;
            case ElementType.ELEKTRO:
                temp = ElementType.LUMEN;
                break;
            case ElementType.CRYO:
                temp = ElementType.ELEKTRO;
                break;
            case ElementType.NATURA:
                temp = ElementType.TERRA;
                break;
            case ElementType.VORTEX:
                temp = ElementType.PSYCHIC;
                break;
            case ElementType.PSYCHIC:
                temp = ElementType.NORMAL;
                break;
        }
        elementType = temp;
    }
}