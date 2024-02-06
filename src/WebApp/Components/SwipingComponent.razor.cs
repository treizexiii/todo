using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WebApp.Components;

public partial class SwipingComponent
{
    private double? _xDown;
    private double? _yDown;

    [Parameter] public RenderFragment ChildContent { get; set; }

    [Parameter] public string Class { get; set; }

    [Parameter] public string Style { get; set; }
    [Parameter] public object? Data { get; set; }

    // [Parameter] public Action<SwipeDirection> OnSwipe { get; set; }
    [Parameter] public EventCallback<SwipeResult> OnSwipe { get; set; }

    [Parameter] public Dictionary<string, object> UserAttributes { get; set; } = new();

    private void OnTouchStart(TouchEventArgs args)
    {
        _xDown = args.Touches[0].ClientX;
        _yDown = args.Touches[0].ClientY;
    }

    private void OnTouchEnd(TouchEventArgs args)
    {
        if (_xDown is null || _yDown is null)
        {
            return;
        }

        var xDiff = _xDown.Value - args.ChangedTouches[0].ClientX;
        var yDiff = _yDown.Value - args.ChangedTouches[0].ClientY;
        if (Math.Abs(xDiff) < 100 && Math.Abs(yDiff) < 100)
        {
            _xDown = null;
            _yDown = null;
            return;
        }

        if (Math.Abs(xDiff) > Math.Abs(yDiff))
        {
            if (xDiff > 0)
            {
                OnSwipe.InvokeAsync(new SwipeResult
                {
                    Direction = SwipeDirection.Left,
                    Data = Data
                });
            }
            else
            {
                // InvokeAsync(() => OnSwipe(SwipeDirection.Right));
                OnSwipe.InvokeAsync(new SwipeResult
                {
                    Direction = SwipeDirection.Right,
                    Data = Data
                });
            }
        }
        else
        {
            if (yDiff > 0)
            {
                // InvokeAsync(() => OnSwipe(SwipeDirection.Top));
                OnSwipe.InvokeAsync(new SwipeResult
                {
                    Direction = SwipeDirection.Up,
                    Data = Data
                });
            }
            else
            {
                // InvokeAsync(() => OnSwipe(SwipeDirection.Bottom));
                OnSwipe.InvokeAsync(new SwipeResult
                {
                    Direction = SwipeDirection.Down,
                    Data = Data
                });
            }
        }

        _xDown = null;
        _yDown = null;
    }

    private void OnTouchCancel(TouchEventArgs args)
    {
        _xDown = null;
        _yDown = null;
    }
}

public enum SwipeDirection
{
    None,
    Left,
    Right,
    Down,
    Up
}

public class SwipeResult
{
    public SwipeDirection Direction { get; set; }
    public object? Data { get; set; }
}