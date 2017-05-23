using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Helpers;

public enum StateMoving
{
    Forwards,
    Backwards,
    NoMove
}
public class ShipController : MonoBehaviour
{
    // Controller State from the Ship
    public ControllerState ControllerState;
    // Charater Parameters from the Ship
    Rigidbody RB;
    public CharacterParameters CharacterParameters;
    public int amunnition;
    private ShipBulletsController ShipBullets;
    public int maxAmunition = 1000;
    public int MaxHealth=100;
    public int Health;
    public bool HandleCollisions;
    public Transform SinkingDir;
    public StateMoving stateMoving=StateMoving.NoMove;
    public bool IsDeath;
    public void RespawnAt(Transform spawnPoint   )
    {
        RB = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.identity;
        Health = MaxHealth;
        amunnition = maxAmunition;
        IsDeath = false;
        GetComponent<ShipBulletsController>().HandleInputs = true;
        HandleCollisions = true;
        RB.useGravity = false;
       
        CharacterParameters.currentVelocityBackwards = 0.0f;
        CharacterParameters.currentVelocityForwards = 0.0f;
        transform.position = spawnPoint.position;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        //  Debug.Log(Health+" "+damage);
        if (Health <= 0)
        {
            LevelManager.Instance.KillPlayer(false);
            Debug.Log("kill");

        }

    }
    public void Kill(bool withRotation)
    {
        Health = 0;
        StartCoroutine(KillAnimation(1f,withRotation));
        GetComponent<ShipBulletsController>().HandleInputs = false;
        HandleCollisions = false;
        IsDeath = true;

        RB.useGravity = true;
      
    }
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        ControllerState = new ControllerState();
        Transform s = Helpers.Helpers.FindDeepChild(transform, "SpawnBulletFront");
        EffectsManager.Instance.CreateFireThrower(gameObject, s.position, s.rotation);

