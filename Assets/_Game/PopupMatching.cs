using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMatching : MonoBehaviour
{
    [SerializeField] List<OnlineLoading> onlineLoadings;

    public void StartMatching() {
        for (int i = 0; i < onlineLoadings.Count; i++) {
            onlineLoadings[i].Loading();
        }
    }
}
