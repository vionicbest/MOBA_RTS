using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    float gold;
    float metal;
    float constantGold;
    float constantMetal;
    float delay;

    [SerializeField]
    float startDelay, startGold, startMetal, startConstantGold, startConstantMetal;
    [SerializeField]
    Text goldText, metalText;
    float time;

    private void Start()
    {
        gold = startGold;
        metal = startMetal;
        constantGold = startConstantGold;
        constantMetal = startConstantMetal;
        delay = startDelay;
        time = delay;
    }
    public void AddGold(float gold)
    {
        this.gold += gold;
    }
    public void AddMetal(float metal)
    {
        this.metal += metal;
    }
    public void ConstantAddResource()
    {
        gold += constantGold;
        metal += constantMetal;
        time = delay;
    }

    private void Update()
    {
        if (time == 0)
        {
            ConstantAddResource();
        }
        if (time > 0)
        {
            time--;
        }
        goldText.text = gold.ToString();
        metalText.text = metal.ToString();
    }
}
