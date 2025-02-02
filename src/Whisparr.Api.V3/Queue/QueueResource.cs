using System;
using System.Collections.Generic;
using System.Linq;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Download.TrackedDownloads;
using NzbDrone.Core.Indexers;
using NzbDrone.Core.Languages;
using NzbDrone.Core.Qualities;
using Whisparr.Api.V3.CustomFormats;
using Whisparr.Api.V3.Episodes;
using Whisparr.Api.V3.Series;
using Whisparr.Http.REST;

namespace Whisparr.Api.V3.Queue
{
    public class QueueResource : RestResource
    {
        public int? SeriesId { get; set; }
        public int? EpisodeId { get; set; }
        public int? SeasonNumber { get; set; }
        public SeriesResource Series { get; set; }
        public EpisodeResource Episode { get; set; }
        public List<Language> Languages { get; set; }
        public QualityModel Quality { get; set; }
        public List<CustomFormatResource> CustomFormats { get; set; }
        public decimal Size { get; set; }
        public string Title { get; set; }
        public decimal Sizeleft { get; set; }
        public TimeSpan? Timeleft { get; set; }
        public DateTime? EstimatedCompletionTime { get; set; }
        public string Status { get; set; }
        public TrackedDownloadStatus? TrackedDownloadStatus { get; set; }
        public TrackedDownloadState? TrackedDownloadState { get; set; }
        public List<TrackedDownloadStatusMessage> StatusMessages { get; set; }
        public string ErrorMessage { get; set; }
        public string DownloadId { get; set; }
        public DownloadProtocol Protocol { get; set; }
        public string DownloadClient { get; set; }
        public string Indexer { get; set; }
        public string OutputPath { get; set; }
        public bool EpisodeHasFile { get; set; }
    }

    public static class QueueResourceMapper
    {
        public static QueueResource ToResource(this NzbDrone.Core.Queue.Queue model, bool includeSeries, bool includeEpisode)
        {
            if (model == null)
            {
                return null;
            }

            return new QueueResource
            {
                Id = model.Id,
                SeriesId = model.Series?.Id,
                EpisodeId = model.Episode?.Id,
                SeasonNumber = model.Episode?.SeasonNumber,
                Series = includeSeries && model.Series != null ? model.Series.ToResource() : null,
                Episode = includeEpisode && model.Episode != null ? model.Episode.ToResource() : null,
                Languages = model.Languages,
                Quality = model.Quality,
                CustomFormats = model.RemoteEpisode?.CustomFormats?.ToResource(false),
                Size = model.Size,
                Title = model.Title,
                Sizeleft = model.Sizeleft,
                Timeleft = model.Timeleft,
                EstimatedCompletionTime = model.EstimatedCompletionTime,
                Status = model.Status.FirstCharToLower(),
                TrackedDownloadStatus = model.TrackedDownloadStatus,
                TrackedDownloadState = model.TrackedDownloadState,
                StatusMessages = model.StatusMessages,
                ErrorMessage = model.ErrorMessage,
                DownloadId = model.DownloadId,
                Protocol = model.Protocol,
                DownloadClient = model.DownloadClient,
                Indexer = model.Indexer,
                OutputPath = model.OutputPath,
                EpisodeHasFile = model.Episode?.HasFile ?? false
            };
        }

        public static List<QueueResource> ToResource(this IEnumerable<NzbDrone.Core.Queue.Queue> models, bool includeSeries, bool includeEpisode)
        {
            return models.Select((m) => ToResource(m, includeSeries, includeEpisode)).ToList();
        }
    }
}
