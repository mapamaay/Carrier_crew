using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Cinemachine;

public class PlayerController1 : MonoBehaviour
{
    //Player number variables//
    [SerializeField]
    private int _playerIndex = 0;

    //Initialization CONTROLLERS

    [SerializeField]
    private GameObject _controler_HandR_Ref;
    [SerializeField]
    private GameObject _controler_HandL_Ref;
    [SerializeField]
    private GameObject _controler_Neck_Ref;
    [SerializeField]
    private GameObject _controler_Armature_Ref;
    [SerializeField]
    private GameObject _controler_Head_Ref;

    // INPUTS Controllers
    public CharacterController controller;//Controller variables//

    //MOVEMENT
    Vector2 stickInput;                 //Mouvement variables//
    public float moveSpeed = 3;
    Vector3 fallVelocity;               //Physics variables//
    public float gravity = -9.81f;
    public float turnSmooth = 0.1f;     //Smoothing variables//
    float turnSmoothVelocity;
    public float joystickDeadzone = 0.1f;
    private float raise_cooldown = 0.0f;

    // RAISE VARIABLES
    private bool isPreparedToRaise = false;
    [SerializeField]
    private float _raisingTime = 0.5f;
    private float _timeSinceRaising = 0.0f;
    private int coeff_raising = 0;
    [SerializeField]
    private float _overheatingValue = 1.0f;
    private bool isRaising = false;
    private float _xPosition;
    private float _zPosition;
    private Quaternion _headQuaternion = Quaternion.Euler(0, 0, 0);

    //EVENTS
    // Event 1 : Prepare to Raise
    public delegate void OnPrepareToRaise();
    public static event OnPrepareToRaise onPrepareToRaise;

    // Event 2 : Raise
    public delegate void OnRaiseBeam(int _playerRaising);
    public static event OnRaiseBeam onRaiseBeam;


