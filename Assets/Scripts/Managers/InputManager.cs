using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    [SerializeField] private Texture2D newMouse;
    public float xInput {  get; private set; }
    public float yInput { get; private set; }
    public bool shoting {  get; private set; }
    public Vector2 mousePos {  get; private set; }

    public bool keyESC {  get; private set; }

    public bool keyEnter { get; private set; }

    private Vector2 hospot;
    void Start()
    {
        hospot = new Vector2(newMouse.width / 2, newMouse.height / 2);
        Cursor.SetCursor(newMouse, hospot, CursorMode.Auto);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        keyESC = Input.GetKeyDown(KeyCode.Escape);
        keyEnter = Input.GetKeyDown(KeyCode.Return);
        if (CanvasManager.Instance.isOpenSettingCV || CanvasManager.Instance.isOpenCV)
        {
            return;
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        shoting = Input.GetMouseButton(0);
    }
}
