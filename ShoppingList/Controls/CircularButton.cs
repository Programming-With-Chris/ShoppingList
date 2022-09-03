using ShoppingList.Handlers;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace ShoppingList.Controls;

public class CircularButton : GraphicsView
{
	/// <summary>
	/// The Color passed to the drawable for the "Fill Color" of the Fill Circle method
	/// </summary>
	public Color ButtonColor
	{
		get => (Color)GetValue(ButtonColorProperty);
		set => SetValue(ButtonColorProperty, value);
	}

	/// <summary>
	/// A string containing the Image name (just the file name, not the directory)
	/// <br/> Expects the file to be in Resources/Images/
	/// </summary>
	public string Image
	{
		get => (string)GetValue(ImageProperty);
		set => SetValue(ImageProperty, value);

	}

	public new bool IsVisible
	{
		get => (bool)GetValue(IsVisibleProperty);
		set => SetValue(IsVisibleProperty, value);
	}

	public static BindableProperty ButtonColorProperty = BindableProperty.Create(
		nameof(ButtonColor), typeof(Color), typeof(CircularButton)); 
		//propertyChanged: OnButtonColorChanged);

		public static BindableProperty ImageProperty = BindableProperty.Create(
			nameof(Image), typeof(string), typeof(CircularButton));
		//propertyChanged: OnImageChanged);

	public new static BindableProperty IsVisibleProperty = BindableProperty.Create(
		nameof(IsVisible), typeof(bool), typeof(CircularButton),
		defaultValue: false); 
		//propertyChanged: OnIsVisibleChanged);

	public CircularButton()
	{
		Handler = new CircularButtonHandler(); 
		var drawable = new CircularButtonDrawable();
		Drawable = drawable;
	}


	// Converted this logic over to the circular button handler
	///<seealso cref = "CircularButtonHandler" />
	static void OnButtonColorChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (CircularButton)bindable;
		var buttonColor = control.ButtonColor;
		if (control.Drawable is CircularButtonDrawable thisDrawable)
			thisDrawable.ButtonColor = buttonColor;
		
		control.Invalidate();
	}

	static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (CircularButton)bindable;
		var image = control.Image;
		if (control.Drawable is CircularButtonDrawable thisDrawable)
			thisDrawable.Image = image;
		control.Invalidate();

	}

	// Converted this logic over to the circular button handler
	///<seealso cref = "CircularButtonHandler" />
	static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (CircularButton)bindable;

		var newValueAsBool = (bool)newValue;

		if (newValueAsBool == true)
		{
			control.FadeTo(1, 500);
		} else
		{
			control.FadeTo(0, 500);
		}

	}

	public async Task<bool> BounceOnPressAsync()
	{
		await this.ScaleTo(1.2, 100, Easing.BounceIn);

		return await this.ScaleTo(1.0, 100, Easing.BounceOut);
	}


}