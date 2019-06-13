using UnityEngine;
using UnityEngine.Events;
using System;

/*
 * CLASS MonoBehaviourEvents
 * -------------------------
 * A pretty basic class that exposes UnityEvent classes in the Unity editor
 * for each of the MonoBehaviour messages
 * -------------------------
 */
public class MonoBehaviourEvents : MonoBehaviour
{
    /*
     * PUBLIC TYPEDEFS 
     */
    [Serializable] public class IntEvent : UnityEvent<int> { };
    [Serializable] public class BoolEvent : UnityEvent<bool> { };
    [Serializable] public class FloatArrayAndIntEvent : UnityEvent<float[], int> { };
    [Serializable] public class CollisionEvent : UnityEvent<Collision> { };
    [Serializable] public class Collision2DEvent : UnityEvent<Collision2D> { };
    [Serializable] public class ControllerColliderHitEvent : UnityEvent<ControllerColliderHit> { };
    [Serializable] public class FloatEvent : UnityEvent<float> { };
    [Serializable] public class Joint2DEvent : UnityEvent<Joint2D> { };
    [Serializable] public class GameObjectEvent : UnityEvent<GameObject> { };
    [Serializable] public class RenderTextureAndRenderTextureEvent : UnityEvent<RenderTexture, RenderTexture> { };
    [Serializable] public class ColliderEvent : UnityEvent<Collider> { };
    [Serializable] public class Collider2DEvent : UnityEvent<Collider2D> { };

    /*
     * PUBLIC DATA
     */ 
    
