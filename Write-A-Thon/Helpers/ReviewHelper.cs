using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Services.Store;
using Windows.System;
using Write_A_Thon.Dialogs;

namespace Write_A_Thon.Helpers
{
    public static class ReviewHelper
    {
        public async static Task tryAskingForReviews()
        {
            await ShowReviewDialog();
        }


        private async static Task ShowReviewDialog()
        {
            ReviewDialog dialog = new ReviewDialog();
            await dialog.ShowAsync();
        }

        public static async Task TryRequestReviewAsync()
        {
            StoreSendRequestResult result = await StoreRequestHelper.SendRequestAsync(
                StoreContext.GetDefault(), 16, String.Empty);

            if (result.ExtendedError == null)
            {
                JObject jsonObject = JObject.Parse(result.Response);
                if (jsonObject.SelectToken("status").ToString() == "success")
                {
                    // The customer rated or reviewed the app.
                }
            }


        }
    }
}