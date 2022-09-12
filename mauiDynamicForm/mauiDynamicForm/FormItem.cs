using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;

namespace mauiDynamicForm;

public enum ViewTypeEnum
{
    Entry,
    Editor,
    DateTimePicker,
}
public enum ValueTypeEnum
{
    String,
    Int,
    Double,
    DateTime,
    TimeSpan,
}
public partial class FormItem : ObservableObject
{
    public string Name { get; set; }
    [ObservableProperty]
    string label;
    public ViewTypeEnum ViewType { get; set; } = ViewTypeEnum.Entry;
    public ValueTypeEnum ValueType { get; set; } = ValueTypeEnum.String;
    [ObservableProperty]
    string valueString;
    [ObservableProperty]
    int valueInt;
    [ObservableProperty]
    double valueDouble;
    DateTime valueDateTime;
    [ObservableProperty]
    TimeSpan valueTimeSpan;
    [ObservableProperty]
    bool isReadOnly = false;
    [ObservableProperty]
    bool isPassword = false;
    [ObservableProperty]
    string placeHolder;
}
