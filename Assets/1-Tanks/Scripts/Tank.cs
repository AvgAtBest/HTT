using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Tanks
{
    public class Tank : MonoBehaviour
    {
        public float speed = 20f;       // Travel distance over time
        public float fuelDuration = 2f; // Time allowed for movement
        [Header("Bullets")]
        public float bulletSpeed = 10f; // Speed to send bullet traveling
        public GameObject bulletPrefab; // Original copy of bullet
        [Header("Explosion")]
        public float explosionForce = 5f;   // Force of explosion
        [Header("Fuel UI")]
        public Transform fuelSliderParent;  // The parent of the UI Canvas
        public GameObject fuelSliderPrefab; // The slider prefab to spawn to UI Canvas
        public Vector3 offset = new Vector3(0, 2f, 0); // Position of the fuel slider
        [Header("References")]
        public Transform gun;               // Reference to gun for rotating the turret
        public Transform spawnPoint;        // Transform point to spawn the bullet
        [Header("Components")]
        public Rigidbody2D rigid;           // Reference to Rigidbody component
        public Health health;               // Reference to Health component

        private float fuelTimer = 0f;       // Elapsed time of movement (fuel)
        private Slider fuelSlider;          // Reference to newly spawned slider (UI)
        private bool isPlaying = false;     // Is this Tank currently playing?

        #region Unity Events
        private void Start()
        {
            // Reset timer to move duration (in seconds)
            fuelTimer = fuelDuration;
            // Spawn the UI under canvas
            SpawnUI();
            //gun = GameObject.Find("Gun").GetComponent<Transform>();
            //spawnPoint = GameObject.Find("Spawnpoint").GetComponent<Transform>();
        }
        private void Update()
        {
            // Update UI's position
            UpdateUI();

            // Handle movement for the Tank
            Move();
            RotateGunToMouse();
            // If we press fire button
            if(Input.GetButtonDown("Fire1"))
            {
                // Shoot bullet out of Gun
                Shoot();
            }
        }
        private void OnDestroy()
        {
            // If there is a fuel slider
            if (fuelSlider)
            {
                // Remove the fuel slider ui
                Destroy(fuelSlider.gameObject);
            }
        }
        private void Reset()
        {

        }
        #endregion

        #region Custom Functions
        private void SpawnUI()
        {
            // Spawn the fuelSlider into the canvas
            GameObject clone = Instantiate(fuelSliderPrefab, fuelSliderParent);
            // Rename the slider
            clone.name = name + "_Fuel";
            // Store Slider component from clone
            fuelSlider = clone.GetComponent<Slider>();
        }
        private void UpdateUI()
        {
            // Convert to screen position
            Vector3 uiPos = Camera.main.WorldToScreenPoint(transform.position + offset);
            // Update slider position
            fuelSlider.transform.position = uiPos;
            // Update the value of the slider
            fuelSlider.value = fuelTimer / fuelDuration;
        }
        private void Move()
        {
            // fuel timer hasn't reached zero yet?
            if (fuelTimer >= 0f)
            {
                // Get horizontal movement i.e, "W" and "D" keys
                float inputH = Input.GetAxis("Horizontal");
                // If the player is pressing one of those keys
                if (inputH != 0)
                {
                    // Count down the timer
                    fuelTimer -= Time.deltaTime;
                }

                // Move the rigidbody
                rigid.velocity = new Vector2(inputH * speed, rigid.velocity.y);
            }
        }
        private void Shoot()
        {
            // Instantiate a new bullet
            GameObject clone = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            // Get Rigidbody from bullet
            Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
            // Add Rigidbody force in the direction of the gun
            rigid.AddForce(gun.right * bulletSpeed, ForceMode2D.Impulse);

        }
        private void RotateGunToMouse()
        {
            // Convert mouse screen to world position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Calculate direction (target - current)
            Vector3 direction = mousePos - gun.position;
            // Calculate gun angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Rotate gun to angle
            gun.eulerAngles = new Vector3(0, 0, angle);
        }
        public void Explode()
        {
            // Search for all components in children that have the "Breakable" script attached
            Breakable[] parts = GetComponentsInChildren<Breakable>();
            // Loop through all breakable parts
            foreach (var part in parts)
            {
                // Detatch from parent
                part.transform.SetParent(null);
                // Check if it doesn't have a Rigidbody2D
                if (part.transform.GetComponent<Rigidbody2D>() == null)
                {
                    // Add a Rigidbody2D
                    Rigidbody2D partRigid = part.gameObject.AddComponent<Rigidbody2D>();
                    // Get direction from part's position to health's last hit point
                    Vector3 force = (part.transform.position - health.lastHitPoint).normalized;
                    // Add force in that direction using explosion force
                    partRigid.AddForce(force * explosionForce, ForceMode2D.Impulse);
                }
                // Check if it doesn't have a Collider2D
                if (part.transform.GetComponent<Collider2D>() == null)
                {
                    // Add a PolygonCollider2D
                    part.gameObject.AddComponent<PolygonCollider2D>();
                }
            }
        }
        public void Died()
        {
            Explode();

            // Destroy Self
            Destroy(gameObject);
        }
        #endregion
    }
}
