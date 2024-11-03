using UnityEngine;

public interface IInspectable
{
    void Inspect();
}

public class Inspectable : MonoBehaviour, IInspectable
{
    public virtual void Inspect()
    {
    }
}
