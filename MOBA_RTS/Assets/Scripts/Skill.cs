using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    float damage, duration, speed;
    Vector3 normalizedSpeed;
    public void init(Quaternion direction, SkillStat stat) {
        transform.rotation = direction;
        List<float> stats = stat.getStats();
        damage = stats[2];
        duration = stats[3];
        speed = stats[4];
        normalizedSpeed = (transform.rotation * new Vector3(1, 0, 0)).normalized * speed;
    }

    void Update() {
        transform.position += normalizedSpeed;
        if (duration <= 0) {
            Destroy(this.gameObject);
        }
        duration--;
    }
    public float GetDamage() {
        return damage;
    }
}
