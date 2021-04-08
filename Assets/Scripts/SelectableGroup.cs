using System.Collections.Generic;
using System.Linq;

public class SelectableGroup
{
    private List<SelectableGroupElemenet> selectables = new List<SelectableGroupElemenet>();

    public SelectableGroup(IEnumerable<SelectableGroupElemenet> selectables)
    {
        this.selectables = selectables.ToList();

        foreach (var selectable in selectables)
        {
            selectable.SelectableGroupEvent += OnSelect;
            selectable.UnSelectableGroupEvent += OnUnSelect;
        }
    }

    private void OnSelect(SelectableGroupElemenet elemenet)
    {
        foreach (var selectable in selectables)
        {
            if (selectable != elemenet)
                selectable.SomeElementInGroupWasSelected();
        }
    }

    private void OnUnSelect()
    {
        foreach (var selectable in selectables)
        {
            selectable.UnSelect();
        }
    }

    public void AddSelectableItem(SelectableGroupElemenet selectable)
    {
        if (!selectables.Contains(selectable))
        {
            selectables.Add(selectable);
        }
    }

    public void RemoveSelectableItem(SelectableGroupElemenet selectable)
    {
        selectables.Remove(selectable);
    }
}