        Transform TransSmoke = Helpers.Helpers.FindDeepChild(transform, "SpawnEffects");
        EffectsManager.Instance.CreateSmokeDamage(gameObject,TransSmoke.position, TransSmoke.rotation,transform.name);
        EffectsManager.Instance.CreateTrailBoat(gameObject, TransSmoke.position, TransSmoke.rotation);
        EffectsManager.Instance.CreateMistBoat(gameObject, TransSmoke.position, TransSmoke.rotation);
    }
    void Awake()
    {
        RB = GetComponent<Rigidbody>();
        Health = MaxHealth;
        HandleCollisions = true;
    }
    void Update()
    {
       // StartCoroutine(BalancingAnimation(1));
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
          stateMoving = StateMoving.NoMove;
        if (HandleCollisions)
        {
            if (!IsDeath)
            {
                //if (CharacterParameters.currentVelocityForwards > 0.0f)
                //{

                //    EffectsManager.Instance.UpdateTrailBoat(-CharacterParameters.currentVelocityForwards);
                //}
                //else if (CharacterParameters.currentVelocityBackwards > 0.0f)
                //{
                //    EffectsManager.Instance.UpdateTrailBoat(CharacterParameters.currentVelocityBackwards);
                //}
                EffectsManager.Instance.UpdateDamage(transform.name,Health,true,EnemyShipBehaivour.Following);
                Backwards();
                Forwards();
                // modify Velocity between max and min
                CharacterParameters.currentVelocityForwards = Mathf.Clamp(CharacterParameters.currentVelocityForwards, CharacterParameters.initialVelocity, CharacterParameters.finalVelocityForwards);
                CharacterParameters.currentVelocityBackwards = Mathf.Clamp(CharacterParameters.currentVelocityBackwards, CharacterParameters.initialVelocity, CharacterParameters.finalVelocityBackwards);
                // set rotation to object getting the Angle from Inpt GetAxix("Horiontal") 
                transform.rotation = transform.rotation * Quaternion.AngleAxis(Input.GetAxis("Horizontal") * CharacterParameters.rotationSpeed, Vector3.up * Time.deltaTime); ;
            }
        }
    }
    // on trigger enter
    void OnTriggerEnter(Collider collision)
    {
        // if collider tag is "Wall"
        if (collision.gameObject.tag == "Wall" )
        {

            // if is moving Forwads
            if (CharacterParameters.currentVelocityForwards > 0.0f)
            {
                // put current velocity forwards to 0.0f
                CharacterParameters.currentVelocityForwards = 0.0f;
                // set true is colliding front var
                ControllerState.isCollidingFront = true;
            }
            // if is moving backwards
            if(CharacterParameters.currentVelocityBackwards>0.0f)
            {
                // put current velocity Backwards to 0.0f
                CharacterParameters.currentVelocityBackwards = 0.0f;
                // set true is colliding back var  
                ControllerState.isCollidingBack = true;
            }      
        }
    }
    // on trigger exit 
    void OnTriggerExit(Collider collider)
    {
        // if collider has tag is "Wall"
       if( collider.gameObject.tag=="Wall" )
        {
            // reset all vars from CollidersState 
            ControllerState.Reset();
        }
    }
    public float Decelerate(float velocity)
    {
        velocity = velocity - (CharacterParameters.decelerationRate * Time.deltaTime);
        // if current velocity forwards is bigger to 0
        if (velocity > 0)
            // translate to that velocity
            transform.Translate(0, 0, velocity);
        else
            // stop de moviment
            transform.Translate(0, 0, 0);
        return velocity;
    }
    // function of movement Backwards
    void Backwards(){
       
        if (Input.GetKey(KeyCode.S) && !ControllerState.isCollidingBack)
        {
            stateMoving = StateMoving.Backwards;
            //increments current velocity backwards
            CharacterParameters.currentVelocityBackwards = CharacterParameters.currentVelocityBackwards + (CharacterParameters.accelerationRate * Time.deltaTime);
            // translate object
            transform.Translate(0, 0, -CharacterParameters.currentVelocityBackwards);
        }
        else
        {
            // decrements current velocity backwards
            CharacterParameters.currentVelocityBackwards = CharacterParameters.currentVelocityBackwards - (CharacterParameters.decelerationRate * Time.deltaTime);
            // if current velocity backwards is bigger than 0
            if (CharacterParameters.currentVelocityBackwards > 0)
                // translate the object
                transform.Translate(0, 0, -CharacterParameters.currentVelocityBackwards);
            else
                // stop the movement
                transform.Translate(0, 0, 0);
        }
    }
    //function on Moviment Forwards
    void Forwards()
    {
     
        if (Input.GetKey(KeyCode.W) && !ControllerState.isCollidingFront)
        {
            stateMoving = StateMoving.Forwards;
            // increments current velocity forwards 
            CharacterParameters.currentVelocityForwards = CharacterParameters.currentVelocityForwards + (CharacterParameters.accelerationRate * Time.deltaTime);
            // move object 
            transform.Translate(0, 0, CharacterParameters.currentVelocityForwards);
        }
        else
        {
            // decrements current velocity forwards
            CharacterParameters.currentVelocityForwards = CharacterParameters.currentVelocityForwards - (CharacterParameters.decelerationRate * Time.deltaTime);
            // if current velocity forwards is bigger to 0
            if (CharacterParameters.currentVelocityForwards > 0)
                // translate to that velocity
                transform.Translate(0, 0, CharacterParameters.currentVelocityForwards);
            else
                // stop de moviment
                transform.Translate(0, 0, 0);
        }
    }
    IEnumerator KillAnimation(float num,bool withRotation)
    {
        yield return null;
        var heading = SinkingDir.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        direction.y = 0;
        float move = 10f*Time.deltaTime;
        // prova d baixar la vida al barco
        if(withRotation)
        GetComponent<Rigidbody>().AddTorque(new Vector3(5f, 50f, 5f));
        GetComponent<Rigidbody>().AddForce(new Vector3(0.5F, 0, 0.5F));
       // transform.position = Vector3.MoveTowards(transform.position, SinkingDir.position,move);
        // transform.MoveTowards(Vector3.Lerp(transform.position,SinkingDir.position,Time.deltaTime*5f);   
        
        //transform.rotation=Quaternion.Slerp(transform.rotation,transform.rotation*Quaternion.Euler(50f,50f,50f),Time.deltaTime*5f);
    }
        
}

