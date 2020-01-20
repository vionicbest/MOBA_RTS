using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuSlot : MonoBehaviour
{
    BuildMenu.Status nextStatus;
    [SerializeField]
    BuildMenu buildMenu;
    void Start() {
    }
    public void SetStatus(BuildMenu.Status status) {
        nextStatus = status;
    }
    public void OnClickSlot() {
        Debug.Log("?");
        buildMenu.ShowBuildMenu(nextStatus);
    }
}
