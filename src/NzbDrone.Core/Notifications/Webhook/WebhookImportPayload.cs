using System.Collections.Generic;

namespace NzbDrone.Core.Notifications.Webhook
{
    public class WebhookImportPayload : WebhookPayload
    {
        public WebhookSeries Series { get; set; }
        public List<WebhookEpisode> Episodes { get; set; }
        public WebhookEpisodeFile EpisodeFile { get; set; }
        public bool IsUpgrade { get; set; }
        public string DownloadClient { get; set; }
        public string DownloadClientType { get; set; }
        public string DownloadId { get; set; }
        public List<WebhookEpisodeFile> DeletedFiles { get; set; }
        public WebhookCustomFormatInfo CustomFormatInfo { get; set; }
    }
}
