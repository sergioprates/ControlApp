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
    public class GroupController : ApiController
    {
        private readonly IGroupAppService _service;

        public GroupController(IGroupAppService service)
        {
            _service = service;
        }

        // GET: api/Group
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Group/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize]
        [Route("api/group")]
        public Task<HttpResponseMessage> Create(CreateGroupModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Create(model.Name, model.Active);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Grupo cadastrado com sucesso!" });
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
        [Route("api/group/{hash}")]
        public Task<HttpResponseMessage> GetByHash(string hash)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AssertionConcern.AssertArgumentNotEmpty(hash, "Hash inválido");

                var group = _service.GetByHash(hash);

                AssertionConcern.AssertArgumentNotNull(group, "Grupo não encontrado.");

                response = Request.CreateResponse(HttpStatusCode.OK, new { group = new CreateGroupModel(group) });
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
        [Route("api/group/update")]
        public Task<HttpResponseMessage> Update(CreateGroupModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ControlApp.Dominio.Model.Group group = _service.GetByHash(model.Hash);

                AssertionConcern.AssertArgumentNotNull(group, "Grupo inexistente.");

                group.ChangeName(model.Name);
                group.ChangeActivation(model.Active);

                _service.Update(group);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Grupo alterado com sucesso!" });
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
        [Route("api/group/search/{group}")]
        public Task<HttpResponseMessage> Search(string group)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                var groups = _service.Search(group);
                response = Request.CreateResponse(HttpStatusCode.OK, new { groups });
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
        [Route("api/group/delete/{hash}")]
        public Task<HttpResponseMessage> Delete(string hash)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                _service.Delete(hash);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Grupo excluído com sucesso." });
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
