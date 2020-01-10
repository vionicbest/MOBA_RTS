using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    Character hero;
    public enum Type{
        mana,
        health,
    }

    [SerializeField]
    Type type;

    void Start() {
        hero = GameObject.Find("Character").GetComponent<Character>();
    }

    void Update() {
        transform.localScale = new Vector3(hero.getRatio(type), 1, 1);
    }
}
