using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(UnityEngine.UI.AspectRatioFitter))]
public class MobileInputController : MonoBehaviour,IDragHandler,IEndDragHandler,IPointerDownHandler,IPointerUpHandler {

    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _knob;

    [Header("Input Values")]
    public float Horizontal = 0;
    public float Vertical = 0;


    public float offset;
    Vector2 PointPosition;
    public void OnDrag(PointerEventData eventData)
    {
        PointPosition = new Vector2(pointPositionX(eventData), pointPositionY(eventData));
        PointPosition = (PointPosition.magnitude > 1.0f) ? PointPosition.normalized : PointPosition;
        _knob.transform.position = new Vector2(KnobRelativeX(), KnobRelativeY());
    }

    private float pointPositionY(PointerEventData eventData)
    {
        return (eventData.position.y - _background.position.y) / ((_background.rect.size.y - _knob.rect.size.y) / 2);
    }

    private float pointPositionX(PointerEventData eventData)
    {
        return (eventData.position.x - _background.position.x) / ((_background.rect.size.x - _knob.rect.size.x) / 2);
    }
    private float KnobRelativeX()
    {
        return (PointPosition.x * ((_background.rect.size.x - _knob.rect.size.x) / 2) * offset) + _background.position.x;
    }

    private float KnobRelativeY()
    {
        return (PointPosition.y * ((_background.rect.size.y - _knob.rect.size.y) / 2) * offset) + _background.position.y;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        PointPosition = new Vector2(0f,0f);
        _knob.transform.position = _background.position;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        OnEndDrag(eventData);
    }
	void Update () 
    {
        Horizontal = PointPosition.x;
        Vertical = PointPosition.y;
    }

    public Vector2 Coordinate()
    {
        return new Vector2(Horizontal,Vertical);
    }
}
