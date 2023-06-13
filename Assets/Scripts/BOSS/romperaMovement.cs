using DamageSystem.ReceiveDamage.Elementals;
using System.Collections;
using UnityEngine;

namespace BOSS {
    public class romperaMovement : MonoBehaviour {
        [SerializeField] private GameObject pulsFirePrefab;
        [SerializeField] private Damageable damageable;

        [SerializeField] private Transform wall1;
        [SerializeField] private Transform wall2;
        [SerializeField] private Transform wall3;
        [SerializeField] private Transform wall4;

        public GameObject chartWatykanski;

        private HealthBar healthBar;


        public float speed = 3f;
        public float movRange = 16f;
        private bool isMovingLeft = true;
        private float periodCount = -0.5f;
        private int TPcounter = 1;

        public Vector3 startingPosition;

        private Transform player;
        private PlayerData playerData;

        // Start is called before the first frame update
        void Start() {
            player = GameObject.Find("Player").transform;
            playerData = player.GetComponent<PlayerData>();

            healthBar = GameObject.Find("BossHPSlider").GetComponent<HealthBar>();
            healthBar.SetMaxHealth(damageable.entity.maxHealth);

            startingPosition = transform.position;

            StartCoroutine(AttackFireball());
        }


        // Update is called once per frame
        void Update() {

            if (Time.time - periodCount >= 5) {
                Teleportation();
                periodCount = Time.time;
            }


            if (TPcounter % 4 == 1 || TPcounter % 4 == 2) {

                if (isMovingLeft) {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);

                    if (Mathf.Abs(startingPosition.x - transform.position.x) >= movRange) {
                        isMovingLeft = false;
                    }
                } else {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);

                    if (Mathf.Abs(startingPosition.x - transform.position.x) >= movRange) {
                        isMovingLeft = true;
                    }
                }
            } else if (TPcounter % 4 == 3 || TPcounter % 4 == 0) {


                if (isMovingLeft) {
                    transform.Translate(Vector3.right * speed * Time.deltaTime);

                    if (Mathf.Abs(wall3.position.z - transform.position.z) >= movRange) {
                        isMovingLeft = false;
                    }
                } else {
                    transform.Translate(Vector3.left * speed * Time.deltaTime);

                    if (Mathf.Abs(wall4.position.z - transform.position.z) >= movRange) {
                        isMovingLeft = true;
                    }
                }

            }

            if (Mathf.Abs(transform.position.x - startingPosition.x) <= 0.1f) {
                periodCount += 0.5f;
            }

        }

        private void chartSpawn() {
            Instantiate(chartWatykanski, transform.position, Quaternion.identity);
        }

        private void Teleportation() {
            if (TPcounter % 4 == 1) {
                Vector3 newPosition = new Vector3(wall2.position.x, transform.position.y, wall2.position.z + 1f);
                transform.position = newPosition;
                transform.Rotate(0f, 180f, 0f);
                chartSpawn();
            } else if (TPcounter % 4 == 2) {

                Vector3 newPosition2 = new Vector3(wall3.position.x + 1f, transform.position.y, wall3.position.z - 1f);
                transform.position = newPosition2;
                transform.Rotate(0f, 90f, 0f);
                // chartSpawn();
                chartSpawn();

            } else if (TPcounter % 4 == 3) {

                Vector3 newPosition3 = new Vector3(wall4.position.x - 1f, transform.position.y, wall4.position.z);
                transform.position = newPosition3;
                transform.Rotate(0f, 180f, 0f);
                chartSpawn();

            } else if (TPcounter % 4 == 0) {

                Vector3 newPosition4 = new Vector3(wall1.position.x, transform.position.y, wall1.position.z - 1f);
                transform.position = newPosition4;
                transform.Rotate(0f, 270f, 0f);
                // chartSpawn();
                chartSpawn();

            }

            TPcounter += 1;

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

                yield return new WaitForSeconds(10);
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