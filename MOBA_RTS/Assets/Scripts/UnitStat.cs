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
    private float mhp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange, attackDelay;
    [SerializeField]
    private List<int> skills;
    [SerializeField]
    private AttackType attackType;
    [SerializeField]
    private Sprite unit;

    public string getName()
    {
        return name;
    }
    public List<float> getStats()
    {
        List<float> stats = new List<float> { mhp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange };
        return stats;
    }
    public List<int> getSkills()
    {
        return skills;
    }
    public Sprite getSprite() {
        return unit;
    }
}