using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    [SerializeField] private GameObject newMouse;
    public float xInput {  get; private set; }
    public float yInput { get; private set; }
    public bool shoting {  get; private set; }
    public Vector2 mousePos {  get; private set; }
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
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newMouse.transform.position = mousePos;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        shoting = Input.GetMouseButtonDown(0);
    }
}
