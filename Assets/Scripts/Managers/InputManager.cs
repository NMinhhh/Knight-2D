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
        MouseShoting();

    }

    public void MouseShoting()
    {
        hospot = new Vector2(newMouse[0].width / 2, newMouse[0].height / 2);
        Cursor.SetCursor(newMouse[0], hospot, CursorMode.Auto);
    }

    public void MouseClick()
    {
        hospot = new Vector2(newMouse[1].width / 2.2f, 0);
        Cursor.SetCursor(newMouse[1], hospot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        keyESC = Input.GetKeyDown(KeyCode.Escape);
        keyEnter = Input.GetKeyDown(KeyCode.Return);
        if (CanvasManager.Instance.isOpenSettingCV || CanvasManager.Instance.isOpenCV)
        {
            shoting = false;
            mouseRight = false;
            xInput = 0;
            yInput = 0;
           
        }
        else
        {
            mouseRight = Input.GetMouseButtonDown(1);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            shoting = Input.GetMouseButton(0);
        }

    }


}
