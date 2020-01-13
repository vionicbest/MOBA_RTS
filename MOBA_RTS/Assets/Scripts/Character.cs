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

    public enum CharacterType
    {
        Hero,
        Unit,
        Building,
    };

    [SerializeField]
    string characterCode;
    [SerializeField]
    CharacterType type;
    Sprite sprite, spriteLeft, spriteRight, hpBar, mpBar;
    public Animator anime; 
    UnitStat stat;
    float hp, mhp, mp, mmp, atk, def, hpRegen, mpRegen, atkRange, sightRange, speed;
    SkillStat currentSkill;
    int currentSkillNum;
    Transform skillRange;
    List<float> cooldown;
    List<SkillStat> skillStats = new List<SkillStat> {null, null, null, null};
    Vector3 nextMovePosition = new Vector3(0, 0, -1);
    bool isMoving;

    CharacterDirection direction;
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
        speed = stats[8];
    }
    void setSprite(Sprite sprite) {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    void setSkills(List<int> skills)
    {
        for (int i=0; i<4; i++) {
            skillStats[i] = (SkillStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Skills/Skill_" + skills[i] + ".asset", typeof(SkillStat));
        }
    }
    public void init(string code, CharacterType type) {
        characterCode = code;
        this.type = type;
    }
    private void Start()
    {
        switch(type)
        {
            case CharacterType.Hero:
                stat = (UnitStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Heroes/Hero_" + characterCode + ".asset", typeof(UnitStat));
                setSkills(stat.GetSkills());
                cooldown = new List<float> { 0.0f, 0.0f, 0.0f, 0.0f };
                break;
            case CharacterType.Unit:
                stat = (UnitStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Units/Unit_" + characterCode + ".asset", typeof(UnitStat));
                break;
            case CharacterType.Building:
                stat = (UnitStat)AssetDatabase.LoadAssetAtPath("Assets/Datas/Buildings/Building_" + characterCode + ".asset", typeof(UnitStat));
                break;
        }
        setStats(stat.GetStats());
        setSprite(stat.GetSprite());
        nextMovePosition = transform.position;
        direction = CharacterDirection.Right;
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
        Debug.Log(stat.GetName() + " " + transform.position + " " + nextMovePosition);
        isMoving = transform.position != nextMovePosition;
        if (type == CharacterType.Hero) {
            for (int i=0; i<4; i++) {
                if (cooldown[i] > 0) {
                    cooldown[i]--;
                }
            }
        }
        if (mp < mmp) {
            mp += mpRegen;
        }
        if (stat.IsHaveAnime())
        {
            anime.SetBool("isMoving", isMoving == true);
            anime.SetBool("isRight", direction == CharacterDirection.Right);
        }

        if (transform.position != nextMovePosition)
        {
            var moveSpeed = finalSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextMovePosition, moveSpeed);
            if (transform.position.x > nextMovePosition.x)
            {
                direction = Character.CharacterDirection.Left;
            }
            if (transform.position.x < nextMovePosition.x)
            {
                direction = Character.CharacterDirection.Right;
            }
        }
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
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.GetComponent<Skill>())
        {
            float damage = col.gameObject.GetComponent<Skill>().GetDamage();
            getDamage(damage);
            Destroy(col.gameObject);
            Debug.Log(hp);
        }
    }

    void getDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void move(Vector3 nextPosition)
    {
        nextMovePosition = nextPosition;
    }
}
