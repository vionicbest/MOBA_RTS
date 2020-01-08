using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HeroData", menuName = "HeroData", order = 2)]

public class HeroStat : ScriptableObject
{
    [SerializeField]
    private int serialNum;
    [SerializeField]
    private string name;
    [SerializeField]
    private float mhp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange;
    [SerializeField]
    private List<int> skills;
    [SerializeField]
    private Sprite hero, heroleft, heroright;

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
    public List<Sprite> getSprites()
    {
        List<Sprite> sprites = new List<Sprite> { hero, heroleft, heroright };
        return sprites;
    }
}