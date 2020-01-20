using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuSlot : MonoBehaviour
{
    BuildMenu.Status nextStatus;

    public void setStatus(BuildMenu.Status status) {
        nextStatus = status;
    }

}
