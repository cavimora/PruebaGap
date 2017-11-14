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
using System.Text;
using SuperZapatos.DTO.Response;
using SuperZapatos.DTO;
using Newtonsoft.Json;

namespace SuperZapatos.Web.Controllers
{
    public class StoresController : Controller
    {
        private const string BASE_URI = "http://localhost:54596/api/";
        private HttpClient _client;
        private HttpResponseMessage _response;


        private void ConfigHttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(BASE_URI);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/xml"));
        }
        // GET: Stores
        public async Task<ActionResult> Index()
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
            return View(res.Response);
        }

        // GET: Stores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigHttpClient();
            _response = await _client.GetAsync("stores/" + id.ToString());
            Result<StoreDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<StoreDto>>();
            }
            else
            {
                return View();
            }
            //Store store = await db.Store.FindAsync(id);
            //if (store == null)
            //{
            //    return HttpNotFound();
            //}
            return View(res.Response);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Address")] StoreDto store)
        {
            if (ModelState.IsValid)
            {
                ConfigHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(store), Encoding.UTF8, "application/json");
                _response = await _client.PostAsync("stores", content);
            }
            Result<StoreDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<StoreDto>>();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Stores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigHttpClient();
            _response = await _client.GetAsync("stores/" + id.ToString());
            Result<StoreDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<StoreDto>>();
            }
            else
            {
                return HttpNotFound();
            }
            //Store store = await db.Store.FindAsync(id);
            //if (store == null)
            //{
            //    return HttpNotFound();
            //}
            return View(res.Response);
        }

        // POST: Stores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address")] StoreDto store)
        {
            if (ModelState.IsValid)
            {
                ConfigHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(store), Encoding.UTF8, "application/json");
                _response = await _client.PutAsync("stores/" + store.Id, content);
            }
            Result<StoreDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<StoreDto>>();
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Stores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigHttpClient();
            _response = await _client.GetAsync("stores/" + id.ToString());
            Result<StoreDto> res;
            if (_response.IsSuccessStatusCode)
            {
                res = await _response.Content.ReadAsAsync<Result<StoreDto>>();
            }
            else
            {
                return HttpNotFound();
            }
            //Store store = await db.Store.FindAsync(id);
            //if (store == null)
            //{
            //    return HttpNotFound();
            //}
            return View(res.Response);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConfigHttpClient();
            //var content = new StringContent(JsonConvert.SerializeObject(store), Encoding.UTF8, "application/json");
            _response = await _client.DeleteAsync("stores/" + id);
            return RedirectToAction("Index");
        }
    }
}
