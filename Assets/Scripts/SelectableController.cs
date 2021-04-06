using System.Collections.Generic;
using System.Linq;

public class SelectableController
{
    private List<ISelectable> selectables = new List<ISelectable>();

    public SelectableController(IEnumerable<ISelectable> selectables)
    {
        this.selectables = selectables.ToList();
    }

    public void Select(ISelectable selectable)
    {
        foreach(var s in selectables)
        {
            if (s != selectable)
                selectable.UnSelect();
        }
    }

    public void UnSelectAll()
    {
        selectables.ForEach(x => x.UnSelect());
    }

    public void AddSelectableItem(ISelectable selectable)
    {
        if(!selectables.Contains(selectable))
        {
            selectables.Add(selectable);
        }
    }

    public void RemoveSelectableItem(ISelectable selectable)
    {
        selectables.Remove(selectable);
    }
}