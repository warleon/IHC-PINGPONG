using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class RealisticPaddle : MonoBehaviour, ITransformer
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _smoothFactor = 25f;   // smoothing en Update
    [SerializeField] private float _maxSpeed = 8f;        // límite velocidad realista
    [SerializeField] private float _maxAngularSpeed = 20f;

    private IGrabbable _grabbable;
    private Pose _grabDeltaLocalPose;
    private Vector3 _lastPosition;
    private Quaternion _lastRotation;

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable;
    }

    public void BeginTransform()
    {
        Pose grabPoint = _grabbable.GrabPoints[0];
        Transform t = _rb.transform;

        _grabDeltaLocalPose = new Pose(
            t.InverseTransformVector(grabPoint.position - t.position),
            Quaternion.Inverse(grabPoint.rotation) * t.rotation
        );

        _lastPosition = _rb.position;
        _lastRotation = _rb.rotation;
    }

    public void UpdateTransform()
    {
        Pose grabPoint = _grabbable.GrabPoints[0];
        Vector3 targetPos = grabPoint.position - _rb.transform.TransformVector(_grabDeltaLocalPose.position);
        Quaternion targetRot = grabPoint.rotation * _grabDeltaLocalPose.rotation;

        // calcular velocidad real utilizando diferencias del frame
        Vector3 desiredVelocity = (targetPos - _lastPosition) * _smoothFactor;
        Vector3 clampedVelocity = Vector3.ClampMagnitude(desiredVelocity, _maxSpeed);

        _rb.velocity = clampedVelocity;

        // rotación con velocidad angular física
        Quaternion deltaRot = targetRot * Quaternion.Inverse(_lastRotation);
        deltaRot.ToAngleAxis(out float angle, out Vector3 axis);
        Vector3 angularVelocity = axis * angle * Mathf.Deg2Rad * _smoothFactor;
        _rb.angularVelocity = Vector3.ClampMagnitude(angularVelocity, _maxAngularSpeed);

        _lastPosition = targetPos;
        _lastRotation = targetRot;
    }

    public void EndTransform()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
