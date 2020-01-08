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
    Sprite sprite, spriteLeft, spriteRight, hpBar, mpBar;

    HeroStat stat;
    float hp, mhp, mp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange;
    List<int> skills;
    SkillStat currentSkill;
    int currentSkillNum;
    Transform skillRange;

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
    void setSprites(List<Sprite> sprites)
    {
        sprite = sprites[0];
        spriteLeft = sprites[1];
        spriteRight = sprites[2];
    }
    void setSkills(List<int> skills)
    {
        this.skills = skills;
    }
    private void Start()
    {
        stat = (HeroStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Heroes/Hero_" + characterCode + ".asset", typeof(HeroStat));
        setStats(stat.getStats());
        setSprites(stat.getSprites());
        setSkills(stat.getSkills());
    }

    public float finalSpeed()
    {
        return speed;
    }

    public void changeSprite(CharacterDirection direction)
    {
        switch(direction)
        {
            case CharacterDirection.Center:
                GetComponent<SpriteRenderer>().sprite = sprite;
                break;
            case CharacterDirection.Left:
                GetComponent<SpriteRenderer>().sprite = spriteLeft;
                break;
            case CharacterDirection.Right:
                GetComponent<SpriteRenderer>().sprite = spriteRight;
                break;
        }
    }
    public void showSkillRange(int skill)
    {
        Debug.Log(skill);
        currentSkill = (SkillStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Skills/Skill_" + skills[skill] + ".asset", typeof(SkillStat));
        Transform transform = GetComponent<Transform>();
        switch(currentSkill.getSkillType())
        {
            case SkillStat.SkillType.Projectile:
                Instantiate(currentSkill.getPrefabs()[1], new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                currentSkillNum = skill;
                break;
        }
    }
    public void deleteSkillRange()
    {
        Destroy(GameObject.Find("skill_"+skills[currentSkillNum]+"_range(Clone)"));
    }
    public void activateSkill()
    {
        deleteSkillRange();
        switch (currentSkill.getSkillType())
        {
            case SkillStat.SkillType.Projectile:
                Instantiate(currentSkill.getPrefabs()[0], new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                currentSkillNum = -1;
                mp -= currentSkill.getStats()[0];
                break;
        }
    }
}
