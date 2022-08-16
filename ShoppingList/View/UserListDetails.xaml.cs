//using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using ShoppingList.ViewModels;
using IImage = Microsoft.Maui.Graphics.IImage;
using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Views.Maui.Controls;
using ShoppingList.Controls;

namespace ShoppingList;

public partial class UserListDetails : ContentPage
{
	UserListDetailViewModel _ulvm;

	bool _firstTime = true; 

	public UserListDetails(UserListDetailViewModel userListDetailViewModel)
	{
		InitializeComponent();
		BindingContext = userListDetailViewModel;
		_ulvm = userListDetailViewModel;

		var cartLottie = this.FindByName("SKLottieViewControlTemplate");
		//var canvas = cartLottie.OnPaintSurface(); 
		

		//var circularButtonGV = this.FindByName("CircularButton") as GraphicsView;
		//var circularButton = circularButtonGV.Drawable as CircularButton;
		//circularButton.StrokeColor = Colors.White;

		//circularButton.AreShadowsEnabled = true;
		//circularButton.Width = 75;
		//circularButton.Height = 75; 

        //IImage image;
        //Assembly assembly = GetType().GetTypeInfo().Assembly;
        //using (Stream stream = assembly.GetManifestResourceStream("ShoppingList.Resources.Images.plus_solid.svg"))
        //{
        //    image = PlatformImage.FromStream(stream);
        //}
		//circularButton.Image = image;
		//circularButtonGV.Invalidate();


    }

	public void OnCheckboxClicked(object sender, CheckedChangedEventArgs e)
	{

		CheckBox thisCheckbox = sender as CheckBox;

		Frame currentFrame = thisCheckbox.BindingContext as Frame;

		Item itemThatWasClicked = currentFrame.BindingContext as Item;


		if (thisCheckbox.IsChecked)
			currentFrame.FadeTo(.4, 1000);
		else
			currentFrame.FadeTo(1, 1000);

		//thisCheckbox.CheckedChanged -= OnCheckboxClicked; 
		itemThatWasClicked.IsCompleted = thisCheckbox.IsChecked;
		_ulvm.ItemWasChecked(itemThatWasClicked);
		//thisCheckbox.CheckedChanged += OnCheckboxClicked; 

	}

}
