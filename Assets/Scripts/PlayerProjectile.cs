using UnityEngine;

public abstract class PlayerProjectile : MonoBehaviour, IPlayerProjectile
{
    public abstract void CalculateDirection(RaycastHit hit, bool raycastHit, CardboardGun gun);
}
