using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MovementPlayer : MonoBehaviour
{    //Player
    [SerializeField] public Transform player;              // Player Transform Value
    private float playerHeightY;                           // Player height Y value Currently  
    [SerializeField] private float jumpRate;               // Jump Rate
    private bool grounded;                         // Grounded Check
    [SerializeField] private Rigidbody2D _myRigidBody;     // Player Rigid Body
    private Vector3 velos = Vector3.up; //Player Transform
     
     // Camera Field
     [SerializeField] private Camera _camera;               // Camera Field    
     
     // Platforms
       
    public Transform regular;      //Store Platform prefabs 1
                            
    public Transform LeftRight;    //Store Platform prefabs 2
         
    
    private int platformNumber;     // Platform Randomizer

    private float platCheck;       // platform check 
    private float spawnPlatTo;     // Spawn Platforms To 
    private float platformEraser;
    [SerializeField] public GameObject startingBase; 
    private void Start()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();           //Catching
        player = GetComponent<Transform>();                 
        
    }
                                                          
     void Update()
     
      
     
     
     {
        // if (instance1.transform.position.y <= playerHeightY - 10)
         //{
          //   Destroy(instance1)
         // }
         
         
         // GYROSCOPE/ACCELERATOR INPUT FOR THE PLAYER MOVEMENT//
        
         transform.Translate(Input.acceleration.x*Time. smoothDeltaTime*10, 0, 0); 
       
        // PLATFORM INSTANTIATION//

         if (playerHeightY > platCheck)
         {
             platformManager();
         }

         // CAMERA //
         
         playerHeightY = player.position.y; // tracks player current Height Y to equal players Y axis
         float currentCameraHeight = _camera.transform.position.y;  // tracks Camera`s current Height   
         float newCameraHeight = Mathf.Lerp(currentCameraHeight, playerHeightY, Time.deltaTime * 10f);  // new height of camera adjust 
         if (playerHeightY > currentCameraHeight)   // if  PlayerHeightY is higher currentCameraHeight allows the camera to move only upwards
         { 
             _camera.transform.position = new Vector3(_camera.transform.position.x, newCameraHeight, _camera.transform.position.z);
         }
         else
         {   // RESTART SCREEN TRANSITION 
             if (playerHeightY < (currentCameraHeight-6f))
             {   
                 Score.OG2D.CheckHighScore();
                 SceneManager.LoadScene(2);
             }
         }
         
         
         // PLAYER JUMPING / FALLING //
            if(grounded)
            {   _myRigidBody.velocity = Vector2.up*jumpRate;
                     grounded = false;

            }

          
         

     }

   // WRAP SCREEN
     private void FixedUpdate()
     {
         if (player.transform.position.x <= -3f)
         {
             transform.position = new Vector3(2.85f, player.transform.position.y, player.transform.position.z);
         }
         
         if (player.transform.position.x >= 3f)
         {
             transform.position = new Vector3(-2.85f, player.transform.position.y, player.transform.position.z); 
             
         }
         
         // Takes Player current Y and assings current score 
         if (playerHeightY > Score.score)
         {
             Score.score = (int) playerHeightY;
         }
         if (LeftRight.transform.position.y <= player.position.y-10 ||
                   regular.transform.position.y <= player.position.y-10)
         {
             Destroy(startingBase);
               }
     }

     // end of fixedUpdate Method
     void platformManager()
     {
         platCheck = player.position.y + 15;
         platformSpawner(platCheck+15);
        
     }
     void platformSpawner(float floatValue)
     { 
         
         float y = spawnPlatTo;
         
         while (y <= floatValue) {
          platformNumber = Random.Range(1, 3);
          
          
      Vector3 posXY = new Vector3(Random.Range(-3.25f, 3.25f), y,0);
      
      if(platformNumber == 1) {
          Instantiate(regular,posXY,quaternion.identity);
         
      } 
      if(platformNumber == 2) {
         Instantiate(LeftRight,posXY,quaternion.identity);
         
      }
      
      y += Random.Range(1.9f, 2.1f);
      } spawnPlatTo = floatValue;  
      
  }    
     private void OnCollisionEnter2D(Collision2D other)
     {  
         grounded = true;
         Debug.Log("grounded");
         Destroy(other.gameObject,10f);
        
     }
 

}
