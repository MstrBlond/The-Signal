using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private Transform spawnPoint;

    private int currentIndex = 0;
    private bool isSwitching = false;

    private void Start()
    {
        UpdateItemsInstant();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) return;
        if (isSwitching || items.Length == 0) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
            StartCoroutine(SwitchItem(1));
        else if (scroll < 0f)
            StartCoroutine(SwitchItem(-1));
    }

    private IEnumerator SwitchItem(int direction)
    {
        isSwitching = true;

        GameObject currentItem = items[currentIndex];

        Vector3 originalPos = currentItem.transform.localPosition;
        Quaternion originalRot = currentItem.transform.localRotation;

        // УЛЕТАЕТ
        Sequence seqOut = DOTween.Sequence();
        seqOut.Join(currentItem.transform.DOLocalMove(spawnPoint.localPosition, 0.25f));
        seqOut.Join(currentItem.transform.DOLocalRotateQuaternion(spawnPoint.localRotation, 0.25f));
        seqOut.SetEase(Ease.InOutSine);

        yield return seqOut.WaitForCompletion();

        currentItem.SetActive(false);

        // смена индекса
        currentIndex += direction;
        if (currentIndex >= items.Length) currentIndex = 0;
        if (currentIndex < 0) currentIndex = items.Length - 1;

        GameObject newItem = items[currentIndex];
        newItem.SetActive(true);

        // ставим в точку вылета
        newItem.transform.localPosition = spawnPoint.localPosition;
        newItem.transform.localRotation = spawnPoint.localRotation;

        // ВЛЕТАЕТ
        Sequence seqIn = DOTween.Sequence();
        seqIn.Join(newItem.transform.DOLocalMove(originalPos, 0.25f));
        seqIn.Join(newItem.transform.DOLocalRotateQuaternion(originalRot, 0.25f));
        seqIn.SetEase(Ease.OutBack);

        yield return seqIn.WaitForCompletion();

        isSwitching = false;
    }

    private void UpdateItemsInstant()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(i == currentIndex);
        }
    }
}