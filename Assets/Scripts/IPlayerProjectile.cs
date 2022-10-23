using UnityEngine;

public interface IPlayerProjectile
{
    void CalculateDirection(RaycastHit hit, bool raycastHit, CardboardGun gun);
}