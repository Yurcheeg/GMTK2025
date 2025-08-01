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
        Vector3[] positions = new Vector3[renderer.positionCount];
        renderer.GetPositions(positions);

        Gradient gradient = new();

        bool anyInside = false;

        for (int i = 0; i < positions.Length; i++)
        {
            if (_collider.bounds.Contains(positions[i]))
            {
                //hack
                if (renderer.positionCount <= 2)
                {
                    Destroy(renderer);
                    anyInside = false;
                    for (int j = 0; j < renderer.positionCount; j++)
                        _inkTracker.AddInk();
                    //unsync w/ slider
                    continue;
                }

                anyInside = true;
                break;
            }
        }

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
