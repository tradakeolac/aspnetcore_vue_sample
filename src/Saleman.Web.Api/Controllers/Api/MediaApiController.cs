using Saleman.Model.ServiceObjects;
using Saleman.Web.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Saleman.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Saleman.Web.Api.Controllers.Api
{

    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/media")]
    public class MediaApiController : SalemanApiControllerBase<MediaViewModel, MediaServiceObject, Guid>
    {
        protected readonly IMediaAssetService MediaAssetService;
        protected readonly IMediaStorageService FileStorageService;

        public MediaApiController(IServiceObjectFactory objectFactory, IViewModelFactory viewModelFactory,
            IMediaAssetService mediaAssetService, IMediaStorageService fileStorageService, IUserService userService)
            : base(objectFactory, viewModelFactory, mediaAssetService, userService)
        {
            this.MediaAssetService = mediaAssetService;
            this.FileStorageService = fileStorageService;
        }

        [HttpPost]
        [Route("")]
        public override async Task<IActionResult> Post([FromForm] MediaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mediaServiceObject = this.CreateRequestServiceObject(viewModel);

            var serviceObject = await this.MediaAssetService.AddAsync(mediaServiceObject);

            return Ok(ViewModelFactory.Create<MediaViewModel>(serviceObject));
        }

        [HttpGet]
        [Route("")]
        public override async Task<IActionResult> Get()
        {
            var currentUserId = this.GetCurrentUserId();
            if(!string.IsNullOrEmpty(currentUserId))
            {
                var media = await this.MediaAssetService.GetMediaByUserIdAsync(currentUserId);

                if(media != null && media.Any())
                {
                    return Ok(media.Select(ViewModelFactory.Create<MediaViewModel>));
                }
            }

            return Ok(Enumerable.Empty<MediaViewModel>());
        }

        [HttpPut]
        [Route("")]
        public async override Task<IActionResult> Put([FromForm] MediaViewModel updateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var serviceObject = this.CreateRequestServiceObject(updateModel);

            if (!serviceObject.IsNullObject())
            {
                serviceObject = await this.MediaAssetService.UpdateAsync(serviceObject);

                return Ok(this.ViewModelFactory.Create<MediaViewModel>(serviceObject));
            }

            return BadRequest();
        }
        
        private MediaServiceObject CreateRequestServiceObject(MediaViewModel viewModel)
        {
            var files = this.Request.Form.Files;
            
            if (files != null && files.Any())
            {
                var file = files.First();

                var mediaServiceObject = this.ObjectFactory.Create<MediaServiceObject>(file);

                mediaServiceObject.Id = viewModel.Id;
                mediaServiceObject.Description = viewModel.Description;
                var currentUserId = this.GetCurrentUserId();

                if (!string.IsNullOrEmpty(currentUserId))
                {
                    mediaServiceObject.CreatedById = currentUserId;
                }

                return mediaServiceObject;
            }

            return MediaServiceObject.NullServiceObject;
        }
    }
}
