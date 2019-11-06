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
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    public EdgeCollider2D edgeCollider;
    public LineRenderer ropeRenderer;

    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;
    private bool firebuttonDown;
    private bool firebuttonUp;
    public GameObject player;
    private float playerHor;
    private float playerVer;

    public AudioSource hook;

    //add collision to line
    private Vector3 startPos;
    private Vector3 endPos;
    void Start()
    {
        // 2
        ropeJoint = transform.GetComponentInParent<DistanceJoint2D>();
        ropeJoint.enabled = false;
        playerPosition = transform.parent.position;
        ropeHingeAnchor = transform.GetChild(0).gameObject;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        ropeJoint.connectedBody = ropeHingeAnchorRb;
        player = transform.parent.gameObject.transform.parent.gameObject;
        hook = GetComponent<AudioSource>();

    }

    void Update()
    {
        // 3
        if (player.name == "Player 4")
        {
            playerHor = Input.GetAxis("HorizontalRStickP4");
            playerVer = Input.GetAxis("VerticalRStickP4");
            firebuttonDown = Input.GetButtonDown("Fire1P4");
            firebuttonUp = Input.GetButtonUp("Fire1P4");
        }
        if (player.name == "Player 1")
        {
            
            playerHor = Input.GetAxis("HorizontalRStick");
            playerVer = Input.GetAxis("VerticalRStick");
            firebuttonDown = Input.GetButtonDown("Fire1");
            firebuttonUp = Input.GetButtonUp("Fire1");
        }
        if (player.name == "Player 2")
        {
            playerHor = Input.GetAxis("HorizontalRStickP2");
            playerVer = Input.GetAxis("VerticalRStickP2");
            firebuttonDown = Input.GetButtonDown("Fire1P2");
            firebuttonUp = Input.GetButtonUp("Fire1P2");
        }
        if (player.name == "Player 3")
        {
            playerHor = Input.GetAxis("HorizontalRStickP3");
            playerVer = Input.GetAxis("VerticalRStickP3");
            firebuttonDown = Input.GetButtonDown("Fire1P3");
            firebuttonUp = Input.GetButtonUp("Fire1P3");
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
        startPos = transform.localPosition + new Vector3(0.1f, 0.1f, 0.1f);
        endPos = ropeHingeAnchor.transform.localPosition;
        Vector2[] points = new Vector2[2]
        {
            startPos,
            endPos
        };
        edgeCollider.points = points.ToArray();
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
        if (firebuttonDown)
        {
            // 2
            Debug.Log("Fire");
            if (ropeAttached) return;
            ropeRenderer.enabled = true;
            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);
            edgeCollider.enabled = true;

            // 3
            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    // 4
                    // Jump slightly to distance the player a little from the ground after grappling to something.
                   // transform.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;
                    hook.Play();
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

        if (firebuttonUp) 
        {
            ResetRope();
            edgeCollider.enabled = false;
        }

    }

    // 6
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
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
}
