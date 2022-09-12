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

