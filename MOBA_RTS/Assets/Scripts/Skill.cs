﻿using System.Collections;
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
                Debug.Log(target.position);
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                
                Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장

                //다음은 아크탄젠트(arctan, 역탄젠트)로 게임 오브젝트의 좌표와 마우스 포인트의 좌표를
                //이용하여 각도를 구한 후, 오일러(Euler)회전 함수를 사용하여 게임 오브젝트를 회전시키기
                //위해, 각 축의 거리차를 구한 후 오일러 회전함수에 적용시킵니다.

                //우선 각 축의 거리를 계산하여, dy, dx에 저장해 둡니다.
                float dy = target.position.y - oPosition.y;
                float dx = target.position.x - oPosition.x;

                //오릴러 회전 함수를 0에서 180 또는 0에서 -180의 각도를 입력 받는데 반하여
                //(물론 270과 같은 값의 입력도 전혀 문제없습니다.) 아크탄젠트 Atan2()함수의 결과 값은 
                //라디안 값(180도가 파이(3.141592654...)로)으로 출력되므로
                //라디안 값을 각도로 변화하기 위해 Rad2Deg를 곱해주어야 각도가 됩니다.
                float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

                //구해진 각도를 오일러 회전 함수에 적용하여 z축을 기준으로 게임 오브젝트를 회전시킵니다.
                transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
                Debug.Log(transform.rotation);
                break;
        }
    }
    public float GetDamage() {
        return damage;
    }
}
