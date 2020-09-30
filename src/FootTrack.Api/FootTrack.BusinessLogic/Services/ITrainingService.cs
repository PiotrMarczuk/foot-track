﻿using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Services
{
    public interface ITrainingService
    {
        Task<Result> StartTrainingAsync(Id userId);
    }
}