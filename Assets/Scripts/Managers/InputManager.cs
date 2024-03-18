using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    [SerializeField] private Texture2D[] newMouse;
    public float xInput {  get; private set; }
    public float yInput { get; private set; }
    public bool shoting {  get; private set; }
    public bool mouseRight { get; private set; }
    public Vector2 mousePos {  get; private set; }

    public bool keyESC {  get; private set; }

    public bool keyEnter { get; private set; }

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
        DontDestroyOnLoad(gameObject);
        MouseShoting();

    }

    public void MouseShoting()
    {
        hospot = new Vector2(newMouse[0].width / 2, newMouse[0].height / 2);
        Cursor.SetCursor(newMouse[0], hospot, CursorMode.Auto);
        clickInput = false;
    }

    public void MouseClick()
    {
        clickInput = true;
        hospot = new Vector2(newMouse[1].width / 2.2f, 0);
        Cursor.SetCursor(newMouse[1], hospot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        keyESC = Input.GetKeyDown(KeyCode.Escape);
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (CanvasManager.Instance.isOpenUI)
        {
            shoting = false;
            mouseRight = false;
            xInput = 0;
            yInput = 0;
           
        }
        else
        {
            if(clickInput)
            {
                shoting = false;
            }
            else
            {
                shoting = Input.GetMouseButton(0);
            }
            mouseRight = Input.GetMouseButtonDown(1);
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
        }

    }


}
