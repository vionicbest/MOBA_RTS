using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterDirection
    {
        Left,
        Right,
    };
    [SerializeField]
    int characterCode;
    [SerializeField]
    float speed;
    [SerializeField]
    Sprite spriteLeft, spriteRight;

    public float finalSpeed()
    {
        return speed;
    }

    public void changeSprite(CharacterDirection direction)
    {
        switch(direction)
        {
            case CharacterDirection.Left:
                GetComponent<SpriteRenderer>().sprite = spriteLeft;
                break;
            case CharacterDirection.Right:
                GetComponent<SpriteRenderer>().sprite = spriteRight;
                break;
        }
    }
}
