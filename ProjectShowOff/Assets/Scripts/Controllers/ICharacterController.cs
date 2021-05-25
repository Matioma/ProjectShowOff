using UnityEngine;

public interface ICharacterController
{
    void SpecialAction();
    void ReleaseSpecialAction();
    void Move(Vector3 direction);
}
