using UnityEngine;
using RayFire;

public class Caught : MonoBehaviour
{
    private TornadoScript tornadoReference;
    private SpringJoint spring;
    private Rigidbody rigid;
    private RayfireRigid rayfireRigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rayfireRigid = GetComponent<RayfireRigid>();
    }

    void Update()
    {
        if (spring != null)
        {
            Vector3 newPosition = spring.connectedAnchor;
            newPosition.y = transform.position.y;
            spring.connectedAnchor = newPosition;
        }
    }

    void FixedUpdate()
    {
        if (tornadoReference != null && rigid != null)
        {
            Vector3 direction = transform.position - tornadoReference.transform.position;
            Vector3 projection = Vector3.ProjectOnPlane(direction, tornadoReference.GetRotationAxis());
            projection.Normalize();
            Vector3 normal = Quaternion.AngleAxis(130, tornadoReference.GetRotationAxis()) * projection;
            normal = Quaternion.AngleAxis(tornadoReference.lift, projection) * normal;
            rigid.AddForce(normal * tornadoReference.GetStrength(), ForceMode.Force);

            if (rayfireRigid != null && normal.magnitude * tornadoReference.GetStrength() > 5f)
            {
                rayfireRigid.Demolish();
            }
        }
    }

    public void Init(TornadoScript tornadoRef, float springForce, float damper, float maxDistance, float minDistance)
    {
        enabled = true;
        tornadoReference = tornadoRef;

        spring = gameObject.AddComponent<SpringJoint>();
        spring.spring = springForce;
        spring.damper = damper;
        spring.maxDistance = maxDistance;
        spring.minDistance = minDistance;
        spring.connectedBody = tornadoRef.gameObject.GetComponent<Rigidbody>();
        spring.autoConfigureConnectedAnchor = false;

        Vector3 initialPosition = Vector3.zero;
        initialPosition.y = transform.position.y;
        spring.connectedAnchor = initialPosition;
    }

    public void Release()
    {
        enabled = false;
        if (spring != null)
        {
            Destroy(spring);
        }
    }
}