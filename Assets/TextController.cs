using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextController : MonoBehaviour
{
    public CanvasGroup imageGroup;
    public TMP_Text text;

    public TMP_Text cheersText;

    public float fadeDuration = 0.5f;
    public float typingSpeed = 0.05f;

    private Sequence currentSequence;
    private Tween hideTween;
    private Tween cheersTween;

    private void Start()
    {
        HideTextInstantly();
        HideCheersInstantly();
    }

    public void ShowText(string message)
    {
        currentSequence?.Kill();
        hideTween?.Kill();

        imageGroup.gameObject.SetActive(true);
        imageGroup.alpha = 0f;

        text.text = "";

        currentSequence = DOTween.Sequence();

        currentSequence.Append(
            imageGroup.DOFade(1f, fadeDuration)
        );

        currentSequence.AppendCallback(() =>
        {
            TypeText(message);
        });
    }

    public void HideText(float delay)
    {
        hideTween?.Kill();

        hideTween = DOVirtual.DelayedCall(delay, () =>
        {
            imageGroup.DOFade(0f, fadeDuration)
                .OnComplete(() =>
                {
                    imageGroup.gameObject.SetActive(false);
                });

            text.text = "";
        });
    }

    public void HideTextInstantly()
    {
        currentSequence?.Kill();
        hideTween?.Kill();

        imageGroup.DOKill();
        text.DOKill();

        imageGroup.alpha = 0f;
        imageGroup.gameObject.SetActive(false);
        text.text = "";
    }

    public void ShowCheersText(string message)
    {
        cheersTween?.Kill();

        cheersText.text = message;

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(5f);
        seq.OnComplete(() =>
        {
            cheersText.text = "";
        });

        cheersTween = seq;
    }

    public void HideCheersInstantly()
    {
        cheersTween?.Kill();

        cheersText.text = "";
    }

    void TypeText(string message)
    {
        text.text = "";

        Sequence typeSeq = DOTween.Sequence();

        foreach (char c in message)
        {
            typeSeq.AppendCallback(() =>
            {
                text.text += c;
            });

            typeSeq.AppendInterval(typingSpeed);
        }
    }
}