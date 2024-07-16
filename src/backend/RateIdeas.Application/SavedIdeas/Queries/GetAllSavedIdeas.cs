﻿namespace RateIdeas.Application.SavedIdeas.Queries;

public record GetAllSavedIdeasQuery : IRequest<IEnumerable<SavedIdeaResultDto>>
{
}

public class GetAllSavedIdeasQueryHandler(IMapper mapper, IRepository<SavedIdea> repository)
    : IRequestHandler<GetAllSavedIdeasQuery, IEnumerable<SavedIdeaResultDto>>
{
    public async Task<IEnumerable<SavedIdeaResultDto>> Handle(GetAllSavedIdeasQuery request,
        CancellationToken cancellationToken)
    {
        var entities = (await Task.Run(() => repository.SelectAll())).ToList();
        return mapper.Map<IEnumerable<SavedIdeaResultDto>>(entities);
    }
}