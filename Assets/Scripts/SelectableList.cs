using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableList<T> : List<T> where T : class
{
    public int SelectedIndex { get; protected set; } = -1;

    public T SelectedItem => (0 <= SelectedIndex) ? this[SelectedIndex] : default(T);
    public event System.EventHandler<SelectChangedEventArgs> SelectionChanged;

    public class SelectChangedEventArgs
    {
        public T SelectItem;
        public T DeselectItem;
    }

    public bool SelectItem(int index)
    {
        if (index >= this.Count) return false;

        int preIndex = this.SelectedIndex;
        SelectedIndex = index;

        OnSelectedChanged(preIndex);
        return true;
    }

    public bool SelectItem(T element)
    {
        if (0 == Count)
            return false;

        int preIndex = this.SelectedIndex;
        SelectedIndex = IndexOf(element);

        OnSelectedChanged(preIndex);
        return true;
    }

    public bool SelectItem(System.Func<T, bool> selector)
    {
        int preIndex = this.SelectedIndex;

        SelectedIndex = -1;
        for (int i = 0; i < Count; i++)
        {
            if (selector(this[i]))
            {
                SelectedIndex = i;
                break;
            }
        }

        if (preIndex != SelectedIndex)
        {
            OnSelectedChanged(preIndex);
            return true;
        }
        return false;
    }

    private void OnSelectedChanged(int preIndex)
    {
        SelectionChanged?.Invoke(this, new SelectChangedEventArgs
        {
            SelectItem = (0 <= SelectedIndex) ? this[SelectedIndex] : null,
            DeselectItem = (0 <= preIndex) ? this[preIndex] : null
        });
    }

    public bool SelectFirst()
    {
        if (0 == Count) return false;

        int preIndex = this.SelectedIndex;
        this.SelectedIndex = 0;

        OnSelectedChanged(preIndex);
        return true;
    }

    public bool SelectLast()
    {
        if (0 == Count) return false;

        int preIndex = SelectedIndex;
        SelectedIndex = Count - 1;

        OnSelectedChanged(preIndex);
        return true;
    }

    public bool SelectNext(bool Loop = false)
    {
        if (SelectedIndex < 0 || Count == 0) return false;

        int preIndex = SelectedIndex;
        if (Count - 1 <= SelectedIndex)
        {
            if (Loop) SelectedIndex = 0;
            else  return false;
        }
        else
        {
            SelectedIndex++;
        }

        OnSelectedChanged(preIndex);

        return true;
    }


    public bool SelectPrev(bool Loop = false)
    {
        if (SelectedIndex < 0 || Count == 0) return false;

        int preIndex = SelectedIndex;

        if (0 == SelectedIndex)
        {
            if (Loop) SelectedIndex = Count - 1;
            else return false;
        }
        else
        {
            SelectedIndex--;
        }

        OnSelectedChanged(preIndex);
        return true;
    }
}
