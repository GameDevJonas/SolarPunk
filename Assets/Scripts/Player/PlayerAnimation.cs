using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerManager manager;
    [SerializeField] private Transform spriteObject;
    private float currentAngle;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = manager.movement.targetAngle;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAngles();
    }

    private void CheckForAngles()
    {
        if (currentAngle != manager.movement.targetAngle)
        {
            Flip();
            currentAngle = manager.movement.targetAngle;
        }
    }

    private void Flip()
    {
        spriteObject.localScale = new Vector3(spriteObject.localScale.x * -1, spriteObject.localScale.y, spriteObject.localScale.z);
    }
}
