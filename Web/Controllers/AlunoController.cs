using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ApplicationService;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using RestSharp;

namespace Web.Controllers
{
    public class AlunoController : Controller
    {
        private AlunoServices Services { get; set; }

        public AlunoController(AlunoServices services)
        {
            this.Services = services;
        }

        // GET: AlunoController
        public ActionResult Index()
        {
            if (this.HttpContext.Session.GetString("AlunosSelecionados") != null)
            {
                var idsSelecionados = JsonConvert.DeserializeObject<List<Aluno>>(this.HttpContext.Session.GetString("AlunosSelecionados"));
                ViewBag.IdsSelecionados = idsSelecionados.Select(x => x.Id.ToString());
            }

            
            var client = new RestClient();

            var requestToken = new RestRequest("https://localhost:5001/api/authenticate/token");
            
            requestToken.AddJsonBody(JsonConvert.SerializeObject(new
            {
                Email = "maria.silva2@al.infnet.edu.br",
                Cpf = "999.999.999-99"
            }));


            var result = client.Post<TokenResult>(requestToken).Data;

            this.HttpContext.Session.SetString("Token", result.Token);

            var request = new RestRequest("https://localhost:5001/api/aluno", DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<List<Aluno>>(request);

            return View(response.Data);

        }

        public ActionResult AlunosSelecionados(string ids)
        {
            List<Aluno> alunosSelecionados = new List<Aluno>();

            this.HttpContext.Session.Clear();

            if (!String.IsNullOrWhiteSpace(ids))
            {
                foreach (var item in ids.Split(","))
                {
                    alunosSelecionados.Add(this.Services.GetAlunoById(new Guid(item)));
                }
            }

            this.HttpContext.Session.SetString("AlunosSelecionados", JsonConvert.SerializeObject(alunosSelecionados));

            return View(alunosSelecionados);
        }

        // GET: AlunoController/Details/5
        public ActionResult Details(Guid id)
        {
            var client = new RestClient();
            var request = new RestRequest("https://localhost:5001/api/aluno/" + id, DataFormat.Json);
            request.AddHeader("Authorization", "Bearer " + this.HttpContext.Session.GetString("Token"));

            var response = client.Get<Aluno>(request);

            return View(response.Data);
        }

        // GET: AlunoController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var aluno = this.Services.GetAlunoById(id);
            return View(aluno);
        }

        // POST: AlunoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aluno aluno)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(aluno);


                var client = new RestClient();
                var request = new RestRequest("https://localhost:5001/api/aluno", DataFormat.Json);
                request.AddJsonBody(aluno);
             
                var response = client.Post<Aluno>(request);

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("APP_ERROR", ex.Message);
                return View(aluno);
            }
        }

        // GET: AlunoController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var aluno = this.Services.GetAlunoById(id);

            return View(aluno);
        }

        // POST: AlunoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Aluno aluno)
        {
            try
            {
                this.Services.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public class TokenResult
    {
        public String Token { get; set; }
    }
}
