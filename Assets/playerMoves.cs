using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoves : MonoBehaviour
{
    [SerializeField] private DialogueSCRIPT dialogueSCRIPT;

    private const float MoveSpeed = 10f;

    public DialogueSCRIPT DialogueSCRIPT => dialogueSCRIPT;
    public Interactable Interactable { get; set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (dialogueSCRIPT.IsOpen) return;

        /* Vector2 input = new Vector2(x:Input.GetAxisRaw("Horizontal"), y:Input.GetAxisRaw("Vertical"));

         rb.MovePosition(rb.position + input.normalized * (MoveSpeed * Time.fixedDeltaTime)); */

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * MoveSpeed, verticalInput * MoveSpeed);


        if (horizontalInput > 0.01f)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);


        if (Input.GetKeyDown(KeyCode.E) && dialogueSCRIPT.IsOpen == false)
        {            
            Interactable?.Interact(playerMoves:this);

             if(Interactable != null)
            {
                Interactable.Interact(playerMoves: this);
            } //tried this to fix nullref. didnt change anything. get error only after pressing E | Works now.
            
        }

    }

}