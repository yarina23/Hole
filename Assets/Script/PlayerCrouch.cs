using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float CrouchingSpeed { get; set; } = 15f;
    public float FlattenSphere { get; set; } = 0.5f;

    private float originalScaleY = 1f;
    void Start()
    {
        originalScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.shiftKey.IsPressed())
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, FlattenSphere, 1f), Time.deltaTime * CrouchingSpeed); 
        }
        
        if(transform.localScale.y < originalScaleY && !Keyboard.current.shiftKey.IsPressed())
        {
            RestorePlayerSize();
        }

    }

    void RestorePlayerSize()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, originalScaleY, 1f), Time.deltaTime * CrouchingSpeed);
    }
}
