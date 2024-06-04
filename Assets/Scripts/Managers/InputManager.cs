using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }

    [SerializeField] private Joystick joystick;
    public float xInput {  get; private set; }
    public float yInput { get; private set; }
    public bool shoting {  get; private set; }
    public bool mouseRight { get; private set; }
    public Vector2 mousePos {  get; private set; }

    public bool clickInput {  get; private set; }

    private Vector2 hospot;
    void Start()
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

   
    // Update is called once per frame
    void Update()
    {
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (CanvasManager.Instance.isOpenUI || MenuSkillUI.Instance.isMenuOp) 
        {
            shoting = false;
            mouseRight = false;
            xInput = 0;
            yInput = 0;
        }
        else
        {
            //if(clickInput)
            //{
                shoting = false;
            //}
            //else
            //{
            //    shoting = Input.GetMouseButton(0);
            //}
            mouseRight = Input.GetMouseButtonDown(1);
            xInput = joystick.Direction.x;
            yInput = joystick.Direction.y;
        }

    }


}
