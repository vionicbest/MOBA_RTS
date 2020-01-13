using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    float damage, duration, speed;
    Vector3 normalizedSpeed;
    SkillStat.SkillType type;

    Transform target;
    public void init(Quaternion direction, SkillStat stat, Transform target) {
        transform.rotation = direction;
        List<float> stats = stat.getStats();
        damage = stats[2];
        duration = stats[3];
        speed = stats[4];
        normalizedSpeed = (transform.rotation * new Vector3(1, 0, 0)).normalized * speed;
        type = stat.getSkillType();
        this.target = target;
    }

    void Update() {
        switch(type)
        {
            case SkillStat.SkillType.Projectile:
                transform.position += normalizedSpeed;
                if (duration <= 0)
                {
                    Destroy(this.gameObject);
                }
                duration--;
                break;
            case SkillStat.SkillType.TargetedProjectile:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                break;
        }
    }
    public float GetDamage() {
        return damage;
    }
}
