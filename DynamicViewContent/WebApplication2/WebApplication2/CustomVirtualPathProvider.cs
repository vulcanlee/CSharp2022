using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace WebApplication2
{
    /// <summary>
    /// CustonmVirtualPathProvider
    /// </summary>
    public class CustomVirtualPathProvider : VirtualPathProvider
    {
        /// <summary>
        /// 動態View的範圍
        /// </summary>
        private string _coutomRootPath;

        /// <summary>
        /// View的附檔名
        /// </summary>
        private string _viewExtension;

        public CustomVirtualPathProvider(string customRootPath, string viewExtension)
        {
            this._coutomRootPath = customRootPath;
            this._viewExtension = viewExtension;
        }

        /// <summary>
        /// 確認檔案是否存在
        /// </summary>
        /// <param name="virtualPath">檔案路徑</param>
        /// <returns></returns>
        public override bool FileExists(string virtualPath)
        {
            if (virtualPath.EndsWith(".cshtml"))
            {
                var isDynamicView = IsDynamicView(virtualPath);
                var isFileExist = base.FileExists(virtualPath);

                return isDynamicView || isFileExist;
            }

            return false;
        }

        /// <summary>
        /// 取得要編譯成View的資料
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            VirtualFile file = default(VirtualFile);
            if (IsDynamicView(virtualPath))
            {
                var content = GetViewSource(virtualPath);
                file = new CustomVirtualFile(virtualPath, content);
            }
            else
            {
                file = base.GetFile(virtualPath);
            }

            return file;
        }

        /// <summary>
        /// 判斷是否為動態View的範圍
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        private bool IsDynamicView(string virtualPath)
        {
            if (!virtualPath.Contains(this._coutomRootPath))
            {
                return false;
            }

            var isCshtml = virtualPath.ToLower().Contains(this._viewExtension);
            return isCshtml;
        }

        /// <summary>
        /// 取得動態View的資料
        /// </summary>
        /// <param name="virtualPath">檔案路徑</param>
        /// <returns></returns>
        private string GetViewSource(string virtualPath)
        {
            var content = string.Empty;
            var pageName = Path.GetFileNameWithoutExtension(virtualPath);

            StringBuilder sb = new StringBuilder();
            var layoutPath = string.Empty;
            var layout = string.Empty;
            var dateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (pageName == "View1")
            {
                content = string.Format("<div>View1</div><div>Now time : {0}</div>", dateTimeStr);

                sb.AppendLine("@model WebApplication2.Person");
                sb.AppendLine("<div>@System.Reflection.Assembly.GetExecutingAssembly().Location</div>");
                sb.AppendLine(content);
                sb.AppendLine("<div><p>@Model.Name</p></div>");
            }
            else if (pageName == "View2")
            {
                content = string.Format("<div>View2</div><div>Now time : {0}</div>", dateTimeStr);

                sb.AppendLine("@model WebApplication2.Person");
                sb.AppendLine("<div>@System.Reflection.Assembly.GetExecutingAssembly().Location</div>");
                sb.AppendLine(content);
                sb.AppendLine("<div><p>@Model.Name</p></div>");
            }

            return sb.ToString();
        }
    }
}