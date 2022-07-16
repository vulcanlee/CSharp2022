namespace MyFirstAndroidApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText editText = FindViewById<EditText>(Resource.Id.editText1);
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += (s, e) =>
            {
                textView.Text = "Hi " + editText.Text;
            };

        }
    }
}