using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAP.CRM.Core.SAL
{
    public class ServiceHelper
    {
        //public void GetTemplates(string templateTypeId)
        //{
        //    // Prepare the WebClient
        //    WebClient client = new WebClient();
        //    Uri uri = new Uri(string.Format("{0}/{1}?sessionid={2}&templateTypeId={3}", App.ViewModel.Settings.SubscriptionSettings.ServiceURI, "Templates", App.ViewModel.SessionId, templateTypeId));
        //    client.Headers[HttpRequestHeader.Authorization] = App.ViewModel.JsonWebToken;
        //    client.Headers[HttpRequestHeader.Accept] = "application/json";
        //    client.DownloadStringCompleted += GetTemplates_Completed;

        //    // Call the service
        //    client.DownloadStringAsync(uri);

        //}
        //void GetTemplates_Completed(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //    {
        //        var status = HandleException(e, "Unable to get report templates.");
        //        ShowErrorMessage("Unable to get report templates.");
        //    }
        //    else
        //    {
        //        string json = e.Result;
        //        var response = JsonConvert.DeserializeObject<GetTemplatesResponse>(json);
        //        App.ViewModel.SelectedTemplateType = response.TemplateTypes.FirstOrDefault();


        //        if (OnGetTemplatesCompleted != null)
        //            OnGetTemplatesCompleted();
        //    }
        //}

    }
}
