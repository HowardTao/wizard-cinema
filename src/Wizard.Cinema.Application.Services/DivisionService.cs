﻿using System.Collections.Generic;
using Infrastructures;
using Infrastructures.Attributes;
using Wizard.Cinema.Application.DTOs.Request.Division;
using Wizard.Cinema.Application.DTOs.Response;
using Wizard.Cinema.Domain.Ministry;
using Wizard.Cinema.Domain.Wizard;
using Wizard.Cinema.QueryServices;
using Wizard.Cinema.QueryServices.DTOs;

namespace Wizard.Cinema.Application.Services
{
    [Service]
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionQueryService _divisionQueryService;
        private readonly IWizardRepository _wizardRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IDivisionRepository _divisionRepository;

        public DivisionService(IDivisionQueryService divisionQueryService, IWizardRepository wizardRepository, ITransactionRepository transactionRepository, IDivisionRepository divisionRepository)
        {
            this._divisionQueryService = divisionQueryService;
            this._wizardRepository = wizardRepository;
            this._transactionRepository = transactionRepository;
            this._divisionRepository = divisionRepository;
        }

        public ApiResult<bool> CreateDivision(CreateDivisionReqs request)
        {
            if (_divisionRepository.QueryByCityId(request.CityId) != null)
                return new ApiResult<bool>(ResultStatus.FAIL, "该城市分部已创建");

            Wizards wizard = _wizardRepository.Query(request.CreatorId);
            if (wizard == null)
                return new ApiResult<bool>(ResultStatus.FAIL, "你是谁");

            long divisionId = NewId.GenerateId();

            var division = new Divisions(divisionId, request.CityId, request.Name, request.CreatorId);

            if (_divisionRepository.Insert(division) <= 0)
                return new ApiResult<bool>(ResultStatus.FAIL, "保存时出错，请稍后再试");

            return new ApiResult<bool>(ResultStatus.SUCCESS, true);
        }

        public ApiResult<bool> ChangeDivision(ChangeDivisionReqs request)
        {
            Wizards wizard = _wizardRepository.Query(request.CreatorId);
            if (wizard == null)
                return new ApiResult<bool>(ResultStatus.FAIL, "你是谁");

            Divisions division = _divisionRepository.Query(request.DivisionId);
            if (division == null)
                return new ApiResult<bool>(ResultStatus.FAIL, "找不到该分部");

            division.Change(request.Name, request.CityId, request.CreateTime);

            if (_divisionRepository.Update(division) <= 0)
                return new ApiResult<bool>(ResultStatus.FAIL, "没有任何更改");

            return new ApiResult<bool>(ResultStatus.SUCCESS, true);
        }

        public ApiResult<DivisionResp> GetById(long id)
        {
            DivisionInfo division = _divisionQueryService.QueryById(id);
            if (division == null)
                return new ApiResult<DivisionResp>(ResultStatus.SUCCESS, default(DivisionResp));

            return new ApiResult<DivisionResp>(ResultStatus.SUCCESS, Mapper.Map<DivisionInfo, DivisionResp>(division));
        }

        public ApiResult<DivisionResp> GetByCityId(long cityId)
        {
            DivisionInfo division = _divisionQueryService.QueryByCityId(cityId);
            if (division == null)
                return new ApiResult<DivisionResp>(ResultStatus.SUCCESS, default(DivisionResp));

            return new ApiResult<DivisionResp>(ResultStatus.SUCCESS, Mapper.Map<DivisionInfo, DivisionResp>(division));
        }

        public ApiResult<IEnumerable<DivisionResp>> GetByIds(long[] divisionIds)
        {
            if (divisionIds.Length <= 0)
                return new ApiResult<IEnumerable<DivisionResp>>(ResultStatus.FAIL, "没有任何Id" +
                                                                                   "");
            IEnumerable<DivisionInfo> division = _divisionQueryService.Query(divisionIds);

            return new ApiResult<IEnumerable<DivisionResp>>(ResultStatus.SUCCESS, Mapper.Map<DivisionInfo, DivisionResp>(division));
        }

        public ApiResult<PagedData<DivisionResp>> Search(PagedSearch search)
        {
            PagedData<DivisionInfo> result = _divisionQueryService.QueryPaged(search);
            return new ApiResult<PagedData<DivisionResp>>(ResultStatus.SUCCESS, Mapper.Map<PagedData<DivisionInfo>, PagedData<DivisionResp>>(result));
        }
    }
}
