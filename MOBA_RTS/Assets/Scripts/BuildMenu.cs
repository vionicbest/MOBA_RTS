using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class BuildMenu : MonoBehaviour
{
    public enum Status
    {
        basic,
        advanced,
        normal,
        none,
    }

    Status status;
    [SerializeField]
    List<GameObject> BuildSlot;
    [SerializeField]
    List<Sprite> normal;
    [SerializeField]
    List<Status> normalClick;
    [SerializeField]
    List<Sprite> basicBuild;
    [SerializeField]
    List<Status> basicClick;
    [SerializeField]
    List<Sprite> advancedBuild;
    [SerializeField]
    List<Status> advancedClick;

    void Start() {
        status = Status.normal;
    }

    public void ShowBuildMenu(Status status) {
        switch (status) {
            case Status.normal:
                for (int i=0; i<9; i++) {
                    BuildSlot[i].GetComponent<Image>().sprite = normal[i];
                    BuildSlot[i].GetComponent<BuildMenuSlot>().SetStatus(normalClick[i]);
                }
                break;
            case Status.basic:
                for (int i=0; i<9; i++) {
                    BuildSlot[i].GetComponent<Image>().sprite = basicBuild[i];
                    BuildSlot[i].GetComponent<BuildMenuSlot>().SetStatus(basicClick[i]);
                }
                break;
            case Status.advanced:
                for (int i=0; i<9; i++) {
                    BuildSlot[i].GetComponent<Image>().sprite = advancedBuild[i];
                    BuildSlot[i].GetComponent<BuildMenuSlot>().SetStatus(advancedClick[i]);
                }
                break;
            case Status.none:
                break;
        }
    }
}
