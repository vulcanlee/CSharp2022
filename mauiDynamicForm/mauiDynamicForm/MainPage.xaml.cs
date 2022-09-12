using CommunityToolkit.Maui.Markup;
using Newtonsoft.Json;

namespace mauiDynamicForm;

public partial class MainPage : ContentPage
{
    private readonly FormHelper formHelper;
    IView view = null;
    Label myLabel;

    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        this.formHelper = new FormHelper();
        var formItems = formHelper.GetFormDefinition();
        this.BindingContext = formItems;

        foreach (var formItem in formItems)
        {
            #region 文字標籤
            if (string.IsNullOrEmpty(formItem.Label) == false)
            {
                hostContainer.Children.Add(new Label()
                {
                    ClassId = formItem.Name,
                }
                .Text(formItem.Label)
                .Margin(new Thickness(0, 15, 0, 0))
                .FontSize(12)
                .Bold()
                .TextColor(Color.FromArgb("bb888888")));
            }
            #endregion

            #region Entry 文字輸入盒
            if (formItem.ViewType == ViewTypeEnum.Entry)
            {
                Entry entry = new Entry()
                {
                    ClassId = formItem.Name,
                    IsPassword = formItem.IsPassword,
                    IsEnabled = !formItem.IsReadOnly,
                }
                .Text(formItem.ValueString)
                .Placeholder(formItem.PlaceHolder)
                .Margin(new Thickness(0, 0, 15, 0));

                entry.TextChanged += (s, e) =>
                {
                    formItem.ValueString = e.NewTextValue;
                };

                //entry.SetBinding(Entry.TextProperty, new Binding(nameof(formItem.ValueString)));

                hostContainer.Children.Add(entry);
            }
            #endregion

            #region DateTimePicker 文字輸入盒
            if (formItem.ViewType == ViewTypeEnum.DatePicker)
            {
                DatePicker datePicker = new DatePicker()
                {
                    ClassId = formItem.Name,
                    IsEnabled = !formItem.IsReadOnly,
                    Date = formItem.ValueDateTime,
                }
                .Margin(new Thickness(0, 0, 15, 0));

                datePicker.DateSelected += (s, e) =>
                {
                    formItem.ValueDateTime = e.NewDate;
                };

                hostContainer.Children.Add(datePicker);
            }
            #endregion

            #region Image 圖片
            if (formItem.ViewType == ViewTypeEnum.Image)
            {
                Image image = new Image()
                {
                    ClassId = formItem.Name,
                    IsEnabled = !formItem.IsReadOnly,
                    Source = formItem.ValueString,
                    WidthRequest = formItem.ImageWidth,
                    HeightRequest = formItem.ImageHeight,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start,
                }
                .Margin(new Thickness(0, 0, 15, 0));

                hostContainer.Children.Add(image);
            }
            #endregion
        }

        Button myButton = new Button()
        {
            Text = "View",
        };
        myButton.Clicked += (s, e) =>
        {
            var outupt = JsonConvert.SerializeObject(formItems);
            myLabel.Text = outupt;
        };

        hostContainer.Children.Add(myButton);
        myLabel = new Label();
        hostContainer.Children.Add(myLabel);
    }
}

