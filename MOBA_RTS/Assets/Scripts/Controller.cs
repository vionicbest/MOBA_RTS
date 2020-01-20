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
    [SerializeField]
    GameObject map, research;
    [SerializeField]
    BuildMenu buildMenu;
    Character hero;
    bool isSkillReady;
    bool isMapOpen;
    bool isResearchOpen;

    bool cameraUp;
    bool cameraDown;
    bool cameraLeft;
    bool cameraRight;

    Transform temp;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        hero = GameObject.Find("Character").GetComponent<Character>();
        isSkillReady = false;
        isResearchOpen = false;
        isMapOpen = false;
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
        //스킬 관련된 업데이트
        if (Input.GetKey(KeyCode.Mouse0) && isSkillReady)
        {
            hero.activateSkill();
            isSkillReady = false;
        }
        if (Input.GetKeyDown (KeyCode.Q))
        {
            if (isSkillReady)
            {
                hero.DeleteSkillRange();
                isSkillReady = false;
            }
            else if (hero.IsSkillValid(0))
            {
                hero.ShowSkillRange(0);
                isSkillReady = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isSkillReady)
            {
                hero.DeleteSkillRange();
                isSkillReady = false;
            }
            else if (hero.IsSkillValid(1))
            {
                hero.ShowSkillRange(1);
                isSkillReady = true;
            }
        }
        //이동 관련된 업데이트
        if (Input.GetKey (KeyCode.Mouse1))
        {
            Vector3 mousePos = Input.mousePosition;
            hero.move(mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z - 1)));
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
        // 전술지도 관련 업데이트
        if (Input.GetKeyDown (KeyCode.Tab))
        {
            isMapOpen = !isMapOpen;
            map.SetActive(isMapOpen);
        }
        // 건물 건설 관련 업데이트
        if (Input.GetKeyDown (KeyCode.B))
        {
            buildMenu.ShowBuildMenu(BuildMenu.Status.basic);
        }
        
        if (Input.GetKeyDown (KeyCode.V))
        {
            buildMenu.ShowBuildMenu(BuildMenu.Status.advanced);
        }

        // 연구 관련 업데이트

        if (Input.GetKeyDown (KeyCode.F1))
        {
            isResearchOpen = !isResearchOpen;
            research.SetActive(isResearchOpen);
        }
        // 디버그 용도
        if (debug) {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (temp)
                {
                    temp.GetComponent<Character>().move(temp.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0));
                }
                unit.gameObject.GetComponent<Character>().init("0", Character.CharacterType.Unit);
                var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var spawnedUnit = Instantiate(unit, new Vector3(mp.x, mp.y, -1), new Quaternion(0, 0, 0, 0));
                temp = spawnedUnit;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                unit.gameObject.GetComponent<Character>().init("0", Character.CharacterType.Building);
                var mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var spawnedUnit = Instantiate(unit, new Vector3(mp.x, mp.y, -1), new Quaternion(0, 0, 0, 0));
                
            }
        }
    }
}
