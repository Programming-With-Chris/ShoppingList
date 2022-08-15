using System;
namespace ShoppingList.Drawable;

public class CircularButton : IDrawable
{
    public Color StrokeColor { get; set; } = Colors.DarkGrey;

    public bool AreShadowsEnabled { get; set; } = true;

    public Microsoft.Maui.Graphics.IImage Image { get; set; }

    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0; 

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = StrokeColor;
        
	    if (AreShadowsEnabled) 
	        canvas.EnableDefaultShadow();

        var width = Width != 0 ? Width : dirtyRect.Width; 
        var height = Height != 0 ? Height : dirtyRect.Height;

        var limitingDim = width > height ? height : width;
        PointF centerOfCircle = new PointF(width / 2, height / 2);

       // canvas.BlendMode = BlendMode.SourceOut;
        canvas.FillColor = Colors.White;
        canvas.FillCircle(centerOfCircle, limitingDim / 2);

        if (Image != null)
        {
            ImagePaint imagePaint = new ImagePaint();
            imagePaint.Image = this.Image; 
            canvas.SetFillPaint(imagePaint, RectF.Zero);

            canvas.FillCircle(centerOfCircle, limitingDim / 2);
        }


//        canvas.DrawImage(Image, centerOfCircle.X, centerOfCircle.Y, limitingDim, limitingDim); 

    }
}

