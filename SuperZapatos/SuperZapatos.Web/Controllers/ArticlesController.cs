using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using SuperZapatos.DTO;
using SuperZapatos.DTO.Response;
using Newtonsoft.Json;
using System.Text;

namespace SuperZapatos.Web.Controllers
{
    public class ArticlesController : Controller
    {


        private const string BASE_URI = "http://localhost:54596/api/";
        private HttpClient _client;
        private HttpResponseMessage _response;


        private void ConfigHttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BASE_URI);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/xml"));
            var byteArray = Encoding.ASCII.GetBytes("my_username:my_password");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<ActionResult> Index(int? Store_Id)
        {
            ConfigHttpClient();
            if (Store_Id != null)
                _response = await _client.GetAsync("articles/store/" + Store_Id.ToString());
            else
            {
                _response = await _client.GetAsync("articles");
            }
            Result<ComplexArticles<List<ArticleDto>>> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<ComplexArticles<List<ArticleDto>>>>();
            }
            else
            {
                return View();
            }
            ViewBag.Store_Id = new SelectList(res.Response.Stores, "Id", "Name");
            return View(res.Response.Articles);
        }

        // GET: Articles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ConfigHttpClient();
            _response = await _client.GetAsync("articles/" + id.ToString());
            Result<ComplexArticles<ArticleDto>> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<ComplexArticles<ArticleDto>>>();
            }
            else
            {
                return View();
            }
            ViewBag.Store_Id = new SelectList(res.Response.Stores, "Id", "Name");
            return View(res.Response.Articles);
        }

        // GET: Articles/Create
        public async Task<ActionResult> Create()
        {
            ConfigHttpClient();
            _response = await _client.GetAsync("stores");
            Result<List<StoreDto>> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<List<StoreDto>>>();
            }
            else
            {
                return View();
            }
            ViewBag.Store_Id = new SelectList(res.Response, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Price,Total_in_shelf,Total_in_vault,Store_Id")] ArticleDto articles)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Articles.Add(articles);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.Store_Id = new SelectList(db.Store, "Id", "Name", articles.Store_Id);
            //return View(articles);
            if (ModelState.IsValid)
            {
                ConfigHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(articles), Encoding.UTF8, "application/json");
                _response = await _client.PostAsync("articles", content);
            }
            Result<ArticleDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<ArticleDto>>();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Articles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Articles articles = await db.Articles.FindAsync(id);
            //if (articles == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.Store_Id = new SelectList(db.Store, "Id", "Name", articles.Store_Id);
            //return View(articles);
            ConfigHttpClient();
            _response = await _client.GetAsync("articles/" + id.ToString());
            Result<ComplexArticles<ArticleDto>> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<ComplexArticles<ArticleDto>>>();
            }
            else
            {
                return View();
            }
            ViewBag.Store_Id = new SelectList(res.Response.Stores, "Id", "Name");
            return View(res.Response.Articles);
        }

        // POST: Articles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price,Total_in_shelf,Total_in_vault,Store_Id")] ArticleDto article)
        {
            if (ModelState.IsValid)
            {
                ConfigHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");
                _response = await _client.PostAsync("articles/" + article.Id, content);
            }
            Result<ArticleDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<ArticleDto>>();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Articles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ConfigHttpClient();
            _response = await _client.GetAsync("articles/" + id.ToString());
            Result<ComplexArticles<ArticleDto>> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<ComplexArticles<ArticleDto>>>();
            }
            else
            {
                return View();
            }
            //ViewBag.Store_Id = new SelectList(res.Response.Stores, "Id", "Name");
            return View(res.Response.Articles);
        }

        //// POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConfigHttpClient();
            _response = await _client.DeleteAsync("articles/" + id.ToString());
            return RedirectToAction("Index");
        }
    }
}
