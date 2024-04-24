using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWinStars : MonoBehaviour
{
    [SerializeField] Sprite dimStar, fullStar;
    [SerializeField] Image[] stars = new Image[3];

    public void UpdateWinStars(WinSave _save)
    {
        bool completed = _save.hasCompleted, beatTime = _save.hasBeatTime, collector = _save.hasAllCollectibles;

        stars[0].sprite = completed ? fullStar : dimStar;
        stars[1].sprite = beatTime ? fullStar : dimStar;
        stars[2].sprite = collector ? fullStar : dimStar;

        if (completed && beatTime && collector)
        {
            FindObjectOfType<AudioManager>().Play("AudienceCheer");
        }
    }
}
