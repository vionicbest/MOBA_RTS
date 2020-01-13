using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitData", menuName = "Create new unit data", order = 2)]

public class UnitStat : ScriptableObject
{
    enum AttackType {
        melee,
        range,
        cannot,
    }
    [SerializeField]
    private int serialNum;
    [SerializeField]
    private string name;
    [SerializeField]
    private float mhp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange, attackDelay, speed;
    [SerializeField]
    private List<int> skills;
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private Sprite unit;
    [SerializeField]
    private bool haveAnime;

    public string GetName()
    {
        return name;
    }
    public List<float> GetStats()
    {
        List<float> stats = new List<float> { mhp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange, speed };
        return stats;
    }
    public List<int> GetSkills()
    {
        return skills;
    }
    public Sprite GetSprite() {
        return unit;
    }
    public bool IsHaveAnime()
    {
        return haveAnime;
    }
}