    [SerializeField]
    private UnityEvent _awake;
    public UnityEvent awake { get { return _awake; } }
    [SerializeField]
    private UnityEvent _fixedUpdate;
    public UnityEvent fixedUpdate { get { return _fixedUpdate; } }
    [SerializeField]
    private UnityEvent _lateUpdate;
    public UnityEvent lateUpdate { get { return _lateUpdate; } }
    [SerializeField]
    private IntEvent _onAnimatorIK;
    public IntEvent onAnimatorIK { get { return _onAnimatorIK; } }
    [SerializeField]
    private UnityEvent _onAnimatorMove;
    public UnityEvent onAnimatorMove { get { return _onAnimatorMove; } }
    [SerializeField]
    private BoolEvent _onApplicationFocus;
    public BoolEvent onApplicationFocus { get { return _onApplicationFocus; } }
    [SerializeField]
    private BoolEvent _onApplicationPause;
    public BoolEvent onApplicationPause { get { return _onApplicationPause; } }
    [SerializeField]
    private UnityEvent _onApplicationQuit;
    public UnityEvent onApplicationQuit { get { return _onApplicationQuit; } }
    [SerializeField]
    private FloatArrayAndIntEvent _onAudioFilterRead;
    public FloatArrayAndIntEvent onAudioFilterRead { get { return _onAudioFilterRead; } }
    [SerializeField]
    private UnityEvent _onBecameInvisibe;
    public UnityEvent onBecameInvisibe { get { return _onBecameInvisibe; } }
    [SerializeField]
    private UnityEvent _onBecameVisible;
    public UnityEvent onBecameVisible { get { return _onBecameVisible; } }
    [SerializeField]
    private CollisionEvent _onCollisionEnter;
    public CollisionEvent onCollisionEnter { get { return _onCollisionEnter; } }
    [SerializeField]
    private Collision2DEvent _onCollisionEnter2D;
    public Collision2DEvent onCollisionEnter2D { get { return _onCollisionEnter2D; } }
    [SerializeField]
    private CollisionEvent _onCollisionExit;
    public CollisionEvent onCollisionExit { get { return _onCollisionExit; } }
    [SerializeField]
    private Collision2DEvent _onCollisionExit2D;
    public Collision2DEvent onCollisionExit2D { get { return _onCollisionExit2D; } }
    [SerializeField]
    private CollisionEvent _onCollisionStay;
    public CollisionEvent onCollisionStay { get { return _onCollisionStay; } }
    [SerializeField]
    private Collision2DEvent _onCollisionStay2D;
    public Collision2DEvent onCollisionStay2D { get { return _onCollisionStay2D; } }
    [SerializeField]
    private UnityEvent _onConnectedToServer;
    public UnityEvent onConnectedToServer { get { return _onConnectedToServer; } }
    [SerializeField]
    private ControllerColliderHitEvent _onControllerColliderHit;
    public ControllerColliderHitEvent onControllerColliderHit { get { return _onControllerColliderHit; } }
    [SerializeField]
    private UnityEvent _onDestroy;
    public UnityEvent onDestroy { get { return _onDestroy; } }
    [SerializeField]
    private UnityEvent _onDisable;
    public UnityEvent onDisable { get { return _onDisable; } }
    [SerializeField]
    private UnityEvent _onDrawGizmos;
    public UnityEvent onDrawGizmos { get { return _onDrawGizmos; } }
    [SerializeField]
    private UnityEvent _onDrawGizmosSelected;
    public UnityEvent onDrawGizmosSelected { get { return _onDrawGizmosSelected; } }
    [SerializeField]
    private UnityEvent _onEnable;
    public UnityEvent onEnable { get { return _onEnable; } }
    [SerializeField]
    private UnityEvent _onGUI;
    public UnityEvent onGUI { get { return _onGUI; } }
    [SerializeField]
    private FloatEvent _onJointBreak;
    public FloatEvent onJointBreak { get { return _onJointBreak; } }
    [SerializeField]
    private Joint2DEvent _onJointBreak2D;
    public Joint2DEvent onJointBreak2D { get { return _onJointBreak2D; } }
    [SerializeField]
    private UnityEvent _onMouseDown;
    public UnityEvent onMouseDown { get { return _onMouseDown; } }
    [SerializeField]
    private UnityEvent _onMouseDrag;
    public UnityEvent onMouseDrag { get { return _onMouseDrag; } }
    [SerializeField]
    private UnityEvent _onMouseEnter;
    public UnityEvent onMouseEnter { get { return _onMouseEnter; } }
    [SerializeField]
    private UnityEvent _onMouseExit;
    public UnityEvent onMouseExit { get { return _onMouseExit; } }
    [SerializeField]
    private UnityEvent _onMouseOver;
    public UnityEvent onMouseOver { get { return _onMouseOver; } }
    [SerializeField]
    private UnityEvent _onMouseUp;
    public UnityEvent onMouseUp { get { return _onMouseUp; } }
    [SerializeField]
    private UnityEvent _onMouseUpAsButton;
    public UnityEvent onMouseUpAsButton { get { return _onMouseUpAsButton; } }
    [SerializeField]
    private GameObjectEvent _onParticleCollision;
    public GameObjectEvent onParticleCollision { get { return _onParticleCollision; } }
    [SerializeField]
    private UnityEvent _onParticleSystemStopped;
    public UnityEvent onParticleSystemStopped { get { return _onParticleSystemStopped; } }
    [SerializeField]
    private UnityEvent _onParticleTrigger;
    public UnityEvent onParticleTrigger { get { return _onParticleTrigger; } }
    [SerializeField]
    private UnityEvent _onPostRender;
    public UnityEvent onPostRender { get { return _onPostRender; } }
    [SerializeField]
    private UnityEvent _onPreCull;
    public UnityEvent onPreCull { get { return _onPreCull; } }
    [SerializeField]
    private UnityEvent _onPreRender;
    public UnityEvent onPreRender { get { return _onPreRender; } }
    [SerializeField]
    private RenderTextureAndRenderTextureEvent _onRenderImage;
    public RenderTextureAndRenderTextureEvent onRenderImage { get { return _onRenderImage; } }
    [SerializeField]
    private UnityEvent _onRenderObject;
    public UnityEvent onRenderObject { get { return _onRenderObject; } }
    [SerializeField]
    private UnityEvent _onInitialized;
    public UnityEvent onInitialized { get { return _onInitialized; } }
    [SerializeField]
    private UnityEvent _onTransformChildrenChanged;
    public UnityEvent onTransformChildrenChanged { get { return _onTransformChildrenChanged; } }
    [SerializeField]
    private UnityEvent _onTransformParentChanged;
    public UnityEvent onTransformParentChanged { get { return _onTransformParentChanged; } }
    [SerializeField]
    private ColliderEvent _onTriggerEnter;
    public ColliderEvent onTriggerEnter { get { return _onTriggerEnter; } }
    [SerializeField]
    private Collider2DEvent _onTriggerEnter2D;
    public Collider2DEvent onTriggerEnter2D { get { return _onTriggerEnter2D; } }
    [SerializeField]
    private ColliderEvent _onTriggerExit;
    public ColliderEvent onTriggerExit { get { return _onTriggerExit; } }
    [SerializeField]
    private Collider2DEvent _onTriggerExit2D;
    public Collider2DEvent onTriggerExit2D { get { return _onTriggerExit2D; } }
    [SerializeField]
    private ColliderEvent _onTriggerStay;
    public ColliderEvent onTriggerStay { get { return _onTriggerStay; } }
    [SerializeField]
    private Collider2DEvent _onTriggerStay2D;
    public Collider2DEvent onTriggerStay2D { get { return _onTriggerStay2D; } }
    [SerializeField]
    private UnityEvent _onValidate;
    public UnityEvent onValidate { get { return _onValidate; } }
    [SerializeField]
    private UnityEvent _onWillRenderObject;
    public UnityEvent onWillRenderObject { get { return _onWillRenderObject; } }
    [SerializeField]
    private UnityEvent _reset;
    public UnityEvent reset { get { return _reset; } }
    [SerializeField]
    private UnityEvent _start;
    public UnityEvent start { get { return _start; } }
    [SerializeField]
    private UnityEvent _update;
    public UnityEvent update { get { return _update; } }

