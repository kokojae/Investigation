using System.Collections;
using UnityEngine;

public class C1_Clock : SceneObject
{
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private float hourHandAngle = 60f;
    [SerializeField] private float minuteHandAngle = 0f;
    [SerializeField] private float toleranceAngle = 5f;
    [SerializeField] private Transform cardKey;

    private HandRotator hourHandRotator;
    private HandRotator minuteHandRotator;
    private bool isHourHandClear = false;
    private bool isMinuteHandClear = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hourHandRotator = hourHand.GetComponent<HandRotator>();
        minuteHandRotator = minuteHand.GetComponent<HandRotator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAngle();
        if (isHourHandClear && isMinuteHandClear)
        {
            ResolvePuzzle();
            enabled = false;
        }
    }

    void CheckAngle()
    {
        if (!isHourHandClear && !hourHandRotator.IsDragging)
        {
            // 시침 처리
            float hz = hourHand.localEulerAngles.z;
            float chz = hz < 0 ? 360 - hz : hz;
            float diffH = Mathf.DeltaAngle(chz, hourHandAngle);
            if (Mathf.Abs(diffH) <= toleranceAngle)
            {
                // 스냅 & 종료
                hourHand.localRotation = Quaternion.Euler(0f, 0f, hourHandAngle);
                hourHandRotator.enabled = false;
                isHourHandClear = true;
            }
        }

        if (!isMinuteHandClear && !minuteHandRotator.IsDragging)
        {
            // 분침 처리
            float mz = minuteHand.localEulerAngles.z;
            float cmz = mz < 0 ? 360 - mz : mz;
            float diffM = Mathf.DeltaAngle(cmz, minuteHandAngle);
            if (Mathf.Abs(diffM) <= toleranceAngle)
            {
                minuteHand.localRotation = Quaternion.Euler(0f, 0f, minuteHandAngle);
                minuteHandRotator.enabled = false;
                isMinuteHandClear = true;
            }
        }
    }

    public override void ObjectZoom()
    {
        base.ObjectZoom();
        if (!isHourHandClear) hourHandRotator.enabled = true;
        if (!isMinuteHandClear) minuteHandRotator.enabled = true;
    }

    public override void ObjectUnZoom()
    {
        base.ObjectUnZoom();
        hourHandRotator.enabled = false;
        minuteHandRotator.enabled = false;
    }

IEnumerator CardKeyMove(float duration = 1f)
{
    Vector3 startPos = cardKey.position;
    Vector3 endPos   = new Vector3(-0.5f, startPos.y, startPos.z);
    float elapsed    = 0f;

    while (elapsed < duration)
    {
        // t ∈ [0,1] 을 계산
        float t = elapsed / duration;
        // Lerp로 보간
        cardKey.position = Vector3.Lerp(startPos, endPos, t);

        elapsed += Time.deltaTime;
        yield return null;
    }

    // 정확히 목표 지점으로 마무리
    cardKey.position = endPos;
}
    public override void ResolvePuzzle()
    {
        base.ResolvePuzzle();
        cardKey.GetComponent<Collider>().enabled = true;
        StartCoroutine(CardKeyMove());
    }
}
