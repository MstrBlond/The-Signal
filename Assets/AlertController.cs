using System.Collections;
using UnityEngine;
using DG.Tweening;

public class AlertController : MonoBehaviour
{
    public RectTransform target;
    public RectTransform startPoint;
    public RectTransform endPoint;

    public float moveDuration = 1f;
    public float waitTime = 5f;

    private bool isPlaying = false;

    public void PlayAlert()
    {
        if (isPlaying) return;
        StartCoroutine(AlertRoutine());
    }

    IEnumerator AlertRoutine()
    {
        isPlaying = true;

        yield return target.DOAnchorPos(endPoint.anchoredPosition, moveDuration)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();

        yield return new WaitForSeconds(waitTime);

        yield return target.DOAnchorPos(startPoint.anchoredPosition, moveDuration)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();

        isPlaying = false;
    }
}