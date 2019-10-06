using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class RopeSystem : MonoBehaviour
{
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    public Movement playerMovement;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;

    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;

    public GameObject player;
    private float playerHor;
    private float playerVer;
    private bool fireButton;

    //add collision to line
    private Vector3 startPos;
    private Vector3 endPos;
    private bool rotated;
    void Start()
    {
        // 2
        ropeJoint = transform.GetComponentInParent<DistanceJoint2D>();
        
        playerMovement = transform.GetComponentInParent<Movement>();
        ropeJoint.enabled = false;
        playerPosition = transform.parent.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        ropeJoint.connectedBody = ropeHingeAnchorRb;
        player = transform.parent.gameObject.transform.parent.gameObject;
        /////

    }

    void Update()
    {
        // 3

        if (player.name == "Player 1")
        {
            
            playerHor = Input.GetAxis("HorizontalRStick");
            playerVer = Input.GetAxis("VerticalRStick");
            fireButton = Input.GetButtonDown("Fire1");

        }
        if (player.name == "Player 2")
        {
            playerHor = Input.GetAxis("HorizontalRStickP2");
            playerVer = Input.GetAxis("VerticalRStickP2");
            fireButton = Input.GetButtonDown("Fire1P2");
        }

        var aimAngle = Mathf.Atan2(playerVer, playerHor);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }
        if(playerVer == 0 && playerHor == 0)
        {
            crosshairSprite.enabled = false;
        }

        // 4
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        // 5
        playerPosition = transform.position;
        startPos = playerPosition;

        // 6
        if (!ropeAttached)
        {
            SetCrosshairPosition(aimAngle);
        }
        else
        {
            crosshairSprite.enabled = false;
        }
        HandleInput(aimDirection);
        UpdateRopePositions();
        endPos = ropeHingeAnchor.transform.position;
        if (ropeRenderer.transform.Find("Collider") == true)
        {
            BoxCollider2D col = ropeRenderer.transform.GetChild(0).GetComponent<BoxCollider2D>();
            float lineLength = Vector3.Distance(startPos, endPos); // length of line
            col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
            Vector3 midPoint = (startPos + endPos) / 2;
            col.transform.position = midPoint; // setting position of collider object
                                               // Following lines calculate the angle between startPos and endPos
            float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
            if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            if (rotated == false)
            {
                col.transform.Rotate(0, 0, angle);
                rotated = true;
            }
        }

    }
    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled && aimAngle != 0)
        {
            crosshairSprite.enabled = true;
        }
        
        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }
    // 1
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // 2
            if (ropeAttached) return;
            ropeRenderer.enabled = true;
            addColliderToLine();
            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);

            // 3
            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    // 4
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                    transform.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                }
            }
            // 5
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            ResetRope();
        }

    }

    // 6
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
        Destroy(ropeRenderer.transform.GetChild(0).gameObject);
        rotated = false;
    }

    private void UpdateRopePositions()
    {
        // 1
        if (!ropeAttached)
        {
            return;
        }

        // 2
        ropeRenderer.positionCount = ropePositions.Count + 1;
        
        // 3
        for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);

                // 4
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1)
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                // 5
                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

    private void addColliderToLine()
    {
        Debug.Log("Add Collider");
        if (ropeRenderer.transform.Find("Collider") == false)
        {
            BoxCollider2D col = new GameObject("Collider").AddComponent<BoxCollider2D>();
            col.transform.parent = ropeRenderer.transform; // Collider is added as child object of line
            float lineLength = Vector3.Distance(startPos, endPos); // length of line
            col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
            Vector3 midPoint = (startPos + endPos) / 2;
            col.transform.position = midPoint; // setting position of collider object
                                               // Following lines calculate the angle between startPos and endPos
            float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
            if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            col.transform.Rotate(0, 0, angle);
        }
    }
}
