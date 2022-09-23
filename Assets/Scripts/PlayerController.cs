using System;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rigidbody2D;
    public VirtualJoystick virtualJoystick;
    public JoystickScript joystickMove;
    
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public PlayerInfo playerInfo;
    
    private Transform _camTransform;

    public float MoveSpeed = 5.0f;
    public float Drag = 0.5f;
    public float TerminalRotationSpeed = 25.0f;
    public int maxHealth = 10;

   
    public string playerNick;
    public int currentHealth;
    public int currentExp;

    public DateTime startTime;
    public DateTime endTime;
    [SerializeField]
    private int stageTime = 51;
    

    public string enemyTag;
    public bool destroyOnDeath;
    public Vector2 initialPosition;

	void Start ()
	{
		//virtualJoystick = GameObject.Find("VirtualJoystick").GetComponent<VirtualJoystick>();

		rigidbody2D = GetComponent<Rigidbody2D>();
        //Controller.1  .maxAngularVelocity = TerminalRotationSpeed;
        rigidbody2D.drag = Drag;

        this.currentHealth = this.maxHealth;
        this.currentExp = 0;
        //this.startTime = DateTime.Now;
        
        this.initialPosition = this.transform.position;
        
        cinemachineVirtualCamera = GameObject.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = gameObject.transform;
        _camTransform = Camera.main.transform;

        GetComponent<ShootBullet>().ReapeatSpawnBullet();
        playerInfo.ReapeatUpdateInfo();
        
        SoundManager.instance.PlaySFX("GameStart");
        SetTime();
	}

	void SetTime()
	{
		this.startTime = DateTime.UtcNow;
		endTime = startTime.AddSeconds(stageTime);

	}
	
	void FixedUpdate() {
		
        Vector3 dir = Vector3.zero;

        // if (virtualJoystick.InputVector != Vector3.zero) {
        //     dir = virtualJoystick.InputVector;
        // }
        if (joystickMove && joystickMove.IsWorking)
	        dir = (Vector3)joystickMove.Output ;
        //transform.Translate(joystickMove.Output * speed * Time.deltaTime);

        
		Vector2  vec2 = Vector2.zero;
		vec2.x = dir.x;
		vec2.y = dir.y;
        //print(vec2);
        rigidbody2D.velocity = (vec2 * MoveSpeed);
        //print(rigidbody2D.velocity);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		//Debug.Log(collider.name);
		
		if(collider.tag == this.enemyTag)
		{
			this.TakeDamage(1);
			Destroy(collider.gameObject);
		}
	}
	
	void TakeDamage (int amount)
	{
			this.currentHealth -= amount;
			RpcPlaySoundDamaged();
			if(this.currentHealth <= 0)
			{
				GameManager.instance.OnEndGame();
			}
	}


	void RpcRespawn ()
	{
		this.transform.position = this.initialPosition;
		SoundManager.instance.PlaySFX("GameStart");
	}
	
	
	public void SendNickToServer(string playerNick)
	{

		this.playerNick = playerNick;
		CmdSendNickToServer(playerNick);
	}


	void CmdSendNickToServer(string playerNick)
	{
		if (string.IsNullOrEmpty(playerNick))
		{
			this.playerNick = "PLAYER" + Random.Range(1, 99);
		}
		else
		{
			this.playerNick = playerNick;
			RpcUpdateNick();
		}
	}

	public void RpcUpdateNick()
	{
		playerInfo.UpdateNick();
	}
	
	public void PlaySoundGetExp()
	{
		SoundManager.instance.PlaySFX("EnemyDamaged");
	}
	
	public void RpcPlaySoundDamaged()
	{
		SoundManager.instance.PlaySFX("PlayerDamaged");
	}
	

	public TimeSpan SurvivalTime()
	{
		if (endTime < DateTime.UtcNow)
		{
			return endTime - endTime ;
		}
		
		return endTime - DateTime.UtcNow ;
	}

	public void GetExp(int exp)
	{
		currentExp += exp;
	}

}