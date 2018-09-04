﻿using Wizard.Cinema.Application.DTOs.Request.Wizards;
using Wizard.Cinema.Application.Services.Dto.Request;
using Wizard.Cinema.Application.Services.Dto.Request.Wizards;
using Wizard.Cinema.Application.Services.Dto.Response;
using Wizard.Infrastructures;

namespace Wizard.Cinema.Application.Services
{
    public interface IWizardService
    {
        ApiResult<WizardResp> GetWizard(string account, string passward);

        ApiResult<WizardResp> GetWizard(long wizardId);

        ApiResult<bool> Register(RegisterWizardReqs request);

        ApiResult<bool> CreateWizard(CreateWizardReqs request);

        ApiResult<bool> UpdateWizard(UpdateWizardReqs request);

        ApiResult<PagedData<WizardResp>> Search(SearchWizardReqs search);

        ApiResult<ProfileResp> GetPrpfile(long wizardId);

        ApiResult<ProfileResp> ChangeProfile(ChangeProfilepReqs request);
    }
}