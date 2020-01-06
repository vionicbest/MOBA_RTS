using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    Camera Camera;
    [SerializeField]
    Character hero;

    Vector3 nextMovePosition;
    void Start()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            nextMovePosition = Camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
        }
        if (hero.transform.position != nextMovePosition)
        {
            float speed = hero.finalSpeed() * Time.deltaTime;
            hero.transform.position = Vector3.MoveTowards(hero.transform.position, nextMovePosition, speed);
            if (hero.transform.position.x > nextMovePosition.x)
            {
                hero.changeSprite(Character.CharacterDirection.Left);
            }
            else if (hero.transform.position.x < nextMovePosition.x)
            {
                hero.changeSprite(Character.CharacterDirection.Right);
            }
        }
    }
}
