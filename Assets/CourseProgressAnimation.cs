using DG.Tweening;
using UnityEngine;

public class CourseProgressAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform courseProgressTransform;

    [Header("Animation settings")]
    [SerializeField]
    private float progressShowDuration;
    [SerializeField]
    private Ease progressShowEase;
    [SerializeField]
    private float targetYPos;

    [SerializeField]
    private float progressHideDuration;
    [SerializeField]
    private Ease progressHideEase;
    private Vector3 startPos;

    private Tween showTween;
    private Tween hideTween;

    // Start is called before the first frame update
    void Start()
    {
        startPos = courseProgressTransform.localPosition;

        showTween = courseProgressTransform.DOMoveY(targetYPos, progressShowDuration)
            .SetEase(progressShowEase);

        hideTween = courseProgressTransform.DOMoveY(startPos.y, progressShowDuration)
          .SetEase(progressShowEase);

    }

    public void ShowProgressCourse()
    {
        showTween.Rewind();
        showTween.Play();
    }
    public void HideProgressCourse()
    {
        showTween.Pause();
        hideTween.Rewind();
        hideTween.Play();
    }
}
