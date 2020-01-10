using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Character : MonoBehaviour
{
    public enum CharacterDirection
    {
        Center,
        Left,
        Right,
    };
    [SerializeField]
    string characterCode;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isHero;
    Sprite sprite, spriteLeft, spriteRight, hpBar, mpBar;
    public Animator anime; 
    UnitStat stat;
    float hp, mhp, mp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange;
    SkillStat currentSkill;
    int currentSkillNum;
    Transform skillRange;
    List<float> cooldown;
    List<SkillStat> skillStats = new List<SkillStat> {null, null, null, null};

    void setStats(List<float> stats)
    {
        hp = stats[0];
        mhp = stats[0];
        mp = stats[1];
        mmp = stats[1];
        atk = stats[2];
        def = stats[3];
        hpRegen = stats[4];
        mpRegen = stats[5];
        atkRange = stats[6];
        sightRange = stats[7];
    }
    void setSkills(List<int> skills)
    {
        for (int i=0; i<4; i++) {
            skillStats[i] = (SkillStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Skills/Skill_" + skills[i] + ".asset", typeof(SkillStat));
        }
    }
    private void Start()
    {
        if (isHero) {
            stat = (UnitStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Heroes/Hero_" + characterCode + ".asset", typeof(UnitStat));
            setSkills(stat.getSkills());
            cooldown = new List<float> {0.0f, 0.0f, 0.0f, 0.0f};
        }
        else {
            stat = (UnitStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Units/Unit_" + characterCode + ".asset", typeof(UnitStat));
        }
        setStats(stat.getStats());
    }

    public float finalSpeed()
    {
        return speed;
    }

    public bool isSkillValid(int skill) {
        if (cooldown[skill] > 0 || mp < skillStats[skill].getStats()[0]) {
            return false;
        }
        return true;
    }
    public void showSkillRange(int skill)
    {
        Debug.Log(skill);
        currentSkill = skillStats[skill];
        Transform transform = GetComponent<Transform>();
        switch(currentSkill.getSkillType())
        {
            case SkillStat.SkillType.Projectile:
                skillRange = Instantiate(currentSkill.getPrefabs()[1], new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                skillRange.GetComponent<SkillRange>().init(this);
                currentSkillNum = skill;
                break;
        }
    }
    public void deleteSkillRange()
    {
        Destroy(skillRange.gameObject);
        skillRange = null;
    }
    public void activateSkill()
    {
        switch (currentSkill.getSkillType())
        {
            case SkillStat.SkillType.Projectile:
                var projectile = Instantiate(currentSkill.getPrefabs()[0], new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                projectile.GetComponent<Skill>().init(skillRange.GetComponent<SkillRange>().direction(), currentSkill);
                mp -= currentSkill.getStats()[0];
                cooldown[currentSkillNum] += currentSkill.getStats()[1];
                currentSkillNum = -1;
                break;
        }
        deleteSkillRange();
    }

    private void Update()
    {
        if (mp < mmp) {
            mp += mpRegen;
        }
        for (int i=0; i<4; i++) {
            if (cooldown[i] > 0) {
                cooldown[i]--;
            }
            
        }
        anime.SetBool("isMoving", Controller.isMoving == true);
        anime.SetBool("isRight", Controller.direction == CharacterDirection.Right);
    }

    public float getRatio(Gauge.Type type) {
        switch(type) {
            case Gauge.Type.health:
                return hp/mhp;
            case Gauge.Type.mana:
                return mp/mmp;
            default:
                return 1;
        }
    }
}
