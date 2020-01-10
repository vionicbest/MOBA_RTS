using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    float damage, duration, speed;
    public void init(Quaternion direction, SkillStat stat) {
        transform.rotation = direction;
        List<float> stats = stat.getStats();
        damage = stats[2];
        duration = stats[3];
        speed = stats[4];
        //GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Debug.Log("asdf");
    }

    void Update() {
        Debug.Log(transform.rotation);
        Vector3 targetDirection = transform.rotation * new Vector3(1, 0, 0);
        Debug.Log(targetDirection);
        transform.position += targetDirection.normalized * speed;
        if (duration <= 0) {
            Destroy(this.gameObject);
        }
        duration--;
    }
}
