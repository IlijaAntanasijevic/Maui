namespace Maui.Components;

public partial class ProductComponent : ContentPage
{
    public ProductComponent()
    {
        InitializeComponent();
    }

    public static BindableProperty IdProp =
		BindableProperty.Create(nameof(Id), typeof(int), typeof(ProductComponent), "/", BindingMode.OneWay);

    public static BindableProperty NameProp =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(ProductComponent), "/", BindingMode.OneWay);

    public static BindableProperty TotalQuantityProp =
           BindableProperty.Create(nameof(TotalQuantity), typeof(int), typeof(ProductComponent), "/", BindingMode.OneWay);

    public static BindableProperty PriceProp =
           BindableProperty.Create(nameof(Price), typeof(decimal), typeof(ProductComponent), "/", BindingMode.OneWay);

    public static BindableProperty ImageProp =
            BindableProperty.Create(nameof(ImagePath), typeof(string), typeof(ProductComponent), "/", BindingMode.OneWay);


    public int Id
    {
        get => (int)GetValue(IdProp);
        set => SetValue(IdProp, value);
    }

	public string Name
	{
        get => (string)GetValue(NameProp);
        set => SetValue(NameProp, value);
    }

    public int TotalQuantity
    {
        get => (int)GetValue(TotalQuantityProp);
        set => SetValue(TotalQuantityProp, value);
    }

    public decimal Price
    {
        get => (decimal)GetValue(PriceProp);
        set => SetValue(PriceProp, value);
    }


    public string ImagePath
    {
        get => (string)GetValue(ImageProp);
        set => SetValue(ImageProp, value);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var id = Id;
    }
}