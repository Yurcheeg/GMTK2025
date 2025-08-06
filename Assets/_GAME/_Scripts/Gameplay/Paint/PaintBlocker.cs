using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PaintBlocker : MonoBehaviour
{
    private Collider2D _collider;

    private InkTracker _inkTracker;

    public bool IsBlockingLine { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _inkTracker = FindAnyObjectByType<InkTracker>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LineRenderer renderer) == false)
            return;

        UpdateColor(renderer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LineRenderer renderer) == false)
            return;

        UpdateColor(renderer);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LineRenderer renderer) == false)
            return;

        UpdateColor(renderer);
    }

    private void UpdateColor(LineRenderer renderer)
    {
        if (renderer == null)
            return;

        if (renderer.gameObject == null)
            return;

        if(renderer.positionCount == 0) 
            return;

        Vector3[] positions = new Vector3[renderer.positionCount];
        renderer.GetPositions(positions);


        bool anyInside = false;

        for (int i = 0; i < positions.Length; i++)
        {
            if (_collider.OverlapPoint(positions[i]))
            {
                //a line with two or less points is practically invisible. so ignore it
                if (renderer.positionCount <= 2)
                {
                    for (int j = 0; j < renderer.positionCount; j++)
                        _inkTracker.AddInk();

                    IsBlockingLine = false;

                    Destroy(renderer.gameObject);

                    return;
                }

                anyInside = true;
                break;
            }
        }

        Gradient gradient = new();
        
        //set both start and end of the gradient to either red or white
        GradientColorKey[] colorKeys = {
            new(anyInside ? Color.red : Color.white, 0f),
            new(anyInside ? Color.red : Color.white, 1f)
        };

        //set alpha to max on both ends
        GradientAlphaKey[] alphaKeys = {
            new(1f,0f),
            new(1f,1f)
        };

        gradient.SetKeys(colorKeys, alphaKeys);

        renderer.colorGradient = gradient;
        IsBlockingLine = anyInside;
    }
}
