using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals;

namespace BOSS
{
   public class romperaMovement : MonoBehaviour
   {
      [SerializeField] private GameObject pulsFirePrefab;
      [SerializeField] private Damageable damageable;
      private HealthBar healthBar;
      private Renderer objectRenderer;
      public Vector3 startingPosition;
      public float speed = 3f;
      public float movRange = 16f;
      private bool isMovingLeft = true;
      private float periodCount = -0.5f;
      private Vector3 positionBeforeTeleportaion;
      private Transform player;
      private PlayerData playerData;

      // Start is called before the first frame update
      void Start()
      {
         player = GameObject.Find("Player").transform;
         playerData = player.GetComponent<PlayerData>();
         objectRenderer = GetComponent<Renderer>();
         startingPosition = transform.position;
         healthBar = GameObject.Find("BossHPSlider").GetComponent<HealthBar>();

         healthBar.SetMaxHealth(damageable.entity.maxHealth);

         StartCoroutine(AttackFireball());
      }


      // Update is called once per frame
      void Update()
      {

         if (Time.time - periodCount >= 10)
         {
            Teleportation();
            periodCount = Time.time;
         }


         if (isMovingLeft)
         {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (Mathf.Abs(startingPosition.x - transform.position.x) >= movRange)
            {
               isMovingLeft = false;
            }
         }
         else
         {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (Mathf.Abs(startingPosition.x - transform.position.x) >= movRange)
            {
               isMovingLeft = true;
            }
         }



         if (Mathf.Abs(transform.position.x - startingPosition.x) <= 0.1f)
         {
            periodCount += 0.5f;
         }       
      }

      private void Teleportation()
      {
         positionBeforeTeleportaion = transform.position;
         positionBeforeTeleportaion.z += 38f;
         transform.Translate(transform.position - positionBeforeTeleportaion);
         transform.Rotate(0f, 180f, 0f);

      }

      private IEnumerator AttackFireball() {
         while (true) {
            Rigidbody rb =
                Instantiate(pulsFirePrefab, transform.position - transform.forward + transform.up, Quaternion.identity)
                    .GetComponent<Rigidbody>();

            FireBall fireball = rb.GetComponent<FireBall>();

            float distance = Mathf.Sqrt(Mathf.Pow(player.position.x - rb.position.x, 2) + Mathf.Pow(player.position.z - rb.position.z, 2));
            float height = fireball.GetBottomY() - playerData.getBottomY();                                                

            float speedZ = distance * Mathf.Sqrt(Mathf.Abs(Physics.gravity.y / (2 * height)));            

            Vector3 directionVector = player.position - rb.position;
            directionVector.y = 0;                                    
            rb.velocity = directionVector.normalized * speedZ;

            yield return new WaitForSeconds(2);
         }
      }

      /// <summary>
      /// used in <see cref="Damageable.HealthUpdate"/>
      /// </summary>   
      public void SetHealth(int currentHP) {
         healthBar.SetHealth(currentHP);
      }

      /// <summary>
      /// used in <see cref="Damageable.Died"/>
      /// </summary>      
      public void OnDead() {
         Destroy(gameObject);
      }
   }
}