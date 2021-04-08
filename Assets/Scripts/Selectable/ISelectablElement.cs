using System.Drawing;

public interface ISelectable<T>
{
    bool IsSelected { get; }

    void Select(T value);

    void UnSelect();
}