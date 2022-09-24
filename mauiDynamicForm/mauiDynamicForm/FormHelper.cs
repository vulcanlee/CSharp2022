using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mauiDynamicForm;

public class FormHelper
{
    public List<FormItem> GetFormDefinition()
    {
        var aForm = new List<FormItem>()
        {
            new FormItem()
            {
                Name = "Entry1",
                Label = "帳號",
                ViewType = ViewTypeEnum.Entry,
                ValueType = ValueTypeEnum.String,
                IsReadOnly = false,
                ValueString = "",
                PlaceHolder = "請輸入使用者帳號"
            },
            new FormItem()
            {
                Name = "Entry2",
                Label = "密碼",
                ViewType = ViewTypeEnum.Entry,
                ValueType = ValueTypeEnum.String,
                IsReadOnly = false,
                IsPassword = true,
                ValueString = "",
                PlaceHolder = "請輸入使用者密碼"
            },
            new FormItem()
            {
                Name = "Entry3",
                Label = "驗證碼",
                ViewType = ViewTypeEnum.Entry,
                ValueType = ValueTypeEnum.String,
                IsReadOnly = true,
                ValueString = "N/A",
                PlaceHolder = ""
            },
            new FormItem()
            {
                Name = "DateTimePicker1",
                Label = "啟用日期",
                ViewType = ViewTypeEnum.DatePicker,
                ValueType = ValueTypeEnum.DateTime,
                IsReadOnly = false,
                ValueDateTime = DateTime.Now.AddDays(-2),
            },
            new FormItem()
            {
                Name = "Image1",
                Label = "申報物品",
                ViewType = ViewTypeEnum.Image,
                ValueType = ValueTypeEnum.String,
                ValueString = "https://ichef.bbci.co.uk/news/976/cpsprodpb/EB2E/production/_126060206_mediaitem126060205.jpg",
                ImageWidth=250,
                ImageHeight=250,
            },
        };
        return aForm;
    }
}
