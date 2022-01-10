using AutoMapper.Configuration;
using JSC_LMS.Application.Features.Feedback.Commands.CreateFeedback;
using JSC_LSM.UI.Helpers;
using JSC_LSM.UI.ResponseModels;
using JSC_LSM.UI.Services.IRepositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JSC_LSM.UI.Models;

namespace JSC_LSM.UI.Services.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private APIRepository _aPIRepository;
        private APICommunicationResponseModel<string> _oApiResponse;
        private string _sToken = string.Empty;
        private readonly IOptions<ApiBaseUrl> _apiBaseUrl;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public FeedbackRepository()
        {

        }

        public async Task<GetAllFeedbackListResponseModel> GetAllFeedbackDetails(string baseurl)
        {
            GetAllFeedbackListResponseModel getAllFeedbackListResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetAllFeedbackDetails, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getAllFeedbackListResponseModel = JsonConvert.DeserializeObject<GetAllFeedbackListResponseModel>(_oApiResponse.data);
                getAllFeedbackListResponseModel.Succeeded = true;
            }

            return getAllFeedbackListResponseModel;
        }

        public async Task<FeedbackResponseModel> AddNewFeedback(string baseurl, CreateFeedbackDto createFeedbackDto)
        {
            FeedbackResponseModel feedbackResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            var json = JsonConvert.SerializeObject(createFeedbackDto, Formatting.Indented);
            byte[] content = Encoding.ASCII.GetBytes(json);
            var bytes = new ByteArrayContent(content);

            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.AddNewFeedback, HttpMethod.Post, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                feedbackResponseModel = JsonConvert.DeserializeObject<FeedbackResponseModel>(_oApiResponse.data);
                if (feedbackResponseModel.Succeeded)
                {
                    feedbackResponseModel.Succeeded = true;
                }
                else
                {
                    feedbackResponseModel.Succeeded = false;
                }
            }

            return feedbackResponseModel;

        }


        public async Task<GetFeedbackByIdResponseModel> GetFeedbackById(string baseurl, int Id)
        {
            GetFeedbackByIdResponseModel getFeedbackByIdResponseModel = null;
            _aPIRepository = new APIRepository(_configuration);

            _oApiResponse = new APICommunicationResponseModel<string>();
            byte[] content = Array.Empty<byte>();
            var bytes = new ByteArrayContent(content);
            _oApiResponse = await _aPIRepository.APICommunication( baseurl, UrlHelper.GetFeedbackById + Id, HttpMethod.Get, bytes, _sToken);
            if (_oApiResponse.data != null)
            {
                getFeedbackByIdResponseModel = JsonConvert.DeserializeObject<GetFeedbackByIdResponseModel>(_oApiResponse.data);
                getFeedbackByIdResponseModel.Succeeded = true;
            }

            return getFeedbackByIdResponseModel;


        }


        //public async Task<AddFeedbackResponseModel> AddFeedbackData(string baseurl,CreateFeedbackDto createFeedbackDto)
        //{
        //    AddFeedbackResponseModel addFeedbackResponseModel = null;
        //    _aPIRepository = new APIRepository(_configuration);

        //    _oApiResponse = new APICommunicationResponseModel<string>();
        //    var json = JsonConvert.SerializeObject(createFeedbackDto, Formatting.Indented);
        //    byte[] content = Encoding.ASCII.GetBytes(json);
        //    var bytes = new ByteArrayContent(content);

        //    _oApiResponse = await _aPIRepository.APICommunication( baseurl,UrlHelper.AddFeedbackData, HttpMethod.Post, bytes, _sToken);
        //    if (_oApiResponse.data != null)
        //    {
        //        addFeedbackResponseModel = JsonConvert.DeserializeObject<AddFeedbackResponseModel>(_oApiResponse.data);
        //        if (addFeedbackResponseModel.Succeeded)
        //        {
        //            addFeedbackResponseModel.Succeeded = true;
        //        }
        //        else
        //        {
        //            addFeedbackResponseModel.Succeeded = false;
        //        }
        //    }

        //    return addFeedbackResponseModel;
        //}


    }
}
