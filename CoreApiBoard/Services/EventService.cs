using AutoMapper;
using CoreApiBoard.Dto;
using CoreApiBoard.Interfaces.IRepositorys;
using CoreApiBoard.Interfaces.IServices;
using CoreApiBoard.PostgreSQLModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Drive.v3.DriveService;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace CoreApiBoard.Services
{
    class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IThemeRepository _themeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private IConfiguration _configuration { get; }
        private static string AccessToken { get; set; }
        private static string RefreshToken { get; set; }
        private static string ClientId { get; set; }
        private static string ClientSecret { get; set; }

        public EventService(IEventRepository eventRepository, IThemeRepository themeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _eventRepository = eventRepository;
            _themeRepository = themeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            AccessToken = _configuration.GetValue<string>("GoogleDrive:AccessToken");
            RefreshToken = _configuration.GetValue<string>("GoogleDrive:RefreshToken");
            ClientId = _configuration.GetValue<string>("GoogleDrive:ClientId");
            ClientSecret = _configuration.GetValue<string>("GoogleDrive:ClientSecret");
        }
        public string IndexGetData()
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Userid = 0;
            var AccessLevelid = 0;

            if (Claims.Count() > 0)
            {
                Int32.TryParse(Claims.Where(x => x.Type == "Userid").First().Value.ToString(), out Userid);
                Int32.TryParse(Claims.Where(x => x.Type == ClaimTypes.Role).First().Value.ToString(), out AccessLevelid);
            }
            

            var IndexEventDto = new IndexEventGetDto();
            
            if (Userid==0) //未登入
            {
                IndexEventDto.EventDtos = _eventRepository.GetAll().Where(x => x.Enabled == true);
            }
            else if (AccessLevelid == 1) //管理者
            {
                IndexEventDto.EventDtos = _eventRepository.GetAll();
            }
            else
            {
                IndexEventDto.EventDtos = _eventRepository.GetAll().Where(x => x.Userid == Userid);
            }

            return JsonConvert.SerializeObject(IndexEventDto);
        }
        public string GetData(int id)
        {
            var DetailEventDto = _mapper.Map<DetailEventDto>(_eventRepository.Get(id));
            return JsonConvert.SerializeObject(DetailEventDto);
        }

        public string CreateEventGetData()
        {
            var CreateEventGetDto = new CreateEventGetDto();
            var ThemeForEventDto = _mapper.Map<IEnumerable<ThemeForEventDto>>(_themeRepository.GetAll().Where(x => x.Enabled == true));

            CreateEventGetDto.ThemeDtos = ThemeForEventDto;
            return JsonConvert.SerializeObject(CreateEventGetDto);
        }

        public string CreateEventSubmitData(CreateEventSubmitDto data)
        {
            try
            {
                var Claims = _httpContextAccessor.HttpContext.User.Claims;

                var Data = _mapper.Map<EventDto>(data);
                Data.UpdateTime = DateTime.Now;
                Data.Views = 0;
                Data.Del = false;
                Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());

                return _eventRepository.Create(Data);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateEventGetData(int id)
        {
            var Data = _eventRepository.UpdateGet(id);
            if (Data!=null)
            {
                var UpdateEventGetDto = new UpdateEventGetDto
                {
                    UpdateEventDto = Data
                };
                var ThemeForEventDto = _mapper.Map<IEnumerable<ThemeForEventDto>>(_themeRepository.GetAll().Where(x => x.Enabled == true));
                UpdateEventGetDto.ThemeDtos = ThemeForEventDto;
                return JsonConvert.SerializeObject(UpdateEventGetDto);
            }
            return "查無資料";
        }

        public string UpdateEventSubmitData(UpdateEventSubmitDto data)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;
            var Data = _mapper.Map<EventDto>(data);
            Data.UpdateTime = DateTime.Now;
            Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());

            return _eventRepository.Update(Data);
        }

        public string DeleteEventData(int id)
        {
            var Claims = _httpContextAccessor.HttpContext.User.Claims;

            var Data = _eventRepository.DeleteGet(id);
            if (Data != null)
            {
                Data.UpdateTime = DateTime.Now;
                Data.Del = true;
                Data.Userid = Int32.Parse(Claims.Where(x => x.Type == "Userid").First().Value.ToString());
                return _eventRepository.Delete(Data);
            }
            return "查無資料";
        }

        public string AddDataView(int id)
        {
            var EventDto = _mapper.Map<EventDto>(_eventRepository.Get(id));
            EventDto.Views += 1;
            var Result = _eventRepository.Update(EventDto);

            return Result;
        }

        public async Task<string> ImageUpload(IFormFileCollection files)
        {
            var FormFile = files[0];
            var UploadFileName = FormFile.FileName;
            var FileName = Guid.NewGuid() + Path.GetExtension(UploadFileName);
            //var SaveDir = @".\upload\";
            //var SavePath = SaveDir + FileName;
            //var PreviewPath = $"https://localhost:5001/event/image?name={FileName}";
            var PreviewPath = $"https://drive.google.com/uc?export=view&id=";
            bool Result = true;
            var rUpload = new
            {
                uploaded = Result,
                url = string.Empty
            };
            string errormessage = "";
            using (var StreamFile = new FileStream(FileName, FileMode.Create))
            {
                FormFile.CopyTo(StreamFile);

                try
                {

                    //本地測試
                    //if (!Directory.Exists(SaveDir))
                    //{
                    //    Directory.CreateDirectory(SaveDir);
                    //}
                    //using (FileStream Fs = System.IO.File.Create(SavePath))
                    //{
                    //    FormFile.CopyTo(Fs);
                    //    Fs.Flush();
                    //}

                    var NewFileId = await UploadFile(StreamFile, FileName, FormFile.FileName);
                    PreviewPath += $"{NewFileId}";
                }
                catch (Exception ex)
                {
                    Result = false;
                    errormessage = ex.ToString();
                }

                rUpload = new
                {
                    uploaded = Result,
                    url = Result ? PreviewPath : errormessage
                };
            }
            
           


            //try
            //{
            //    var localpath = ($@"{FileName}").Replace(@"\\", @"\");
            //    File.Delete(localpath);
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
           

            return JsonConvert.SerializeObject(rUpload);
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        /// <summary>
        /// 連線google
        /// </summary>
        /// <returns></returns>
        private static DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                //AccessToken = Environment.GetEnvironmentVariable("AccessToken "),
                //RefreshToken = Environment.GetEnvironmentVariable("RefreshToken "),
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
            };


            var applicationName = "littlewhaleboard"; // Use the name of the project in Google Cloud
            var username = "littlewhaleboard@gmail.com"; // Use your email


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    //ClientId = Environment.GetEnvironmentVariable("ClientId "),
                    //ClientSecret = Environment.GetEnvironmentVariable("ClientSecret "),
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return service;
        }

        /// <summary>
        /// google drive創建文件夾
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public string CreateFolder(string parent, string folderName)
        {
            var service = GetService();
            var driveFolder = new Google.Apis.Drive.v3.Data.File();
            driveFolder.Name = folderName;
            driveFolder.MimeType = "application/vnd.google-apps.folder";
            //driveFolder.Parents = new string[] { parent };
            var command = service.Files.Create(driveFolder);
            var file = command.Execute();
            return file.Id;
        }


        /// <summary>
        /// google上傳檔案
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="fileMime"></param>
        /// <param name="folder"></param>
        /// <param name="fileDescription"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(Stream file, string fileName, string fileDescription)
        {
            DriveService service = GetService();

            string fileMime;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out fileMime);
            var driveFile = new Google.Apis.Drive.v3.Data.File();
            driveFile.Name = fileName;
            driveFile.Description = fileDescription;
            driveFile.MimeType = fileMime;
            driveFile.Parents = new List<string> { "1zzcGaiZMDDJmpFxEzvE-3s6c9uBM6OMC" };


            var request = service.Files.Create(driveFile, file, fileMime);
            request.Fields = "id";

            var response = await request.UploadAsync();
            if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                throw response.Exception;

            return request.ResponseBody.Id;
        }
    }
}
