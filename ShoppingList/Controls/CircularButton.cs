using System;
namespace ShoppingList.Controls;

public class CircularButton : GraphicsView, IGraphicsView
{
	public new IDrawable Drawable { get; set; }

	public CircularButton()
	{
		var drawable = new ShoppingList.Drawable.CircularButton();
		this.Drawable = drawable; 
	}
}

