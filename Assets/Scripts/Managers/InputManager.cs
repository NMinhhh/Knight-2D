using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }

    [SerializeField] private Joystick joystick;
    public float xInput {  get; private set; }
    public float yInput { get; private set; }


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
   
    void Update()
    {
        
        if (CanvasManager.Instance.isOpenUI || MenuSkillUI.Instance.isMenuOp) 
        {
            xInput = 0;
            yInput = 0;
        }
        else
        {
            xInput = joystick.Direction.x;
            yInput = joystick.Direction.y;
        }

    }


}
