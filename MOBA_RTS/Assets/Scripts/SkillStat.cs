using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillData", menuName = "Create new skill data", order = 2)]

public class SkillStat : ScriptableObject
{
    public enum SkillType
    {
        Projectile,
        TargetedProjectile,
    };
    [SerializeField]
    private string name;
    [SerializeField]
    private SkillType skillType;
    [SerializeField]
    private int num;
    [SerializeField]
    private float mp, cooldown, damage, duration, speed;
    [SerializeField]
    private Transform skillPrefab, skillRangePrefab;
    [SerializeField]
    private bool isTarget;

    public string getName()
    {
        return name;
    }
    public List<float> getStats()
    {
        List<float> stats = new List<float> {mp, cooldown, damage, duration, speed};
        return stats;
    }
    public List<Transform> getPrefabs()
    {
        List<Transform> prefabs = new List<Transform> { skillPrefab, skillRangePrefab };
        return prefabs;
    }
    public SkillType getSkillType()
    {
        return skillType;
    }
}
