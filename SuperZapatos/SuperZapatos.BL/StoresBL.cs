using SuperZapatos.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperZapatos.DTO;
using SuperZapatos.DTO.Response;

namespace SuperZapatos.BL
{
    public class StoresBL
    {
        public StoresBL() { }

        private SuperZapatosContext db;

        public Result<StoreDto> Add(StoreDto store)
        {
            try
            {
                Store newStore;
                using (db = new SuperZapatosContext())
                {
                    newStore = new Store()
                    {
                        Address = store.Address.Trim(),
                        Name = store.Name.Trim()
                    };
                    newStore = db.Store.Add(newStore);
                    store.Id = newStore.Id;
                    if (db.SaveChanges() > 0)
                        return new Result<StoreDto>
                        {
                            Response = store,
                            Success = true,
                            TotalElements = 1,
                            Error = null
                        };
                }
                return new Result<StoreDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<StoreDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<List<StoreDto>> Get()
        {
            try
            {
                List<StoreDto> stores;
                using (db = new SuperZapatosContext())
                {
                    stores = (from sto in db.Store
                              select new StoreDto
                              {
                                  Id = sto.Id,
                                  Name = sto.Name.Trim(),
                                  Address = sto.Address.Trim()
                              }).ToList();
                }
                if (stores.Count > 0)
                    return new Result<List<StoreDto>>
                    {
                        Response = stores,
                        Success = true,
                        TotalElements = stores.Count,
                        Error = null
                    };
                return new Result<List<StoreDto>>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<List<StoreDto>>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<StoreDto> Get(int id)
        {
            try
            {
                StoreDto store;
                using (db = new SuperZapatosContext())
                {
                    store = (from sto in db.Store
                             where sto.Id == id
                             select new StoreDto
                             {
                                 Id = sto.Id,
                                 Name = sto.Name.Trim(),
                                 Address = sto.Address.Trim()
                             }).FirstOrDefault();
                }
                if (store != null)
                    return new Result<StoreDto>
                    {
                        Response = store,
                        Success = true,
                        TotalElements = 1,
                        Error = null
                    };
                return new Result<StoreDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<StoreDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 500, ErrorMsg = ex.Message }
                };
            }
        }

        public Result<StoreDto> Update(StoreDto store)
        {
            try
            {

                using (db = new SuperZapatosContext())
                {
                    Store obj = db.Store.Find(store.Id);
                    if (obj != null)
                    {
                        obj.Name = store.Name.Trim();
                        obj.Address = store.Address.Trim();
                        if (db.SaveChanges() > 0)
                        {
                            return new Result<StoreDto>
                            {
                                Response = new StoreDto { Id = obj.Id, Address = obj.Address.Trim(), Name = obj.Name.Trim() },
                                Success = true,
                                TotalElements = 1,
                                Error = null
                            };
                        }
                    }
                }
                return new Result<StoreDto>
                {
                    Response = null,
                    Success = false,
                    TotalElements = 0,
                    Error = new Error { ErrorCode = 404, ErrorMsg = "Not Found" }
                };
            }
            catch (Exception ex)
            {
                return new Result<StoreDto>
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
                    Store obj = db.Store.Find(id);
                    if (obj != null)
                    {
                        db.Store.Remove(obj);
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
