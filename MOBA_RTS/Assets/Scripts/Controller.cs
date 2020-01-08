using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    float scrollSpeed, maxZoom, minZoom;

    Vector3 nextMovePosition;
    Character hero;
    Character.CharacterDirection direction;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hero = GameObject.Find("Character").GetComponent<Character>();
        direction = Character.CharacterDirection.Right;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.Mouse1))
        {
            Vector3 mousePos = Input.mousePosition;
            nextMovePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
        }
        if (hero.transform.position != nextMovePosition)
        {
            float speed = hero.finalSpeed() * Time.deltaTime;
            hero.transform.position = Vector3.MoveTowards(hero.transform.position, nextMovePosition, speed);
            if (hero.transform.position.x > nextMovePosition.x)
            {
                direction = Character.CharacterDirection.Left;
            }
            if (hero.transform.position.x < nextMovePosition.x)
            {
                direction = Character.CharacterDirection.Right;
            }
        }
        if (hero.transform.position != mainCamera.transform.position)
        {
            mainCamera.transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, mainCamera.transform.position.z);
        }
        if (Input.GetAxis ("Mouse ScrollWheel") != 0f)
        {
            float nextCameraZ = mainCamera.transform.position.z + scrollSpeed * Input.GetAxis("Mouse ScrollWheel");
            nextCameraZ = Mathf.Min(nextCameraZ, maxZoom);
            nextCameraZ = Mathf.Max(nextCameraZ, minZoom);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, nextCameraZ);
        }
        hero.changeSprite(direction);
    }
}
