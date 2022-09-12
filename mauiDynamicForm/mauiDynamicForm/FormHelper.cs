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
                ValueString = "Vulcan",
                PlaceHolder = "請輸入使用者帳號"
            },
            new FormItem()
            {
                Name = "Entry2",
                Label = "密碼",
                ViewType = ViewTypeEnum.Entry,
                ValueType = ValueTypeEnum.String,
                IsReadOnly = false,
                ValueString = "123",
                PlaceHolder = "請輸入使用者密碼"
            },
        };
        return aForm;
    }
}
