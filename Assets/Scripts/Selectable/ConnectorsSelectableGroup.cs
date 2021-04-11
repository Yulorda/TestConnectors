using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectorsSelectableGroup
{
    private List<ISelectable<Color>> selectables = new List<ISelectable<Color>>();

    public ConnectorsSelectableGroup(IEnumerable<ISelectable<Color>> selectables)
    {
        this.selectables = selectables.ToList();
    }

    public void SelectGroup(Color color)
    {
        foreach (var selectable in selectables)
        {
            if (!selectable.IsSelected)
                selectable.Select(color);
        }
    }

    public void UnSelectAll()
    {
        foreach (var selectable in selectables)
        {
            selectable.UnSelect();
        }
    }

    public void AddSelectableItem(ISelectable<Color> selectable)
    {
        if (!selectables.Contains(selectable))
        {
            selectables.Add(selectable);
        }
    }

    public void RemoveSelectableItem(ISelectable<Color> selectable)
    {
        selectables.Remove(selectable);
    }

    public bool Contains(ISelectable<Color> selectable)
    {
        return selectables.Contains(selectable);
    }
}