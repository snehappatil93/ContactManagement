using ContactMgmtClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactMgmtClient.Controllers
{
    public class ContactController : Controller
    {
        readonly string baseUrl = ConfigurationManager.AppSettings["ContactMgmtServiceUrl"];
        #region Get
        /// <summary>
        /// Get list of contacts
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ListContacts()
        {
            List<Contact> contacts = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("Contact/ListContacts");
                    if (Res.IsSuccessStatusCode)
                    {
                        contacts = JsonConvert.DeserializeObject<List<Contact>>(Res.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception ex)
            {
                //log exception
            }

            return View(contacts);
        }

        #endregion Get

        #region Create
        /// <summary>
        /// Create contact
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Action = "Get";
            return View();
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contact">contact info</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(Contact contact)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.PostAsync("Contact/Create",
                                           new StringContent(JsonConvert.SerializeObject(contact),
                                          Encoding.UTF8, "application/json"));

                    if (Res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Contact created successfully";
                        return RedirectToAction("ListContacts");
                    }
                }
            }
            catch (Exception ex)
            {
                //log exception
            }

            return View(contact);
        }

        #endregion Create

        #region Edit

        /// <summary>
        /// Edit contact info
        /// </summary>
        /// <param name="id">Id of contact to be edited</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Action = "Edit";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"Contact/GetContactById/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    string response = Res.Content.ReadAsStringAsync().Result;
                    Contact contact = JsonConvert.DeserializeObject<Contact>(response);
                    return View(contact);
                }
                else
                {
                    TempData["Message"] = "Something went wrong";
                    return RedirectToAction("ListContacts");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage Res = await client.PutAsync("Contact/UpdateContact",
                                               new StringContent(JsonConvert.SerializeObject(contact),
                                              Encoding.UTF8, "application/json"));
                        if (Res.IsSuccessStatusCode)
                        {
                            TempData["Message"] = "Contact updated successfully";
                            return RedirectToAction("ListContacts");

                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Something went wrong. Please try again later.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log exception
            }
            return View(contact);
        }

        #endregion Edit

        #region Delete
        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="id">Id of contact to be deleted</param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            bool isDeleted = false;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);

                    HttpResponseMessage Res = await client.DeleteAsync($"Contact/DeleteContact/{id}");

                    isDeleted = Res.IsSuccessStatusCode;
                    if (Res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Contact deleted successfully.";
                    }
                    else
                    {
                        TempData["Message"] = "Something went wrong. Please try again later.";
                    }
                }
            }
            catch (Exception ex)
            {
                //log exception
            }

            return Json(isDeleted);
        }
        #endregion Delete

    }
}