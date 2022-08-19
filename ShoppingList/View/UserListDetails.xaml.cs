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

		itemThatWasClicked.IsCompleted = thisCheckbox.IsChecked;
		_ulvm.ItemWasChecked(itemThatWasClicked);

	}

	public void NewItemButtonPressed(object sender, EventArgs e)
	{

		var circularButton = sender as CircularButton;
		//need to grab the drawable, add a method in the drawable to animate a scale, and then call it here

		_ulvm.NewItemDialog(); 


	}

}
