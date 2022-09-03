using Microsoft.Maui.Handlers;

namespace ShoppingList.Handlers;

public partial class CircularButtonHandler : GraphicsViewHandler
{
    static IPropertyMapper<CircularButton, CircularButtonHandler> PropertyMapper = new PropertyMapper<CircularButton, CircularButtonHandler>(GraphicsViewHandler.Mapper)
    {
        [nameof(CircularButton.ButtonColor)] = MapButtonColor,
        [nameof(CircularButton.Image)] = MapImage,
        [nameof(CircularButton.IsVisible)] = MapIsVisible
    };

    public CircularButtonHandler() : base(PropertyMapper)
    {
    }
    static void MapButtonColor(CircularButtonHandler handler, CircularButton button)
    {
        if (button.Drawable is CircularButtonDrawable drawable)
            drawable.ButtonColor = button.ButtonColor; 
        button.Invalidate();
    }
    
    static void MapImage(CircularButtonHandler handler, CircularButton button)
    {
        if (button.Drawable is CircularButtonDrawable drawable)
            drawable.Image = button.Image; 
        button.Invalidate();
    }
    static void MapIsVisible(CircularButtonHandler handler, CircularButton button)
    {
		if (button.IsVisible)
			button.FadeTo(1, 500);
		else
			button.FadeTo(0, 500);
    }
    
}