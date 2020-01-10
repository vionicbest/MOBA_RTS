using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    float scrollSpeed, maxZoom, minZoom;
    [SerializeField]
    bool debug;
    Vector3 nextMovePosition;
    Character hero;
    public static Character.CharacterDirection direction;
    public static bool isMoving;
    bool isSkillReady;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hero = GameObject.Find("Character").GetComponent<Character>();
        direction = Character.CharacterDirection.Right;
        isMoving = false;
        isSkillReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = hero.transform.position != nextMovePosition;
        //스킬 관련된 업데이트
        if (Input.GetKeyDown (KeyCode.Q) && !isSkillReady && hero.isSkillValid(0))
        {
            hero.showSkillRange(0);
            isSkillReady = true;
        }
        if (Input.GetKeyUp (KeyCode.Q) && isSkillReady) {
            hero.deleteSkillRange();
            isSkillReady = false;
        }
        if (Input.GetKey (KeyCode.Mouse0) && isSkillReady)
        {
            hero.activateSkill();
            isSkillReady = false;
        }
        //이동 관련된 업데이트
        if (Input.GetKey (KeyCode.Mouse1))
        {
            Vector3 mousePos = Input.mousePosition;
            nextMovePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
        }
        // 캐릭터 방향 관련된 업데이트
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
        // 카메라 관련된 업데이트
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

        // 디버그 용도
        if (debug) {
            if (Input.GetKey(KeyCode.Z)) {
            }
        }
    }
}
