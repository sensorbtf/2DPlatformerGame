using UnityEngine;

public class DestructableObject : Enemy
{
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = baseConfig.Health;
    }
}
