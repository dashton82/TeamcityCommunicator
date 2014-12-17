using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TeamCityCommunicator.Services;

namespace TeamCityCommunicator.Controllers
{
    public class UpdateBuildStatusController : ApiController
    {
        private readonly ITeamCityService _teamCityService;
        private readonly ITestEnvironmentService _testEnvironmentService;
        private readonly IBuildStatusDataStorageService _buildStatusDataStorageService;

        public UpdateBuildStatusController(ITeamCityService teamCityService, ITestEnvironmentService testEnvironmentService, IBuildStatusDataStorageService buildStatusDataStorageService)
        {
            _teamCityService = teamCityService;
            _testEnvironmentService = testEnvironmentService;
            _buildStatusDataStorageService = buildStatusDataStorageService;
        }

        // POST api/values
        ///api/UpdateBuildStatus/827572
        [HttpPost]
        public HttpResponseMessage Post([FromUri]string id)
        {
            try
            {
                var status = _teamCityService.GetBuildStatus(id);

                var buildNumber = _testEnvironmentService.GetBuildVersion("135");

                var result = _buildStatusDataStorageService.StoreSanityBuildInformation(buildNumber, status);

                var response = Request.CreateResponse(HttpStatusCode.Created);

                return response;
            }
            catch (Exception)
            {

                var response = Request.CreateResponse(HttpStatusCode.BadRequest);

                return response;
            }
            
        }

    }
}
