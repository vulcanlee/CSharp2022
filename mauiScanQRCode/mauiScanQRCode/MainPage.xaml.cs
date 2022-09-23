using ZXing.Net.Maui;

namespace mauiScanQRCode;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		string Result = "";
        foreach (var barcode in e.Results)
            Result += $"Barcodes: {barcode.Format} -> {barcode.Value}  ";

		MainThread.BeginInvokeOnMainThread(() =>
		{
			labelResult.Text = Result;
		});
    }
}

