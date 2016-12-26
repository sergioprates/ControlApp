using ControlApp.Application.Interfaces;
using ControlApp.Resources;
using ControlAppWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ControlAppWebApp.Controllers
{
    public class ApplicationController : ApiController
    {
        private readonly IApplicationAppService _service;

        public ApplicationController(IApplicationAppService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        [Route("api/application/active")]
        public Task<HttpResponseMessage> GetActiveApplications()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {       
                var applications = _service.GetAllActive();

                response = Request.CreateResponse(HttpStatusCode.OK, new { applications = applications });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Authorize]
        [Route("api/application/{hash}")]
        public Task<HttpResponseMessage> GetByHash(string hash)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AssertionConcern.AssertArgumentNotNull(hash, "Aplicação inválida");

                var application = _service.GetByHash(hash);

                AssertionConcern.AssertArgumentNotNull(application, "Aplicação não encontrada.");

                response = Request.CreateResponse(HttpStatusCode.OK, new { application = new CreateApplicationModel(application) });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Authorize]
        [Route("api/application")]
        public Task<HttpResponseMessage> Create(CreateApplicationModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Create(model.Name, model.Description, model.Active);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Aplicação cadastrada com sucesso!" });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Authorize]
        [Route("api/application/update")]
        public Task<HttpResponseMessage> Update(CreateApplicationModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ControlApp.Dominio.Model.Application app = _service.GetByHash(model.Hash);

                AssertionConcern.AssertArgumentNotNull(app, "Aplicação inexistente.");

                app.ChangeName(model.Name);
                app.ChangeDescription(model.Description);
                app.ChangeActivation(model.Active);

                if (model.Permissions != null)
                {
                    for (int i = 0; i < model.Permissions.Count; i++)
                    {
                        app.AddPermission(model.Permissions[i]);
                    }
                }
                
                _service.Update(app);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Aplicação alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Authorize]
        [Route("api/application/search/{name}")]
        public Task<HttpResponseMessage> Search(string name)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                var apps = _service.Search(name);
                response = Request.CreateResponse(HttpStatusCode.OK, new { applications = apps });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
        
        [HttpGet]
        [Authorize]        
        [Route("api/application/delete/{hash}")]
        public Task<HttpResponseMessage> Delete(string hash)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                _service.Delete(hash);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Aplicação excluída com sucesso." });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}
