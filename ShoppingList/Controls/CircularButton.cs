using System;
namespace ShoppingList.Controls;

public class CircularButton : GraphicsView, IGraphicsView
{
	//public static new IDrawable Drawable { get; set; }

	public Color ButtonColor
	{
        get => (Color?)GetValue(ButtonColorProperty);
        set => SetValue(ButtonColorProperty, value);
    }

	public BindableProperty ButtonColorProperty = BindableProperty.Create(
		nameof(ButtonColor), typeof(Color), typeof(CircularButton), propertyChanged: OnButtonColorChanged);

	public CircularButton()
	{
		var drawable = new ShoppingList.Drawable.CircularButton();
		Drawable = drawable;
	}

	static void OnButtonColorChanged(BindableObject bindable, object oldValue, object newValue)
	{
        var control = (CircularButton)bindable;
        var buttonColor = control.ButtonColor;
		var thisDrawable = control.Drawable as ShoppingList.Drawable.CircularButton;
		thisDrawable.ButtonColor = buttonColor;
        control.Invalidate();
    }

}