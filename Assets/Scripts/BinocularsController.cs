using UnityEngine;
using DG.Tweening;

public class BinocularsController : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam;

    [SerializeField] private Transform targetPoint;
    [SerializeField] private Renderer rend;

    private Material mat;

    private void Start()
    {
        mat = rend.material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            ActivateBinoculars();
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            DeactivateBinoculars();
    }

    private void ActivateBinoculars()
    {
        Sequence seq = DOTween.Sequence();

        seq.Join(DOTween.To(
            () => cam.m_Lens.FieldOfView,
            x => cam.m_Lens.FieldOfView = x,
            15f,
            0.5f
        ));

        seq.Join(transform.DOLocalMove(targetPoint.localPosition, 0.5f));
        seq.Join(transform.DOLocalRotateQuaternion(targetPoint.localRotation, 0.5f));
        seq.Join(mat.DOFade(0f, 0.5f));

        seq.SetEase(Ease.InOutSine);
    }

    private void DeactivateBinoculars()
    {
        Sequence seq = DOTween.Sequence();

        // возвращаемся в ноль
        seq.Join(transform.DOLocalMove(Vector3.zero, 0.5f));
        seq.Join(transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f));

        seq.Join(DOTween.To(
            () => cam.m_Lens.FieldOfView,
            x => cam.m_Lens.FieldOfView = x,
            75f,
            0.5f
        ));

        seq.Join(mat.DOFade(1f, 0.5f));

        seq.SetEase(Ease.InOutSine);
    }

    private void OnDisable()
    {
        DeactivateBinoculars();
    }
}