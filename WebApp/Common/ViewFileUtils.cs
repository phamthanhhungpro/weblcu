using Common.Entity;
using Datas.Models.ViewModels;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;

namespace WebApp.Common
{
    public class ViewFileUtils
    {
        public List<ViewFile> Datas { get; private set; } = new List<ViewFile>();

        public void Add(ViewFile viewFile)
        {
            Datas.Add(viewFile);
        }

        public ViewFile? Remove(string key) { 
            var view = Datas.FirstOrDefault(x => x.Key == key);
            if (view != null)
            {
                Datas.Remove(view);
            }
            return view;
        }
       
    }
}
