using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] int value = 1;
    public int Value { get { return value; } }
}