using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStars : MonoBehaviour
{
    [SerializeField] Sprite _000, _100, _101, _110, _111;

    public void ChangeTo(WinSave _save)
    {
        bool completed = _save.hasCompleted, beatTime = _save.hasBeatTime, collector = _save.hasAllCollectibles;
        GetComponent<Image>().sprite = completed ? (!beatTime && !collector ? _100 : beatTime && !collector ? _110 : !beatTime && collector ? _101 : _111) : _000;
    }
}
