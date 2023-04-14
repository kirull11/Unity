using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserControlSystem;

public sealed class SelectableOutlinerPresenter : MonoBehaviour
{
    [SerializeField]
    private ColorFrationSet [] _colourFractionSset;

    [SerializeField] private SelectableValue _selectedObject;
    private Outline _currentOutline;

    private void Start()
    {
        _selectedObject.OnNewValue += ChangeSelection;
    }

    private void ChangeSelection(ISelectable selected)
    {
        if (selected is not MonoBehaviour selectable)
        {
            CleanCurrentOutline();
            return;
        }
        if (selectable.TryGetComponent<Outline>(out _))
        {
            return;
        }
        CleanCurrentOutline();
        var outline = selectable.gameObject.AddComponent<Outline>();

        SetOutlineParameters(outline);

        if (selectable.TryGetComponent<FactionMember>(out var candid))
        {
            var pair = _colourFractionSset.FirstOrDefault(pair => pair.Fraction == candid.FactionId);
            if (!pair.Equals(default(ColorFrationSet)))
            {
                SetOutlineParameters(outline, pair.Color);
            }

        }
        _currentOutline = outline;
    }

    private void CleanCurrentOutline()
    {
        if (_currentOutline != null)
        {
            Destroy(_currentOutline);
        }
    }

    private void SetOutlineParameters(Outline outline)
    {
        SetOutlineParameters(outline, Color.red);
    }

    private void SetOutlineParameters(Outline outline, Color colour)
    {
        outline.OutlineColor = colour;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 4f;
    }

    private void OnDestroy()
    {
        _selectedObject.OnNewValue -= ChangeSelection;
    }

    [Serializable]
    private struct ColorFrationSet
    {
        public int Fraction;
        public Color Color;
    }
}