using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    float zoomSpeed, verticalScrollSpeed, horizontalScrollSpeed, maxZoom, minZoom;
    [SerializeField]
    bool debug;
    [SerializeField]
    Transform unit;
    Vector3 nextMovePosition;
    Character hero;
    public static Character.CharacterDirection direction;
    public static bool isMoving;
    bool isSkillReady;

    bool cameraUp;
    bool cameraDown;
    bool cameraLeft;
    bool cameraRight;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hero = GameObject.Find("Character").GetComponent<Character>();
        direction = Character.CharacterDirection.Right;
        isMoving = false;
        isSkillReady = false;
    }
    public void MoveCamera(CameraBoundary.Boundary boundary, bool isTurnOn)
    {
        switch (boundary)
        {
            case CameraBoundary.Boundary.Up:
                cameraUp = isTurnOn;
                break;
            case CameraBoundary.Boundary.Down:
                cameraDown = isTurnOn;
                break;
            case CameraBoundary.Boundary.Right:
                cameraRight = isTurnOn;
                break;
            case CameraBoundary.Boundary.Left:
                cameraLeft = isTurnOn;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        isMoving = hero.transform.position != nextMovePosition;
        //스킬 관련된 업데이트
        if (Input.GetKey(KeyCode.Mouse0) && isSkillReady)
        {
            hero.activateSkill();
            isSkillReady = false;
        }
        if (Input.GetKeyDown (KeyCode.Q) && !isSkillReady && hero.isSkillValid(0))
        {
            hero.showSkillRange(0);
            isSkillReady = true;
        }
        if (Input.GetKeyUp (KeyCode.Q) && isSkillReady) {
            hero.deleteSkillRange();
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
        if (cameraUp)
        {
            mainCamera.transform.position += new Vector3(0, verticalScrollSpeed, 0);
        }
        if (cameraDown)
        {
            mainCamera.transform.position += new Vector3(0, -verticalScrollSpeed, 0);
        }
        if (cameraRight)
        {
            mainCamera.transform.position += new Vector3(horizontalScrollSpeed, 0, 0);
        }
        if (cameraLeft)
        {
            mainCamera.transform.position += new Vector3(-horizontalScrollSpeed, 0, 0);
        }
        if (Input.GetAxis ("Mouse ScrollWheel") != 0f)
        {
            float nextCameraZ = mainCamera.transform.position.z + zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
            nextCameraZ = Mathf.Min(nextCameraZ, maxZoom);
            nextCameraZ = Mathf.Max(nextCameraZ, minZoom);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, nextCameraZ);
        }

        // 디버그 용도
        if (debug) {
            if (Input.GetKey(KeyCode.Z)) {
                unit.gameObject.GetComponent<Character>().init("0");
                var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var spawnedUnit = Instantiate(unit, new Vector3(mp.x, mp.y, -1), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
