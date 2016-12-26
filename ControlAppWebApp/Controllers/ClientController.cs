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
    public class ClientController : ApiController
    {
       private readonly IClientAppService _service;

       public ClientController(IClientAppService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        [Route("api/client/{acronym}")]
       public Task<HttpResponseMessage> GetByAcronym(string acronym)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AssertionConcern.AssertArgumentNotNull(acronym, "Sigla inválida");

                var client = _service.GetByAcronym(acronym);

                AssertionConcern.AssertArgumentNotNull(client, "Cliente não encontrado.");

                response = Request.CreateResponse(HttpStatusCode.OK, new { client = new RegisterClientModel(client) });
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
        [Route("api/client")]
        public Task<HttpResponseMessage> Register(RegisterClientModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Register(model.Acronym, model.Name, model.SocialReazon, model.Active);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Cliente cadastrado com sucesso!" });
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
        [Route("api/client/update")]
        public Task<HttpResponseMessage> Update(RegisterClientModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ControlApp.Dominio.Model.Client client = _service.GetByAcronym(model.Acronym);

                AssertionConcern.AssertArgumentNotNull(client, "Cliente inexistente.");

                client.ChangeActivation(model.Active);
                client.ChangeAcronym(model.Acronym);
                client.ChangeName(model.Name);
                client.ChangeSocialReazon(model.SocialReazon);

                _service.Update(client);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Cliente alterado com sucesso!" });
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
        [Route("api/client/search/{name}")]
        public Task<HttpResponseMessage> Search(string name)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                var clients = _service.Search(name);
                response = Request.CreateResponse(HttpStatusCode.OK, new { clients = clients });
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
        [Route("api/client/delete/{acronym}")]
        public Task<HttpResponseMessage> Delete(string acronym)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                _service.Delete(acronym);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Cliente excluído com sucesso." });
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
