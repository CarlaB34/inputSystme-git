using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    //represente les input personaliser
    private InputActionAsset m_controlsInput = null;

    private Vector2 m_MovementInput = Vector2.zero;

    [SerializeField]
    private float m_speed =  6f;

    private void Awake()
    {
        //recup la map
        InputActionMap playerMap = m_controlsInput.FindActionMap("Player");

        //recup les input/action volue
        InputAction shootAction = playerMap.FindAction("Shoot");
        //les deleguate = liste de fonction refenrencer (mettre plein de fonction et on pourra touts les recup)
        //linker une fonction en une ligne
        //ctx =nom de ka variable, contexte, contien les info besoin
        //=> c'est une fleche appeler lambda permet d'ecrire sur une seul ligne
        shootAction.performed += (ctx) => { Shoot(); };
        //autre façon mais moins pratique
        //shootAction.performed += Shoot; +  public void Shoot(InputAction.CallbackContext ctx) {}

        //move
        InputAction moveAction = playerMap.FindAction("Move");
        moveAction.performed += (ctx) => { m_MovementInput = ctx.ReadValue<Vector2>(); };
        //eviter qu'il avance alors qu'on appui pas donc on rement a 0
        moveAction.canceled += (ctx) => { m_MovementInput =Vector2.zero; };

        //dit quand les input sont activer
        playerMap.Enable();
    }

    
    public void Update()
    {
        Move(m_MovementInput, Time.deltaTime);
    }

    //V2 _direction = m_MovementInput
    public void Move(Vector2 _Direction, float _DeltaTime)
    {
        //pour faire deplacer en pas diagonal
        _Direction.Normalize();
        //convertir vector2 en 3
        Vector3 movement = new Vector3(_Direction.x, 0f, _Direction.y);
        //sans le translate
        transform.position += movement * m_speed * _DeltaTime;
        
        //Debug.DrawRay(transform.position, _Direction, Color.yellow, 0.2f);
        
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
    }
}