    private void Awake()
    {
        _awake.Invoke();
    }
    private void FixedUpdate()
    {
        _fixedUpdate.Invoke();
    }
    private void LateUpdate()
    {
        _lateUpdate.Invoke();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        _onAnimatorIK.Invoke(layerIndex);
    }
    private void OnAnimatorMove()
    {
        _onAnimatorMove.Invoke();   
    }
    private void OnApplicationFocus(bool focus)
    {
        _onApplicationFocus.Invoke(focus);
    }
    private void OnApplicationPause(bool pause)
    {
        _onApplicationPause.Invoke(pause);
    }
    private void OnApplicationQuit()
    {
        _onApplicationQuit.Invoke();
    }
    private void OnAudioFilterRead(float[] data, int channels)
    {
        _onAudioFilterRead.Invoke(data, channels);
    }
    private void OnBecameInvisible()
    {
        _onBecameInvisibe.Invoke();
    }
    private void OnBecameVisible()
    {
        _onBecameVisible.Invoke();
    }
    private void OnCollisionEnter(Collision collision)
    {
        _onCollisionEnter.Invoke(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _onCollisionEnter2D.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        _onCollisionExit.Invoke(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _onCollisionExit2D.Invoke(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        _onCollisionStay.Invoke(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _onCollisionStay2D.Invoke(collision);
    }
    private void OnConnectedToServer()
    {
        _onConnectedToServer.Invoke();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _onControllerColliderHit.Invoke(hit);
    }
    private void OnDestroy()
    {
        _onDestroy.Invoke();
    }
    private void OnDisable()
    {
        _onDisable.Invoke();
    }
    private void OnDrawGizmos()
    {
        _onDrawGizmos.Invoke();
    }
    private void OnDrawGizmosSelected()
    {
        _onDrawGizmosSelected.Invoke();
    }
    private void OnEnable()
    {
        _onEnable.Invoke();
    }
    private void OnGUI()
    {
        _onGUI.Invoke();
    }
    private void OnJointBreak(float breakForce)
    {
        _onJointBreak.Invoke(breakForce);
    }
    private void OnJointBreak2D(Joint2D joint)
    {
        _onJointBreak2D.Invoke(joint);
    }
    private void OnMouseDown()
    {
        _onMouseDown.Invoke();
    }
    private void OnMouseDrag()
    {
        _onMouseDrag.Invoke();
    }
    private void OnMouseEnter()
    {
        _onMouseEnter.Invoke();
    }
    private void OnMouseOver()
    {
        _onMouseOver.Invoke();
    }
    private void OnMouseUp()
    {
        _onMouseUp.Invoke();
    }
    private void OnMouseUpAsButton()
    {
        _onMouseUpAsButton.Invoke();
    }
    private void OnParticleCollision(GameObject other)
    {
        _onParticleCollision.Invoke(other);
    }
    private void OnParticleTrigger()
    {
        _onParticleSystemStopped.Invoke();
    }
    private void OnPostRender()
    {
        _onPostRender.Invoke();
    }
    private void OnPreCull()
    {
        _onPreCull.Invoke();
    }
    private void OnPreRender()
    {
        _onPreRender.Invoke();
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _onRenderImage.Invoke(source, destination);
    }
    private void OnRenderObject()
    {
        _onRenderObject.Invoke();
    }
    private void OnTransformChildrenChanged()
    {
        _onTransformChildrenChanged.Invoke();
    }
    private void OnTransformParentChanged()
    {
        _onTransformParentChanged.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnter.Invoke(other);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _onTriggerEnter2D.Invoke(collision);
    }
    private void OnTriggerExit(Collider other)
    {
        _onTriggerExit.Invoke(other);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _onTriggerExit2D.Invoke(collision);
    }
    private void OnTriggerStay(Collider other)
    {
        _onTriggerStay.Invoke(other);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _onTriggerStay2D.Invoke(collision);
    }
    private void OnValidate()
    {
        _onValidate.Invoke();
    }
    private void OnWillRenderObject()
    {
        _onWillRenderObject.Invoke();
    }
    private void Reset()
    {
        _reset.Invoke();
    }
    private void Start()
    {
        _start.Invoke();
    }
    private void Update()
    {
        _update.Invoke();
    }
}
