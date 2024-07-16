﻿namespace RateIdeas.Application.Categories.Commands;

public record UpdateCommentCommand : IRequest<CommentResultDto>
{
    public UpdateCommentCommand(UpdateCommentCommand command)
    {
        Id = command.Id;
        IdeaId = command.IdeaId;
        UserId = command.UserId;
        Content = command.Content;
    }

    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public long IdeaId { get; set; }
    public long UserId { get; set; }
}

public class UpdateCommentCommandHandler(IMapper mapper,
    IRepository<Comment> repository) : IRequestHandler<UpdateCommentCommand, CommentResultDto>
{
    public async Task<CommentResultDto> Handle(UpdateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await repository.SelectAsync(entity => entity.Id == request.Id)
            ?? throw new NotFoundException($"The Comment is not found by id={request.Id}");

        mapper.Map(request, entity);

        repository.Update(entity);
        await repository.SaveAsync();

        return mapper.Map<CommentResultDto>(entity);
    }
}