    void Start()
    {
        _headQuaternion = gameObject.transform.Find("Armature").transform.Find("1_B_BallDown").transform.Find("2_B_FootDown").transform.Find("3_B_BodyInvisible").transform.Find("4_B_Head").transform.localRotation;
        if (gameObject.tag == "Player 1")
        {
            GameManager.Player1 = gameObject;
            GameManager.Player1.name = "Player 1";
        }
        else if (gameObject.tag == "Player 2")
        {
            GameManager.Player2 = gameObject;
            GameManager.Player2.name = "Player 2";
        }

        #region Camera Parameters
        GameObject.Find("Main Camera").transform.Find("Target_MainCamera").GetComponent<CinemachineTargetGroup>().m_Targets[_playerIndex].target = gameObject.transform;
        GameObject.Find("Main Camera").transform.Find("Follow_MainCamera").GetComponent<CinemachineTargetGroup>().m_Targets[_playerIndex].target = gameObject.transform;
        GameObject.Find("TriggerCamera").transform.Find("Target_TriggerCamera").GetComponent<CinemachineTargetGroup>().m_Targets[_playerIndex].target = gameObject.transform;
        GameObject.Find("TriggerCamera").transform.Find("Follow_TriggerCamera").GetComponent<CinemachineTargetGroup>().m_Targets[_playerIndex].target = gameObject.transform;

        #endregion
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public int GetPlayerIndex()
    {
        return _playerIndex;
    }

    public void SetInputVector(Vector2 inputdirection)
    {
        if (isPreparedToRaise)  
        {
            stickInput = new Vector2(0, 0);
            if (inputdirection.x > 0.99f || inputdirection.y >0.99f)
            {
                // ATTENTION : cooldown TOUT POURRI : à modifier
                raise_cooldown -= 0.2f;
                if (raise_cooldown <= 0.0f)
                {
                    isPreparedToRaise = false;
                    gameObject.GetComponent<Animator>().SetBool("PrepareToRaise", false);
                }
            }

        } // Movement stop
        else
        {
            stickInput = inputdirection;
            gameObject.GetComponent<Animator>().SetFloat("RunBlend", Mathf.Max(Mathf.Abs(stickInput.x), Mathf.Abs(stickInput.y)));

        }
    }

    public void OnInteract(InputActionPhase phase)
    {
        if (phase == InputActionPhase.Started)
        {
            // Event 1 : OnPrepareToRaise
            if (onPrepareToRaise != null)
            {
                onPrepareToRaise();
            }
            if (gameObject == BeamManager.PlayerNorth || gameObject == BeamManager.PlayerSouth)
            {
                if (isPreparedToRaise)
                {
                    #region Décompte HOLD
                    coeff_raising = 1;
                    #endregion
                    #region Déclenchement animation
                    gameObject.GetComponent<Animator>().SetBool("PrepareToRaise", false);
                    gameObject.GetComponent<Animator>().SetBool("TryToRaise", true);
                    #endregion
                } // Action : SOULEVER LA POUTRE
                else
                {
                    #region Positionnement des controllers
                    if (gameObject.transform.tag == "Player 1")
                    {
                        if (BeamManager.Player1IsIn != null)
                        {
                            _controler_HandL_Ref.transform.position = BeamManager.Player1IsIn.transform.Find("HandL").transform.position;
                            _controler_HandR_Ref.transform.position = BeamManager.Player1IsIn.transform.Find("HandR").transform.position;
                            _controler_Armature_Ref.transform.position = BeamManager.Player1IsIn.transform.position;
                        }
                    }
                    else if (gameObject.transform.tag == "Player 2")
                    {
                        if (BeamManager.Player2IsIn != null)
                        {
                            _controler_HandL_Ref.transform.position = BeamManager.Player2IsIn.transform.Find("HandL").transform.position;
                            _controler_HandR_Ref.transform.position = BeamManager.Player2IsIn.transform.Find("HandR").transform.position;
                            _controler_Armature_Ref.transform.position = BeamManager.Player2IsIn.transform.position;
                        }
                    }
                    _controler_Head_Ref.transform.position = GameObject.Find("HeadAim").transform.position;
                    #endregion
                    #region Déclencement animation
                    gameObject.GetComponent<Animator>().SetBool("PrepareToRaise", true);
                    #endregion
                    isPreparedToRaise = true;
                    raise_cooldown = 1.0f;
                } // Action : SE PREPARER A SOULEVER
            }
        }
   
        if (phase == InputActionPhase.Performed)
        {

        }
        if (phase == InputActionPhase.Canceled)
        {
            if (gameObject == BeamManager.PlayerNorth || gameObject == BeamManager.PlayerSouth)
            {

                if (isPreparedToRaise)
                {
                    #region FIN Décompte HOLD
                    coeff_raising = 0;
                    _timeSinceRaising = 0.0f;
                    #endregion
                    #region Déclenchement animation
                    gameObject.GetComponent<Animator>().SetBool("PrepareToRaise", true);
                    gameObject.GetComponent<Animator>().SetBool("TryToRaise", false);
                    #endregion
                } //Action : SOULEVER LA POUTRE

            }
        }
    }
    // Quand le joueur COMMENCE à essayer de soulever la poutre : --> ANIMATION A FAIRE 
    // Si SURCHAUFFE --> ANIMATION A FAIRE 
    // Si relache la poutre --> CANCEL A FAIRE 
    // Quand le joueur TERMINE son soulevage de poutre --> DECLENCHEMENT ANIMATION DE LA POUTRE + ANIMATION PERSOS

    // Quand le joueur S'ELOIGNE de la poutre : re set à 0

    private void Update()
    {
       #region MOUVEMENT
        //Set direction//
        Vector3 direction = new Vector3(-stickInput.x, 0f, -stickInput.y);
        direction = Mathf.Clamp(direction.magnitude, 0f, 1f) * direction.normalized;

        //Gravity//
        //Gravity reset//

        if (controller.isGrounded && fallVelocity.y <= -2)
        {
            fallVelocity.y = -2f;
        }

        //Gravity force over time//
        if (!controller.isGrounded)
        {
            fallVelocity.y += gravity * Time.deltaTime;
            controller.Move(fallVelocity * Time.deltaTime);
        }

        //Move if joystick is pushed far enough//
        if (stickInput.magnitude < joystickDeadzone)
        {
            stickInput = Vector2.zero;
        }

        else
        {
            //Turn//
            float lookAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            //Move//
            controller.Move(direction * moveSpeed * Time.deltaTime * ((stickInput.magnitude - joystickDeadzone) / (1 - joystickDeadzone)));

        }
        #endregion

                
       #region RAISING BEAM : time since raising
        // Raising Beam
        _timeSinceRaising += Time.deltaTime * coeff_raising;

        if (_timeSinceRaising >= _overheatingValue || _timeSinceRaising <_raisingTime)              //SURCHAUFFE 
        {
            if (isRaising)
            {
                // Event 2 : OnRaiseBeam
                if (onRaiseBeam != null)
                {
                    onRaiseBeam(-1);
                }
            }
            isRaising = false;
        }
        else if (_timeSinceRaising > _raisingTime)                //sinon RAISING OK si supérieur à .5
        {
            if (isRaising == false)
            {
                // Event 2 : OnRaiseBeam
                if (onRaiseBeam != null)
                {
                    onRaiseBeam(1);
                }
            }
            isRaising = true;
        }

        #endregion

    }
    public void OnEnable()
    {
        BeamManager.onReinitialization += OnReinitialization;
    }

    public void OnReinitialization(int frameValue)
    {
        if (frameValue == 1)
        {
            gameObject.GetComponent<Animator>().SetBool("RaisingBeam", true);
            gameObject.GetComponent<Animator>().SetBool("TryToRaise", false);
            gameObject.GetComponent<Animator>().SetBool("PrepareToRaise", false);

            _xPosition = _controler_Armature_Ref.transform.position.x;
            _zPosition = _controler_Armature_Ref.transform.position.z;

        } 
        else if (frameValue == 0)
        {
            gameObject.transform.position = _controler_Armature_Ref.transform.position;      // Position non récupérée
            gameObject.transform.rotation = gameObject.transform.Find("Armature").transform.Find("1_B_BallDown").transform.Find("2_B_FootDown").transform.Find("3_B_BodyInvisible").transform.Find("4_B_Head").transform.rotation;         // Rotation bien récupérée

            gameObject.GetComponent<Animator>().SetBool("RaisingBeam", false);              //OK
            gameObject.GetComponent<Animator>().SetBool("PrepareToRaise", false);           //OK
            gameObject.GetComponent<Animator>().SetFloat("RunBlend", 0.0f);                 //OK

            _controler_HandL_Ref.transform.localPosition = new Vector3(0, 0, 0);            // OK
            _controler_HandR_Ref.transform.localPosition = new Vector3(0, 0, 0);                 // OK
            _controler_Armature_Ref.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.transform.Find("Armature").transform.Find("1_B_BallDown").transform.Find("2_B_FootDown").transform.Find("3_B_BodyInvisible").transform.Find("4_B_Head").transform.rotation = _headQuaternion;

        }
        else
        {
            if (gameObject.transform.tag == "Player 1")
            {
                _controler_HandL_Ref.transform.position = BeamManager.Player1IsIn.transform.Find("HandL").transform.position;
                _controler_HandR_Ref.transform.position = BeamManager.Player1IsIn.transform.Find("HandR").transform.position;
                _controler_Armature_Ref.transform.position =
                    new Vector3(_controler_Armature_Ref.transform.position.x - (_xPosition - BeamManager.Player1IsIn.transform.parent.transform.position.x) / 29,
                    _controler_Armature_Ref.transform.position.y,
                    _controler_Armature_Ref.transform.position.z - (_zPosition - BeamManager.Player1IsIn.transform.parent.transform.position.z) / 29);
            }

            else if (gameObject.transform.tag == "Player 2")
            {
                _controler_HandL_Ref.transform.position = BeamManager.Player2IsIn.transform.Find("HandL").transform.position;
                _controler_HandR_Ref.transform.position = BeamManager.Player2IsIn.transform.Find("HandR").transform.position;
                _controler_Armature_Ref.transform.position =
                    new Vector3(_controler_Armature_Ref.transform.position.x - (_xPosition - BeamManager.Player2IsIn.transform.parent.transform.position.x) / 29,
                    _controler_Armature_Ref.transform.position.y,
                    _controler_Armature_Ref.transform.position.z - (_zPosition - BeamManager.Player2IsIn.transform.parent.transform.position.z) / 29);
            }
        }
    }

    public void OnDisable()
    {
        BeamManager.onReinitialization -= OnReinitialization;
    }
}
