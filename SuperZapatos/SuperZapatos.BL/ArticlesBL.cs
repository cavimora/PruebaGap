using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZapatos.DA;
using SuperZapatos.DTO;
using SuperZapatos.DTO.Response;

namespace SuperZapatos.BL
{
    public class ArticlesBL
    {
        public ArticlesBL() { }


        private SuperZapatosContext db;


        public Result<ArticleDto> Add(ArticleDto article)
        {
            try
            {
                Articles newArticle;
                using (db = new SuperZapatosContext())
                {
                    newArticle = new Articles()
                    {
                        Id = article.Id,
                        Name = article.Name.Trim(),
                        Description = article.Description.Trim(),
                        Price = article.Price,
                        Store_Id = article.Store_Id,
                        Total_in_shelf = article.Total_in_shelf,
                        Total_in_vault = article.Total_in_vault
                    };
                    newArticle = db.Articles.Add(newArticle);
                    article.Id = newArticle.Id;
                    if (db.SaveChanges() > 0)
                        return new Result<ArticleDto>
                        {
                            Response = article,
                            Success = true,
                            TotalElements = 1,
                            Error = null
                        };
                }
               
                return new Result<ArticleDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<ArticleDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<ComplexArticles<List<ArticleDto>>> Get()
        {
            try
            {
                List<ArticleDto> articles;
                List<StoreDto> stores;
                using (db = new SuperZapatosContext())
                {
                    articles = (from art in db.Articles
                                select new ArticleDto
                                {
                                    Id = art.Id,
                                    Name = art.Name.Trim(),
                                    Description = art.Description.Trim(),
                                    Price = art.Price,
                                    Store_Id = art.Store_Id,
                                    Total_in_shelf = art.Total_in_shelf,
                                    Total_in_vault = art.Total_in_vault
                                }).ToList();

                    stores = (from sto in db.Store
                              select new StoreDto
                              {
                                  Address = sto.Address,
                                  Id = sto.Id,
                                  Name = sto.Name
                              }).ToList();
                }
                if (articles.Count > 0)
                    return new Result<ComplexArticles<List<ArticleDto>>>
                    {
                        Response = new ComplexArticles<List<ArticleDto>>
                        {
                            Articles = articles,
                            Stores = stores
                        },
                        Success = true,
                        TotalElements = articles.Count,
                        Error = null
                    };
                return new Result<ComplexArticles<List<ArticleDto>>>
                {
                    Response = new ComplexArticles<List<ArticleDto>>
                    {
                        Articles = new List<ArticleDto>(),
                        Stores = stores
                    },
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<ComplexArticles<List<ArticleDto>>>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<ComplexArticles<List<ArticleDto>>> GetByStore(int storeId)
        {
            try
            {
                List<StoreDto> stores;
                List<ArticleDto> articles;
                using (db = new SuperZapatosContext())
                {
                    articles = (from art in db.Articles
                                where art.Store_Id == storeId
                                select new ArticleDto
                                {
                                    Id = art.Id,
                                    Name = art.Name.Trim(),
                                    Description = art.Description.Trim(),
                                    Price = art.Price,
                                    Store_Id = art.Store_Id,
                                    Total_in_shelf = art.Total_in_shelf,
                                    Total_in_vault = art.Total_in_vault
                                }).ToList();
                    stores = (from sto in db.Store
                              select new StoreDto
                              {
                                  Address = sto.Address,
                                  Id = sto.Id,
                                  Name = sto.Name
                              }).ToList();
                }
                if (articles.Count > 0)
                    return new Result<ComplexArticles<List<ArticleDto>>>
                    {
                        Response = new ComplexArticles<List<ArticleDto>>
                        {
                            Articles = articles,
                            Stores = stores
                        },
                        Success = true,
                        TotalElements = articles.Count,
                        Error = null
                    };
                return new Result<ComplexArticles<List<ArticleDto>>>
                {
                    Response = new ComplexArticles<List<ArticleDto>>
                    {
                        Articles = new List<ArticleDto>(),
                        Stores = stores
                    },
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<ComplexArticles<List<ArticleDto>>>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<ComplexArticles<ArticleDto>> Get(int id)
        {
            try
            {
                ArticleDto article;
                List<StoreDto> stores;
                using (db = new SuperZapatosContext())
                {
                    article = (from art in db.Articles
                               where art.Id == id
                               select new ArticleDto
                               {
                                   Id = art.Id,
                                   Name = art.Name.Trim(),
                                   Description = art.Description.Trim(),
                                   Price = art.Price,
                                   Store_Id = art.Store_Id,
                                   Total_in_shelf = art.Total_in_shelf,
                                   Total_in_vault = art.Total_in_vault
                               }).FirstOrDefault();
                    stores = (from sto in db.Store
                              select new StoreDto
                              {
                                  Address = sto.Address,
                                  Id = sto.Id,
                                  Name = sto.Name
                              }).ToList();
                }
                if (article != null)
                    return new Result<ComplexArticles<ArticleDto>>
                    {
                        Response = new ComplexArticles<ArticleDto>
                        {
                            Articles = article,
                            Stores = stores
                        },
                        Success = true,
                        TotalElements = 1,
                        Error = null
                    };
                return new Result<ComplexArticles<ArticleDto>>
                {
                    Response = new ComplexArticles<ArticleDto>
                    {
                        Articles = new ArticleDto(),
                        Stores = stores
                    },
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<ComplexArticles<ArticleDto>>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<ArticleDto> Update(ArticleDto article)
        {
            try
            {

                using (db = new SuperZapatosContext())
                {
                    Articles obj = db.Articles.Find(article.Id);
                    if (obj != null)
                    {
                        obj.Id = article.Id;
                        obj.Name = article.Name.Trim();
                        obj.Description = article.Description.Trim();
                        obj.Price = article.Price;
                        obj.Store_Id = article.Store_Id;
                        obj.Total_in_shelf = article.Total_in_shelf;
                        obj.Total_in_vault = article.Total_in_vault;
                        if (db.SaveChanges() > 0)
                        {
                            return new Result<ArticleDto>
                            {

                                Response = new ArticleDto
                                {
                                    Id = obj.Id,
                                    Name = obj.Name.Trim(),
                                    Description = obj.Description.Trim(),
                                    Price = obj.Price,
                                    Store_Id = obj.Store_Id,
                                    Total_in_shelf = obj.Total_in_shelf,
                                    Total_in_vault = obj.Total_in_vault
                                },
                                Success = true,
                                TotalElements = 1,
                                Error = null
                            };
                        }
                    }
                }
                return new Result<ArticleDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<ArticleDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<bool> Delete(int id)
        {
            try
            {
                using (db = new SuperZapatosContext())
                {
                    Articles obj = db.Articles.Find(id);
                    if (obj != null)
                    {
                        db.Articles.Remove(obj);
                        if (db.SaveChanges() > 0)
                        {
                            return new Result<bool>
                            {
                                Response = true,
                                Success = true,
                                TotalElements = 1,
                                Error = null
                            };
                        }
                    }
                }
                return new Result<bool>
                {
                    Response = false,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<bool>
                {
                    Response = false,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

    }
}
